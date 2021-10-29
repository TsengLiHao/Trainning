using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trainning.DBSource;

namespace Trainning.SystemAdmin
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             
            var id = Request.QueryString["ID"];
            
            if (Request.QueryString["ID"] != null)
            {
                var dr = ListInfoManager.GetListInfoByID(id);
            
                this.txtName.Text = dr["ListName"].ToString();
                this.txtContent.Text = dr["ListContent"].ToString();

                var dt = QuestionInfoManager.GetQuestionByID(id);

                gvQuestionStatus.DataSource = dt;
                gvQuestionStatus.DataBind();
            }
            
            
            GridView1.DataSource = this.Session["QuestionInfo"];
            GridView1.DataBind();
        }
        protected void btnCancel1_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            var name = this.txtName.Text;
            var content = this.txtContent.Text;
            var startTime = Convert.ToDateTime(this.txtStart.Text).ToString("yyyyMMdd");
            var endTime = Convert.ToDateTime(this.txtEnd.Text).ToString("yyyyMMdd");

            if(this.cbxTurnOn.Checked)
            {
                this.HiddenField1.Value = "投票中";
            }
            else
            {
                this.HiddenField1.Value = "未開放";
            }

            var status = this.HiddenField1.Value;
            
            ListInfoManager.CreateList(name,content, status, startTime,endTime);
            Response.Redirect("/SystemAdmin/List.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            var id = Request.QueryString["ID"];

            var questionName = this.txtQuestion.Text;
            var type = this.ddlQuestionType.SelectedValue;

            if (this.cbxCheck.Checked)
            {
                this.HiddenField2.Value = "1";
                questionName += "(必填)";
            }
            else
            {
                this.HiddenField2.Value = "0";
            }

            var required = Convert.ToInt32(this.HiddenField2.Value);

            DataTable dt = (DataTable)gvQuestionStatus.DataSource;

            DataTable dtQuestion = new DataTable();
            
            dtQuestion.Columns.Add(new DataColumn("#"));
            dtQuestion.Columns.Add(new DataColumn("問題"));
            dtQuestion.Columns.Add(new DataColumn("種類"));
            dtQuestion.Columns.Add(new DataColumn("必填", typeof(bool)));

            DataRow drQuestion = dtQuestion.NewRow();

            if (Request.QueryString["ID"] != null)
            {
                var number = 0;
                for (var i = 0; i <= dt.Rows.Count; i++)
                {
                    number++;
                }
                drQuestion[0] = number;
                drQuestion[1] = questionName;
                drQuestion[2] = type;
                drQuestion[3] = required;
            }
            else
            {
                var number = 0;
                
                number++;
                
                drQuestion[0] = number;
                drQuestion[1] = questionName;
                drQuestion[2] = type;
                drQuestion[3] = required;
            }
            dtQuestion.Rows.Add(drQuestion);
            dtQuestion.AcceptChanges();
            GridView1.DataSource = dtQuestion;

            this.Session["QuestionInfo"] = dtQuestion;

            Response.Redirect(this.Request.RawUrl);
        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
            {
                Response.Write("<script>alert('尚未有問卷')</script>");
                return;
            }

            DataTable dt = (DataTable)this.Session["QuestionInfo"];

            foreach(DataRow dr in dt.Rows)
            {
                var id = Request.QueryString["ID"];
                var questionID = Convert.ToInt32(dr[1]);
                var name = dr[2].ToString();
                var type = dr[3].ToString();
                var required = Convert.ToInt32(dr[4]);

                QuestionInfoManager.CreateQuestion(id, questionID, name, type, required);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            var cbxChoose = (CheckBox)this.gvQuestionStatus.FindControl("cbxChoose");
            if(cbxChoose.Checked)
            {

            }
        }
    }
}