using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                if (this.Session["UserInfo"] == null)
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ucPager.Visible = false;
                this.cbxTurnOn.Checked = true;

                var commonQuestion = CommonQuestionInfoManager.GetCommonQuestionInfo();

                ddlType.DataSource = commonQuestion;
                ddlType.DataTextField = "CommonQuestionTitle";
                ddlType.DataBind();
                ddlType.Items.Insert(0, new ListItem("自訂問題"));

                var id = Request.QueryString["ID"];

                ddlQuestionType.Items.Insert(0, new ListItem("--Choose Type--"));

                DataTable dtQuestion = new DataTable();

                dtQuestion.Columns.Add(new DataColumn("QuestionID", typeof(int)));
                dtQuestion.Columns.Add(new DataColumn("QuestionName", typeof(string)));
                dtQuestion.Columns.Add(new DataColumn("Type", typeof(string)));
                dtQuestion.Columns.Add(new DataColumn("Required", typeof(bool)));
                dtQuestion.Columns.Add(new DataColumn("Answer", typeof(string)));

                this.Session["QuestionInfo"] = dtQuestion;

                this.BindGrid();


                if (Request.QueryString["ID"] != null)
                {
                    var dr = ListInfoManager.GetListInfoByID(id);

                    this.txtName.Text = dr["ListName"].ToString();
                    this.txtContent.Text = dr["ListContent"].ToString();
                    this.txtStart.Text = dr["StartTime"].ToString();
                    this.txtEnd.Text = dr["EndTime"].ToString();

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

                    var dtReply = ReplyInfoManager.GetReplyInfoByListID(id);

                    this.gvReply.DataSource = dtReply;
                    this.gvReply.DataBind();

                    //if (dtReply.Rows.Count > 0)
                    //{
                    //    this.ucPager.Visible = true;

                    //    var pagedList = this.GetPagedDataTable(dtReply);

                    //    this.gvReply.DataSource = pagedList;
                    //    this.gvReply.DataBind();

                    //    this.ucPager.TotalSize = dtReply.Rows.Count;
                    //    this.ucPager.Bind();
                    //}
                    //else
                    //{
                    //    this.ucPager.Visible = false;
                    //    this.ltMsg.Text = "No Data";
                    //    return;
                    //}

                    if (dtReply.Rows.Count <= 0)
                        this.ltMsg.Text = "No Data";
                    else
                        this.ltMsg.Text = "";
                }
            }

            if (Request.QueryString["ID"] != null)
            {
                var id = Request.QueryString["ID"];
                var dt = QuestionInfoManager.GetQuestionByID(id);


                foreach (DataRow drQuestion in dt.Rows)
                {
                    var questionID = Convert.ToInt32(drQuestion["QuestionID"]);

                    var dtOfAnswer = QuestionInfoManager.GetAnswerInfoByID(id, questionID);
                    var dtOfReply = ReplyInfoManager.GetReplyInfo(id, questionID);

                    this.PlaceHolder4.Controls.Add(new LiteralControl(drQuestion["QuestionID"] + "."));

                    this.PlaceHolder4.Controls.Add(new LiteralControl(drQuestion["QuestionName"] + "<br />"));

                    if (drQuestion["Type"].ToString() == "文字方塊")
                    {
                        for (int i = 1; i <= dt.Rows.Count; i++)
                        {
                            if (i == Convert.ToInt32(drQuestion["QuestionID"]))
                            {
                                this.PlaceHolder4.Controls.Add(new LiteralControl("-" + "<br />"));
                            }
                        }
                    }
                    else if (drQuestion["Type"].ToString() == "單選方塊")
                    {
                        for (int i = 1; i <= dt.Rows.Count; i++)
                        {
                            if (i == Convert.ToInt32(drQuestion["QuestionID"]))
                            {
                                for (int j = 0; j < dtOfAnswer.Rows.Count; j++)
                                {
                                    this.PlaceHolder4.Controls.Add(new LiteralControl($"第{j + 1}單選:"));

                                    DataRow drAnswer = dtOfAnswer.Rows[j];

                                    var value = drAnswer["value"].ToString();
                                    var dtCount = QuestionInfoManager.GetReplySin(value, id, i);
                                    int count = dtCount.Rows.Count;
                                    var dtInputCount = QuestionInfoManager.GetInputCount(id, i);
                                    int inputCount = dtInputCount.Rows.Count;

                                    if (count == 0)
                                        this.PlaceHolder4.Controls.Add(new LiteralControl($"0%" + $"({0 + count })" + "<br />"));
                                    else
                                        this.PlaceHolder4.Controls.Add(new LiteralControl($"{(100 / inputCount) * count}%" + $"({0 + count })" + "<br />"));

                                }
                            }
                        }
                    }
                    else if (drQuestion["Type"].ToString() == "複選方塊")
                    {
                        for (int i = 1; i <= dt.Rows.Count; i++)
                        {
                            if (i == Convert.ToInt32(drQuestion["QuestionID"]))
                            {
                                for (int j = 0; j < dtOfAnswer.Rows.Count; j++)
                                {
                                    this.PlaceHolder4.Controls.Add(new LiteralControl($"第{j + 1}複選:"));

                                    DataRow drAnswer = dtOfAnswer.Rows[j];

                                    var value = drAnswer["value"].ToString();
                                    var dtCount = QuestionInfoManager.GetReplyMul(value, id, i);
                                    int count = dtCount.Rows.Count;
                                    var dtMulCount = QuestionInfoManager.GetMulCount(id, i);
                                    int mulCount = dtMulCount.Rows.Count;

                                    if (count == 0)
                                        this.PlaceHolder4.Controls.Add(new LiteralControl($"0%" + $"({0 + count })" + "<br />"));
                                    else
                                        this.PlaceHolder4.Controls.Add(new LiteralControl($"{(count * 100) / mulCount}%" + $"({0 + count })" + "<br />"));
                                }
                            }
                        }
                    }
                }
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
            Response.Redirect("/SystemAdmin/List.aspx");
        }

        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text) || string.IsNullOrEmpty(this.txtContent.Text) || string.IsNullOrEmpty(this.txtStart.Text) || string.IsNullOrEmpty(this.txtEnd.Text))
            {
                this.ltListMsg.Text = "請填寫空白處";
                return;
            }

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



            if (Request.QueryString["ID"] != null)
            {
                var id = Request.QueryString["ID"].ToString();

                ListInfoManager.UpdateListInfo(id, name, content, status, startTime, endTime);
                
            }
            else
            {
                ListInfoManager.CreateList(name, content, status, startTime, endTime);
            }

            Response.Redirect("/SystemAdmin/List.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];

            if(this.ddlQuestionType.Text == "--Choose Type--")
            {
                this.ltQuestionMsg.Text = "請選擇問題種類";
                return;
            }

            if (string.IsNullOrEmpty(this.txtQuestion.Text))
            {
                this.ltQuestionMsg.Text = "請填寫空白處";
                return;
            }

            if(string.IsNullOrEmpty(this.txtAnswer.Text))
            {
                if (this.ddlQuestionType.Text == "單選方塊" || this.ddlQuestionType.Text == "複選方塊")
                {
                    this.ltQuestionMsg.Text = "請填寫空白處";
                    return;
                }
            }

            if (!this.txtAnswer.Text.Contains(";"))
            {
                if (this.ddlQuestionType.Text == "單選方塊" || this.ddlQuestionType.Text == "複選方塊")
                {
                    this.ltQuestionMsg.Text = "回答部分的答案請以:分隔";
                    return;
                }
            }

            this.ltQuestionMsg.Text = "";

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
            Response.Redirect("/SystemAdmin/List.aspx");
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

            if(dt == null)
            {
                this.ltQuestionMsg.Text = "尚未有題目可以新增,請先加入題目在執行";
                return;
            }

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

                if (this.ddlQuestionType.Text == "單選方塊" || this.ddlQuestionType.Text == "複選方塊")
                    this.txtAnswer.Enabled = true;
                else
                    this.txtAnswer.Enabled = false;

            }
        }

        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;
            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;
            if (intPage <= 0)
                return 1;
            return intPage;
        }

        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone();

            int startIndex = (this.GetCurrentPage() - 1) * 10;
            int endIndex = (this.GetCurrentPage()) * 10;
            if (endIndex > dt.Rows.Count)
                endIndex = dt.Rows.Count;

            for (var i = startIndex; i < endIndex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            this.PlaceHolder1.Visible = false;
            this.PlaceHolder2.Visible = true;
            this.PlaceHolder3.Visible = true;

            var btnLoad = (Control)sender;
            GridViewRow row = (GridViewRow)btnLoad.NamingContainer;

            var name = row.Cells[1].Text;

            var dr = ReplyInfoManager.GetReplyByName(name);

            this.txtDetailName.Text = dr["Name"].ToString();
            this.txtDetailPhone.Text = dr["Phone"].ToString();
            this.txtDetailEmail.Text = dr["Email"].ToString();
            this.txtDetailAge.Text = dr["Age"].ToString();
            this.ltReplyTime.Text = dr["ReplyTime"].ToString();

            var id = Request.QueryString["ID"];

            var dt = QuestionInfoManager.GetQuestionByID(id);

            foreach (DataRow drQuestion in dt.Rows)
            {
                this.PlaceHolder3.Controls.Add(new LiteralControl(drQuestion["QuestionID"] + "."));

                this.PlaceHolder3.Controls.Add(new LiteralControl(drQuestion["QuestionName"] + "<br />"));

                if (drQuestion["Type"].ToString() == "文字方塊")
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (i == Convert.ToInt32(drQuestion["QuestionID"]))
                        {
                            var drQuestionID = ReplyInfoManager.GetReply(name,i);
                            this.PlaceHolder3.Controls.Add(new LiteralControl(drQuestionID["ReplyAnswer"] + "<br />"));
                        }
                    }
                }
                else if (drQuestion["Type"].ToString() == "單選方塊")
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (i == Convert.ToInt32(drQuestion["QuestionID"]))
                        {
                            var drQuestionID = ReplyInfoManager.GetReply(name, i);
                            this.PlaceHolder3.Controls.Add(new LiteralControl(drQuestionID["ReplyAnswer"] + "<br />"));
                        }
                    }
                }
                else if (drQuestion["Type"].ToString() == "複選方塊")
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (i == Convert.ToInt32(drQuestion["QuestionID"]))
                        {
                            var drQuestionID = ReplyInfoManager.GetReply(name, i);
                            this.PlaceHolder3.Controls.Add(new LiteralControl(drQuestionID["ReplyAnswer"] + "<br />"));
                        }
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            this.PlaceHolder1.Visible = true;
            this.PlaceHolder2.Visible = false;
            this.PlaceHolder3.Visible = false;
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];

            if (id != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string dbCommandString =
                    $@" SELECT Name as 姓名, Phone as 電話, Email, Age as 年齡, ListInfo.ListName as 問卷, QuestionInfo.QuestionName as 問題, QuestionInfo.Type as 問題種類, ReplyAnswer as 回答
                    FROM ReplyInfo
				    JOIN QuestionInfo
					ON ReplyInfo.QuestionID = QuestionInfo.QuestionID AND ReplyInfo.ListID = QuestionInfo.ID
					JOIN ListInfo
					ON ReplyInfo.ListID = ListInfo.ID
                    WHERE ReplyInfo.ListID = @id
					ORDER BY Name ASC
                ";

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(dbCommandString, con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);

                                //Build the CSV file data as a Comma separated string.
                                string csv = string.Empty;

                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Header row for CSV file.
                                    csv += column.ColumnName + ',';
                                }

                                //Add new line.
                                csv += "\r\n";

                                foreach (DataRow row in dt.Rows)
                                {
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        //Add the Data rows.
                                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                    }

                                    //Add new line.
                                    csv += "\r\n";
                                }

                                //Download the CSV file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
                                Response.Charset = "";
                                Response.ContentType = "application/text";
                                Response.Output.Write(csv);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                }
            }
            else
                this.ltMsg.Text = "NO DATA";
        }
    }
}