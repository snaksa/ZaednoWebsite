<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ShowAcceptedWorks.aspx.cs" Inherits="FullSchoolWebsite.AdminPanel.ShowAcceptedWorks" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Одобрени новини</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Одобрени творби</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full" id="newsArticle">
            <header>
                <h3 class="tabs_involved" id="sectionName">Одобрени творби</h3>
            </header>
            <div class="tab_container" style="max-height: 400px; overflow-y: scroll;">
                <div id="tab1" class="tab_content">
                    <table class="tablesorter" cellspacing="0">
                        <thead>
                            <tr>
                                <th style="width: 290px; text-align: center;">Заглавие</th>
                                <th style="width: 150px; text-align: center;">Автор</th>
                                <th style="width: 150px; text-align: center;">Дата</th>
                                <th style="width: 150px; text-align: center;">Email</th>
                                <th style="width: 100px; text-align: center;">Статус</th>
                                <th style="width: 150px; text-align: center;">Действия</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="AcceptedWorksRepeater">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Title") %></td>
                                        <td style="text-align: center;"><%#Eval("Author") %></td>
                                        <td style="text-align: center;"><%#Eval("Date") %></td>
                                        <td style="text-align: center;"><%#Eval("Email") %></td>
                                        <td style="text-align: center;"><%#(bool)Eval("Status") == true ? "Видима" : "Невидима" %></td>
                                        <td style="width: 90px; text-align: center;">
                                            <asp:ImageButton ID="EditSingleNewsWithID" runat="server" ImageUrl="images/icn_edit.png"
                                                ToolTip="Редактиране" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="EditSingleNewsWithID_Command" />
                                            <img id='<%#Eval("ID") %>' src="images/icn_trash.png" class="imageActions" onclick="PaintBackgroundRed(this.id, 'worksToDelete')" />
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/loadComments.png" ToolTip="Зареди коментарите"
                                                CssClass="imageActions" OnCommand="LoadCommentsWithID_Command" CommandName='<%#Eval("ID") %>' />
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
                    <asp:Button runat="server" ID="ButtonDeleteSelectedNews" CssClass="alt_btn"
                        OnClick="ButtonDeleteSelectedWorks_Click" Text="Изтрий маркираните новини" />
                </div>
            </footer>
        </article>
        <!-- end of content manager article -->

        <asp:HiddenField runat="server" ID="commentsToDelete" Value="" />
        <asp:HiddenField runat="server" ID="worksToDelete" Value="" />

        <div class="testDiv" id="testDiv" style="display: none; margin-top: -500px;">
            <article class="module width_full" id="commentsArticle">
                <header>
                    <h3 class="tabs_involved" id="H1">Коментари за
                        <asp:Label runat="server" ID="newsName" /></h3>
                    <a onclick="CloseArticle('#testDiv')">
                        <img src="images/icn_logout.png" style="float: right; margin-top: 6px; margin-right: 6px;" /></a>
                </header>
                <div class="tab_container" style="height: 300px; overflow-y: scroll;">
                    <div id="tab2" class="tab_content">
                        <table class="tablesorter" cellspacing="0">
                            <thead>
                                <tr>
                                    <th style="width: 440px; text-align: center;">Коментар</th>
                                    <th style="width: 100px; text-align: center;">Автор</th>
                                    <th style="width: 150px; text-align: center;">Дата</th>
                                    <th style="width: 150px; text-align: center;">E-mail</th>
                                    <th style="width: 90px; text-align: center;">Действия</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="CommentsRepeater">
                                    <ItemTemplate>
                                        <tr>
                                            <td title='<%#Eval("Text") %>'><%#Eval("Text") %></td>
                                            <td style="text-align: center;"><%#Eval("AuthorName") %></td>
                                            <td style="text-align: center;"><%#Eval("AuthorEmail") %></td>
                                            <td style="text-align: center;"><%#Eval("Date") %></td>
                                            <td style="text-align: center;">
                                                <img id='<%#"Comment" + Eval("ID") %>' src="images/icn_trash.png" onclick="PaintBackgroundRed('<%#"Comment" + Eval("ID") %>', 'commentsToDelete')" />
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
                        <asp:Button runat="server" ID="DeleteComments" CssClass="alt_btn"
                            OnClick="DeleteComments_Click" Text="Изтрий маркираните коментари" />
                    </div>
                </footer>
            </article>
        </div>
        <!-- end of content manager article -->
    </section>

</asp:Content>
