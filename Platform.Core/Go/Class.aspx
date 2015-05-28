<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Class.aspx.cs" Inherits="Go.GoClass" %>

<%@ Register Src="~/Control/SideBar.ascx" TagPrefix="homory" TagName="SideBar" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>基础平台</title>
    <link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Core/css/common.css" rel="stylesheet" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <script src="../Content/Semantic/javascript/semantic.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="formHome" runat="server">
        <div>
            <homory:SideBar runat="server" ID="SideBar" />
        </div>
        <telerik:RadAjaxLoadingPanel ID="loading" runat="server">
            <i class="ui huge teal loading icon" style="margin-top: 50px;"></i>
            <div>&nbsp;</div>
            <div style="color: #564F8A; font-size: 16px;">正在加载 请稍候....</div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="panel" runat="server" CssClass="ui left aligned stackable page grid" Style="margin: 0; padding: 0;" LoadingPanelID="loading">
            <div class="sixteen wide column">
                <telerik:RadComboBox ID="combo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combo_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" Label="选择学校：" Width="220px" Filter="Contains" MarkFirstMatch="true" AllowCustomText="true" Height="202px">
                    <ItemTemplate>
                        <%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, 0) %>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </div>
            <div class="sixteen wide column">
                <table class="coreAuto">
                    <tr class="coreTop">
                        <td>
                            <telerik:RadTreeView ID="tree" runat="server" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="false" DataTextField="Name" DataValueField="Id" DataFieldID="Id" DataFieldParentID="ParentId" OnNodeClick="tree_NodeClick" OnNodeDrop="tree_NodeDrop">
                                <NodeTemplate>
                                    <i class='<%# FormatTreeNode(Container.DataItem, Container.Level) %>'></i>&nbsp;<%# GenerateTreeName((Homory.Model.Department)Container.DataItem, Container.Index, Container.Level) %>
                                </NodeTemplate>
                            </telerik:RadTreeView>
                        </td>
                        <td class="coreFull">
                            <telerik:RadGrid ID="grid" runat="server" CssClass="coreCenter coreFull" AllowPaging="false" AutoGenerateColumns="False" LocalizationPath="../Language" AllowSorting="True" PageSize="10" GridLines="None" OnNeedDataSource="grid_NeedDataSource">
                                <MasterTableView DataKeyNames="Id" CssClass="coreFull" CommandItemDisplay="None" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="学届" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Ordinal") + "届" %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="名称" DataField="Name" SortExpression="Name" UniqueName="Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# GenerateGridName((Homory.Model.Department)Container.DataItem) %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadGrid ID="gridX" runat="server" CssClass="coreCenter" AllowPaging="true" AutoGenerateColumns="false" LocalizationPath="../Language" AllowSorting="True" PageSize="20" GridLines="None" OnNeedDataSource="gridX_NeedDataSource" OnBatchEditCommand="gridX_BatchEditCommand" OnItemCreated="grid_ItemCreated">
                                <MasterTableView EditMode="Batch" DataKeyNames="Id" CommandItemDisplay="Top" HorizontalAlign="NotSet" ShowHeader="true" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="">
                                    <BatchEditingSettings EditType="Row" OpenEditingEvent="DblClick" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="班号 *" DataField="Ordinal" SortExpression="Ordinal" UniqueName="Ordinal">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Ordinal") %>'></asp:Label>
                                            </ItemTemplate>
                                            <InsertItemTemplate>
                                                <telerik:RadNumericTextBox ID="Ordinal" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MinValue="1" MaxValue="99" AllowOutOfRangeAutoCorrect="true" Value='<%# Bind("Ordinal") %>'>
                                                    <NumberFormat DecimalDigits="0" AllowRounding="true" />
                                                </telerik:RadNumericTextBox>
                                            </InsertItemTemplate>
                                            <EditItemTemplate></EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="别名" DataField="DisplayName" SortExpression="DisplayName" UniqueName="DisplayName">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("DisplayName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="Name" runat="server" EnabledStyle-HorizontalAlign="Center" Width="64" MaxLength="16" Text='<%# Bind("Name") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="状态" DataField="State" SortExpression="State" UniqueName="State">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadComboBox ID="State" runat="server" Width="64" EnableTextSelection="true" Text='<%# Bind("State") %>'>
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="" Value="-1" />
                                                        <telerik:RadComboBoxItem Text="启用" Value="1" />
                                                        <telerik:RadComboBoxItem Text="停用" Value="4" />
                                                        <telerik:RadComboBoxItem Text="删除" Value="5" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true" PagerTextFormat="{4} 第{0}页，共{1}页；第{2}-{3}项，共{5}项" />
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:Panel ID="gridXX" runat="server" CssClass="ui left middle aligned stackable grid" Style="border: solid 1px #828282; padding: 20px; margin: 20px;">
                                <div class="sixteen wide column">
                                    <h6 class="ui teal header"><i class="ui teal circle icon"></i>班主任：</h6>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
										<telerik:RadButton ID="charging" runat="server" CssClass="ui purple mini button" ButtonType="ToggleButton" AutoPostBack="True" OnClick="charging_OnClick">
                                        </telerik:RadButton>
                                </div>
                                <div class="coreCenter">
                                    <telerik:RadSearchBox ID="peek" runat="server" OnSearch="peek_Search" EmptyMessage="查找...." EnableAutoComplete="false">
                                    </telerik:RadSearchBox>
                                </div>
                                <telerik:RadListView ID="view" runat="server" DataKeyNames="Id" ClientDataKeyNames="Id" OnNeedDataSource="view_OnNeedDataSource">
                                    <ItemTemplate>
                                        <div class="rootPointer two wide column">
                                            <i class='<%# Eval("Id").ToString() == charging.CommandArgument ? "ui icon" : "ui circle purple icon" %>'></i>
                                            <telerik:RadButton ID="charger" runat="server" CssClass='<%# Eval("Id").ToString() == charging.CommandArgument ? "ui purple mini button" : "ui black mini button" %>' ButtonType="ToggleButton" CommandArgument='<%# Eval("Id") %>' AutoPostBack="True" Text='<%# Eval("RealName").ToString().Length == 2 ? Eval("RealName").ToString()[0] + "　" + Eval("RealName").ToString()[1] : Eval("RealName").ToString() %>' OnClick="charger_OnClick">
                                            </telerik:RadButton>
                                        </div>
                                    </ItemTemplate>
                                    <ClientSettings AllowItemsDragDrop="true"></ClientSettings>
                                </telerik:RadListView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
