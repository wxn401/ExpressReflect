using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 订单信息实体
    /// </summary>
    [EnitityMapping(TableName = "sel_Order", PrimaryKey = "sel_Order_id")]
    public class sel_Order
    {
        /// <summary>
        /// 用户订单编号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_id")]
        public string sel_Order_id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_user_Name")]
        public string sel_Order_user_Name { get; set; }
        /// <summary>
        /// 是否已经支付 (0 未付清， 1 已付清 ) 采用复试记账的方式进行
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Pay")]
        public int sel_Order_Pay { get; set; }
        /// <summary>
        /// 用户支付编号(与流水关联)
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Pay_id")]
        public string sel_Order_Pay_id { get; set; }
        /// <summary>
        /// 商品状态（已发货、未发货、预订商品）
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Give")]
        public string sel_Order_Give { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Add_Order_Time")]
        public DateTime? sel_Order_Add_Order_Time { get; set; }
        /// <summary>
        /// 订单状态 0 代表正常 ，1 代表退货
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Site")]
        public string sel_Order_Site { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Remark")]
        public string sel_Order_Remark { get; set; }
        /// <summary>
        /// 商品发货日期
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Give_Time")]
        public DateTime? sel_Order_Give_Time { get; set; }
    }
}
