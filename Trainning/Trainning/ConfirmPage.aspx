<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmPage.aspx.cs" Inherits="Trainning.ConfirmPage" %>

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
        <h1>前台</h1>
        <div class="RightTop">
            <asp:Literal ID="ltStatus" runat="server"></asp:Literal><br />
            <asp:Literal ID="ltStart" runat="server"></asp:Literal>
            <asp:Label ID="Label2" runat="server" Text="～"></asp:Label>
            <asp:Literal ID="ltEnd" runat="server"></asp:Literal>
        </div>
        <asp:Label ID="lblTitle" runat="server" ></asp:Label><br />
        <asp:TextBox ID="txtContent" runat="server" ReadOnly="true"  TextMode="MultiLine" BorderStyle="None"></asp:TextBox><br />
        姓名
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
        手機
        <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone"></asp:TextBox><br />
        Email
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox><br />
        年齡
        <asp:TextBox ID="txtAge" runat="server" TextMode="Number"></asp:TextBox><br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        </asp:PlaceHolder>
        <asp:Button ID="btnCancel" runat="server" Text="取消"/>
        <asp:Button ID="btnSubmit" runat="server" Text="送出"/>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
