<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="EditSingleFile.aspx.cs" Inherits="AdminPanel.EditSingleFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на файл</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на файл</h2>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Добавяне на файл към архив</h3>
            </header>

            <div class="module_content">
                    <fieldset>
                    <label>Заглавие</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxTitle" />
                </fieldset>

                <fieldset>
                    <label>Описание</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxDescription" />
                </fieldset>

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>Файл</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="FilePathTextBox" />
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Снимка - 285x200</label>
                    <asp:FileUpload runat="server" ID="FileImageFileUpload" Style="margin-left: 10px;" />
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="EditFile" Text="Редактирай" CssClass="alt_btn" OnClick="EditFile_Click" />
                </div>
            </footer>
        </article>
    </section>
</asp:Content>
