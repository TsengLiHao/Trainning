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
            if (!IsPostBack)
            {
                var commonQuestion = CommonQuestionInfoManager.GetCommonQuestionInfo();

                ddlType.DataSource = commonQuestion;
                ddlType.DataTextField = "CommonQuestionTitle";
                ddlType.DataBind();
                ddlType.Items.Insert(0, new ListItem("自訂問題"));

                var id = Request.QueryString["ID"];

                if (Request.QueryString["ID"] != null)
                {
                    var dr = ListInfoManager.GetListInfoByID(id);

                    this.txtName.Text = dr["ListName"].ToString();
                    this.txtContent.Text = dr["ListContent"].ToString();

                    var dt = QuestionInfoManager.GetQuestionByID(id);

                    gvQuestionStatus.DataSource = dt;
                    gvQuestionStatus.DataBind();

                    if (dt != null)
                    {
                        this.Session["DataOfQuestion"] = dt;
                    }

                    //DataTable dtCurrent = new DataTable();
                    //dtCurrent.Columns.AddRange(new DataColumn[4] { new DataColumn("#"), new DataColumn("問題"), new DataColumn("種類"), new DataColumn("必填") });
                    //Session["CurrentTable"] = dtCurrent;
                    //this.BindGrid();
                }

                ddlQuestionType.Items.Insert(0, new ListItem("--Choose Type--"));

                DataTable dtQuestion = new DataTable();

                dtQuestion.Columns.Add(new DataColumn("QuestionID", typeof(int)));
                dtQuestion.Columns.Add(new DataColumn("QuestionName", typeof(string)));
                dtQuestion.Columns.Add(new DataColumn("Type", typeof(string)));
                dtQuestion.Columns.Add(new DataColumn("Required",typeof(bool)));
                dtQuestion.Columns.Add(new DataColumn("Answer", typeof(string)));

                this.Session["QuestionInfo"] = dtQuestion;

                this.BindGrid();
            }

            this.ddlQuestionType.Items[0].Attributes["disabled"] = "disabled";
        }
        protected void BindGrid()
        {
            GridView1.DataSource = this.Session["NewRow"] as DataTable;
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

            if (this.Session["LoadID"] != null)
            {
                var loadQuestionID = Convert.ToInt32(this.Session["LoadID"].ToString());
                var loadQuestionName = this.txtQuestion.Text;
                var loadType = this.ddlQuestionType.SelectedValue;
                var loadAnswer = this.txtAnswer.Text;
                if (this.cbxCheck.Checked)
                {
                    this.HiddenField2.Value = "1";
                    loadQuestionName += "(必填)";
                }
                else
                {
                    this.HiddenField2.Value = "0";
                }

                var loadRequired = Convert.ToInt32(this.HiddenField2.Value);

                DataTable dtQuestion = this.Session["QuestionInfo"] as DataTable;

                DataRow drQuestion = dtQuestion.NewRow();

                drQuestion[0] = loadQuestionID;
                drQuestion[1] = loadQuestionName;
                drQuestion[2] = loadType;
                drQuestion[3] = loadRequired;
                drQuestion[4] = loadAnswer;


                dtQuestion.Rows.Add(drQuestion);
                dtQuestion.AcceptChanges();

                this.Session["NewRow"] = dtQuestion;
            }
            else
            {
                var questionName = this.txtQuestion.Text;
                var type = this.ddlQuestionType.SelectedValue;
                var answer = this.txtAnswer.Text;
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


                DataTable dt = this.Session["DataOfQuestion"] as DataTable;

                DataTable dtCurrent;
                if (this.Session["NewRow"] != null)
                {
                    dtCurrent = this.Session["NewRow"] as DataTable;

                    DataRow drCurrent = dtCurrent.NewRow();

                    if (Request.QueryString["ID"] != null)
                    {
                        var number = dtCurrent.Rows.Count;
                        for (var i = 0; i <= dt.Rows.Count; i++)
                        {
                            number++;
                        }
                        drCurrent[0] = number;
                        drCurrent[1] = questionName;
                        drCurrent[2] = type;
                        drCurrent[3] = required;
                        drCurrent[4] = answer;
                    }
                    else
                    {
                        var number = dtCurrent.Rows.Count;

                        number++;

                        drCurrent[0] = number;
                        drCurrent[1] = questionName;
                        drCurrent[2] = type;
                        drCurrent[3] = required;
                        drCurrent[4] = answer;
                    }
                    dtCurrent.Rows.Add(drCurrent);
                    dtCurrent.AcceptChanges();

                    this.Session["NewRow"] = dtCurrent;
                }
                else //First Row
                {
                    DataTable dtQuestion = this.Session["QuestionInfo"] as DataTable;

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
                        drQuestion[4] = answer;
                    }
                    else
                    {
                        var number = 0;

                        number++;

                        drQuestion[0] = number;
                        drQuestion[1] = questionName;
                        drQuestion[2] = type;
                        drQuestion[3] = required;
                        drQuestion[4] = answer;
                    }

                    dtQuestion.Rows.Add(drQuestion);
                    dtQuestion.AcceptChanges();

                    this.Session["NewRow"] = dtQuestion;
                }
            }


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

            DataTable dtQuestion = this.Session["DataOfQuestion"] as DataTable;

            var rowCount = dtQuestion.Rows.Count;

            DataTable dt = (DataTable)this.Session["NewRow"];

            foreach(DataRow dr in dt.Rows)
            {
                var id = Request.QueryString["ID"];
                var questionID = Convert.ToInt32(dr["QuestionID"]);
                var loadID = Convert.ToInt32(dr["QuestionID"]);
                var name = dr["QuestionName"].ToString();
                var type = dr["Type"].ToString();
                var required = Convert.ToInt32(dr["Required"]);
                var answer = dr["Answer"].ToString();

                if (this.Session["LoadID"] != null)
                    QuestionInfoManager.UpdateQuestion(id, loadID, name, type, required, answer);
                else
                    QuestionInfoManager.CreateQuestion(id, questionID, name, type, required,answer);
            }

            this.Session["NewRow"] = null;
            this.Session["LoadID"] = null;

            Response.Redirect(this.Request.RawUrl);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //foreach (GridViewRow grow in GridView1.Rows)
            //{
            //    //Searching CheckBox("chkDel") in an individual row of Grid  
            //    CheckBox chkdel = (CheckBox)grow.FindControl("cbxChoose");
            //    //If CheckBox is checked than delete the record with particular empid  
            //    if (chkdel.Checked)
            //    {
            //        this.Session["NewRow"] = null;
            //    }
            //}
            foreach (GridViewRow row in gvQuestionStatus.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chxDelete = row.FindControl("cbxChoose") as CheckBox;
                    if (chxDelete.Checked)
                    {
                        int id = Convert.ToInt32(row.Cells[1].Text);
                        QuestionInfoManager.DeleteQuestion(id);
                    }
                }
            }
            Response.Redirect(this.Request.RawUrl);
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable table = Session["NewRow"] as DataTable;// get data from session
            if (table != null)
            {
                foreach (DataRow item in table.Rows) //loop through all the row
                {
                    if ((int)item["QuestionID"] == (int)e.Keys[0]) // convert id from object to int 
                    {
                        table.Rows.Remove(item); // delete the row
                        break;
                    }
                }
                GridView1.DataSource = table; // rebind data
                GridView1.DataBind();
            }
        }

        protected void ddlQuestionType_TextChanged(object sender, EventArgs e)
        {
            this.ddlQuestionType.Items[0].Attributes["disabled"] = "disabled";

            if (this.ddlQuestionType.Text == "單選方塊" || this.ddlQuestionType.Text == "複選方塊")
            {
                this.txtAnswer.Enabled = true;
            }
            else
            {
                this.txtAnswer.Enabled = false;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btnLoad = (Control)sender;
            GridViewRow row = (GridViewRow)btnLoad.NamingContainer;

            int id = Convert.ToInt32(row.Cells[1].Text);
            string name = row.Cells[2].Text;
            string type = row.Cells[3].Text;
            //string answer = row.Cells[5].Text.Replace("&nbsp;", "");
            string answer = Server.HtmlDecode(row.Cells[5].Text);

            this.txtQuestion.Text = name;
            this.ddlQuestionType.SelectedValue = type;

            if (this.ddlQuestionType.Text == "單選方塊" || this.ddlQuestionType.Text == "複選方塊")
                this.txtAnswer.Enabled = true;
            else
                this.txtAnswer.Enabled = false;

            if (this.ddlQuestionType.Text == "文字方塊")
                this.txtAnswer.Text = string.Empty;

            if (string.IsNullOrEmpty(answer))
                 this.txtAnswer.Text = string.Empty;
            else
                this.txtAnswer.Text = answer;

            var chx = row.Cells[4].FindControl("cbxRequired") as CheckBox;
            if(chx.Checked)
            {
                this.cbxCheck.Checked = true;
            }
            else
            {
                this.cbxCheck.Checked = false;
            }

            this.Session["LoadID"] = id;

        }

        protected void ddlType_TextChanged(object sender, EventArgs e)
        {
            var commonInfo = CommonQuestionInfoManager.GetCommonQuestionInfoByTitle(ddlType.SelectedValue);

            if(commonInfo != null)
            {
                this.txtQuestion.Text = commonInfo["CommonQuestionName"].ToString();
                this.ddlQuestionType.Text = commonInfo["Type"].ToString();
                this.txtAnswer.Text = commonInfo["Answer"].ToString();
                var required = Convert.ToInt32(commonInfo["Required"].ToString());

                if (required == 1)
                    this.cbxCheck.Checked = true;
                else
                    this.cbxCheck.Checked = false;
                
            }
        }
    }
}