using System;
using System.Data;

namespace Tools
{
    public class PublicMethod
    {
        protected DataTable HandleNullTable(DataTable dt)
        {
            if(dt.Rows.Count==0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].DataType = typeof(System.String);
                    dt.Columns[i].DefaultValue = "null";
                }
                dt.Rows.Add("null");
            }
            return dt;
        }

        protected static DataTable HandleStaticNullTable(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].DataType = typeof(System.String);
                    dt.Columns[i].DefaultValue = "null";
                }
                dt.Rows.Add("null");
            }
            return dt;
        }
    }
}
