using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 客户实体
    /// </summary>
    [EnitityMapping(TableName = "sel_user", PrimaryKey = "sel_user_id")]
    public class sel_user
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_id")]
        public string sel_user_id { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Name")]
        public string sel_user_Name { get; set; }
        /// <summary>
        /// 客户密码
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Pwd")]
        public string sel_user_Pwd { get; set; }
        /// <summary>
        /// 用户类型编号  0 客户  1 供应商
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Type_id")]
        public int? sel_user_Type_id { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Tel")]
        public string sel_user_Tel { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Mail")]
        public string sel_user_Mail { get; set; }
        /// <summary>
        /// 用户住址
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Addr")]
        public string sel_user_Addr { get; set; }
        /// <summary>
        /// 用户所在片区名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Addr_Code_Name")]
        public string sel_user_Addr_Code_Name { get; set; }
        /// <summary>
        /// 用户账号余额
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Money")]
        public decimal? sel_user_Money { get; set; }
        /// <summary>
        /// 用户状态 0 启用 1 注销
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Site")]
        public int? sel_user_Site { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_Add_DateTime")]
        public DateTime? sel_user_Add_DateTime { get; set; }
        /// <summary>
        /// 预留字段1
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_More1")]
        public string sel_user_More1 { get; set; }
        /// <summary>
        /// 预留字段2
        /// </summary>
        [EnitityMapping(ColumnName = "sel_user_More2")]
        public string sel_user_More2 { get; set; }

    }
}
