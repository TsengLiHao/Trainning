using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trainning
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        /// <summary> 頁面 url </summary>
        public string Url { get; set; }
        /// <summary> 總筆數 </summary>
        public int TotalSize { get; set; }
        /// <summary> 頁面筆數 </summary>
        public int PageSize { get; set; }
        /// <summary> 目前頁數 </summary>
        public int CurrentPage { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int GetCurrentPage()
        {
            string pageText = this.Request.QueryString["page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int pageIndex = 0;
            if (!int.TryParse(pageText, out pageIndex))
                return 1;

            return pageIndex;
        }
        public void Bind()
        {
            if (this.PageSize <= 0)
            {
                throw new DivideByZeroException();
            }

            int totalPage = this.TotalSize / this.PageSize;
            if (this.TotalSize % this.PageSize > 0)
                totalPage += 1;

            this.aLinkFirst.HRef = $"{this.Url}?page=1";
            this.aLinkLast.HRef = $"{this.Url}?page={totalPage}";


            this.CurrentPage = this.GetCurrentPage();
            this.ltlCurrentPage.Text = this.CurrentPage.ToString();

            int prevM1 = this.CurrentPage - 1;
            int prevM2 = this.CurrentPage - 2;

            this.aLink2.HRef = $"{this.Url}?page={prevM1}";
            this.aLink2.InnerHtml = prevM1.ToString();
            this.aLink1.HRef = $"{this.Url}?page={prevM2}";
            this.aLink1.InnerHtml = prevM2.ToString();

            int nextP1 = this.CurrentPage + 1;
            int nextP2 = this.CurrentPage + 2;

            this.aLink4.HRef = $"{this.Url}?page={nextP1}";
            this.aLink4.InnerHtml = nextP1.ToString();
            this.aLink5.HRef = $"{this.Url}?page={nextP2}";
            this.aLink5.InnerHtml = nextP2.ToString();

            this.aLink1.Visible = (prevM2 > 0);
            this.aLink2.Visible = (prevM1 > 0);
            this.aLink4.Visible = (nextP1 <= totalPage);
            this.aLink5.Visible = (nextP2 <= totalPage);

            //this.ltPager.Text = $"共 {this.TotalSize} 筆，共 {totalPage} 頁，目前在第 {this.GetCurrentPage()} 頁<br/>";
        }
    }
}