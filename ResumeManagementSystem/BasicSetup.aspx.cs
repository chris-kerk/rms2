using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ResumeManagementSystem
{
    public partial class BasicSetup : System.Web.UI.Page
    {
        public int ID = 0;
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
           if(!Page.IsPostBack)
            {
                Session["LoadStatus"] = null;
            }
        }

        private void BindData()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string sqlQuery = "Select * from BasicDetails where ResumeYear=" + ddlYear;
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);
                da.Fill(ds, "Basic");
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables["Basic"].Rows[0];
                byte[] ImageData = string.IsNullOrEmpty(dr["ImageData"].ToString()) ? null : (byte[])(dr["ImageData"]);
                txtName.Text = dr["NAME"].ToString().Trim();
                txtAlias.Text = dr["ALIAS"].ToString().Trim();
                ddlNation.SelectedValue = dr["NATION_CODE"].ToString().Trim();
                ddlHolder.SelectedValue = dr["HOLDER_CODE"].ToString().Trim();
                txtContact.Text = dr["CONTACT"].ToString().Trim();
                txtAddress.Text = dr["ADDRESS"].ToString().Trim();
                txtOthers.Text = dr["OTHERS"].ToString().Trim();

                string strBase64 = (ImageData == null) ? "" : Convert.ToBase64String(ImageData);
                imgPhoto.ImageUrl = (ImageData == null) ? "" : "data:Image/png;base64," + strBase64;

                Session["LoadStatus"] = string.Empty;
            }
            else
            {
                txtName.Text = string.Empty;
                txtAlias.Text = string.Empty;
                ddlNation.SelectedValue = "";
                ddlHolder.SelectedValue = "";
                txtContact.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtOthers.Text = string.Empty;
                imgPhoto.ImageUrl = string.Empty;

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
                SqlCommand cmd = new SqlCommand("SPUpdateBasic", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Alias", txtAlias.Text);
                cmd.Parameters.AddWithValue("@Nation", ddlNation.SelectedValue);
                cmd.Parameters.AddWithValue("@Holder", ddlHolder.SelectedValue);
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                cmd.Parameters.AddWithValue("@Add", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Others", txtOthers.Text);
                cmd.Parameters.AddWithValue("@Year", ddlYear);

                SqlParameter outParam = new SqlParameter();
                outParam.ParameterName = "@ID";
                outParam.SqlDbType = System.Data.SqlDbType.Int;
                outParam.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                con.Open();
                cmd.ExecuteNonQuery();

                ID = Convert.ToInt32(outParam.Value);

                MessageBox.Show("Update Successfully!");
            }

            Session["LoadStatus"] = String.Empty;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ddl = Master.FindControl("ddlYear") as DropDownList;
            ddlYear = ddl.SelectedValue;

            HttpPostedFile postedFile = PhotoUpload.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;

            string cs = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
            lblMsgPhoto.Text = string.Empty;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".gif")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spUpdatePhoto", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ResumeYear", ddlYear);
                    cmd.Parameters.AddWithValue("@ImageData", bytes);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    //con.Open();
                    //byte[] bytes = (byte[])cmd.ExecuteScalar();
                    string strBase64 = Convert.ToBase64String(bytes);
                    imgPhoto.ImageUrl = "data:Image/png;base64," + strBase64;
                    //PhotoUpload.FileName = fileName;
                }
                MessageBox.Show("Upload Successfully!");
            }
            else
            {
                lblMsgPhoto.Visible = true;
                lblMsgPhoto.Text = "Only images (.jpg, .png, .gif and .bmp) can be uploaded";
                lblMsgPhoto.ForeColor = System.Drawing.Color.Red;

            }
        }

        private DataTable GetTempTable()
        {
            DataTable Temp = new DataTable("Basic");

            Temp.Columns.Add("NAME", typeof(string));
            Temp.Columns.Add("ALIAS", typeof(string));
            Temp.Columns.Add("NATION_CODE", typeof(string));
            Temp.Columns.Add("HOLDER_CODE", typeof(string));
            Temp.Columns.Add("CONTACT", typeof(string));
            Temp.Columns.Add("ADDRESS", typeof(string));
            Temp.Columns.Add("OTHERS", typeof(string));
            Temp.Columns.Add("RESUMEYEAR", typeof(string));
            Temp.Columns.Add("ID", typeof(int));

            DataRow dr = Temp.NewRow();

            dr["NAME"] = txtName.Text;
            dr["ALIAS"] = txtAlias.Text;
            dr["NATION_CODE"] = ddlNation.SelectedValue;
            dr["HOLDER_CODE"] = ddlHolder.SelectedValue;
            dr["CONTACT"] = txtContact.Text;
            dr["ADDRESS"] = txtAddress.Text;
            dr["OTHERS"] = txtOthers.Text;
            dr["RESUMEYEAR"] = ddlYear;
            dr["ID"] = Convert.ToInt32(ViewState["ID"].ToString().Trim());

            Temp.Rows.Add(dr);

            return Temp;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}