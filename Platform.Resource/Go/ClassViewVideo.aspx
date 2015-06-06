<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassViewVideo.aspx.cs" Inherits="Go.GoViewVideo" %>

<%@ Import Namespace="Homory.Model" %>
<%@ Register Src="~/Control/XsfxPlayer.ascx" TagPrefix="homory" TagName="XsfxPlayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= CurrentResource.Title %>-互动资源平台</title>
    <link href="../Style/common.css" rel="stylesheet" />
    <link href="../Style/common_002.css" rel="stylesheet" />
    <link href="../Style/detail.css" rel="stylesheet" />
    <link href="../Style/commentInputBox.css" rel="stylesheet" />
    <link href="../Style/1.css" rel="stylesheet" />
      <base target="_top" />
  <script src="../Script/jquery.min.js"></script>
    <script>
        function GetUrlParms() {
            var args = new Object();
            var query = location.search.substring(1);//获取查询串   
            var pairs = query.split("&");//在逗号处断开   
            for (var i = 0; i < pairs.length; i++) {
                var pos = pairs[i].indexOf('=');//查找name=value   
                if (pos == -1) continue;//如果没有找到就跳过   
                var argname = pairs[i].substring(0, pos);//提取name   
                var value = pairs[i].substring(pos + 1);//提取value   
                args[argname] = unescape(value);//存为属性   
            }
            return args;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxPanel runat="server">
            <div class="srx-bg22">
                <div class="srx-wrap">
                    <div class="srx-main" id="mainBox">
                        <div class="srx-left" style="background-color: #FFF">
                            <telerik:RadCodeBlock runat="server">
                                <div class="title-bar clearfix">
                                    <h1 class="p-title fl"><%= CurrentResource.Title %></h1>
                                    <div class="fr" data-action-data="" data-pid="618101" data-aid="23778" data-inout="in" data-position-type="classe" data-otype="photo" data-oid="618101">
                                    </div>
                                </div>
                                <div class="photo-info">
                                    <span>作者：<a href='<%= string.Format("../Go/Personal?Id={0}", TargetUser.Id) %>'><%= CurrentResource.User.DisplayName %></a></span>&nbsp;&nbsp;
                                <span id="catalog" runat="server">栏目：<%= CurrentResource.ResourceCatalog.Where(o=>o.State==State.启用 &&o.Catalog.Type== CatalogType.视频).Aggregate(string.Empty,Combine).CutString(null) %></span><br />
                                    <span>年级：<%= CurrentResource.ResourceCatalog.Where(o=>o.State==State.启用 &&(o.Catalog.Type== CatalogType.年级_幼儿园||o.Catalog.Type== CatalogType.年级_六年制||o.Catalog.Type== CatalogType.年级_九年制)).Aggregate(string.Empty,Combine).CutString(null) %></span>&nbsp;&nbsp;
                                <span>学科：<%= CurrentResource.ResourceCatalog.Where(o=>o.State==State.启用 &&o.Catalog.Type== CatalogType.课程).Aggregate(string.Empty,Combine).CutString(null) %></span><br />
                                    <span>时间：<%= CurrentResource.Time.ToString("yyyy-MM-dd HH:mm") %></span>
                                </div>


                                <div class="j-content clearfix">
                                    <%= CurrentResource.Content %>
                                    <br />
                                    <div style="background-color: black;">
                                        <homory:XsfxPlayer runat="server" ID="player" />

                                    </div>
                                </div>



                            </telerik:RadCodeBlock>

                        </div>
                        <script src="../Script/index.js"></script>
                        <style>
                            .poviewvideo {
                                cursor: pointer;
                            }
                        </style>
                    </div>
                </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
