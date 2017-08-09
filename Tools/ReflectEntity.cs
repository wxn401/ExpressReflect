using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using MyAttribute;

namespace Tools
{
    public class ReflectEntity
    {
        public ReflectEntity()
        {
        }

        public enum getReflectProperty
        {
            TableName = 1,
            PrimaryKey = 2,
            ColumnName = 3,
        }

        #region 对外操作菜单
        public enum getCommand
        {
            insert = 1,
            update = 2,
            select = 3,
            delete = 4,
        };
        #endregion

        #region 反射实体类  对外公开
        /// <summary>
        /// 反射实体类
        /// </summary>
        /// <param name="sqlCommand">insert、update、select命令</param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static ConcurrentQueue<mutilReflect> ReflectEntityForSql<T>(Tools.ReflectEntity.getCommand sqlCommand, T obj, T where) where T : class
        {
            #region //
            //Object result = null;

            //sql_com sql = VisitProperties<T>(obj);
            //sql_com sql_where = VisitProperties<T>(where);
            //SqlParameter[] sql_paramert = sqlparamter(sql.param_object, sql_where.param_object); //  
            //string sqlStr = "";
            //if (null == sql_where && string.IsNullOrEmpty(sql_where.table_name))
            //{
            //    sql_where.table_name = sql.table_name;
            //}

            //switch (Convert.ToInt32(sqlCommand))
            //{
            //    case 1:  // insert
            //        sqlStr = Insert(sql);
            //        result = SqlHelper.NonQuery(sqlStr, sql_paramert);
            //        break;
            //    case 2:  // update
            //        sqlStr = Update(sql, sql_where);
            //        result = SqlHelper.NonQuery(sqlStr, sql_paramert);
            //        break;
            //    case 3:  // select
            //        sqlStr = Select(sql, sql_where);
            //        result = SqlHelper.Query(sqlStr, sql_paramert).Tables[0];
            //        break;
            //    case 4:  // delete
            //        sqlStr = Delete(sql_where);
            //        result = SqlHelper.NonQuery(sqlStr, sql_paramert);
            //        break;
            //    default:
            //        break;
            //} 
            #endregion

            ConcurrentQueue<mutilReflect> muilsql = new ConcurrentQueue<mutilReflect>();
            mutilReflect allsql = new mutilReflect();
            allsql.sql = VisitProperties<T>(obj);
            sql_com sql_where = VisitProperties<T>(where);
            allsql.sql_paramert = sqlparamter(allsql.sql.param_object, sql_where.param_object);
            string sqlStrs = "";
            if (null == sql_where || string.IsNullOrEmpty(sql_where.table_name))
            {

            }
            else
            {

                allsql.sql.table_name = sql_where.table_name;
            }

            switch (Convert.ToInt32(sqlCommand))
            {
                case 1:  // insert
                    allsql.sqlStr = Insert(allsql.sql);
                    break;
                case 2:  // update   // 有问题，要设置默认值为空的时候去
                    allsql.sqlStr = Update(allsql.sql, sql_where);
                    break;
                case 3:  // select
                    allsql.sqlStr = Select(allsql.sql, sql_where);
                    //result = SqlHelper.Query(sqlStr, sql_paramert).Tables[0];
                    break;
                case 4:  // delete   // 有问题，要重载，根据ID、来删除    并且加入事物的办法
                    if (null == sql_where && string.IsNullOrEmpty(sql_where.table_name))
                    {
                        sql_where = allsql.sql;
                    }
                    allsql.sqlStr = Delete(sql_where);
                    break;
            }
            muilsql.Enqueue(allsql);
            //});
            return muilsql;

        }
        #endregion



        public static ConcurrentQueue<mutilReflect> ReflectEntityForSql<T>(Tools.ReflectEntity.getCommand sqlCommand, T obj) where T : class
        {

            ConcurrentQueue<mutilReflect> muilsql = new ConcurrentQueue<mutilReflect>();
            mutilReflect allsql = new mutilReflect();
            allsql.sql = VisitProperties<T>(obj);
            string primaryKeyValue = "";
            allsql.sql_paramert = sqlparamter(allsql.sql.param_object,sqlCommand,allsql.sql.PrimaryKey,ref primaryKeyValue);
            allsql.sql.PrimaryKeyValue = primaryKeyValue;

            //ConcurrentQueue<mutilReflect> muilsql = new ConcurrentQueue<mutilReflect>();
            //mutilReflect allsql = new mutilReflect();
            //allsql.sql = VisitProperties<T>(obj);

            //string sqlStrs = "";


            switch (Convert.ToInt32(sqlCommand))
            {
                case 1:  // insert
                    allsql.sqlStr = Insert(allsql.sql);
                    break;
                case 2:  // update   // 有问题，要设置默认值为空的时候去
                    allsql.sqlStr = Update(allsql.sql);
                    break;
                case 3:  // select
                    allsql.sqlStr = Select(allsql.sql);
                    //result = SqlHelper.Query(sqlStr, sql_paramert).Tables[0];
                    break;
                case 4:  // delete   // 有问题，要重载，根据ID、来删除    并且加入事物的办法
                    //if (null == sql_where && string.IsNullOrEmpty(sql_where.table_name))
                    //{
                    //    sql_where = allsql.sql;
                    //}
                    allsql.sqlStr = Delete(allsql.sql);
                    break;
            }
            muilsql.Enqueue(allsql);
            //});
            return muilsql;

        }


        public static Object ExecuteNonQuery(ConcurrentQueue<mutilReflect> muilsql)
        {
            return SqlHelper.ExecuteSqlTran(muilsql);
        }

        public static Object ExecuteQueryDataSet(ConcurrentQueue<mutilReflect> muilsql)
        {
            return SqlHelper.Query(muilsql);
        }

        public static Object ExecuteQueryDataSqlTran(ConcurrentQueue<mutilReflect> muilsql)
        {
            return SqlHelper.ExecuteSqlTran(muilsql);
        }


        #region 反射实体类  对外公开
        /// <summary>
        /// 反射实体类
        /// </summary>
        /// <param name="sqlCommand">insert、update、select命令</param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        //public static Object ReflectEntityForSql<T>(int sqlCommand, List<T> objs, List<T> wheres) where T : class
        public static ConcurrentQueue<mutilReflect> ReflectEntityForSql<T>(Tools.ReflectEntity.getCommand sqlCommand, List<T> objs) where T : class
        {
            // todo: select  查询不允许多个
            //Object result = null;

            ConcurrentQueue<mutilReflect> muilsql = new ConcurrentQueue<mutilReflect>();

            // 验证，多个的时候，是否会出问题，会覆盖原值
            Parallel.ForEach(objs, (obj) =>
            {
                mutilReflect allsql = new mutilReflect();
                allsql.sql = VisitProperties<T>(obj);
                allsql.sql_paramert = sqlparamter(allsql.sql.param_object, null); //  


                switch (Convert.ToInt32(sqlCommand))
                {
                    case 1:  // insert
                        allsql.sqlStr = Insert(allsql.sql);
                        break;
                    case 2:  // update   // 有问题，要设置默认值为空的时候去
                        allsql.sqlStr = Update(allsql.sql, null);
                        break;
                    case 4:  // delete   // 有问题，要重载，根据ID、来删除    并且加入事物的办法
                        allsql.sqlStr = Delete(allsql.sql);
                        break;
                }
                muilsql.Enqueue(allsql);
            });

            return muilsql;
            //return SqlHelper.ExecuteSqlTran(muilsql);

            //sql_com sql = VisitProperties<T>(obj);
            //SqlParameter[] sql_paramert = sqlparamter(sql.param_object); //  
            //sql_where.table_name = sql.table_name;
            //string sqlStr = "";


            //switch (sqlCommand)
            //{
            //    case 1:  // insert
            //        sqlStr = Insert(sql);
            //        result = SqlHelper.NonQuery(sqlStr, sql_paramert);
            //        break;
            //    case 2:  // update
            //        sqlStr = Update(sql, sql_where);
            //        result = SqlHelper.NonQuery(sqlStr, sql_paramert);
            //        break;
            //    case 4:  // delete
            //        sqlStr = Delete(sql_where);
            //        result = SqlHelper.NonQuery(sqlStr, sql_paramert);
            //        break;
            //}
            //return SqlHelper.NonQuery(sqlStr, sql_paramert);
        }
        #endregion

        #region 创建sqlparameter
        /// <summary>
        /// 创建sqlparameter
        /// </summary>
        /// <param name="sql_object">反射实体</param>
        /// <param name="sql_where">where条件实体</param>
        /// <returns></returns>
        private static SqlParameter[] sqlparamter(ConcurrentQueue<sql_param> sql_object, ConcurrentQueue<sql_param> sql_where)
        {
            int count = 0;
            if (null != sql_object && sql_object.Count > 0)
            {
                count += sql_object.Count;
            }
            if (null != sql_where && sql_where.Count > 0)
            {
                count += sql_where.Count;
            }
            SqlParameter[] parameters = new SqlParameter[count];
            if (count > 0)
            {
                int i = 0;
                object obj = new object();
                if (null != sql_object && sql_object.Count > 0)
                    foreach (var sql in sql_object)
                    {
                        parameters[i] = new SqlParameter(sql.param_name, sql.param_value);
                        i++;
                    }

                if (null != sql_where && sql_where.Count > 0)
                    foreach (var sql in sql_where)
                    {
                        parameters[i] = new SqlParameter(sql.param_name, sql.param_value);
                        i++;
                    }
            }
            return parameters;
        }
        #endregion

        #region 创建sqlparameter
        /// <summary>
        /// 创建sqlparameter
        /// </summary>
        /// <param name="sql_object">反射实体</param>
        /// <param name="sql_where">where条件实体</param>
        /// <returns></returns>
        private static SqlParameter[] sqlparamter(ConcurrentQueue<sql_param> sql_object,Tools.ReflectEntity.getCommand sqlCommand,string primarykey,ref string primaryKeyValue)
        {
            int count = 0;
            if (null != sql_object && sql_object.Count > 0)
            {
                count += sql_object.Count;
            }

            //if (Convert.ToInt32(sqlCommand) == 2)
            //{
            //    count = count + 1;
            //}
            SqlParameter[] parameters = new SqlParameter[count];
            if (count > 0)
            {
                bool eprimarykey = false;
                int number = 0;
                int i = 0;
                //object obj = new object();
                if (null != sql_object && sql_object.Count > 0)
                {
                    foreach (var sql in sql_object)
                    {
                        if (!string.IsNullOrWhiteSpace(sql.param_value.ToString()) && primarykey.Equals(sql.param_name))
                        {
                            eprimarykey = true;
                            number = i;
                            primaryKeyValue = sql.param_value.ToString();
                        }
                        parameters[i] = new SqlParameter(sql.param_name, sql.param_value);
                        i++;
                    }
                }
                if (Convert.ToInt32(sqlCommand) == 2)
                {
                    if (eprimarykey)
                    {
                        int index = 0;
                        i = 0;
                        count--;
                        if (count > 0)
                        {
                            SqlParameter[] parameterss = new SqlParameter[count];
                            if (null != sql_object && sql_object.Count > 0)
                            {
                                foreach (var sql in sql_object)
                                {
                                    if (i == number)
                                    {
                                    }
                                    else
                                    {
                                        parameterss[index] = new SqlParameter(sql.param_name, sql.param_value);
                                        index++;
                                    }
                                    i++;
                                }
                                parameters = parameterss;
                            }
                        }
                    }
                }
            }
            return parameters;
        }
        #endregion

        #region 拼接反射属性字段 @value
        /// <summary>
        /// 拼接反射属性字段
        /// </summary>
        /// <param name="pre_value"></param>
        /// <param name="sp"></param>
        /// <param name="last_value"></param>
        /// <returns></returns>
        private static string sql_value(string pre_value, ConcurrentQueue<sql_param> sp, string last_value)
        {
            // 表明 +  非空属性字段
            StringBuilder sb = new StringBuilder();
            if (sp.Count > 0)
            {
                sb.Append("(");

                foreach (var sql in sp)
                {
                    if (!string.IsNullOrEmpty(pre_value))
                    {
                        sb.Append(pre_value);
                    }
                    sb.Append(sql.param_name + ",");
                    if (!string.IsNullOrEmpty(last_value))
                    {
                        sb.Append(last_value);
                    }
                }

                //sp.ForEach
                //    (
                //         delegate(sql_param sql)
                //         {
                //             if (!string.IsNullOrEmpty(pre_value))
                //             {
                //                 sb.Append(pre_value);
                //             }
                //             sb.Append(sql.param_name + ",");
                //             if (!string.IsNullOrEmpty(last_value))
                //             {
                //                 sb.Append(last_value);
                //             }
                //         }
                //     );
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                sb.Append(")");
            }

            return sb.ToString();
        }
        #endregion

        #region sql插入语句
        /// <summary>
        /// sql插入语句
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string Insert(sql_com obj)
        {
            StringBuilder sb = new StringBuilder();
            ConcurrentQueue<sql_param> sp = obj.param_object;
            sb.Append("insert into " + obj.table_name + "");
            // 表明 +  非空属性字段
            sb.Append(sql_value("", sp, ""));
            sb.Append(" VALUES");
            // @ + 非空属性字段
            sb.Append(sql_value("@", sp, ""));
            return sb.ToString();
        }

        #endregion

        #region 拼接update操作的sql语句
        /// <summary>
        /// 拼接update操作的sql语句
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        private static string sql_update_set(ConcurrentQueue<sql_param> sp,string primaryKey)
        {
            StringBuilder sb = new StringBuilder();
            if (sp.Count > 0)
            {

                foreach (var sql in sp)
                {
                    if (!primaryKey.Equals(sql.param_name))
                    {
                        sb.Append(" " + sql.param_name + "=" + "@" + sql.param_name + ",");
                    }
                }
                //sp.ForEach
                //    (
                //         delegate(sql_param sql)
                //         {
                //             sb.Append(" " + sql.param_name + "=" + "@" + sql.param_name + ",");
                //         }
                //     );
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
            }

            return sb.ToString();
        }

        #endregion


        #region 查询语句（不分页）
        private static string Update(sql_com obj)
        {
            StringBuilder sb = new StringBuilder();
            ConcurrentQueue<sql_param> sp = obj.param_object;

            sb.Append("update " + obj.table_name + " set ");
            // 表明 +  set + 非空属性字段 =  @ + 非空属性字段
            sb.Append(sql_update_set(sp,obj.PrimaryKey));
            // where 
            if (!string.IsNullOrWhiteSpace(obj.PrimaryKey))
            {
                sb.Append(" where ");
                sb.Append(" " + obj.PrimaryKey +" = '" + obj.PrimaryKeyValue +"'");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 查询语句（不分页）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj_where"></param>
        /// <returns></returns>
        private static string Select(sql_com obj)
        {
            ConcurrentQueue<sql_param> sp = obj.param_object;

            StringBuilder sb = new StringBuilder();
            if (null != obj)
            {
                if (sp.Count > 0)
                {
                    sb.Append("select * from " + obj.table_name + " ");
                    sb.Append(" where ");
                    // 非空属性字段 =  @ + 非空属性字段
                    sb.Append(sql_equal(sp));
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 查询语句（不分页）
        private static string Update(sql_com obj, sql_com obj_where)
        {
            StringBuilder sb = new StringBuilder();
            ConcurrentQueue<sql_param> sp = obj.param_object;
            ConcurrentQueue<sql_param> sp_where = obj_where.param_object;

            sb.Append("update " + obj.table_name + " set ");
            // 表明 +  set + 非空属性字段 =  @ + 非空属性字段
            sb.Append(sql_update_set(sp,obj.PrimaryKey));
            // where 
            sb.Append(" where ");
            sb.Append(sql_equal(sp_where));
            return sb.ToString();
        }

        /// <summary>
        /// 查询语句（不分页）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj_where"></param>
        /// <returns></returns>
        private static string Select(sql_com obj, sql_com obj_where)
        {
            ConcurrentQueue<sql_param> sp = obj.param_object;
            ConcurrentQueue<sql_param> sp_where = obj_where.param_object;
            StringBuilder sb = new StringBuilder();
            if (null != obj_where)
            {
                if (sp_where.Count > 0)
                {
                    sb.Append("select * from " + obj_where.table_name + " ");
                    sb.Append(" where ");
                    // 非空属性字段 =  @ + 非空属性字段
                    sb.Append(sql_equal(sp_where));
                }
            }
            else if (null != obj)
            {
                if (sp.Count > 0)
                {
                    sb.Append("select * from " + obj.table_name + " ");
                    sb.Append(" where ");
                    // 非空属性字段 =  @ + 非空属性字段
                    sb.Append(sql_equal(sp));
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 删除操作
        #region 拼接where之后的语句
        /// <summary>
        /// 拼接where之后的语句
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        private static string sql_equal(ConcurrentQueue<sql_param> sp)
        {
            StringBuilder sb = new StringBuilder();
            if (sp.Count > 0)
            {
                foreach (var sql in sp)
                {
                    sb.Append(" " + sql.param_name + "=" + "@" + sql.param_name + " and ");
                }

                //sp.ForEach
                //    (
                //         delegate(sql_param sql)
                //         {
                //             sb.Append(" " + sql.param_name + "=" + "@" + sql.param_name + " and ");
                //         }
                //     );
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 4, 4);
                }
            }

            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="obj_where"></param>
        /// <returns></returns>
        private static string Delete(sql_com obj_where)
        {
            ConcurrentQueue<sql_param> sp_where = obj_where.param_object;
            StringBuilder sb = new StringBuilder();
            if (sp_where.Count > 0)
            {
                sb.Append("delete from " + obj_where.table_name + " ");
                // 表明 +  where + 非空属性字段 =  @ + 非空属性字段
                sb.Append(" where ");
                // string result = string.Join()

                sb.Append(sql_equal(sp_where));
            }
            return sb.ToString();
        }
        #endregion

        #region 获取自定义属性

        #region 获取所有自定义属性信息
        private static List<string> getAllAttribute<T>(Type objType) where T : class,new()
        {
            List<string> listColumnName = new List<string>();
            //Type objType = typeof(T);
            //取属性上的自定义特性
            foreach (PropertyInfo propInfo in objType.GetProperties())
            {
                object[] objAttrs = propInfo.GetCustomAttributes(typeof(EnitityMappingAttribute), true);
                if (objAttrs.Length > 0)
                {
                    EnitityMappingAttribute attr = objAttrs[0] as EnitityMappingAttribute;
                    if (attr != null)
                    {
                        listColumnName.Add(attr.ColumnName); //列名
                    }
                }
            }
            return listColumnName;
        }
        #endregion

        #region 获取表名
        private static string getTableName<T>(Type objType) where T : class,new()
        {
            string tableName = string.Empty;
            //取类上的自定义特性
            //Type objType = typeof(T);
            object[] objs = objType.GetCustomAttributes(typeof(EnitityMappingAttribute), true);
            foreach (object obj in objs)
            {
                EnitityMappingAttribute attr = obj as EnitityMappingAttribute;
                if (attr != null)
                {

                    tableName = attr.TableName;//表名只有获取一次
                    break;
                }
            }
            return tableName;
        }
        #endregion

        #region 获取主键
        private static string getPrimaryKey(Type objType)
        {
            string primaryKey = string.Empty;
            //取类上的自定义特性
            //Type objType = typeof(T);
            object[] objs = objType.GetCustomAttributes(typeof(EnitityMappingAttribute), true);
            foreach (object obj in objs)
            {
                EnitityMappingAttribute attr = obj as EnitityMappingAttribute;
                if (attr != null)
                {

                    primaryKey = attr.PrimaryKey;//表名只有获取一次
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(primaryKey))
            {
                primaryKey = null;
            }
            return primaryKey;
        }
        #endregion

        #endregion

        #region 反射实体对象
        /// <summary>
        /// 便利实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static sql_com VisitProperties<T>(T obj)
        {
            int mu = 1;
            sql_com sc = new sql_com();
            //List<sql_param> sql_object = new List<sql_param>();
            ConcurrentQueue<sql_param> sql_object = new ConcurrentQueue<sql_param>();
            if (null == obj)
            { }
            else
            {
                #region //获取属性
                //var type = obj.GetType();
                //var names = type.DeclaringType;
                ////var name2 = type.DeclaringMethod;
                //var name3 = type.FullName;
                //var name4 = type.GetProperties();  // 获取所有属性
                ////var name5 = type.GetProperty();
                //var name6 = type.GetType();
                //var name7 = type.IsClass;
                //var name8 = type.IsUnicodeClass;
                //var name9 = type.ReflectedType;
                //var name10 = type.TypeInitializer;
                //string name = type.Name;  // 类名
                //string namespaces = type.Namespace;  // 命名空间 
                #endregion

                var type = obj.GetType();
                string name = type.Name;  // 类名
                var paraExpression = Expression.Parameter(typeof(T), name);

                sc.PrimaryKey = getPrimaryKey(type);

                Parallel.ForEach(type.GetProperties(), (prop) =>
                {
                    var propType = prop.PropertyType;
                    // 判断是否为积累型或string类型
                    // 访问方式的表达式树为 obj => obj.Property
                    if (propType.IsPrimitive || propType == typeof(String))
                    {
                        VisitProperty<T>(obj, prop, paraExpression, paraExpression, sql_object);   // 获取每个属性的名称值
                    }
                    else
                    {
                        // 对于访问的表达式树为a：o obj => obj.otherObj.Property.
                        var otherType = prop.PropertyType;
                        MemberExpression memberExpression = Expression.Property(paraExpression, prop);
                        // 访问obj otherObj里的所有公共属性

                        Parallel.ForEach(otherType.GetProperties(), (otherProp) =>
                        {
                            VisitProperty<T>(obj, otherProp, memberExpression, paraExpression, sql_object);
                            //MessageBox.Show(li.Count.ToString());
                        });


                        //foreach (var otherProp in otherType.GetProperties())
                        //{
                        //    VisitProperty<T>(obj, otherProp, memberExpression, paraExpression, sql_object);
                        //}
                    }
                });

                #region //

                //foreach (var prop in type.GetProperties()) // 获取类的所有属性
                //{
                //    var propType = prop.PropertyType;
                //    // 判断是否为积累型或string类型
                //    // 访问方式的表达式树为 obj => obj.Property
                //    if (propType.IsPrimitive || propType == typeof(String))
                //    {
                //        VisitProperty<T>(obj, prop, paraExpression, paraExpression, sql_object);   // 获取每个属性的名称值
                //    }
                //    else
                //    {
                //        // 对于访问的表达式树为a：o obj => obj.otherObj.Property.
                //        var otherType = prop.PropertyType;
                //        MemberExpression memberExpression = Expression.Property(paraExpression, prop);
                //        // 访问obj otherObj里的所有公共属性



                //        foreach (var otherProp in otherType.GetProperties())
                //        {
                //            VisitProperty<T>(obj, otherProp, memberExpression, paraExpression, sql_object);
                //        }
                //    }
                //} 
                #endregion
                if (sql_object.Count > 0)
                {
                    sc.table_name = name;
                    sc.param_object = sql_object;
                }
            }
            return sc;
        }
        #endregion

        #region 发射实体对象中的属性
        /// <summary>
        /// 发射实体对象中的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="prop"></param>
        /// <param name="instanceExpression"></param>
        /// <param name="parameterExpression"></param>
        /// <param name="sql_object"></param>
        static void VisitProperty<T>(Object obj, PropertyInfo prop, Expression instanceExpression, ParameterExpression parameterExpression, ConcurrentQueue<sql_param> sql_object)
        {
            sql_param sp = new sql_param();
            Func<T, object> func;

            MemberExpression memExpresion = Expression.Property(instanceExpression, prop);
            Expression objectExpression = Expression.Convert(memExpresion, typeof(object));
            Expression<Func<T, object>> lambaExpression = Expression.Lambda<Func<T, object>>(objectExpression, parameterExpression);
            func = lambaExpression.Compile();
            sp.param_value = func((T)obj);
            if (null != sp.param_value)
            {
                sp.param_name = prop.Name;
                sp.param_value = func((T)obj);
                sql_object.Enqueue(sp);
            }
            // MessageBox.Show(prop.Name + ": " + func((T)obj));
        }
        #endregion
    }

    #region 存放反射实体对象信息
    #region 存放反射得到的实体集合以及实体名称（表名） 非空字段
    /// <summary>
    /// 存放反射得到的实体集合以及实体名称（表名） 非空字段
    /// </summary>
    public class sql_com
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey;
        public string PrimaryKeyValue;
        /// <summary>
        /// 表名
        /// </summary>
        public string table_name;
        /// <summary>
        /// 字段信息列表
        /// </summary>
        public ConcurrentQueue<sql_param> param_object;
    }
    #endregion
    #region 存放反射得到的字段信息  非空字段
    /// <summary>
    ///  存放反射得到的字段信息  非空字段
    /// </summary>
    public class sql_param
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string param_name;
        /// <summary>
        /// 字段值
        /// </summary>
        public Object param_value;
    }
    #endregion
    #endregion
}
