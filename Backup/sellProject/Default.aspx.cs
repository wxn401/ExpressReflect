using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;
using System.Collections.Concurrent;
using System.Data;
using ReflectToList;
using sel_Entity;
using System.Reflection;

namespace sellProject
{
    public partial class _Default : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            sel_Entity.sel_Admin admin = new sel_Entity.sel_Admin();
            sel_Entity.sel_Admin admin_where = new sel_Entity.sel_Admin();
            admin.sel_Admin_id = "1B27A3C7-B21F-4983-881F-21BCB0BB0701";
            //admin.sel_Admin_Name = "skywhere";
            //admin.sel_Admin_Pwd = "123where";
            //admin.sel_user_Type_id = new Guid().ToString();
            // admin.sel_user_Type_id = 1;
            //admin_where.sel_Admin_id = new Guid().ToString();
            Tools.ReflectEntity tools = new ReflectEntity();
            //ConcurrentQueue<mutilReflect> result = Tools.ReflectEntity.ReflectEntityForSql(Tools.ReflectEntity.getCommand.insert, admin, null);

            ConcurrentQueue<mutilReflect> result = Tools.ReflectEntity.ReflectEntityForSql(Tools.ReflectEntity.getCommand.select, admin);

            //var tt = Tools.ReflectEntity.ExecuteNonQuery(result);
            DataSet tt = Tools.ReflectEntity.ExecuteQueryDataSet(result) as DataSet;

            GenericList li = new GenericList();

            List<sel_Admin> list = li.DataSetToList<sel_Admin>(tt, tt.Tables[0].TableName);


            //var tt = Tools.ReflectEntity.ExecuteQueryDataSqlTran(result);
        }
    }
}
