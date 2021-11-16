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
            <asp:Label ID="lbMark" runat="server" Text="～"></asp:Label>
            <asp:Literal ID="ltEnd" runat="server"></asp:Literal>
        </div>
        <div align="center">
        <asp:Label ID="lblTitle" runat="server" Font-Size="20"></asp:Label><br />
        <asp:TextBox ID="txtContent" runat="server" ReadOnly="true"  TextMode="MultiLine" style="text-align: center" BorderStyle="None" Font-Size="16"></asp:TextBox>
        </div><br />
        姓名:
        <asp:Literal ID="ltName" runat="server"></asp:Literal><br />
        手機:
        <asp:Literal ID="ltPhone" runat="server"></asp:Literal><br />
        Email:
        <asp:Literal ID="ltEmail" runat="server"></asp:Literal><br />
        年齡:
        <asp:Literal ID="ltAge" runat="server"></asp:Literal><br /><br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        </asp:PlaceHolder>
        <div align="center">
        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" OnClientClick="history.back(-1);event.returnValue=false;"/>
            &emsp;
        <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click"/>
        </div>    
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
