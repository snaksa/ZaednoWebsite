<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="EditAdmin.aspx.cs" Inherits="AdminPanel.EditAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на администратор</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на администратор</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Редактиране на администратор</h3>
            </header>

            <div class="module_content">

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>Име</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="AdminNameTextBox" />
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Парола</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="AdminPasswordTextBox" />
                </fieldset>

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>E-mail</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="AdminEmailTextBox" />
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Роля</label>
                    <asp:DropDownList CssClass="dropDownBox" runat="server" ID="AdminRoleDropDownList" Style="width: 92%;">
                        <asp:ListItem title="Може да редактира, добавя и премахва администратори">Главен администратор</asp:ListItem>
                        <asp:ListItem title="Не може да редактира, добавя и премахва администратори">Помощник-администратор</asp:ListItem>
                    </asp:DropDownList>
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="ButtonEditAdmin" Text="Редактирай" CssClass="alt_btn" OnClick="ButtonEditAdmin_Click" />
                </div>
            </footer>
        </article>
        <!-- end of post new article -->
</asp:Content>
