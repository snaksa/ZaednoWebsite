<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="SuggestWork.aspx.cs" Inherits="FullSchoolWebsite.SuggestWork" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Предложете творба - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title"><h1>Предложете творба</h1></div>

    <div id="side-content">
        <!-- Respond -->
        <div id="respond">
            <form runat="server" id="commentform">
                <label for="name" style="display: inline-block; width: 150px; text-align: right;">Автор<small style="color: red;">*</small></label>
                <input runat="server" type="text" name="name" id="AuthorNameTextBox" value="">
                <br />

                <label for="email" style="display: inline-block; width: 150px; text-align: right;">Email<small style="color: red;">*</small></label>
                <input runat="server" type="text" name="email" id="AuthorEmailTextBox" value="">
                <asp:DropDownList runat="server" ID="ShowEmailDropDownList" CssClass="dropDownList">
                    <asp:ListItem title="Покажи имейла на потребителите" Value="1">Покажи</asp:ListItem>
                    <asp:ListItem title="Скрий имейла от потребителите" Value="0">Скрий</asp:ListItem>
                </asp:DropDownList>
                <br>

                <label for="title" style="display: inline-block; width: 150px; text-align: right;">Заглавие на новина<small style="color: red;">*</small></label>
                <input runat="server" type="text" name="title" id="NewsTitleTextBox" value="">
                <br>

                <div>
                    <label for="image" style="display: inline-block; width: 150px; text-align: right; float: left; margin-top: 10px; margin-right: 4px;">Снимка 590x260<small style="color: red;">*</small></label>
                    <asp:FileUpload runat="server" ID="ImageFileUpload" CssClass="fileUpload" Style="float: left;" />
                    <div class="clear"></div>
                </div>
                <br />

                <CKEditor:CKEditorControl runat="server" ID="CKEditor" BasePath="/AdminPanel/ckeditor"></CKEditor:CKEditorControl>

                <p>
                    <asp:Button runat="server" id="submit" value="Предложи" Text="Предложи" OnClick="submit_Click" />
                </p>

            </form>
        </div>

        <div class="clear"></div>
        <!-- ENDS Respond -->
    </div>


    <div class="clear"></div>
</asp:Content>
