using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Trainning.DBSource;

namespace Trainning
{
    public partial class Stastic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
            {
                Response.Redirect("/List.aspx");
            }
            var id = Request.QueryString["ID"];

            var drList = ListInfoManager.GetListInfoByID(id);

            this.lblTitle.Text = drList["ListName"].ToString();

            var dt = QuestionInfoManager.GetQuestionByID(id);

            foreach (DataRow dr in dt.Rows)
            {
                var questionID = Convert.ToInt32(dr["QuestionID"]);

                var dtOfAnswer = QuestionInfoManager.GetAnswerInfoByID(id, questionID);

                if (dr["Type"].ToString() == "單選方塊" || dr["Type"].ToString() == "複選方塊")
                {
                    this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionID"] + "."));

                    this.PlaceHolder1.Controls.Add(new LiteralControl(dr["QuestionName"] + "<br />"));
                }
                if (dr["Type"].ToString() == "單選方塊")
                {

                    Chart Chart1 = new Chart();
                    Series Series1 = new Series();
                    Chart1.Series.Add(Series1);
                    ChartArea ChartArea1 = new ChartArea();
                    Chart1.ChartAreas.Add(ChartArea1);
                    Series1.ChartType = SeriesChartType.Pie;

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("Answer");
                    dt1.Columns.Add("Persentage");

                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                       
                        if (i == Convert.ToInt32(dr["QuestionID"]))
                        {
                            for (int j = 0; j < dtOfAnswer.Rows.Count; j++)
                            {
                                DataRow drAnswer = dtOfAnswer.Rows[j];

                                var value = drAnswer["value"].ToString();
                                var dtCount = QuestionInfoManager.GetReplySin(value, id, i);
                                int count = dtCount.Rows.Count;
                                var dtInputCount = QuestionInfoManager.GetInputCount(id, i);
                                int inputCount = dtInputCount.Rows.Count;
                                
                                if (count == 0)
                                {
                                    dt1.Rows.Add($"{value}:"+ "0%" + $"({0 + count })", 0);

                                    this.PlaceHolder1.Controls.Add(Chart1);
                                    Chart1.DataSource = dt1;
                                    Chart1.Series["Series1"].XValueMember = "Answer";
                                    Chart1.Series["Series1"].YValueMembers = "Persentage";
                                    Chart1.DataBind();
                                }
                                else
                                {
                                    dt1.Rows.Add($"{value}:" + $"{(100 / inputCount) * count}%" + $"({0 + count })", (100 / inputCount) * count);

                                    this.PlaceHolder1.Controls.Add(Chart1);
                                    Chart1.DataSource = dt1;
                                    Chart1.Series["Series1"].XValueMember = "Answer";
                                    Chart1.Series["Series1"].YValueMembers = "Persentage";
                                    Chart1.DataBind();
                                }

                                this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                            }
                        }
                    }
                }
                else if (dr["Type"].ToString() == "複選方塊")
                {
                    Chart Chart1 = new Chart();
                    Series Series1 = new Series();
                    Chart1.Series.Add(Series1);
                    ChartArea ChartArea1 = new ChartArea();
                    Chart1.ChartAreas.Add(ChartArea1);
                    Series1.ChartType = SeriesChartType.Pie;

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("Answer");
                    dt1.Columns.Add("Persentage");

                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (i == Convert.ToInt32(dr["QuestionID"]))
                        {
                            for (int j = 0; j < dtOfAnswer.Rows.Count; j++)
                            {
                                DataRow drAnswer = dtOfAnswer.Rows[j];

                                var value = drAnswer["value"].ToString();
                                var dtCount = QuestionInfoManager.GetReplyMul(value, id, i);
                                int count = dtCount.Rows.Count;
                                var dtMulCount = QuestionInfoManager.GetMulCount(id, i);
                                int mulCount = dtMulCount.Rows.Count;

                                if (count == 0)
                                {
                                    dt1.Rows.Add($"{value}:" + "0%" + $"({0 + count })", 0);

                                    this.PlaceHolder1.Controls.Add(Chart1);
                                    Chart1.DataSource = dt1;
                                    Chart1.Series["Series1"].XValueMember = "Answer";
                                    Chart1.Series["Series1"].YValueMembers = "Persentage";
                                    Chart1.DataBind();
                                }
                                else
                                {
                                    dt1.Rows.Add($"{value}:" + $"{(count * 100) / mulCount}%" + $"({0 + count })", (count * 100) / mulCount);

                                    this.PlaceHolder1.Controls.Add(Chart1);
                                    Chart1.DataSource = dt1;
                                    Chart1.Series["Series1"].XValueMember = "Answer";
                                    Chart1.Series["Series1"].YValueMembers = "Persentage";
                                    Chart1.DataBind();
                                }
                                this.PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                            }
                        }
                    }
                }
            }
        }
    }
}