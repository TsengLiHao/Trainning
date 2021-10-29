using System;
using System.Collections.Generic;
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
        }
    }
}