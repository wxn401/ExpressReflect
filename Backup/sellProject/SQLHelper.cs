using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
//using Tool;
using System.Xml;
using System.IO;
//using Utility;

namespace Tools
{
    public  class SQLHelper
    {
      
        static string absolutepath = "";
        static XmlDocument xmlDocument = new XmlDocument();
        static string FtpDir = "";
        static string localpath = "";
        static string hostname = "";
        static string ftpport = "";
        static string username = "";
        static string password = "";
        static string configurationStr = "";
        static XmlNode node;


        //连接字符串
        //static string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        public static string GetConnectionString()
        {
            //验证权限  是否需要？


            return ConfigurationManager.AppSettings["sqlConnectionString"];
        }
        #region 执行查询，返回DataTable对象-----------------------
        /// <summary>
        /// 执行查询，返回DataTable对象
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable GetTable(string strSQL)
        {
            return GetTable(strSQL, null);
        }
        /// <summary>
        /// 执行存储过程，返回DataTable对象 
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable GetTableByProc(string ProcName)
        {
            return GetTable(ProcName, null, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 执行存储过程，返回DataTable对象 带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable GetTableByProc(string ProcName, SqlParameter[] pas)
        {
            return GetTable(ProcName, pas, CommandType.StoredProcedure);
        }
        public static DataTable GetTable(string strSQL, SqlParameter[] pas)
        {
            return GetTable(strSQL, pas, CommandType.Text);
        }
        /// <summary>
        /// 执行查询，返回DataTable对象
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <param name="pas">参数数组</param>
        /// <param name="cmdtype">Command类型</param>
        /// <returns>DataTable对象</returns>
        public static DataTable GetTable(string strSQL, SqlParameter[] pas, CommandType cmdtype)
        {
            DataTable dt = new DataTable(); ;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
                da.SelectCommand.CommandType = cmdtype;
                if (pas != null)
                {
                    da.SelectCommand.Parameters.AddRange(pas);
                }
                da.Fill(dt);
                da.SelectCommand.Parameters.Clear();
            }
            return dt;
        }
        #endregion
        #region 执行查询，返回DataSet对象-------------------------
        /// <summary>
        /// 执行查询，返回DataSet对象 不带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string strSQL)
        {
            return GetDataSet(strSQL, null);
        }
        /// <summary>
        /// 执行存储过程，返回DataSet对象 
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataSet GetDataSetByProc(string ProcName)
        {
            return GetDataSet(ProcName, null, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 执行存储过程，返回DataSet对象 带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataSet GetDataSetByProc(string ProcName, SqlParameter[] pas)
        {
            return GetDataSet(ProcName, pas, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 执行查询，返回DataSet对象 带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string strSQL, SqlParameter[] pas)
        {
            return GetDataSet(strSQL, pas, CommandType.Text);
        }
        /// <summary>
        /// 执行查询，返回DataSet对象
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <param name="pas">参数数组</param>
        /// <param name="cmdtype">Command类型</param>
        /// <returns>DataSet对象</returns>
        public static DataSet GetDataSet(string strSQL, SqlParameter[] pas, CommandType cmdtype)
        {
            DataSet dt = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
                da.SelectCommand.CommandType = cmdtype;
                if (pas != null)
                {
                    da.SelectCommand.Parameters.AddRange(pas);
                }
                da.Fill(dt);
                da.SelectCommand.Parameters.Clear();
            }
            return dt;
        }
        #endregion
        #region 执行查询事务
        /// <summary>
        /// 执行查询事务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> GetDataSetTran(List<string> SQLStringList)
        {



            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    List<string> list = new List<string>();
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            DataSet dt = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                            cmd.CommandText = strsql;
                            da.Fill(dt);
                            da.SelectCommand.Parameters.Clear();
                            if (dt != null && dt.Tables[0].Rows.Count > 0)
                            {
                                list.Add(dt.Tables[0].Columns[0].ColumnName.ToString());
                                dt.Clear();
                            }

                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw;
                    return null;
                }


            }
        }
        #endregion
        #region 执行非查询存储过程和SQL语句-----------------------------
        /// <summary>
        /// 执行非查询存储过程和SQL语句 不带参数
        /// </summary>
        /// <param name="ProcName"></param>
        /// <returns></returns>
        public static int ExcuteProc(string ProcName)
        {
            return ExcuteSQL(ProcName, null, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 执行非查询存储过程和SQL语句 带参数
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static int ExcuteProc(string ProcName, SqlParameter[] pars)
        {
            return ExcuteSQL(ProcName, pars, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 执行SQL语句 不带参数 返回受影响的行数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static int ExcuteSQL(string strSQL)
        {
            return ExcuteSQL(strSQL, null);
        }
        /// <summary>
        /// 执行SQL语句 带参数 返回受影响的行数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExcuteSQL(string strSQL, SqlParameter[] paras)
        {
            return ExcuteSQL(strSQL, paras, CommandType.Text);
        }
        /// 执行非查询存储过程和SQL语句
        /// 增、删、改
        /// </summary>
        /// <param name="strSQL">要执行的SQL语句</param>
        /// <param name="paras">参数列表，没有参数填入null</param>
        /// <param name="cmdType">Command类型</param>
        /// <returns>返回影响行数</returns>
        public static int ExcuteSQL(string strSQL, SqlParameter[] paras, CommandType cmdType)
        {
            int i = 0;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.CommandType = cmdType;
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                i = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            return i;
        }
        #endregion
        #region 执行查询返回第一行，第一列---------------------------------
        /// <summary>
        /// 执行查询返回第一行，第一列 不带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static string ExcuteScalarSQL(string strSQL)
        {
            return ExcuteScalarSQL(strSQL, null);
        }
        /// <summary>
        /// 执行查询返回第一行，第一列 带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string ExcuteScalarSQL(string strSQL, SqlParameter[] paras)
        {
            return ExcuteScalarSQL(strSQL, paras, CommandType.Text);
        }
        /// <summary>
        /// 执行查询存储过程返回第一行，第一列 带参数
        /// </summary>
        /// <param name="strProcName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string ExcuteScalarProc(string strProcName, SqlParameter[] paras)
        {
            return ExcuteScalarSQL(strProcName, paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 执行SQL语句，返回第一行，第一列
        /// </summary>
        /// <param name="strSQL">要执行的SQL语句</param>
        /// <param name="paras">参数列表，没有参数填入null</param>
        /// <returns>返回影响行数</returns>
        public static string ExcuteScalarSQL(string strSQL, SqlParameter[] paras, CommandType cmdType)
        {
            string str = string.Empty;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.CommandType = cmdType;
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                str = cmd.ExecuteScalar().ToString();
                cmd.Parameters.Clear();
                conn.Close();
            }
            return str;

        }
        #endregion
        #region 查询获取单个值------------------------------------
        /// <summary>
        /// 调用不带参数的存储过程获取单个值
        /// </summary>
        /// <param name="ProcName"></param>
        /// <returns></returns>
        public static object GetObjectByProc(string ProcName)
        {
            return GetObjectByProc(ProcName, null);
        }
        /// <summary>
        /// 调用带参数的存储过程获取单个值
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object GetObjectByProc(string ProcName, SqlParameter[] paras)
        {
            return GetObject(ProcName, paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 根据sql语句获取单个值
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static object GetObject(string strSQL)
        {
            return GetObject(strSQL, null);
        }
        /// <summary>
        /// 根据sql语句 和 参数数组获取单个值
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object GetObject(string strSQL, SqlParameter[] paras)
        {
            return GetObject(strSQL, paras, CommandType.Text);
        }

        /// <summary>
        /// 执行SQL语句，返回首行首列
        /// </summary>
        /// <param name="strSQL">要执行的SQL语句</param>
        /// <param name="paras">参数列表，没有参数填入null</param>
        /// <returns>返回的首行首列</returns>
        public static object GetObject(string strSQL, SqlParameter[] paras, CommandType cmdtype)
        {
            object o = null;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.CommandType = cmdtype;
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                o = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
            }
            return o;
        }
        #endregion
        #region 查询获取DataReader------------------------------------
        /// <summary>
        /// 调用不带参数的存储过程，返回DataReader对象
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns>DataReader对象</returns>
        public static SqlDataReader GetReaderByProc(string procName)
        {
            return GetReaderByProc(procName, null);
        }
        /// <summary>
        /// 调用带有参数的存储过程，返回DataReader对象
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="paras">参数数组</param>
        /// <returns>DataReader对象</returns>
        public static SqlDataReader GetReaderByProc(string procName, SqlParameter[] paras)
        {
            return GetReader(procName, paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 根据sql语句返回DataReader对象
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>DataReader对象</returns>
        public static SqlDataReader GetReader(string strSQL)
        {
            return GetReader(strSQL, null);
        }
        /// <summary>
        /// 根据sql语句和参数返回DataReader对象
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <param name="paras">参数数组</param>
        /// <returns>DataReader对象</returns>
        public static SqlDataReader GetReader(string strSQL, SqlParameter[] paras)
        {
            return GetReader(strSQL, paras, CommandType.Text);
        }
        /// <summary>
        /// 查询SQL语句获取DataReader
        /// </summary>
        /// <param name="strSQL">查询的SQL语句</param>
        /// <param name="paras">参数列表，没有参数填入null</param>
        /// <returns>查询到的DataReader（关闭该对象的时候，自动关闭连接）</returns>
        public static SqlDataReader GetReader(string strSQL, SqlParameter[] paras, CommandType cmdtype)
        {
            SqlDataReader sqldr = null;
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandType = cmdtype;
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras);
            }
            conn.Open();
            //CommandBehavior.CloseConnection的作用是如果关联的DataReader对象关闭，则连接自动关闭
            sqldr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return sqldr;
        }
        #endregion
        #region 执行事务
        /// <summary>
        /// 执行多条SQL语句，实现数据库插入和上传文件到文件夹事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        /// <returns>影响的行数</returns> 
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
         
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
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
        ///// <summary>
        ///// 执行多条SQL语句，实现数据库删除事务。
        ///// </summary>
        ///// <param name="SQLStringList">多条SQL语句</param>
        ///// <param name="strReturn">返回信息</param>
        ///// <returns>影响的行数</returns>
        public static int ExecuteSqlTranDel(List<String> SQLStringList)
        {

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
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
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
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
        #region 批量插入数据---------------------------------------------
        /// <summary>
        /// 往数据库中批量插入数据
        /// </summary>
        /// <param name="sourceDt">数据源表</param>
        /// <param name="targetTable">服务器上目标表</param>
        public static void BulkToDB(DataTable sourceDt, string targetTable)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);   //用其它源的数据有效批量加载sql server表中
            bulkCopy.DestinationTableName = targetTable;    //服务器上目标表的名称
            bulkCopy.BatchSize = sourceDt.Rows.Count;   //每一批次中的行数

            try
            {
                conn.Open();
                if (sourceDt != null && sourceDt.Rows.Count != 0)
                    bulkCopy.WriteToServer(sourceDt);   //将提供的数据源中的所有行复制到目标表中
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }

        }
        #endregion




    }
}
