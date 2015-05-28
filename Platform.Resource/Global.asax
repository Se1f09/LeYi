<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Homory.Startup" %>

<script RunAt="server">

	void Application_Start(object sender, EventArgs e)
	{
		AreaRegistration.RegisterAllAreas();
		RouteConfig.RegisterRoutes(RouteTable.Routes);
	}

	void Application_End(object sender, EventArgs e)
	{

	}

	void Application_PostAuthorizeRequest()
	{
		HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
	}

	void Application_Error(object sender, EventArgs e)
	{

	}

	void Session_Start(object sender, EventArgs e)
	{

	}

	void Session_End(object sender, EventArgs e)
	{

	}
       
</script>
