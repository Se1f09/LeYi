<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalActionCenter.ascx.cs" Inherits="Control.ControlPersonalActionCenter" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="PersonalActionPanel">

    <asp:Repeater runat="server" ID="actions">
        <ItemTemplate>

            <asp:Panel runat="server" Visible='<%# ((Homory.Model.ActionType)Eval("Type")) == ActionType.用户评论资源 %>'>
                <dl class="fd-item"  id="item_2404030"  data-action-data=""  data-id="2404030"  data-type="fenxiang"  data-uid="10005"  data-inout="in"  data-otype="diary"  data-oid="2393530"  data-position-type="classe"  data-cat-id="12004"
                    <dt class="fd-left"><a href='<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>'>
                        <asp:Image Width="49" Height="49" ImageUrl='<%# U(Eval("Id3")).Icon%>' CssClass="face face_40" runat="server"></asp:Image></a></dt>
                    <dd class="fd-right" style="margin-top:-50px;">
                        <div class="fd-top"><a class="fd-actor" href="<%# string.Format("../Go/Personal?Id={0}", Eval("id3")) %>"><%# U(Eval("Id3")).DisplayName %></a>  <span class="fd-verb"></span> <span class="fd-verb1"><%# ((DateTime)Eval("Time")).FormatTime() %></span></div>
                        <div class="fd-incon1 clearfix">
                            <p  class="fd-title"> <a  href='<%# string.Format("../Go/ViewPlain?Id={0}",R(Eval("Id2")).Id) %>' ><%# R(Eval("Id2")).Title %> </a> </p>

                            <p><%# R(Eval("Id2")).Content %> </p>
                            <p>
                                <img src='<%# R(Eval("Id2")).Image %>' width="100" height="80" /></p>
                            <p class="from">年级：<%# R(Eval("Id2")).ResourceCatalog.Where(o=>o.State==State.启用 && (o.Catalog.Type== CatalogType.年级_幼儿园 || o.Catalog.Type == CatalogType.年级_六年制 || o.Catalog.Type == CatalogType.年级_九年制)).Aggregate(string.Empty,Combine).CutString(null) %></p>
                             <p>回复：<%# Eval("Content1") %></p>
                        </div>
                        <div class="plqgr">
                            <div class="fd1-item1">

                                <div class="fl hv-light mt10">

                                    <span>
                                        <i class="icon14 icon14-like"></i>
                                        <a href="javascript:;" data-action-data="" data-oid="2326235" data-position-type="classe" data-otype="diary" data-inout="in" data-action="like">浏览（<%# R(Eval("Id2")).View %>）</a>
                                    </span>
                                    <i>|</i>
                                    <span>
                                        <i class="icon14 icon14-comment"></i>
                                        <a href="../内容页/neirong.htm">收藏（<%# R(Eval("Id2")).Favourite %>）</a>
                                    </span>
                                    <i>|</i>
                                    <span>
                                        <i class="icon14 icon14-view"></i>
                                        <a href="javascript:;" class="hv-none">评论（<%# R(Eval("Id2")).Comment %>）</a>
                                    </span>
                                    <i>|</i>
                                    <span>
                                        <i class="icon14 icon14-view"></i>
                                        <a href="javascript:;" class="hv-none">下载（<%# R(Eval("Id2")).Download %>）</a>
                                    </span>
                                </div>
                            </div>
                            <div class="srx-comment-iptbox" id="srxCommentInputBox">
                                <textarea name="textarea" data-input-limit-uid="0"></textarea>
                            </div>
                        </div>
                    </dd>
                </dl>
            </asp:Panel>

        </ItemTemplate>
    </asp:Repeater>

</telerik:RadAjaxPanel>
