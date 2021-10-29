<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Trainning.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>動態問券</h1>
            <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click"/>
            <asp:Button ID="btnWrite" runat="server" Text="填寫" OnClick="btnWrite_Click"/>
        </div>
    </form>
</body>
</html>
