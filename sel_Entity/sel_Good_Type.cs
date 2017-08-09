using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAttribute;

namespace sel_Entity
{
    /// <summary>
    /// 商品类别
    /// </summary>
    [EnitityMapping(TableName = "sel_Good_Type", PrimaryKey = "sel_Good_Type_Id")]
    public class sel_Good_Type
    {
        /// <summary>
        /// 商品类别
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Type_Id")]
        public string sel_Good_Type_Id { get; set; }
        /// <summary>
        /// 商品类别名称
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Type_Name")]
        public string sel_Good_Type_Name { get; set; }
        /// <summary>
        /// 商品类别状态  0 启用 1 注销
        /// </summary>
        [EnitityMapping(ColumnName = "sel_Good_Type_Site")]
        public int sel_Good_Type_Site { get; set; }
    }
}
