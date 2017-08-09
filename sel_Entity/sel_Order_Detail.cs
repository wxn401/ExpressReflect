using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 订单详细信息实体
    /// </summary>
    [EnitityMapping(TableName = "sel_Order_Detail", PrimaryKey = "sel_Order_Detail_id")]
    public class sel_Order_Detail
    {
        /// <summary>
        /// 详细订单信息编号（日期、用户id、商品编号）
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_id")]
        public string sel_Order_Detail_id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_user_Name")]
        public string sel_Order_Detail_user_Name { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_Good_Name")]
        public string sel_Order_Detail_Good_Name { get; set; }
        /// <summary>
        /// 商品类别名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_Good_Type_Name")]
        public string sel_Order_Detail_Good_Type_Name { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_Good_Price")]
        public decimal? sel_Order_Detail_Good_Price { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_Good_Number")]
        public decimal? sel_Order_Detail_Good_Number { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_Money_id")]
        public decimal? sel_Order_Detail_Money_id { get; set; }
        /// <summary>
        /// 商品折扣
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_DePrices")]
        public decimal? sel_Order_Detail_DePrices { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Order_Detail_Time")]
        public DateTime? sel_Order_Detail_Time { get; set; }
    }
}
