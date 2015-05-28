<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Go.GoHome" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
	<title>基础平台</title>
	<link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
	<link href="../Content/Homory/css/common.css" rel="stylesheet" />
	<link href="../Content/Core/css/home.css" rel="stylesheet" />
	<link href="../Content/Core/css/common.css" rel="stylesheet" />
	<script src="../Content/jQuery/jquery.min.js"></script>
	<script src="../Content/Semantic/javascript/semantic.min.js"></script>
	<script src="../Content/Homory/js/common.js"></script>
	<script src="../Content/Homory/js/notify.min.js"></script>
	<script src="../Content/Core/js/home.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
	<form id="formHome" runat="server">
		<telerik:RadScriptManager ID="scriptManager" runat="server"></telerik:RadScriptManager>
		<div class="ui stackable middle aligned page grid">
			<div class="nine wide column">
				<img class="ui image" src="../Common/配置/CoreLogo.png" alt="" />
			</div>
			<div class="seven wide center aligned column">
				<asp:Label ID="u" runat="server" CssClass="ui black label circular"></asp:Label>
				<asp:LinkButton ID="qb" runat="server" Text="退出" CssClass="ui black label circular" OnClick="qb_click"></asp:LinkButton>
			</div>
		</div>
		<br />
		<div id="allItems" class="ui center aligned stackable page grid">
			<asp:Repeater ID="repeater" runat="server">
				<ItemTemplate>
					<div class="five wide column rootPointer">
						<h2 class="ui center aligned icon header">
							<div><i class='<%# string.Format("circular emphasized icon {0}", Eval("Icon")) %>'></i></div>
							<div class="content">
								<div><%# Eval("Name") %></div>
								<div class="padSubMenu"></div>
								<div class="sub header"><%# SubMenu(Container.DataItem as Homory.Model.Menu) %></div>
							</div>
						</h2>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
		<br />
		<br />
		<br />
		<div class="ui center aligned page grid">
			<div class="fifteen wide column">
				<img alt="" style="width: 100%; margin: auto; height: auto;" src="../Common/默认/图标.png" class="ui image" />
			</div>
		</div>
        <style>
            .rb_panel{
                right: 0;
                bottom: 0;
                position: absolute;
            }
        </style>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="rb_panel"></telerik:RadAjaxPanel>
	</form>
</body>
</html>
