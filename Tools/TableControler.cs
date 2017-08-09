using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Tools
{
    public class TableControler:PublicMethod
    {
        /// <summary>
        /// 返回相应教师的试卷表
        /// </summary>
        /// <param name="teacherid">教师ID{0}为查询所有教师</param>
        /// <returns></returns>
        public static DataTable TeacherExamTable(int teacherid)
        {
            string sqlStr = "SELECT * FROM T_Exam ORDER BY BeginTime DESC";
            if (teacherid != 0)
            {
                sqlStr = "SELECT * FROM T_Exam WHERE Teacher_ID=" + teacherid + " ORDER BY BeginTime DESC";
            }
            DataTable dt = SqlHelper.Query(sqlStr).Tables[0];
            HandleStaticNullTable(dt);
            return dt;
        }

        /// <summary>
        /// 试题表
        /// </summary>
        /// <param name="examid"></param>
        /// <returns></returns>
        public static DataTable ExamItemTable(int examid)
        {
            string sqlStr = "SELECT ExamItemNo,ID_Item,TypeName,Title,Answer FROM T_Item,ExamInfo,T_ItemType WHERE ID_Item=Item_ID AND ItemType_ID=ID_ItemType AND ID_Exam="+examid+"";
            DataTable dt = SqlHelper.Query(sqlStr).Tables[0];
            HandleStaticNullTable(dt);
            return dt;
        }
        //select Title,T_Item.Answer,T_AnswerItem.Answer,T_AnswerItem.more2 as stuID  from T_Item,T_AnswerItem  where T_Item.ID_Item='"+ +"'  and T_AnswerItem.ID_Item='"+ +"' and T_AnswerItem.More3='examid'
        /// <summary>
        /// 主观题查找表
        /// </summary>
        /// <param name="examid"></param>
        /// <returns></returns>
        public static DataTable SujectExamItemTable(int ItemID, int examid,string stuID)
        {
            string sqlStr = "select Title as 题目,T_Item.Answer as 答案,T_AnswerItem.Answer as 学生答案,T_AnswerItem.more2 as 学号,T_ExamItem.Score as 分数  from T_Item,T_AnswerItem,T_ExamItem  where T_AnswerItem.more2='" + stuID + "' and T_Item.ID_Item=" + ItemID + "  and T_AnswerItem.Item_ID=" + ItemID + " and T_AnswerItem.More3=" + examid + " and T_ExamItem.Exam_ID=" + examid + " and T_ExamItem.Item_ID=" + ItemID + "";
            DataTable dt = SqlHelper.Query(sqlStr).Tables[0];
            HandleStaticNullTable(dt);
            return dt;
        }

        /// <summary>
        /// 试题表
        /// </summary>
        /// <param name="examid"></param>
        /// <returns></returns>
        public static DataTable ExamItemTable(int examid,int itemno)
        {
            string sqlStr = "SELECT ExamItemNo,ID_Item,TypeName,Title,Answer FROM T_Item,ExamInfo,T_ItemType WHERE ID_Item=Item_ID AND ItemType_ID=ID_ItemType AND ID_Exam="+examid+" AND ExamItemNo="+itemno+"";
            DataTable dt = SqlHelper.Query(sqlStr).Tables[0];
            HandleStaticNullTable(dt);
            return dt;
        }

        /// <summary>
        ///   学生学号
        /// </summary>
        /// <param name="examid"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static DataTable SujectStuID(int examid, int ItemID)
        {
            string sqlStr = "select distinct more2 from T_AnswerItem where Item_ID="+ ItemID +" and More3="+ examid +" ";
            DataTable dt = SqlHelper.Query(sqlStr).Tables[0];
            HandleStaticNullTable(dt);
            return dt;
        }

        //public static DataTable SujectStuID(int examid)
        //{
        //    string sqlStr = "select distinct more2 from T_AnswerItem where More3=" + examid + " ";
        //    DataTable dt = SqlHelper.Query(sqlStr).Tables[0];
        //    HandleStaticNullTable(dt);
        //    return dt;
        //}

        //SELECT ExamItemNo,ID_Item,TypeName,Title,T_Item.Answer,T_AnswerItem.Answer FROM T_Item,ExamInfo,T_ItemType,T_AnswerItem WHERE ID_Item= ExamInfo.Item_ID and ID_Item= T_AnswerItem.Item_ID AND ItemType_ID=ID_ItemType AND ID_Exam=" + examid + " AND ExamItemNo=" + itemno


    }
}
