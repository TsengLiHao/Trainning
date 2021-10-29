<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Trainning.SystemAdmin.List" %>

<%@ Register Src="~/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1">
            <tr>
                <td colspan="4">
                    <h1>後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="plcEdit" runat="server">
                      <a href="/SystemAdmin/Detail.aspx">問卷管理</a><br />
                      <a href="/SystemAdmin/Question.aspx">常用問題管理</a><br />
                    </asp:PlaceHolder>
                </td>
                <td>
                    問卷標題
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                    &ensp;
                    <asp:Button ID="btnTitleSearch" runat="server" Text="搜尋" OnClick="btnTitleSearch_Click"/><br />
                    開始/結束
                    <asp:TextBox ID="txtStart" runat="server" TextMode="Date"></asp:TextBox>
                    &ensp;
                    <asp:TextBox ID="txtEnd" runat="server" TextMode="Date"></asp:TextBox>
                    &ensp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click"/><br />
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ListID" HeaderText="#" />
                            <asp:TemplateField HeaderText="問卷">
                                <ItemTemplate>
                                    <a href="/SystemAdmin/Detail.aspx?ID=<%# Eval("ID") %>"><%# Eval("ListName") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Status" HeaderText="狀態" />
                            <asp:BoundField DataField="StartTime" HeaderText="開始時間" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="EndTime" HeaderText="結束時間" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:TemplateField HeaderText="觀看統計">
                                <ItemTemplate>
                                    <a href="/Stastic.aspx?ID=<%# Eval("ID") %>">前往</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <uc1:ucPager runat="server" ID="ucPager" PageSize="10" Url="/SystemAdmin/List.aspx" />
                </td>
            </tr>
        </table>
        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
    </form>
</body>
</html>
