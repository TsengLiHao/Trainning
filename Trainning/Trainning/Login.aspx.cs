using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trainning.DBSource;

namespace Trainning
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.Session["UserInfo"] != null)
            {
                Response.Redirect("/Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPWD.Text;

            if (string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            {
                this.ltMsg.Text = "Account or Password is required.";
                return;
            }

            var dr = UserInfoManager.GetUserInfoAccount(inp_Account);

            if (dr == null)
            {
                this.ltMsg.Text = "Account doesn't exists.";
                return;
            }

            if (string.Compare(dr["Account"].ToString(), inp_Account, true) == 0 && string.Compare(dr["PWD"].ToString(), inp_PWD, false) == 0)
            {
                this.Session["UserInfo"] = dr["Account"].ToString();
                Response.Redirect("/SystemAdmin/List.aspx");
            }
            else
            {
                this.ltMsg.Text = "Login fail.";
                return;
            }
        }
    }
}