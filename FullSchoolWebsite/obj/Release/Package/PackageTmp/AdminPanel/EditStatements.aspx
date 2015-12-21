<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="EditStatements.aspx.cs" Inherits="AdminPanel.EditStatements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на началната страница</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на изявленията</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Редактиране на изявление</h3>
            </header>
            <div class="module_content">
                <fieldset>
                    <label>Заглавие</label>
                    <asp:TextBox runat="server" CssClass="textBox" ID="TextBoxTitle" />
                </fieldset>

                <fieldset>
                    <label>Текст</label>
                    <asp:TextBox runat="server" CssClass="textBox" ID="TextBoxDescription" />
                </fieldset>

                <fieldset style="width: 48%; float: left; margin-right: 3%;">
                    <label>Позиция</label>
                    <asp:DropDownList CssClass="dropDownBox" runat="server" ID="Position" Style="width: 92%;">
                        <asp:ListItem Value="1">Първа позиция</asp:ListItem>
                        <asp:ListItem Value="2">Втора позиция</asp:ListItem>
                        <asp:ListItem Value="3">Трета позиция</asp:ListItem>
                        <asp:ListItem Value="4">Четвърта позиция</asp:ListItem>
                        <asp:ListItem Value="5">Пета позиция</asp:ListItem>
                        <asp:ListItem Value="6">Шеста позиция</asp:ListItem>
                    </asp:DropDownList>
                </fieldset>
                <fieldset style="width: 48%; float: left;">
                    <label>Снимка</label><br/>
                    <input type="radio" name="imgGroup" value="article.png" style="margin-left:-190px;" /><img src="images/article.png" />
                    <input type="radio" name="imgGroup" value="bargraph.png" /><img src="images/bargraph.png" />
                    <input type="radio" name="imgGroup" value="cup.png" /><img src="images/cup.png" />
                    <input type="radio" name="imgGroup" value="heart.png" /><img src="images/heart.png" />
                    <input type="radio" name="imgGroup" value="lightbulb.png" /><img src="images/lightbulb.png" />
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="EditStatementButton" Text="Обнови" CssClass="alt_btn" OnClick="EditStatementButton_Click" />
                </div>
            </footer>
        </article>
    </section>
</asp:Content>

