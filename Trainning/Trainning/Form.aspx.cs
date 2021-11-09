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
    public partial class Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] == null)
                {
                    Response.Redirect("/List.aspx");
                }

                var id = Request.QueryString["ID"];

                var drList = ListInfoManager.GetListInfoByID(id);

                this.ltStatus.Text = drList["Status"].ToString();
                this.ltStart.Text = Convert.ToDateTime(drList["StartTime"]).ToString("yyyy-MM-dd");
                this.ltEnd.Text = Convert.ToDateTime(drList["EndTime"]).ToString("yyyy-MM-dd");
                this.lblTitle.Text = drList["ListName"].ToString();
                this.txtContent.Text = drList["ListContent"].ToString();

                var start = Convert.ToDateTime(drList["StartTime"]);
                if (start > DateTime.Today)
                {
                    Response.Redirect("/List.aspx");
                    return;
                }

                var end = Convert.ToDateTime(drList["EndTime"]);
                if (end < DateTime.Today)
                {
                    Response.Redirect("/List.aspx");
                    return;
                }

                var name = this.txtName.Text;
                var phone = this.txtPhone.Text;
                var email = this.txtEmail.Text;
                var age = this.txtAge.Text;

                this.Session["Name"] = name;
                this.Session["Phone"] = phone;
                this.Session["Email"] = email;
                this.Session["Age"] = age;


                var dt = QuestionInfoManager.GetQuestionByID(id);

                foreach (DataRow dr in dt.Rows)
                {
                    var questionID = Convert.ToInt32(dr["QuestionID"]);

                    var dtOfAnswer = QuestionInfoManager.GetAnswerInfoByID(id, questionID);

                    this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionID"] + "."));

                    this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionName"] + "<br />"));

                    Literal ID = new Literal();
                    ID.Text = dr["QuestionID"].ToString();


                    if (dr["Type"].ToString() == "文字方塊")
                    {
                        TextBox txtBox = new TextBox();
                        txtBox.ID = "txtBox" + dr["QuestionID"];
                        this.PlaceHolder1.Controls.Add(txtBox);
                        this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    }
                    else if (dr["Type"].ToString() == "單選方塊")
                    {
                        RadioButtonList radioList = new RadioButtonList();
                        radioList.ID = "radioList" + dr["QuestionID"];
                        this.PlaceHolder1.Controls.Add(radioList);

                        radioList.DataSource = dtOfAnswer;
                        radioList.DataTextField = "value";
                        radioList.DataBind();

                        this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    }
                    else if (dr["Type"].ToString() == "複選方塊")
                    {
                        CheckBoxList checkList = new CheckBoxList();
                        checkList.ID = "checkList" + dr["QuestionID"];
                        this.PlaceHolder1.Controls.Add(checkList);

                        checkList.DataSource = dtOfAnswer;
                        checkList.DataTextField = "value";
                        checkList.DataBind();

                        this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/List.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];
            var dt = QuestionInfoManager.GetQuestionByID(id);

            for(int i = 1; i <= dt.Rows.Count; i++)
            {
                var getTextValue = (TextBox)this.PlaceHolder1.FindControl($"txtBox{i}");
                
                if (getTextValue != null)
                {
                    this.Literal1.Text = getTextValue.Text;
                    this.Session[$"ResponseOfTest{i}"] = this.Literal1.Text;
                }
                else
                    continue;
            }
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                var getRadioValue = (RadioButtonList)this.PlaceHolder1.FindControl($"radioList{i}");

                if (getRadioValue != null)
                {
                    this.Literal2.Text = getRadioValue.SelectedValue;
                    this.Session[$"ResponseOfRadio{i}"] = this.Literal2.Text;
                }
                else
                    continue;
            }

            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                var getCheckValue = (CheckBoxList)this.PlaceHolder1.FindControl($"checkList{i}");

                if (getCheckValue != null)
                {
                    //for (int j = 0; j < getCheckValue.Items.Count; j++)
                    //{
                    //    if (getCheckValue.Items[j].Selected)
                    //    {
                    //    this.Literal3.Text += getCheckValue.Items[j].Text;
                    //    }
                    //}

                    List<String> CheckedList = new List<string>();

                    foreach (ListItem item in getCheckValue.Items)
                    {
                        if (item.Selected)
                        {
                            CheckedList.Add(item.Value);
                        }
                    }

                    string Checked = string.Join(",", CheckedList.ToArray());

                    this.Session[$"ResponseOfCheck{i}"] = Checked;
                }
                else
                    continue;
            }

            Response.Redirect($"/ConfirmPage.aspx?ID={id}");
        }
    }
}