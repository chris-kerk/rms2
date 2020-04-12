using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResumeManagementSystem
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["LoadStatus"] != null)
                lblLoadStatus.Text = Session["LoadStatus"].ToString().Trim();
            else
                lblLoadStatus.Text = string.Empty;
        }
            public void SetDirtyOff()
        {
            string theScript = "";
            theScript = "var dty=document.getElementById('BP_bIsDirty');" + "\r\n";
            theScript += "if (dty != null) { dty.value='false';isClean=false; }" + "\r\n";
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "setdirtyoff", theScript, true);
        }
        protected void btnMasterLoad_Click(object sender, EventArgs e)
        {
            SetDirtyOff();

        }
    }
}