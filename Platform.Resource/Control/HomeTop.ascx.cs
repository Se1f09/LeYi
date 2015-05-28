using System;
using Homory.Model;

namespace Control
{
    public partial class ControlHomeTop : HomoryResourceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeHomoryControl();
            }
        }

        protected void InitializeHomoryControl()
        {
            home_top_sign_on_go.Visible = !IsOnline;
            home_top_user_label.Visible = home_top_sign_off_go.Visible = IsOnline;
            home_top_user_label.InnerText = IsOnline ? string.Format("你好，{0}", CurrentUser.DisplayName) : string.Empty;
        }

        protected void home_top_sign_on_go_OnServerClick(object sender, EventArgs e)
        {
            Session["RESOURCE"] = "RESOURCE";
            SignOn();
        }

        protected void home_top_sign_off_go_OnServerClick(object sender, EventArgs e)
        {
            Session.Clear();
            Session["RESOURCE"] = "RESOURCE";
            SignOff();
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }
    }
}
