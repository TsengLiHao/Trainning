<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Trainning.SystemAdmin.List" %>

<%@ Register Src="~/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Script/jquery-3.6.0.min.js"></script>
    <style>
       .RightTop{
           position:absolute;
           top:0px;
           right:0px;
       }
   </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1">
            <tr>
                <td colspan="4">
                    <h1>後台</h1>
                    <div class="RightTop">
                    <asp:Button ID="btnLogout" runat="server" Text="登出" Visible="false" OnClick="btnLogout_Click"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="plcEdit" runat="server">
                      <a href="/SystemAdmin/List.aspx">問卷管理</a><br />
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
                     <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="image/Trash Can.png" Width="35" Height="35" OnClick="ImageButton1_Click"  OnClientClick="return confirm('確定刪除嗎?')"/>
                    &ensp;
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="image/Add.png" Width="35" Height="35" OnClick="ImageButton2_Click"/>
                    <br />
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxChoose" runat="server" />
                                        </ItemTemplate>
                            </asp:TemplateField>
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
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
                    <br />
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    <uc1:ucPager runat="server" ID="ucPager" PageSize="10" Url="/SystemAdmin/List.aspx" /><br />
                    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
