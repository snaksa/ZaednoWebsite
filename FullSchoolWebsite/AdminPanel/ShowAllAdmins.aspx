<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true"
    CodeBehind="ShowAllAdmins.aspx.cs" Inherits="AdminPanel.ShowAllAdmins" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Преглед на администраторите</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Преглед на администраторите</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full" id="newsArticle">
            <header>
                <h3 class="tabs_involved" id="sectionName">Преглед на администраторите</h3>
            </header>
            <div class="tab_container" style="max-height: 400px; overflow-y: scroll;">
                <div id="tab1" class="tab_content">
                    <table class="tablesorter" cellspacing="0">
                        <thead>
                            <tr>
                                <th style="width: 40%; text-align: center;">Име</th>
                                <th style="width: 25%; text-align: center;">E-mail</th>
                                <th style="width: 15%; text-align: center;">Роля</th>
                                <th style="width: 10%; text-align: center;">Действия</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="AllAdminsRepeater">
                                <ItemTemplate>
                                    <tr style="background-color: #F1F1F1;">
                                        <td><%#Eval("Name") %></td>
                                        <td style="text-align: center;"><%#Eval("Email") %></td>
                                        <td style="text-align: center;"><%#Eval("Role") %></td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="EditAdminWithID" runat="server" ImageUrl="images/icn_edit.png" ToolTip="Редактиране" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="EditAdminWithID_Command" />
                                            <asp:ImageButton runat="server" ID="DeleteAdmin" ImageUrl="images/icn_trash.png" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="DeleteAdmin_Command" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </article>
        <!-- end of content manager article -->

    </section>

</asp:Content>
