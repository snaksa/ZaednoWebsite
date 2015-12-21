<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="EditAcceptedNews.aspx.cs" Inherits="FullSchoolWebsite.AdminPanel.EditAcceptedNews" ClientIDMode="Static" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на новина</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на новина</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Редактиране на новина</h3>
            </header>

            <div class="module_content">

                <fieldset>
                    <label>Заглавие на новината</label>
                    <asp:TextBox runat="server" ID="NewsTitleTextBox" CssClass="textBox"></asp:TextBox>
                </fieldset>

                <fieldset>
                    <CKEditor:CKEditorControl runat="server" ID="CKEditor" BasePath="/AdminPanel/ckeditor"></CKEditor:CKEditorControl>
                </fieldset>

                <fieldset>
                    <label>Автор</label>
                    <asp:TextBox runat="server" ID="AuthorNameTextBox" CssClass="smallTextBox"></asp:TextBox>
                </fieldset>

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>Email</label>
                    <asp:TextBox runat="server" ID="AuthorEmailTextBox" CssClass="smallTextBox"></asp:TextBox>
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Снимка 590x260</label>
                    <asp:FileUpload runat="server" ID="ImageFileUpload" Style="margin-left: 10px;" />
                </fieldset>

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>Покажи имейл</label>
                    <asp:DropDownList runat="server" ID="ShowEmailDropDownList" CssClass="dropDownBox">
                        <asp:ListItem title="Покажи имейла на потребителите" Value="1">Покажи</asp:ListItem>
                        <asp:ListItem title="Скрий имейла от потребителите" Value="0">Скрий</asp:ListItem>
                    </asp:DropDownList>
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Статус</label>
                    <asp:DropDownList runat="server" ID="StatusDropDownList" CssClass="dropDownBox">
                        <asp:ListItem Value="1">Видима</asp:ListItem>
                        <asp:ListItem Value="0">Невидима</asp:ListItem>
                    </asp:DropDownList>
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="EditNews" Text="Редактирай" CssClass="alt_btn" OnClick="EditNews_Click" />
                </div>
            </footer>
        </article>
        <!-- end of post new article -->
    </section>

</asp:Content>

