using System;
using System.Configuration;

namespace Tools
{
    public class SqlConnect
    {
        /// <summary>
        /// 获得Web.config中的数据库连接字符串
        /// </summary>
        public static string ConnectString
        {
            get
            {
                //return ConfigurationManager.ConnectionStrings["ExamSystemOLConnectionString"].ConnectionString;
                return ConfigurationManager.AppSettings["sqlConnectionString"];
            }
        }
        /// <summary>
        /// 获得Web.config中的数据库连接字符串
        /// </summary>
        /// <param name="configName">Web.config中连接字符串的Key</param>
        /// <returns>返回相应的数据库连接字符串</returns>
        public static string GetConnectString(string configName)
        {
            return ConfigurationManager.AppSettings[configName];
        }

    }
}