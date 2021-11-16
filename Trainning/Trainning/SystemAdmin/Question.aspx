<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="Trainning.SystemAdmin.Question" %>

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
                <td colspan="8">
                    <h1>後台-常用問題管理</h1>
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
                    <div>
                        問卷名稱
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
                        問題
                            <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                        種類
                            <asp:DropDownList ID="ddlQuestionType" runat="server" AutoPostBack="true" OnTextChanged="ddlQuestionType_TextChanged">
                                <asp:ListItem Text="文字方塊"></asp:ListItem>
                                <asp:ListItem Text="單選方塊"></asp:ListItem>
                                <asp:ListItem Text="複選方塊"></asp:ListItem>
                            </asp:DropDownList>
                        <asp:CheckBox ID="cbxCheck" runat="server" Text="必填" /><br />
                        回答
                            <asp:TextBox ID="txtAnswer" runat="server" Enabled="false"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Text="(多個答案以:分隔)"></asp:Label>
                        <asp:Button ID="btnAdd" runat="server" Text="加入" OnClick="btnAdd_Click"/><br />
                        <asp:GridView ID="gvCommonQuestion" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="CommonQuestionID" HeaderText="#" />
                                <asp:BoundField DataField="CommonQuestionTitle" HeaderText="常用問題名稱" />
                                <asp:BoundField DataField="CommonQuestionName" HeaderText="問題" />
                                <asp:BoundField DataField="Type" HeaderText="種類" />
                                 <asp:TemplateField HeaderText="必填" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxRequired" runat="server" Enabled="false" Checked='<%#(int)Eval("Required")==0 ? false : true %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="Answer" HeaderText="回答" />
                                <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnUpdate" runat="server" Text="編輯" OnClick="btnUpdate_Click"/>
                                        </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <span style="color: red">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                        </span>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
