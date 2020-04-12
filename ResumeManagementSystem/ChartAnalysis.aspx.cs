using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ResumeManagementSystem
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["LoadStatus"] = null;
                GetChartType();
                BindChart();
            }
        }

        private void BindChart() 
        {
            string cs = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT LanguageType, LevelOfMaster FROM SKILLS", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                //SkillChart.DataBindTable(rdr, "LanguageType");
                SkillChart.Series[0].XValueMember = "LanguageType";
                SkillChart.Series[0].YValueMembers = "LevelOfMaster";
                SkillChart.DataSource = rdr;
                SkillChart.DataBind();
            }  
        }

        private void GetChartType()
        {
            foreach (int chartType in Enum.GetValues(typeof(SeriesChartType)))
            {
                ListItem li = new ListItem(Enum.GetName(typeof(SeriesChartType), chartType), chartType.ToString());
                ddlChartType.Items.Add(li);
            }
        }
        protected void ddlChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            SkillChart.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddlChartType.SelectedValue);
            BindChart();

        }

     
    }
}