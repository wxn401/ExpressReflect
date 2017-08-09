using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 支付类型实体
    /// </summary>
    [EnitityMapping(TableName = "sel_Pay_Type", PrimaryKey = "sel_Pay_id")]
    public class sel_Pay_Type
    {
        /// <summary>
        /// 支付类型编号
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_id")]
        public string sel_Pay_id { get; set; }
        /// <summary>
        /// 支付类型名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_Name")]
        public string sel_Pay_Name { get; set; }
        /// <summary>
        /// 支付类型（0 代表正常金额，1 代表 损失）
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Pay_Satie")]
        public int sel_Pay_Satie { get; set; }
    }
}
