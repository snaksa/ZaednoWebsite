<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ShowSuggestedNews.aspx.cs" Inherits="FullSchoolWebsite.AdminPanel.ShowSuggestedNews" ClientIDMode="Static"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Предложени новини</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Предложени новини</h2>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full" id="newsArticle">
            <header>
                <h3 class="tabs_involved" id="sectionName">Предложени новини</h3>
            </header>
            <div class="tab_container" style="max-height: 400px; overflow-y: scroll;">
                <div id="tab1" class="tab_content">
                    <table class="tablesorter" cellspacing="0">
                        <thead>
                            <tr>
                                <th style="width: 440px; text-align: center;">Заглавие</th>
                                <th style="width: 200px; text-align: center;">Автор</th>
                                <th style="width: 150px; text-align: center;">Дата</th>
                                <th style="width: 130px; text-align: center;">Email</th>
                                <th style="width: 150px; text-align: center;">Действия</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="ShowSuggestedNewsRepeater">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Title") %></td>
                                        <td style="text-align: center;"><%#Eval("Author") %></td>
                                        <td style="text-align: center;"><%#Eval("Date") %></td>
                                        <td style="text-align: center;"><%#Eval("Email") %></td>
                                        <td style="width:90px; text-align: center;">
                                            <asp:ImageButton ID="Show" runat="server" ImageUrl="images/show.png"
                                                ToolTip="Покажи" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="Show_Command" />
                                            <asp:ImageButton ID="Accept" runat="server" ImageUrl="images/accept.png"
                                                ToolTip="Приеми" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="Accept_Command" />
                                            <asp:ImageButton ID="Refuse" runat="server" ImageUrl="images/refuse.png"
                                                ToolTip="Откажи" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="Refuse_Command" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </article>
    </section>
</asp:Content>
