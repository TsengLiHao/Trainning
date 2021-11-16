<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="Trainning.Form" %>

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
       .lbl{
          display:inline-block;
          width:40px;
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
        <div align="center">
        <asp:Label ID="lblTitle" runat="server" Font-Size="20"></asp:Label><br />
        <asp:TextBox ID="txtContent" runat="server" ReadOnly="true"  TextMode="MultiLine" style="text-align: center" BorderStyle="None" Font-Size="16"></asp:TextBox>
        </div><br />
        <asp:Label ID="lblName" runat="server" Text="姓名:" CssClass="lbl"></asp:Label>
        <asp:TextBox ID="txtName" runat="server"  ></asp:TextBox><br />
        <asp:Label ID="lblPhone" runat="server" Text="手機:" CssClass="lbl"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" ></asp:TextBox><br />
        <asp:Label ID="lalEmail" runat="server" Text="Email:" CssClass="lbl"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" ></asp:TextBox><br />
        <asp:Label ID="lblAge" runat="server" Text="年齡:" CssClass="lbl"></asp:Label>
        <asp:TextBox ID="txtAge" runat="server" TextMode="Number"></asp:TextBox><br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        </asp:PlaceHolder>
        <div align="center">
            <asp:Literal ID="ltAmount" runat="server"></asp:Literal><br /><br />
        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click"/>
            &emsp;
        <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click"/>
        </div><br />
        <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="Literal2" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="Literal3" runat="server" Visible="false"></asp:Literal>
        <span style="color: red">
            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
            <asp:Literal ID="ltMsg1" runat="server"></asp:Literal>
            <asp:Literal ID="ltMsg2" runat="server"></asp:Literal>
        </span>
    </form>
</body>
</html>
