<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Trainning.SystemAdmin.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1">
            <tr>
                <td colspan="8">
                    <h1>後台-問卷管理</h1>
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
                    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                        <li class="nav-item" role="presentation">
                            <button class="nav-link active" id="pills-list-tab" data-bs-toggle="pill" data-bs-target="#pills-list" type="button" role="tab" aria-controls="pills-list" aria-selected="true">問卷</button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button class="nav-link" id="pills-question-tab" data-bs-toggle="pill" data-bs-target="#pills-question" type="button" role="tab" aria-controls="pills-question" aria-selected="false">問題</button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button class="nav-link" id="pills-infomation-tab" data-bs-toggle="pill" data-bs-target="#pills-infomation" type="button" role="tab" aria-controls="pills-infomation" aria-selected="false">填寫資料</button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button class="nav-link" id="pills-result-tab" data-bs-toggle="pill" data-bs-target="#pills-result" type="button" role="tab" aria-controls="pills-result" aria-selected="false">統計</button>
                        </li>
                    </ul>
                    <div class="tab-content" id="pills-tabContent">
                        <div class="tab-pane fade show active" id="pills-list" role="tabpanel" aria-labelledby="pills-list-tab">
                            問卷名稱
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                            描述內容
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                            開始時間
                            <asp:TextBox ID="txtStart" runat="server" TextMode="Date"></asp:TextBox><br />
                            結束時間
                            <asp:TextBox ID="txtEnd" runat="server" TextMode="Date"></asp:TextBox><br />
                            <asp:CheckBox ID="cbxTurnOn" runat="server" Text="已啟用" Checked="false"/><br />
                            <asp:HiddenField ID="HiddenField1" runat="server"/>
                            <asp:Button ID="btnCancel1" runat="server" Text="取消" OnClick="btnCancel1_Click"/>
                            <asp:Button ID="btnSubmit1" runat="server" Text="送出" OnClick="btnSubmit1_Click"/>
                        </div>
                        <div class="tab-pane fade" id="pills-question" role="tabpanel" aria-labelledby="pills-question-tab">
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem>自訂問卷</asp:ListItem>
                            </asp:DropDownList><br />
                            問題
                            <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                            種類
                            <asp:DropDownList ID="ddlQuestionType" runat="server">
                                <asp:ListItem>文字方塊</asp:ListItem>
                                <asp:ListItem>單選方塊</asp:ListItem>
                                <asp:ListItem>複選方塊</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="cbxCheck" runat="server" Text="必填"/><br />
                            回答
                            <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" Text="(多個答案以:分隔)"></asp:Label>
                            <asp:Button ID="btnAdd" runat="server" Text="加入" OnClick="btnAdd_Click"/><br />
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="image/Trash Can.png" Width="35" Height="35" OnClick="ImageButton1_Click"/><br />
                            <asp:GridView ID="gvQuestionStatus" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxChoose" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuestionID" HeaderText="#"/>
                                    <asp:BoundField DataField="QuestionName" HeaderText="問題" />
                                    <asp:BoundField DataField="Type" HeaderText="種類" />
                                    <asp:TemplateField HeaderText="必須">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxRequired" runat="server" Checked='<%#(int)Eval("Required")==0 ? false : true %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="/SystemAdmin/Detail.aspx?ID=<%# Eval("QuestionID") %>">編輯</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <asp:Button ID="btnCancel2" runat="server" Text="取消" OnClick="btnCancel2_Click"/>
                            <asp:Button ID="btnSubmit2" runat="server" Text="送出" OnClick="btnSubmit2_Click"/>
                        </div>
                        <div class="tab-pane fade" id="pills-infomation" role="tabpanel" aria-labelledby="pills-infomation-tab">
                            <asp:GridView ID="GridView2" runat="server">

                            </asp:GridView>
                        </div>
                        <div class="tab-pane fade" id="pills-result" role="tabpanel" aria-labelledby="pills-result-tab">...</div>
                    </div>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
