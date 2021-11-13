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
    public partial class ConfirmPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var id = Request.QueryString["ID"];

            var drList = ListInfoManager.GetListInfoByID(id);

            this.ltStatus.Text = drList["Status"].ToString();
            this.ltStart.Text = Convert.ToDateTime(drList["StartTime"]).ToString("yyyyMMdd");
            this.ltEnd.Text = Convert.ToDateTime(drList["EndTime"]).ToString("yyyyMMdd");
            this.lblTitle.Text = drList["ListName"].ToString();
            this.txtContent.Text = drList["ListContent"].ToString();

            this.ltName.Text = this.Session["Name"].ToString();
            this.ltPhone.Text = this.Session["Phone"].ToString();
            this.ltEmail.Text = this.Session["Email"].ToString();
            this.ltAge.Text = this.Session["Age"].ToString();

            var dt = QuestionInfoManager.GetQuestionByID(id);

            foreach (DataRow dr in dt.Rows)
            {
                //var questionID = Convert.ToInt32(dr["QuestionID"]);
                //var dtOfAnswer = QuestionInfoManager.GetAnswerInfoByID(id, questionID);

                this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionID"] + "."));

                this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionName"] + "<br />"));
                

                Literal ID = new Literal();
                ID.Text = dr["QuestionID"].ToString();

                if (dr["Type"].ToString() == "文字方塊")
                {
                    //TextBox txtBox = new TextBox();
                    //txtBox.ID = "txtBox" + dr["QuestionID"];
                    //txtBox.Enabled = false;
                    //this.PlaceHolder1.Controls.Add(txtBox);

                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (this.Session[$"ResponseOfTest{i}"] != null)
                        {
                            if (i == Convert.ToInt32(dr["QuestionID"]))
                                this.PlaceHolder1.Controls.Add(new LiteralControl(this.Session[$"ResponseOfTest{i}"] + "<br />"));
                        }
                        else
                            continue;
                    }

                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    
                }
                else if (dr["Type"].ToString() == "單選方塊")
                {
                    //RadioButtonList radioList = new RadioButtonList();
                    //radioList.ID = "radioList" + dr["QuestionID"];
                    //radioList.Enabled = false;
                    //this.PlaceHolder1.Controls.Add(radioList);

                    //radioList.DataSource = dtOfAnswer;
                    //radioList.DataTextField = "Answer";
                    //radioList.DataBind();

                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (this.Session[$"ResponseOfRadio{i}"] != null)
                        {
                            if (i == Convert.ToInt32(dr["QuestionID"]))
                                this.PlaceHolder1.Controls.Add(new LiteralControl(this.Session[$"ResponseOfRadio{i}"] + "<br />"));
                        }
                        else
                            continue;
                    }

                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
                else if (dr["Type"].ToString() == "複選方塊")
                {
                    //CheckBoxList checkList = new CheckBoxList();
                    //checkList.ID = "checkList" + dr["QuestionID"];
                    //checkList.Enabled = false;
                    //this.PlaceHolder1.Controls.Add(checkList);

                    //checkList.DataSource = dtOfAnswer;
                    //checkList.DataTextField = "Answer";
                    //checkList.DataBind();

                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (this.Session[$"ResponseOfCheck{i}"] != null)
                        {
                            if(i == Convert.ToInt32(dr["QuestionID"]))
                            this.PlaceHolder1.Controls.Add(new LiteralControl(this.Session[$"ResponseOfCheck{i}"] + "<br />"));
                        }
                        else
                            continue;
                    }

                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
            }

            //for (int i = 1; i < dt.Rows.Count; i++)
            //{
            //    var getTextValue = (TextBox)this.PlaceHolder1.FindControl($"txtBox{i}");

            //    if (getTextValue != null)
            //    {
            //        getTextValue.Text = this.Session[$"ResponseOfTest{i}"].ToString();
            //    }
            //    else
            //        continue;
            //}
            //for (int i = 1; i < dt.Rows.Count; i++)
            //{
            //    var getRadioValue = (RadioButtonList)this.PlaceHolder1.FindControl($"radioList{i}");

            //    if (getRadioValue != null)
            //    {
            //        getRadioValue.SelectedValue = this.Session[$"ResponseOfRadio{i}"].ToString();
            //    }
            //    else
            //        continue;
            //}

            //for (int i = 1; i < dt.Rows.Count; i++)
            //{
            //    var getCheckValue = (CheckBoxList)this.PlaceHolder1.FindControl($"checkList{i}");

            //    if (getCheckValue != null)
            //    {
            //        getCheckValue.SelectedItem.Text.Contains(this.Session[$"ResponseOfCheck{i}"].ToString()) ;
            //    }
            //    else
            //        continue;
            //}
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];

            var name = this.ltName.Text;
            var txtphone = Convert.ToInt32(this.ltPhone.Text) ;
            var email = this.ltEmail.Text;
            var txtage = Convert.ToInt32(this.ltAge.Text);

            int phone = txtphone;
            int age = txtage;

            var dt = QuestionInfoManager.GetQuestionByID(id);

            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                if (this.Session[$"ResponseOfTest{i}"] != null)
                {
                    var answer = this.Session[$"ResponseOfTest{i}"].ToString();
                    ReplyInfoManager.CreateReply(name, phone, email, age, id, i, answer);
                }
                else
                    continue;
            }

            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                if (this.Session[$"ResponseOfRadio{i}"] != null)
                {
                    var answer = this.Session[$"ResponseOfRadio{i}"].ToString();
                    ReplyInfoManager.CreateReply(name, phone, email, age, id, i, answer);
                }
                else
                    continue;
            }

            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                if (this.Session[$"ResponseOfCheck{i}"] != null)
                {
                    var answer = this.Session[$"ResponseOfCheck{i}"].ToString();
                    ReplyInfoManager.CreateReply(name, phone, email, age, id, i, answer);
                }
                else
                    continue;
            }



            Response.Redirect("/List.aspx"); 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Form.aspx");
        }
    }
}