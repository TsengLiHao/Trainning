using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trainning.DBSource;

namespace Trainning.SystemAdmin
{
    public partial class Question : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dt = CommonQuestionInfoManager.GetCommonQuestionInfo();

                this.gvCommonQuestion.DataSource = dt;
                this.gvCommonQuestion.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var title = this.txtTitle.Text;
            var name = this.txtQuestion.Text;
            var type = this.ddlQuestionType.Text;
            var answer = this.txtAnswer.Text;

            if (this.cbxCheck.Checked)
            {
                this.HiddenField1.Value = "1";
                name += "(必填)";
            }
            else
            {
                this.HiddenField1.Value = "0";
            }

            var required = Convert.ToInt32(this.HiddenField1.Value);
            if (this.Session["LoadID"] != null)
            {
                var id = Convert.ToInt32(this.Session["LoadID"].ToString());
                CommonQuestionInfoManager.UpdateCommonQuestion(id, title, name, type, required, answer);
            }
            else
            {
                CommonQuestionInfoManager.CreateCommonQuestion(title, name, type, required, answer);
            }

            this.Session["LoadID"] = null;

            Response.Redirect(this.Request.RawUrl);
        }

        protected void ddlQuestionType_TextChanged(object sender, EventArgs e)
        {
            if (this.ddlQuestionType.Text == "單選方塊" || this.ddlQuestionType.Text == "複選方塊")
            {
                this.txtAnswer.Enabled = true;
            }
            else
            {
                this.txtAnswer.Enabled = false;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var btnLoad = (Control)sender;
            GridViewRow row = (GridViewRow)btnLoad.NamingContainer;

            int id = Convert.ToInt32(row.Cells[0].Text);
            string title = row.Cells[1].Text;
            string name = row.Cells[2].Text;
            string type = row.Cells[3].Text;
            //string answer = row.Cells[5].Text.Replace("&nbsp;", "");
            string answer = Server.HtmlDecode(row.Cells[5].Text);

            this.txtTitle.Text = title;
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
            if (chx.Checked)
            {
                this.cbxCheck.Checked = true;
            }
            else
            {
                this.cbxCheck.Checked = false;
            }

            this.Session["LoadID"] = id;
        }
    }
}