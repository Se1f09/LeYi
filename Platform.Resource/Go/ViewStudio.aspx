﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStudio.aspx.cs" Inherits="Go.GoViewStudio" %>

<%@ Import Namespace="Homory.Model" %>
<%@ Register Src="~/Control/CommonTop.ascx" TagPrefix="homory" TagName="CommonTop" %>
<%@ Register Src="~/Control/CommonBottom.ascx" TagPrefix="homory" TagName="CommonBottom" %>


<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <title>互动资源平台 名师工作室</title>
    <link rel="stylesheet" href="css/common.css">
    <link rel="stylesheet" href="css/common(1).css">
    <link rel="stylesheet" href="css/detail.css">
    <link rel="stylesheet" href="css/plaza2.css">
    <link rel="stylesheet" href="css/1.css" id="skinCss">
    <base target="_top" />
</head>
<body class="srx-pclass">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="Rsm" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <homory:CommonTop runat="server" ID="CommonTop" />
        <div class="srx-bg">
            <div class="srx-wrap">
                <div class="c-plaza" id="cPlaza">
                    <div class="c-p-content clearfix">
                        <div class="c-pc-left fl">
                            <asp:Repeater runat="server" ID="catalogs" OnItemDataBound="catalogs_OnItemDataBound">
                                <ItemTemplate>
                                    <div class="center_right">
                                        <div class="c-p-title" style="margin-top: 30px;">
                                            <div class="box-hd"><%# Eval("Name") %></div>
                                            <a id="aMore" runat="server">+更多</a>
                                        </div>
                                        <ul>
                                            <asp:Repeater runat="server" ID="resources">
                                                <ItemTemplate>
                                                    <li style="height: 250px; width: 200px; float: left; margin-bottom: 30px; margin-left: 30px; border: solid 1px #ddd; overflow: hidden;">
                                                        <div>
                                                            &nbsp;&nbsp;<img src='<%# string.Format("../Image/img/{0}.jpg", Eval("Thumbnail")) %>' style="width: 20px; height: 20px; margin-top: 8px;" />&nbsp;&nbsp;
                                                            <strong><a href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>'><%# Eval("Title") %></a></strong>
                                                        </div>
                                                        <div style="float: right;">
                                                            [<a href='<%# string.Format("../Go/Personal?Id={0}", Eval("UserId")) %>'><%# U(Eval("UserId")).DisplayName %></a>
                                                            @<%# ((System.DateTime)Eval("Time")).FormatTimeShort() %>]&nbsp;&nbsp;
                                                        </div>
                                                        <div style="overflow: hidden; text-align: left; vertical-align: middle; clear: both; width: 100%; margin-left: -20px; margin-top: -20px;">
                                                            <a href='<%# string.Format("../Go/{0}?Id={1}", ((Homory.Model.ResourceType)Eval("Type"))== Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain", Eval("Id")) %>'>
                                                                <img src='<%# Eval("Image").ToString() %>' width="300" height="200" /></a>
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                        <br />
                                        <br />
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="c-p-right fl">
                            <div class="c-pr-box">
                                <div class="c-prb-role clearfix">
                                    <div class="c-prb-title ">
                                        <div class="box-hd">团队简介 </div>
                                    </div>
                                    <div class="l_schPrincipal">
                                        <p id="introduction" runat="server">
                                        </p>


                                    </div>
                                </div>
                                <div class="c-prb-role clearfix">
                                    <div class="c-prb-title ">
                                        <div class="box-hd">团队引领 </div>
                                    </div>
                                    <ul id="role_1" data-count="7">
                                        <asp:Repeater runat="server" ID="leader">
                                            <ItemTemplate>
                                                <li class="none  first-child" style="display: list-item; height: 50px;">
                                                    <div class="c-prb-face">
                                                        <a style="border: none;" href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                                            <asp:Image runat="server" ID="icon" ImageUrl='<%# P(Eval("Icon")) %>' Width="40" Height="40" /></a>
                                                    </div>
                                                    <div class="ml50">
                                                        <div class="c-prb-nickname"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a> </div>
                                                        <div style="display: none;"><a href="javascript:;" class="unflw" data-id="24760" data-type="person" data-action="follow"><strong>×</strong>删除</a> </div>
                                                    </div>
                                                </li>
                                                <a style="display: none;">删除</a>
                                                <a style="display: none;">设为管理员</a>
                                                <a style="display: none;">关注</a>
                                                <a style="display: none;">取消关注</a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                                <div class="c-prb-role clearfix">
                                    <div class="c-prb-title mb10">
                                        <div class="box-hd">团队成员 </div>
                                    </div>
                                    <ul id="role_0" data-count="7">
                                        <asp:Repeater runat="server" ID="members">
                                            <ItemTemplate>
                                                <li class="none  first-child" style="display: list-item; height: 50px;">
                                                    <div class="c-prb-face">
                                                        <a style="border: none;" href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'>
                                                            <asp:Image runat="server" ID="icon" ImageUrl='<%# P(Eval("Icon")) %>' Width="40" Height="40" /></a>
                                                    </div>
                                                    <div class="ml50">
                                                        <div class="c-prb-nickname"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("Id")) %>'><%# Eval("DisplayName") %></a> </div>
                                                        <div style="display: none;"><a href="javascript:;" class="unflw" data-id="24760" data-type="person" data-action="follow"><strong>×</strong>删除</a> </div>
                                                    </div>
                                                </li>
                                                <a style="display: none;">删除</a>
                                                <a style="display: none;">设为管理员</a>
                                                <a style="display: none;">关注</a>
                                                <a style="display: none;">取消关注</a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                                <div class="c-prb-role clearfix">
                                    <div class="c-prb-title mb10">
                                        <div class="box-hd">留言板 </div>
                                    </div>
                                    <div>
                                        <telerik:RadAjaxPanel runat="server" ID="cDo">
                                            <textarea id="cContent" runat="server" style="width: 180px; height: 56px; max-width: 170px;"></textarea>
                                            <div class="srx-ciptbox-toolbar">
                                                <span class="srx-ciptbox-acts"></span>
                                                <a id="dodo" runat="server" class="button24 srx-ciptbox-submit" data-action="submit" onserverclick="dodo_ServerClick"><em>发表</em></a>
                                                <span class="srx-ciptbox-counter" data-ui-role="counter"></span>
                                            </div>
                                        </telerik:RadAjaxPanel>
                                        <br />
                                        <div style="width: 170px; height: 440px; overflow: auto; border: solid 1px silver;">
                                            <telerik:RadAjaxPanel runat="server" ID="cPanel" OnAjaxRequest="cPanel_AjaxRequest">
                                                <asp:Timer runat="server" ID="timer" Interval="3000" Enabled="True" OnTick="timer_Tick"></asp:Timer>
                                                <table>
                                                    <asp:Repeater runat="server" ID="cComment">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 40px;"><%# ((DateTime)Eval("Time")).ToShortTimeString() %></td>
                                                                <td style="width: 50px;"><%# U(Eval("UserId")).DisplayName %>：</td>
                                                                <td><%# Eval("Content") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <homory:CommonBottom runat="server" ID="CommonBottom" />
            </div>
        </div>
        <script src="js/h.js" type="text/javascript"></script>
    </form>
</body>
</html>
