<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EmpReport._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 89px;
        }
        .auto-style3 {
            width: 108px;
        }
        .auto-style4 {
            width: 159px;
        }
        .auto-style5 {
            width: 3px;
        }
        .auto-style6 {
            width: 19px;
        }
        .auto-style8 {
            width: 89px;
            height: 41px;
        }
        .auto-style9 {
            width: 108px;
            height: 41px;
        }
        .auto-style10 {
            width: 3px;
            height: 41px;
        }
        .auto-style11 {
            width: 159px;
            height: 41px;
        }
        .auto-style12 {
            width: 19px;
            height: 41px;
        }
        .auto-style13 {
            height: 41px;
        }
        .auto-style14 {
            width: 70px;
        }
        .auto-style15 {
            width: 70px;
            height: 41px;
        }
        .auto-style16 {
            width: 70px;
            height: 42px;
        }
        .auto-style17 {
            width: 89px;
            height: 42px;
        }
        .auto-style18 {
            width: 108px;
            height: 42px;
        }
        .auto-style19 {
            width: 3px;
            height: 42px;
        }
        .auto-style20 {
            width: 159px;
            height: 42px;
        }
        .auto-style21 {
            width: 19px;
            height: 42px;
        }
        .auto-style22 {
            height: 42px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td class="auto-style16"></td>
                <td class="auto-style17"></td>
                <td class="auto-style18"></td>
                <td class="auto-style19"></td>
                <td class="auto-style20"></td>
                <td class="auto-style17"></td>
                <td class="auto-style21"></td>
                <td class="auto-style22"></td>
            </tr>
            <tr>
                <td class="auto-style15"></td>
                <td class="auto-style8">
                    <asp:Label ID="Label1" runat="server" Text="เลือกปีที่จ้าง"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" Height="27px" style="margin-top: 0px" Width="99px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style10"></td>
                <td class="auto-style11">
                    <asp:Label ID="Label2" runat="server" Text="เลือกสถานะการแต่งงาน"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:DropDownList ID="ddlStatus" runat="server" Height="27px" Width="99px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style12"></td>
                <td class="auto-style13">
                    <asp:Button ID="bt_ok" runat="server" Text="OK" Width="63px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style14">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
