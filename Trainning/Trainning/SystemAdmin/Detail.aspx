<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Trainning.SystemAdmin.Detail" %>

<%@ Register Src="~/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Script/jquery-3.6.0.min.js"></script>
    <link href="/css/bootstrap.css" rel="stylesheet" />
    <script src="/js/bootstrap.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
    
    <script>
            $(document).ready(function () {
                $('button[data-bs-toggle="pill"]').on('show.bs.tab', function (e) {
                    localStorage.setItem('activeTab', $(e.target).attr('data-bs-target'));
                });
                var activeTab = localStorage.getItem('activeTab');
                if (activeTab) {
                    $('#pills-tab button[data-bs-target="' + activeTab + '"]').tab('show');
                }
            });
    </script> 
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
                            <button class="nav-link " id="pills-list-tab" data-bs-toggle="pill" data-bs-target="#pills-list" type="button" role="tab" aria-controls="pills-list" aria-selected="true">問卷</button>
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
                            <asp:TextBox ID="txtName" runat="server" Width="300"></asp:TextBox><br />
                            描述內容
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="300"></asp:TextBox><br />
                            開始時間
                            <asp:TextBox ID="txtStart" runat="server" TextMode="Date"></asp:TextBox><br />
                            結束時間
                            <asp:TextBox ID="txtEnd" runat="server" TextMode="Date"></asp:TextBox><br />
                            <asp:CheckBox ID="cbxTurnOn" runat="server" Text="已啟用" Checked="false" /><br />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:Button ID="btnCancel1" runat="server" Text="取消" OnClick="btnCancel1_Click" />
                            <asp:Button ID="btnSubmit1" runat="server" Text="送出" OnClick="btnSubmit1_Click" /><br />
                            <span style="color: red">
                            <asp:Literal ID="ltListMsg" runat="server"></asp:Literal>
                            </span>
                        </div>
                        <div class="tab-pane fade" id="pills-question" role="tabpanel" aria-labelledby="pills-question-tab">
                            <asp:DropDownList ID="ddlType" runat="server" OnTextChanged="ddlType_TextChanged" AutoPostBack="true">
                            </asp:DropDownList><br />
                            問題
                            <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                            種類
                            <asp:DropDownList ID="ddlQuestionType" runat="server" OnTextChanged="ddlQuestionType_TextChanged" AutoPostBack="true">
                                <asp:ListItem Text="文字方塊"></asp:ListItem>
                                <asp:ListItem Text="單選方塊"></asp:ListItem>
                                <asp:ListItem Text="複選方塊"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="cbxCheck" runat="server" Text="必填" /><br />
                            回答
                            <asp:TextBox ID="txtAnswer" runat="server" Enabled="false"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" Text="(多個答案以:分隔)"></asp:Label>
                            <asp:Button ID="btnAdd" runat="server" Text="加入" OnClick="btnAdd_Click" /><br />
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="image/Trash Can.png" Width="35" Height="35" OnClick="ImageButton1_Click" /><br />
                            <asp:GridView ID="gvQuestionStatus" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxChoose" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuestionID" HeaderText="#" />
                                    <asp:BoundField DataField="QuestionName" HeaderText="問題" />
                                    <asp:BoundField DataField="Type" HeaderText="種類" />
                                    <asp:TemplateField HeaderText="必填">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxRequired" runat="server" Enabled="false" Checked='<%#(int)Eval("Required")==0 ? false : true %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Answer" HeaderText="回答" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Text="編輯" OnClick="btnEdit_Click" />
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
                            <asp:Literal ID="ltMode" runat="server"></asp:Literal>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting" AutoGenerateDeleteButton="true" DataKeyNames="QuestionID">
                                <Columns>
                                    <asp:BoundField DataField="QuestionID" HeaderText="#" />
                                    <asp:BoundField DataField="QuestionName" HeaderText="問題" />
                                    <asp:BoundField DataField="Type" HeaderText="種類" />
                                    <asp:CheckBoxField DataField="Required" HeaderText="必填" ReadOnly="true" />
                                    <asp:BoundField DataField="Answer" HeaderText="回答" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <asp:Button ID="btnCancel2" runat="server" Text="取消" OnClick="btnCancel2_Click" />
                            <asp:Button ID="btnSubmit2" runat="server" Text="送出" OnClick="btnSubmit2_Click" />
                            <span style="color: red">
                                <asp:Literal ID="ltQuestionMsg" runat="server"></asp:Literal>
                            </span>
                        </div>
                        <div class="tab-pane fade" id="pills-infomation" role="tabpanel" aria-labelledby="pills-infomation-tab">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                                <asp:Button ID="btnDownload" runat="server" Text="匯出" OnClick="btnDownload_Click" /><br />
                                <asp:GridView ID="gvReply" runat="server" AutoGenerateColumns="false" PageSize="10" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#gvReply.PageIndex * gvReply.PageSize + gvReply.Rows.Count + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="姓名" />
                                        <asp:BoundField DataField="ReplyTime" HeaderText="填寫時間" />
                                        <asp:TemplateField HeaderText="觀看細節">
                                            <ItemTemplate>
                                                <asp:Button ID="btnInfo" runat="server" Text="前往" OnClick="btnInfo_Click" />
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
                                <uc1:ucPager runat="server" ID="ucPager" PageSize="10" Url="/SystemAdmin/Detail.aspx" />
                                <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="false">姓名:&ensp;
                                <asp:Literal ID="ltName" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtDetailName" runat="server" ReadOnly="true" Width="150" BorderStyle="None"></asp:TextBox>
                                &emsp;
                                手機:
                                <asp:Literal ID="ltPhone" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtDetailPhone" runat="server" ReadOnly="true" Width="150" BorderStyle="None" ></asp:TextBox>&emsp;
                                <br />
                                Email:
                                <asp:Literal ID="ltEmail" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtDetailEmail" runat="server" ReadOnly="true" Width="153" BorderStyle="None"></asp:TextBox>
                                &emsp;
                                年齡:
                                <asp:Literal ID="ltAge" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtDetailAge" runat="server" ReadOnly="true" Width="150" BorderStyle="None"></asp:TextBox>&emsp;
                                <br />
                                填寫時間:
                                <asp:Literal ID="ltReplyTime" runat="server"></asp:Literal>
                                <br />
                                <br />
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="PlaceHolder3" runat="server" Visible="false"></asp:PlaceHolder>
                            <br />
                            <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />
                        </div>
                        <div class="tab-pane fade" id="pills-result" role="tabpanel" aria-labelledby="pills-result-tab">
                            <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
