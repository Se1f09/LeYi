<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Board.aspx.cs" Inherits="Go.GoBoard" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,Chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>面板</title>
    <link href="../Content/Semantic/css/semantic.min.css" rel="stylesheet" />
    <link href="../Content/Homory/css/common.css" rel="stylesheet" />
    <link href="../Content/Sso/css/sign.css" rel="stylesheet" />
    <link href="../Content/Sso/css/board.css" rel="stylesheet" />
    <script src="../Content/jQuery/jquery.min.js"></script>
    <script src="../Content/Semantic/javascript/semantic.min.js"></script>
    <script src="../Content/Homory/js/common.js"></script>
    <script src="../Content/Homory/js/notify.min.js"></script>
    <script src="../Content/Sso/js/board.js"></script>
    <!--[if lt IE 9]>
	    <script src="../Content/Homory/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <div class="ui stackable page grid">
            <div class="sixteen wide column">
                <div id="bar" class="ui center aligned segment" style="background-color: transparent; border: none; box-shadow: none;">
                    <asp:Image ID="icon" runat="server" CssClass="ui image" Width="60" Height="60" />
                    <label id="headInfo" runat="server" class="ui tiny teal button" style="margin: 10px; padding: 11px;"></label>
                    <a href="../Go/SignOff" id="quit" class="ui animated tiny purple button floated right">
                        <div class="visible content">退出</div>
                        <div class="hidden content">
                            <i class="right sign out icon"></i>&nbsp;
                        </div>
                    </a>
                    <div style="width: 100%; height: 1px; border-bottom: dashed 1px silver;"></div>
                </div>
            </div>
        </div>
        <div class="ui stackable center aligned page grid">
            <asp:Repeater ID="items" runat="server">
                <ItemTemplate>
                    <div class="five wide column">
                        <div class="item signLink" data-url='<%# (string.Format("{0}?OnlineId={1}", Eval("Home"), Session[Homory.Model.HomoryConstant.SessionOnlineId])) %>'>
                            <p class="ui image square200" style="background: none; text-align: center;">
                                <img style="margin: auto;" onerror="this.src = '../Common/默认/应用.png';" src='<%# Eval("Icon") %>' alt="" />
                            </p>
                            <p class="ui red large circular button">
                                <%# Eval("Name") %>
                            </p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="ui right aligned page grid">
            <div class="right aligned column">
                <img class="ui image" style="float: right;" src="../Common/配置/SsoLogo.png" width="350" height="70" />
            </div>
        </div>
        <style>
            html .image.ui {
                background: none;
            }
        </style>
    </form>
</body>
</html>
