<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignOn.aspx.cs" Inherits="Go.GoSignOn" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
	<title>乐翼教育云平台</title>
	<link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
	<link href="../Content/Homory/css/common.css" rel="stylesheet" />
	<link href="../Content/Sso/css/sign.css" rel="stylesheet" />
	<script src="../Content/jQuery/jquery.min.js"></script>
	<script src="../Content/Semantic/javascript/semantic.min.js"></script>
	<script src="../Content/Homory/js/common.js"></script>
	<script src="../Content/Homory/js/notify.min.js"></script>
	<script src="../Content/Sso/js/signOn.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
	<form id="form" runat="server">
		<telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
		<div class="ui two column page grid">
			<div class="ten wide column">
				<img class="ui image" src="../Common/配置/SsoLogo.png" />
			</div>
			<div class="six wide column">
				<img class="ui image" src="../Common/配置/SsoTitle.png" style="float:right;" />
			</div>
		</div>
		<div class="ui two column stackable page grid">
			<div class="five wide column">
				<div class="ui form">
					<div class="ui white segment">
						<telerik:RadAjaxPanel ID="areaAction" runat="server">
							<div class="ui black inverted center aligned segment">
								<label>用户登录</label>
							</div>
							<label class="ui blue ribbon label width80">账号</label>
							<div class="field padTop15">
								<div class="ui left labeled icon input">
									<input id="userName" runat="server" type="text" value="" maxlength="256" />
									<i class="user icon"></i>
								</div>
							</div>
							<label class="ui blue ribbon label width80">密码</label>
							<div class="field padTop15">
								<div class="ui left labeled icon input">
									<input id="userPassword" runat="server" type="password" maxlength="64" value="" />
									<i class="lock icon"></i>
								</div>
							</div>
							<div class="ui right aligned column padTop15">
								<div id="buttonAuto" class="ui toggle checkbox">
									<input name="autoPassword" type="checkbox">
									<label id="autoPasswordLabel">不记住密码</label>
								</div>
							</div>
							<div class="ui divider"></div>
							<div class="ui center aligned grid">
								<div class="column">
									<asp:Button ID="buttonSign" runat="server" OnClientClick="doRemember();" OnClick="buttonSign_OnClick" CssClass="circular ui blue button" Style="margin-left: 10px; margin-right: 10px;" Text="登录"></asp:Button>
									<asp:Button ID="buttonRegister" runat="server" OnClick="buttonRegister_OnClick" CssClass="circular ui blue button" Style="margin-left: 10px; margin-right: 10px;" Text="注册"></asp:Button>
								</div>
							</div>
						</telerik:RadAjaxPanel>
					</div>
				</div>
			</div>
			<div class="eleven wide column">
				<div class="ui white segment">
					<telerik:RadImageGallery ID="gallery" runat="server" ShowLoadingPanel="False" AllowPaging="false" Height="415px" ImagesFolderPath="~/Common/配置/SsoSplash" DisplayAreaMode="Image" LoopItems="true">
						<ThumbnailsAreaSettings Mode="ImageSlider" ShowScrollbar="false" ShowScrollButtons="false" />
						<ImageAreaSettings NavigationMode="Zone" NextImageButtonText="下一图" PrevImageButtonText="上一图" ShowDescriptionBox="false" ShowNextPrevImageButtons="false" />
						<ClientSettings>
							<ClientEvents OnImageGalleryCreated="galleryLoaded" />
							<AnimationSettings SlideshowSlideDuration="6000">
								<NextImagesAnimation Type="Random" Easing="Random" Speed="3000" />
								<PrevImagesAnimation Type="Random" Easing="Random" Speed="3000" />
							</AnimationSettings>
						</ClientSettings>
						<ToolbarSettings ShowFullScreenButton="true" ShowSlideshowButton="true" EnterFullScreenButtonText="全屏" ExitFullScreenButtonText="退出全屏" PlayButtonText="播放" PauseButtonText="暂停" ShowItemsCounter="false" Position="None" />
					</telerik:RadImageGallery>
				</div>
			</div>
		</div>
		<div class="ui center aligned page grid">
			<telerik:RadAjaxPanel ID="areaFavourite" runat="server">
				<div class="twelve wide column">
					<div id="signThumb" class="ui horizontal icon divider" style="width: 80%;">
						<i id="signThumbButton" class="circular blue inverted thumbs up icon signOnPointer"></i>
						<div id="signThumbCount" class="floating ui blue label width100">
							<asp:Label ID="FavouriteCount" runat="server"></asp:Label><asp:Button ID="signThumbPost" runat="server" CssClass="signHidden" OnClick="signThumbPost_OnClick" />
						</div>
					</div>
				</div>
			</telerik:RadAjaxPanel>
		</div>
		<div class="ui four middle aligned column stackable page grid">
			<div class="four wide column">
				<div class="ui blue segment">
					<div class="ui inverted segment">
						<div class="ui blue header">
							<i class="green cloud icon"></i>
							<div class="content signBreak" style="font-size:16px;">
								<asp:Label ID="ApplicationCount" runat="server"></asp:Label>个应用
   
              <div class="sub header">平台一体化</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="four wide column">
				<div class="ui green segment">
					<div class="ui inverted segment">
						<div class="ui green header">
							<i class="blue video icon"></i>
							<div class="content signBreak" style="font-size:16px;">
								<asp:Label ID="ResourceCount" runat="server"></asp:Label>资源

								<div class="sub header">资源多样化</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="four wide column">
				<div class="ui blue segment">
					<div class="ui inverted segment">
						<div class="ui blue header">
							<i class="green users icon"></i>
							<div class="content signBreak" style="font-size:16px;">
								<asp:Label ID="UserCount" runat="server"></asp:Label>位用户
   
              <div class="sub header">用户专业化</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="four wide column">
				<div class="ui green segment">
					<div class="ui inverted segment">
						<div class="ui green header">
							<i class="blue dashboard icon"></i>
							<div class="content signBreak" style="font-size:16px;">
								<asp:Label ID="VisitCount" runat="server"></asp:Label>次登录
   
              <div class="sub header">登录统一化</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="ui center aligned page grid">
			<div class="center aligned column">
					<img alt="" class="ui image" style="width: 100%; height: auto; margin: auto;" src="../Common/配置/SsoCopyright.png" />
			</div>
		</div>
	</form>
</body>
</html>
