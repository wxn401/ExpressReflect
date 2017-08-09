using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 商品实体
    /// </summary>
    [EnitityMapping(TableName = "sel_Good",PrimaryKey = "sel_Good_id")]
    public class sel_Good
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_id")]
        public string sel_Good_id { get ;set ;}
        /// <summary>
        /// 商品名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Name")]
        public string sel_Good_Name { get ; set ; }
        /// <summary>
        /// 商品价格
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Price")]
        public decimal? sel_Good_Price { get ; set ; }
        /// <summary>
        /// 商品进货价
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_InPrice")]
        public decimal? sel_Good_InPrice { get ; set ; }
        /// <summary>
        /// 商品数量
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Number")]
        public float? sel_Good_Number { get ; set ; }
        /// <summary>
        /// 计量方式（个、公斤、平米）
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Number_Type")]
        public string sel_Good_Number_Type { get ; set ; }
        /// <summary>
        /// 商品类别
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Type_Name")]
        public string sel_Good_Type_Name { get ; set ; }
        /// <summary>
        /// 入库时间
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Time")]
        public DateTime? sel_Good_Time { get ; set ; }
        /// <summary>
        /// 生产日期
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Start_Time")]
        public DateTime? sel_Good_Start_Time { get ; set ; }
        /// <summary>
        /// 过期日期
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_End_Time")]
        public DateTime? sel_Good_End_Time { get ;set ; }
        /// <summary>
        /// 保质期
        /// </summary>
        [System.ComponentModel.DataObjectField(false)]
        public decimal? sel_Good_Static_Time { get ; set ; }
        /// <summary>
        /// 最低库存
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Low_Number")]
        public DateTime? sel_Good_Low_Number { get ; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Now_Number")]
        public decimal? sel_Good_Now_Number { get ;set ; }
        /// <summary>
        /// 规格
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Format")]
        public DateTime? sel_Good_Format { get ; set ; }
        /// <summary>
        /// 单位
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Unit")]
        public DateTime? sel_Good_Unit { get ; set ; }
    }
}
