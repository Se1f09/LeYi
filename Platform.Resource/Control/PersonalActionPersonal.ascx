<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalActionPersonal.ascx.cs" Inherits="Control.ControlPersonalActionPersonal" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="Homory.Model" %>

<telerik:RadAjaxPanel runat="server" ID="PersonalActionPanel">


    <telerik:RadListView runat="server" ID="actions" AllowPaging="true" PageSize="5" OnNeedDataSource="resultX_NeedDataSource">
        <ItemTemplate>
            <div class="srx-comment-list-box" id="srxCommentListBox">
                <div class="srx-comment-list">
                    <dl class="srx-comment-item">
                        <dt>
                            <a style="border: none;" href='<%# string.Format("../Go/Personal?Id={0}", U(((Homory.Model.Action)Container.DataItem).Id3).Id) %>'>
                                <asp:Image runat="server" ID="icon" ImageUrl='<%# U(((Homory.Model.Action)Container.DataItem).Id3).Icon %>' Width="35" Height="35" /></a>
                        </dt>
                        <dd>
                            <div class="srx-comment-content" style="font-size: 14px;">
                                <a href='<%# string.Format("../Go/Personal?Id={0}", U(((Homory.Model.Action)Container.DataItem).Id3).Id) %>'><font color="#949494"><%# UC(U(((Homory.Model.Action)Container.DataItem).Id3).Id) %></font>&nbsp;<font color="#333333"><b><%# U(((Homory.Model.Action)Container.DataItem).Id3).DisplayName %></b></font></a>&nbsp;<%# ((Homory.Model.Action)Container.DataItem).Type == ActionType.用户评分资源 ? "评分" : (((Homory.Model.Action)Container.DataItem).Type == ActionType.用户评论资源 ?"" : "") %>&nbsp;
                             
                              <span style="float:right;"> <%# ((DateTime)Eval("Time")).FormatTime() %></span>
                            </div>
                            <table>
                                <tr>
                                    <td>

 <label style="color: #227DC5; font-size: 12px; font-weight: normal;color:#333333;"><%# ((Homory.Model.Action)Container.DataItem).Type == ActionType.用户评分资源 ? Eval("Content1") + "分" : Eval("Content1") %></label></br>
                                        <a style="border: none;" href='<%# string.Format("../Go/{1}?Id={0}",R(Eval("Id2")).Id,R(Eval("Id2")).Type == Homory.Model.ResourceType.视频?"ViewVideo":"ViewPlain") %>'>
                                            <img src='<%# R(Eval("Id2")).Image %>' width="100" height="90" style="border:1px #CDCDCD solid;"/></a></br><a href='<%# string.Format("../Go/{1}?Id={0}",R(Eval("Id2")).Id,R(Eval("Id2")).Type == Homory.Model.ResourceType.视频?"ViewVideo":"ViewPlain") %>'><font color="#333333"><%# R(Eval("Id2")).Title %></font></a>&nbsp;
                                    </td>
                                    <td style="vertical-align: top; padding-left: 20px;">
                                       
                                    </td>
                                </tr>
                            </table>
                            <div>
                                
                            </div>
                        </dd>
                    </dl>



                </div>




            </div>
        </ItemTemplate>
    </telerik:RadListView>
    <br />
    <telerik:RadDataPager runat="server" ID="pager" PageSize="20" PagedControlID="actions">
        <Fields>
            <telerik:RadDataPagerSliderField HorizontalPosition="NoFloat" LabelTextFormat="第{0}页 共{1}页" SliderDecreaseText="前翻" SliderIncreaseText="后翻" SliderDragText="拖动" SliderOrientation="Horizontal" />
        </Fields>
    </telerik:RadDataPager>


</telerik:RadAjaxPanel>
