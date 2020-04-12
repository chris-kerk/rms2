using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ResumeManagementSystem
{
    public partial class Experience : System.Web.UI.Page
    {
        string ddlYear = string.Empty;
        string cs = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
        DropDownList ddl = new DropDownList();

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ddl = Master.FindControl("ddlYear") as DropDownList;
            ddlYear = ddl.SelectedValue;

            BindData();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["LoadStatus"] = null;
                this.PopulateMonth();
                this.PopulateYear();

            }
            else
            {
               
            }
        }
     
        private void PopulateMonth()
        {
            ddlMonthF.Items.Clear();
            ddlMonthT.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "MM";
            lt.Value = "0";
            ddlMonthF.Items.Add(lt);
            ddlMonthT.Items.Add(lt);
            for (int i = 1; i <= 12; i++)
            {
                lt = new ListItem();
                lt.Text = Convert.ToDateTime("1/" + i.ToString() + "/1900").ToString("MMMM");
                lt.Value = i.ToString();
                ddlMonthF.Items.Add(lt);
                ddlMonthT.Items.Add(lt);
            }
            ddlMonthF.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
            ddlMonthT.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }

        private void PopulateYear()
        {
            ddlYearF.Items.Clear();
            ddlYearT.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "YYYY";
            lt.Value = "0";
            ddlYearF.Items.Add(lt);
            ddlYearT.Items.Add(lt);
            for (int i = DateTime.Now.Year; i >= 1950; i--)
            {
                lt = new ListItem();
                lt.Text = i.ToString();
                lt.Value = i.ToString();
                ddlYearF.Items.Add(lt);
                ddlYearT.Items.Add(lt);
            }
            ddlYearF.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
            ddlYearT.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        }

        private void BindData()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string sqlQuery = "Select * from CareerHistory where ResumeYear=" + ddlYear;
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);
                da.Fill(ds, "Career");
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables["Career"].Rows[0];
                txtComName.Text = dr["CompanyName"].ToString().Trim();
                txtPosition.Text = dr["Position"].ToString().Trim();

                //ddlMonthF.SelectedValue = dr["NATION_CODE"].ToString().Trim();
                //ddlYearF.SelectedValue = dr["HOLDER_CODE"].ToString().Trim();
                //ddlMonthT.SelectedValue = dr["NATION_CODE"].ToString().Trim();
                //ddlYearT.SelectedValue = dr["HOLDER_CODE"].ToString().Trim();
                lblDuration.Text = dr["Duration"].ToString().Trim();
                txtDesc.InnerText = dr["JobDescription"].ToString().Trim();
                
                Session["LoadStatus"] = string.Empty;
            }
            else
            {
                txtComName.Text = string.Empty;
                txtPosition.Text = string.Empty;

                //ddlMonthF.SelectedValue = string.Empty;
                //ddlYearF.SelectedValue = string.Empty;
                //ddlMonthT.SelectedValue = string.Empty;
                //ddlYearT.SelectedValue = string.Empty;
                lblDuration.Text = string.Empty;
                txtDesc.InnerText = string.Empty;

                Session["LoadStatus"] = "No record in the Year of " + ddlYear + ".";
            }
            lblStatus.Text = string.Empty;
        }
        protected void btnUpdate_Click(object sender, EventArgs e) 
        {
            ddl = Master.FindControl("ddlYear") as DropDownList;
            ddlYear = ddl.SelectedValue;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SPUpdateCareer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ComName", txtComName.Text);
                cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                cmd.Parameters.AddWithValue("@StartP", GetPeriod(ddlYearF.SelectedValue, ddlMonthF.SelectedValue)); 
                cmd.Parameters.AddWithValue("@EndP", GetPeriod(ddlYearT.SelectedValue, ddlMonthT.SelectedValue));
                cmd.Parameters.AddWithValue("@Duration", GetDuration());
                cmd.Parameters.Add("@JobDescription", SqlDbType.VarChar).Value = txtDesc.InnerText;
                //cmd.Parameters.AddWithValue("@JobDescription", txtDesc.InnerText);
                cmd.Parameters.AddWithValue("@Year",ddlYear);
            
                con.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Update Successfully!");
            }

            Session["LoadStatus"] = String.Empty;
        }

        private string GetPeriod(string YearTxt, string MonthTxt)
        {
            string value = string.Empty;

            if (MonthTxt.Length < 2)
                value = YearTxt + "0" + MonthTxt;
            else
                value = YearTxt + MonthTxt;

            return value;
        }
        private string GetDuration()
        {
            string Duration = string.Empty;


            return Duration;
        }
    }
}