using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAttribute
{
    /// <summary>
    /// 自定义特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = true)]
    public class EnitityMappingAttribute : Attribute
    {
        private string primaryKey;
        /// <summary>
        /// 对应数据表中的主键
        /// </summary>
        public string PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }

        private string tableName;
        /// <summary>
        /// 实体实际对应的表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string columnName;
        /// <summary>
        /// 中文列名
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

    }
}
