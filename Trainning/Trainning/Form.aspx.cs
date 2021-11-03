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
            if(Request.QueryString["ID"] == null)
            {
                Response.Redirect("/List.aspx");
            }

            var id = Request.QueryString["ID"];
            
            var drList = ListInfoManager.GetListInfoByID(id);
            
            this.ltStatus.Text = drList["Status"].ToString();
            this.ltStart.Text = Convert.ToDateTime(drList["StartTime"]).ToString("yyyyMMdd");
            this.ltEnd.Text = Convert.ToDateTime(drList["EndTime"]).ToString("yyyyMMdd");
            this.lblTitle.Text = drList["ListName"].ToString();
            this.txtContent.Text = drList["ListContent"].ToString();
            
            var dt = QuestionInfoManager.GetQuestionByID(id);
            
            foreach (DataRow dr in dt.Rows)
            {
                var questionID = Convert.ToInt32(dr["QuestionID"]);
            
                var dtOfAnswer = AnswerInfoManager.GetAnswerInfoByID(id, questionID);
            
                this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionID"] + "."));
            
                this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionName"] + "<br />"));
            
                Literal ID = new Literal();
                ID.Text = dr["QuestionID"].ToString();
            
                if (dr["Type"].ToString() == "文字方塊")
                {
                    TextBox txtBox = new TextBox();
                    txtBox.ID = "txtBox";
                    this.PlaceHolder1.Controls.Add(txtBox);
                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
                else if (dr["Type"].ToString() == "單選方塊")
                {
                    RadioButtonList radioList = new RadioButtonList();
                    radioList.ID = "radioList";
                    this.PlaceHolder1.Controls.Add(radioList);
            
                    radioList.DataSource = dtOfAnswer;
                    radioList.DataTextField = "Answer";
                    radioList.DataBind();
            
                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
                else if (dr["Type"].ToString() == "多選方塊")
                {
                    CheckBoxList checkList = new CheckBoxList();
                    checkList.ID = "checkList";
                    this.PlaceHolder1.Controls.Add(checkList);
            
                    checkList.DataSource = dtOfAnswer;
                    checkList.DataTextField = "Answer";
                    checkList.DataBind();
            
                    this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
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


            var getTextValue = (TextBox)this.PlaceHolder1.FindControl("txtBox");
            this.Literal1.Text = getTextValue.Text;

            this.Session["Response"] = this.Literal1.Text;
            //var getRadioValue = (RadioButtonList)this.PlaceHolder1.FindControl("radioList");
            ////this.Literal1.Text = getRadioValue.SelectedValue;

            //var getCheckValue = (CheckBoxList)this.PlaceHolder1.FindControl("checkList");
            ////this.Label1.Text = getCheckValue.SelectedValue;

            //for (int i = 0; i < getCheckValue.Items.Count; i++)
            //{
            //    if (getCheckValue.Items[i].Selected)
            //    {
            //        this.Label1.Text += getCheckValue.Items[i].Value.Trim() + ";";
            //    }
            //}

            Response.Redirect($"/ConfirmPage.aspx?ID={id}");
        }
    }
}