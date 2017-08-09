using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 用户片区实体
    /// </summary>
    [EnitityMapping(TableName = "sel_user_Addr_Code", PrimaryKey = "sel_user_Addr_Code_id")]
    public class sel_user_Addr_Code
    {
        /// <summary>
        /// 用户所处片区编码
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Addr_Code_id")]
        public string sel_user_Addr_Code_id { get; set; }
        /// <summary>
        /// 用户所处片区名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Addr_Code_Name")]
        public string sel_user_Addr_Code_Name { get; set; }
        /// <summary>
        /// 片区的父id   扩展字段，为以后做准备，方便扩大到市
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Addr_Code_Parentid")]
        public string sel_user_Addr_Code_Parentid { get; set; }
        /// <summary>
        /// 是否注销  0 未注销  1 已注销
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Addr_Code_Site")]
        public int sel_user_Addr_Code_Site { get; set; }
        /// <summary>
        /// 预留字段1
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Addr_Code_More1")]
        public int sel_user_Addr_Code_More1 { get; set; }
    }
}
