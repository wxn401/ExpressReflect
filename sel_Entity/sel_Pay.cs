using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 支付方式实体
    /// </summary>
    [EnitityMapping(TableName = "sel_Pay", PrimaryKey = "sel_Pay_id")]
    public class sel_Pay
    {
        /// <summary>
        /// 支付编号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_id")]
        public string sel_Pay_id { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_Money")]
        public decimal? sel_Pay_Money { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_Remark")]
        public string sel_Pay_Remark { get; set; }
        /// <summary>
        /// 是否为已付款（0 未付款，1 已付款（也可以是用户定金））
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_State")]
        public string sel_Pay_State { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_Time")]
        public DateTime? sel_Pay_Time { get; set; }
     }
}
