<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true"
    CodeBehind="EditContests.aspx.cs" Inherits="AdminPanel.EditContests" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на конкурси</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на конкурси</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full" id="newsArticle">
            <header>
                <h3 class="tabs_involved" id="sectionName">Редактиране на конкурси</h3>
            </header>
            <div class="tab_container" style="max-height: 400px; overflow-y: scroll;">
                <div id="tab1" class="tab_content">
                    <table class="tablesorter" cellspacing="0">
                        <thead>
                            <tr>
                                <th style="width: 55%; text-align: center;">Заглавие</th>
                                <th style="width: 15%; text-align: center;">Дата</th>
                                <th style="width: 11%; text-align: center;">Статус</th>
                                <th style="width: 19%; text-align: center;">Действия</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="ContestsRepeater">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Title") %></td>
                                        <td style="text-align: center;"><%#Eval("Date") %></td>
                                        <td style="text-align: center;"><%#(bool)Eval("Status") == true ? "Видим" : "Невидим" %></td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="EditContestWithID" runat="server" ImageUrl="images/icn_edit.png"
                                                ToolTip="Редактиране" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="EditContestWithID_Command" />
                                            <img id='<%#Eval("ID") %>' src="images/icn_trash.png" class="imageActions" onclick="PaintBackgroundRed('<%#Eval("ID") %>', 'contestsToDelete')" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="ButtonDeleteSelectedContests" CssClass="alt_btn"
                        Text="Изтрий маркираните конкурси" OnClick="ButtonDeleteSelectedContests_Click" />
                </div>
            </footer>
        </article>
        <!-- end of content manager article -->

        <asp:HiddenField runat="server" ID="contestsToDelete" Value="" />
    </section>

</asp:Content>
