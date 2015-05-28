<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenterStudio.aspx.cs" Inherits="Go.GoCenterStudio" %>

<%@ Import Namespace="System.Web.Configuration" %>

<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>
<%@ Register Src="~/Control/CommonPush.ascx" TagPrefix="homory" TagName="CommonPush" %>
<%@ Register Src="~/Control/CenterLeft.ascx" TagPrefix="homory" TagName="CenterLeft" %>
<%@ Register Src="~/Control/CenterRight.ascx" TagPrefix="homory" TagName="CenterRight" %>






<!DOCTYPE html>

<html>
<head runat="server">
	<title>资源平台 - 个人中心</title>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<meta http-equiv="Pragma" content="no-cache">
	<script src="../Script/jquery.min.js"></script>
	<link rel="stylesheet" href="../Style/common.css">
	<link rel="stylesheet" href="../Style/common(1).css">
	<link rel="stylesheet" href="../Style/index1.css">
	<link rel="stylesheet" href="../Style/2.css" id="skinCss">

</head>
<body class="srx-phome">
	<form runat="server">
		<telerik:RadScriptManager ID="Rsm" runat="server">
			<Scripts>
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
				<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
			</Scripts>
		</telerik:RadScriptManager>
		<telerik:RadWindowManager runat="server" ID="Rwm" Skin="Metro">
			<Windows>
                <telerik:RadWindow ID="popup_push" runat="server" OnClientClose="pushPopped" Title="呈送" ReloadOnShow="True" Width="600" Height="400" Top="60" Left="200" ShowContentDuringLoad="True" VisibleStatusbar="false" Behaviors="Move,Close" Modal="True" CenterIfModal="False" Localization-Close="关闭">
				</telerik:RadWindow>
                <telerik:RadWindow ID="popup_publish" runat="server" AutoSize="False" Width="360" Height="200" ShowContentDuringLoad="True" ReloadOnShow="False" KeepInScreenBounds="true" VisibleStatusbar="false" Behaviors="Close" Modal="True" Localization-Close="关闭" EnableEmbeddedScripts="True" EnableEmbeddedBaseStylesheet="True" VisibleTitlebar="True">
                    <ContentTemplate>
                        <div style="width: 320px; text-align: center;">

                        </div>
                        &nbsp;&nbsp;<a style="cursor: pointer; margin: auto;" href="Publishing.aspx?Type=Media">发布视频</a><br />
                        &nbsp;&nbsp;<a style="cursor: pointer; margin: auto;" href="Publishing.aspx?Type=Article">发布文章</a><br />
                        &nbsp;&nbsp;<a style="cursor: pointer; margin: auto;" href="Publishing.aspx?Type=Courseware">发布课件</a><br />
                        &nbsp;&nbsp;<a style="cursor: pointer; margin: auto;" href="Publishing.aspx?Type=Paper">发布试卷</a><br />
                        &nbsp;&nbsp;<a style="cursor: pointer; margin: auto;" onclick="closePublish();">取消发布</a>
                    </ContentTemplate>
                </telerik:RadWindow>
			</Windows>
		</telerik:RadWindowManager>
		<script>
			var window_publish;

			function popupPublish() {
				window_publish = window.radopen(null, "popup_publish");
				return false;
			}
			function closePublish() {
				window_publish.close();
				return false;
			}

			function popupPush(url) {
				window.radopen(url, "popup_push");
				return false;
			}
		</script>
		<telerik:RadCodeBlock runat="server">
			<script>

				function pushPopped(sender, e) {
					var toRefresh = $find("<%= pushPanel.ClientID %>");
					toRefresh.ajaxRequest("PushRefresh");
				}
			</script>
		</telerik:RadCodeBlock>
		<homory:CommonTop runat="server" ID="CommonTop" />

		<div class="srx-bg">
			<div class="srx-wrap">

				<%--左上方个人信息区--%>
				<div class="srx-main srx-main-bg">

                    <homory:CenterLeft runat="server" ID="CenterLeft" />
					<div class="srx-right">
						<div class="srx-r1">
							<div class="msgFeed user_feed mt15">
								<telerik:RadAjaxPanel runat="server" ID="pushPanel" OnAjaxRequest="pushPanel_OnAjaxRequest">

								<homory:CommonPush runat="server" ID="CommonPush" />
									</telerik:RadAjaxPanel>
							</div>
						</div>

                        <homory:CenterRight runat="server" ID="CenterRight" />
					</div>
				</div>
				<homory:CommonBottom runat="server" ID="CommonBottom" />



			</div>
		</div>



	</form>
</body>
</html>













