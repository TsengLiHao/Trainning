﻿using System;
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
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.Session["UserInfo"] == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var dt = ListInfoManager.GetListInfo();

            if (dt.Rows.Count > 0)
            {
                var pagedList = this.GetPagedDataTable(dt);

                this.gvList.DataSource = pagedList;
                this.gvList.DataBind();

                this.ucPager.TotalSize = dt.Rows.Count;
                this.ucPager.Bind();
            }
            else
            {
                this.ucPager.Visible = false;
                this.ltMsg.Text = "No Data";
                return;
            }

        }

        private void SearchTitle()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT ListID, ID, ListName, ListContent, Status, StartTime, Endtime FROM ListInfo";
                    if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
                    {
                        sql += " WHERE ListName LIKE @ListName + '%'";
                        cmd.Parameters.AddWithValue("@ListName", txtTitle.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvList.DataSource = dt;
                        gvList.DataBind();
                    }
                }
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

        protected void btnTitleSearch_Click(object sender, EventArgs e)
        {
            this.SearchTitle();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var startTime = this.txtStart.Text;
            var endTime = this.txtEnd.Text;

            var dt = ListInfoManager.SearchDate(startTime, endTime);
            this.gvList.DataSource = dt;
            this.gvList.DataBind();

            if(string.IsNullOrEmpty(this.txtEnd.Text))
            {
                var dtStart = ListInfoManager.SearchDateStart(startTime);
                this.gvList.DataSource = dtStart;
                this.gvList.DataBind();
            }

            if(string.IsNullOrEmpty(this.txtStart.Text))
            {
                var dtEnd = ListInfoManager.SearchDateEnd(endTime);
                this.gvList.DataSource = dtEnd;
                this.gvList.DataBind();
            }

            if (!string.IsNullOrEmpty(this.txtStart.Text) && !string.IsNullOrEmpty(this.txtEnd.Text))
            {
                DateTime start = Convert.ToDateTime(startTime);
                DateTime end = Convert.ToDateTime(endTime);

                TimeSpan diff = end - start;

                if (Convert.ToInt32(diff.TotalDays) < 0)
                {
                    //this.ltMsg.Text = "選取日期範圍不合理";
                    var sEnd = this.txtStart.Text;
                    var eStart = this.txtEnd.Text;

                    var fakeDt = ListInfoManager.SearchDate(eStart, sEnd);
                    this.gvList.DataSource = fakeDt;
                    this.gvList.DataBind();
                }
                //else
                //{
                //    this.ltMsg.Text = "";
                //}
            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow row in gvList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chxDelete = row.FindControl("cbxChoose") as CheckBox;
                    if (chxDelete.Checked)
                    {
                        int id = Convert.ToInt32(row.Cells[1].Text);
                        ListInfoManager.DeleteList(id);
                    }
                }
            }
            Response.Redirect(this.Request.RawUrl);
        }
    }
}