using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Tools
{
    public class SqlHelper
    {
        //数据据连接字符串
        public static string connectionString = SqlConnect.ConnectString;
        
        public SqlHelper()
        {
        }

        #region =====公用方法=====
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns>返回数据库连接</returns>
        private static SqlConnection CreateCon()
        {
            return new SqlConnection(connectionString);
        }

        #endregion

        #region ====执行简单Sql语句====

        /// <summary>
        /// 执行一条Sql计算查询结果的语句，返回查询结果
        /// </summary>
        /// <param name="sqlString">Sql查询语句</param>
        /// <returns>查询语句的首行首列，如果为空则返回null</returns>
        public static object GetSingle(string sqlString)
        {
            using (SqlConnection connection = CreateCon())
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if (Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value))
                        {
                            connection.Close();
                            return null;
                        }
                        else
                        {
                            connection.Close();
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                }
            }

        }

        /// <summary>
        /// 执行一条Sql计算查询结果的语句，在设定查询等待时间范围内返回查询结果
        /// </summary>
        /// <param name="sqlString">Sql查询语句</param>
        /// <param name="times">语句执行等待时间</param>
        /// <returns>查询语句的首行首列，如果为空则返回null</returns>
        public static object GetSingle(string sqlString, int times)
        {
            using (SqlConnection connection = CreateCon())
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = times;
                        object obj = cmd.ExecuteScalar();
                        if (Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                }
            }
        }
         

        /// <summary>
        /// 执行一条Sql语句，返回数据库影响的行数，只对增删改有效，查询返回是-1
        /// </summary>
        /// <param name="sqlString">Sql执行语句</param>
        /// <returns>数据库影响行数</returns>
        public static int NonQuery(string sqlString)
        {
            using (SqlConnection connection = CreateCon())
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                }
            }
        }

        public static DataSet Query(string sqlString)
        {
            using (SqlConnection connection = CreateCon())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlString, connection);
                    sda.Fill(ds, "Table");
                    return ds;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                    throw ex;
                }
            }
        }

        public static DataSet Query(ConcurrentQueue<mutilReflect> mutil)
        {
            using (SqlConnection connection = CreateCon())
            {
                DataSet ds = new DataSet();
                
                try
                {
                    mutilReflect temp = new mutilReflect();
                    mutil.TryDequeue(out temp);
                    SqlCommand cmd = new SqlCommand(temp.sqlStr, connection);
                    connection.Open();
                    cmd.Parameters.AddRange(temp.sql_paramert);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    
                    sda.Fill(ds, temp.sql.table_name);
                    cmd.Dispose();
                    cmd.Clone();
                    return ds;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 执行多条Sql语句，实现数据库事务，不适用Select语句
        /// </summary>
        /// <param name="sqlStringList">Sql语句集合IList<String></param>
        /// <returns>全部Sql语句执行成功所受影响的总条数</returns>
        public static int NonQueryTran(IList<String> sqlStringList)
        {
            using (SqlConnection connection = CreateCon())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                connection.Open();
                SqlTransaction st = connection.BeginTransaction();
                cmd.Transaction = st;
                try
                {
                    int count = 0;
                    for (int i=0; i < sqlStringList.Count; i++)
                    {
                        string sqlStr = sqlStringList[i];
                        if (sqlStr.Trim().Length > 1)
                        {
                            cmd.CommandText = sqlStr;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    st.Commit();
                    return count;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    st.Rollback();
                    connection.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader(注意:调用该方法后，一定要对SqlDataReader进行Close)
        /// </summary>
        /// <param name="sqlStr">Sql查询语句</param>
        /// <returns>查询语句对应的SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sqlStr)
        {
            SqlConnection connection = CreateCon();
            SqlCommand cmd = new SqlCommand(sqlStr, connection);
            try
            {
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection在执行该命令时，如果关闭关联的 DataReader 对象，则关联的 Connection 对象也将关闭。
                return sdr;
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                connection.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlStr">Sql查询语句</param>
        /// <returns>查询语句对应的DataSet</returns>
        public static DataSet ExecuteDataSet(string sqlStr)
        {
            using (SqlConnection connection = CreateCon())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlStr, connection);
                    sda.Fill(ds, "Table");
                    return ds;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlStr">Sql查询语句</param>
        /// <param name="times">Sql查询语句的等待时间</param>
        /// <returns>查询语句对应的DataSet</returns>
        public static DataSet ExecuteDataSet(string sqlStr, int times)
        {
            using (SqlConnection connection = CreateCon())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlStr, connection);
                    sda.SelectCommand.CommandTimeout = times;
                    sda.Fill(ds, "Table");
                    connection.Close();
                    return ds;
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    throw ex;
                }
            }
        }

        #endregion

        #region ====执行带参数的Sql语句====

        /// <summary>
        /// 对Sql使用SqlParameter参数构建的SqlCommand对其进行Add参数
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="conn">SqlConnection</param>
        /// <param name="trans">SqlTransaction</param>
        /// <param name="cmdText">SqlCommandText</param>
        /// <param name="cmdParms">SqlParameters</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }        

        /// <summary>
        /// 执行一条带参数的Sql语句，并返回执行影响记录数，select不适用
        /// </summary>
        /// <param name="sqlStr">Sql语句</param>
        /// <param name="cmdParams">Sqlparameter[]</param>
        /// <returns>Sql语句执行影响记录数</returns>
        public static int NonQuery(string sqlStr, params SqlParameter[] cmdParams)
        {
            using (SqlConnection connection = CreateCon())
            {
                using(SqlCommand cmd=new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlStr, cmdParams);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        connection.Close();
                        return rows;
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int NonQuery(string sqlStr, string content)
        {
            using (SqlConnection connection = CreateCon())
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, connection))
                {
                    SqlParameter myParameter = new SqlParameter("@content", SqlDbType.NText);
                    myParameter.Value = content;
                    cmd.Parameters.Add(myParameter);
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        connection.Close();
                        return rows;
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条Sql语句，实现事务
        /// </summary>
        /// <param name="sqlStringList">使用Hashtable 存储SqlString,其中key为Sql语句，value为Parameter[]</param>
        /// <returns>返回总的记录影响条数</returns>
        public static int NonQueryTran(Hashtable sqlStringList)
        {
            using (SqlConnection connection = CreateCon())
            {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int count = 0;
                        foreach (DictionaryEntry myDe in sqlStringList)
                        {
                            string cmdText = myDe.Key.ToString();
                            SqlParameter[] cmdParams = (SqlParameter[])myDe.Value;
                            PrepareCommand(cmd, connection, null, cmdText, cmdParams);
                            count += cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    }
                    catch (SqlException ex)
                    {
                        trans.Rollback();
                        connection.Close();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">查询语句需要增加的参数SqlParameter[]</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return myReader;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "Table");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                    return ds;
                }
            }
        }

        #endregion

        #region 执行事务
        /// <summary>
        /// 执行多条SQL语句，实现数据库插入和上传文件到文件夹事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        /// <returns>影响的行数</returns> 
        //public static int ExecuteSqlTran(List<String> SQLStringList, List<SqlParameter[]> cmdParams)
        public static int ExecuteSqlTran(ConcurrentQueue<mutilReflect> mutil)
        {
            using (SqlConnection conn = CreateCon())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    SqlTransaction tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    try
                    {
                        int count = 0;
                        foreach (var sql in mutil)
                        {                          
                                string strsql = sql.sqlStr;
                                if (strsql.Trim().Length > 1)
                                {

                                    PrepareCommand(cmd, conn, null, strsql, sql.sql_paramert);
                                    count += cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                    //cmd.CommandText = strsql;
                                    //count += cmd.ExecuteNonQuery();
                                }
                        }
                        tx.Commit();
                        conn.Close();
                        cmd.Parameters.Clear();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        return 0;
                    }
                }
            }
        }
        ///// <summary>
        ///// 执行多条SQL语句，实现数据库删除事务。
        ///// </summary>
        ///// <param name="SQLStringList">多条SQL语句</param>
        ///// <param name="strReturn">返回信息</param>
        ///// <returns>影响的行数</returns>
        public static int ExecuteSqlTranDel(List<String> SQLStringList)
        {

            using (SqlConnection conn = CreateCon())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    cmd.Parameters.Clear();
                    return count;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }



        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        /// <param name="strReturn">返回信息</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteSqlTran(List<String> SQLStringList, out string strReturn)
        {
            strReturn = "";
            using (SqlConnection conn = CreateCon())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    cmd.Parameters.Clear();
                    return count;
                }
                catch (Exception ex)
                {
                    strReturn = ex.Message;
                    tx.Rollback();
                    return 0;
                }
            }
        }
        #endregion
    }
}