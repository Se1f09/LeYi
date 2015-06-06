<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeTop.ascx.cs" Inherits="Control.ControlHomeTop" %>

<script>
    function goSearchAction() {
        var homory_peek = "../Go/Search?Content=" + encodeURIComponent($("#home_top_search_content").val());
        window.open(homory_peek);
    }
</script>

<div class="lg-top">
	<h1 class="logo"><a class="fixpng" href="#" title="互动教育资源管理平台">乐翼云资源平台</a></h1>
	<div class="lg-search">
		<input type="text" class="srx-ns-input" id="home_top_search_content" value="" data-prevcolor="" style="color: rgb(170, 170, 170);" />
		<a class="srx-ns-btn" id="A2" target="_self" style="cursor:pointer;" onclick="goSearchAction();">检索</a>
	</div>
	<div class="lg-top-right">

		<span>
			<a runat="server" id="home_top_sign_on_go" target="_self" onserverclick="home_top_sign_on_go_OnServerClick">登录</a>
            <a runat="server" id="home_top_user_label" href="../Go/Center"></a>
			<a runat="server" id="home_top_sign_off_go" target="_self" onserverclick="home_top_sign_off_go_OnServerClick">退出</a>
		</span>

	</div>
</div>
<div class="nav">
	<ul class="lg_nav">
		<li class="cur"><a href="../Go/Home">首页</a>

		</li>
		<li>|</li>
		
		<li><a href="../Go/Studio">名师工作室</a></li>
		<li>|</li>
		<li><a href="../Go/Group">教研团队</a></li>
		<li>|</li>
		<li><a href="../Go/Rooms">阳光课堂</a></li>
        <li>|</li>
		<li><a href="../Go/Catalog">资源目录</a></li>
        <li>|</li>
        <li><a href="../Go/Teachers">教师研训</a></li>
        <li>|</li>
        <li><a href="../Go/Center">个人中心</a></li>
		

	</ul>
	<div class="curBg"></div>
	<div class="cls"></div>
</div>
