<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBar.ascx.cs" Inherits="Control.ControlSideBar" %>

<link href="../Content/Core/css/sidebar.css" rel="stylesheet" />
<script src="../Content/Core/js/sidebar.js"></script>

<telerik:RadScriptManager ID="sm" runat="server"></telerik:RadScriptManager>

<div class="ui vertical labeled icon sidebar menu" id="sidebar">
	<div class="item">
		<a href="../Go/Home.aspx"><i id="sideHome" class="circular large home icon"></i>
			<br />
			<br />
			<b>基础平台</b></a>
	</div>
	<asp:Repeater ID="repeater" runat="server">
		<ItemTemplate>
			<div class="item">
				<div class='<%# string.Format(" ui small circular button {0}", Eval("Icon")) %>' data-id='<%# Eval("Id") %>'><%# Eval("Name") %></div>
				<div class='<%# string.Format("sub menu {0}", Eval("Icon")) %>' data-id='<%# Eval("Id") %>'><%# SubMenu(Container.DataItem as Homory.Model.Menu) %></div>
			</div>
		</ItemTemplate>
	</asp:Repeater>
</div>
<div class="container">
	<a id="sidebarSwitcher" class="launch item rootPointer" title="菜单">
		<i class="icon list layout large"></i><a id="u" runat="server" onclick="nameClick(); return false;" class="welcome rootPointer"></a>&nbsp;&nbsp;&nbsp;&nbsp;<a id="qb" runat="server" class="welcome rootPointer" onserverclick="qb_Click">退出</a>
	</a>
</div>
<div class="ui center aligned page grid" style="margin:0;padding:0;">
	<div class="column">
		<div id="bar" class="ui inverted black center aligned segment">
			<h2 id="headInfo" class="ui big header"></h2>
		</div>
	</div>
</div>
