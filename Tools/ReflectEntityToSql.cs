using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public class ReflectEntityToSql
    {
        //public static 
        public ReflectEntityToSql()
        {
        }

        /// <summary>
        /// 反射实体类
        /// </summary>
        /// <param name="sqlCommand">insert、update、select命令</param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static string ReflectEntityForSql(string sqlCommand, Object obj)
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

    }
}
