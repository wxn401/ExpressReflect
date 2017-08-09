using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 用户类型
    /// </summary>
    [EnitityMapping(TableName = "sel_user_Type", PrimaryKey = "sel_user_Type_id")]
    public class sel_user_Type
    {
        /// <summary>
        /// 用户类型编号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Type_id")]
        public string sel_user_Type_id { get; set; }
        /// <summary>
        /// 用户类型名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Type_Name")]
        public string sel_user_Type_Name { get; set; }
        /// <summary>
        /// 用户类型 0 管理员：经理（所有权限）、员工（部分权限）  1 用户  3 供应商
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Types")]
        public int? sel_user_Types { get; set; }
    }
}
