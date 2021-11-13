using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trainning.DBSource;

namespace Trainning
{
    public partial class Stastic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
            {
                Response.Redirect("/List.aspx");
            }
            var id = Request.QueryString["ID"];

            var drList = ListInfoManager.GetListInfoByID(id);

            this.lblTitle.Text = drList["ListName"].ToString();

            var dt = QuestionInfoManager.GetQuestionByID(id);

            foreach (DataRow dr in dt.Rows)
            {
                var questionID = Convert.ToInt32(dr["QuestionID"]);

                var dtOfAnswer = QuestionInfoManager.GetAnswerInfoByID(id, questionID);

                if (dr["Type"].ToString() == "單選方塊" || dr["Type"].ToString() == "複選方塊")
                {
                    this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionID"] + "."));

                    this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionName"] + "<br />"));
                }

                if (dr["Type"].ToString() == "單選方塊")
                {
                    

                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
                else if (dr["Type"].ToString() == "複選方塊")
                {
                    

                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }

                Chart1.DataSource = dt;
                Chart1.Series["Series1"].XValueMember = "Answer";
                Chart1.Series["Series1"].YValueMembers = "Type";
                Chart1.DataBind();
            }
        }
    }
}