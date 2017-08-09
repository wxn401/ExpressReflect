using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 系统管理员
    /// </summary>
    [EnitityMapping(TableName = "sel_Admin",PrimaryKey = "sel_Admin_id")]
    public class sel_Admin
    {
        /// <summary>
        /// 管理员流水号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Admin_id")]
        public string sel_Admin_id { get; set; }
        /// <summary>
        /// 管理员登录名
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Admin_Name")]
        public string sel_Admin_Name { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Admin_Pwd")]
        public string sel_Admin_Pwd { get; set; }
        /// <summary>
        /// 用户类型 
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Type_id")]
        public string sel_user_Type_id { get; set; }
    }
}
