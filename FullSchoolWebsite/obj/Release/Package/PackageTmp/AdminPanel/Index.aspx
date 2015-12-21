<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AdminPanel.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Начало</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Начална страница</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Посещения</h3>
            </header>
            <div class="module_content">

                <article class="stats_overview">
                    <div class="overview_today">
                        <p class="overview_day">Днес</p>
                        <p class="overview_count">1,876</p>
                    </div>
                    <div class="overview_previous">
                        <p class="overview_day">Вчера</p>
                        <p class="overview_count">1,646</p>
                    </div>
                    <div class="overview_previous">
                        <p class="overview_day">Тази седмица</p>
                        <p class="overview_count">1,646</p>
                    </div>
                    <div class="overview_previous">
                        <p class="overview_day">Този месец</p>
                        <p class="overview_count">1,646</p>
                    </div>
                    <div class="overview_previous">
                        <p class="overview_day">Всички</p>
                        <p class="overview_count">1,646</p>
                    </div>
                </article>
                <div class="clear"></div>
            </div>
        </article>
        <!-- end of stats article -->

        <article class="module width_full">
            <header>
                <h3>Съобщения от администраторите</h3>
            </header>
            <div class="message_list">
                <div class="module_content">
                    <asp:Repeater runat="server" ID="AdminPosts">
                        <ItemTemplate>
                            <div class="message">
                                <p>
                                    Име: <strong><%#Eval("Name") %></strong>
                                    <asp:ImageButton ID="DeletePostImageButton" runat="server"
                                        ToolTip="Изтрий съобщението" ImageUrl="images/deleteIcon.png"
                                        Style="width: 25px; float: right" CommandName='<%#Eval("ID")%>'
                                        OnCommand="DeletePostImageButton_Command" />
                                    <br />
                                    E-mail: <strong><%#Eval("Email")%></strong>
                                </p>
                                <p><i><%#Eval("Message") %></i></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
            <footer>
                <div class="post_message">
                    <asp:TextBox runat="server" ID="TextBoxMessage" CssClass="textBox" />
                    <asp:Button runat="server" ID="ButtonSubmitMessage" class="btn_post_message" OnClick="ButtonSubmitMessage_Click" />
                </div>
            </footer>
        </article>
        <!-- end of messages from admins article -->

        <article class="module width_full">
            <header>
                <h3>Съобщения от потребители</h3>
            </header>
            <div class="message_list">
                <div class="module_content">
                    <asp:Repeater runat="server" ID="UserMessages">
                        <ItemTemplate>
                            <div class="message">
                                <p>
                                    Име: <strong><%#Eval("SenderName") %></strong>
                                    <asp:ImageButton ID="DeleteUserMessageButton" runat="server"
                                        ToolTip="Изтрий съобщението" ImageUrl="images/deleteIcon.png"
                                        Style="width: 25px; float: right" CommandName='<%#Eval("ID")%>'
                                        OnCommand="DeleteUserMessageButton_Command" />
                                    <br />
                                    E-mail: <strong><%#Eval("SenderEmail") %></strong>
                                    <br />
                                    Дата: <strong><%#Eval("Date") %></strong>
                                </p>
                                <p>
                                    <i><%#Eval("Message") %></i>
                                </p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </article>
        <!-- end of messages  from users article -->

        <div class="clear"></div>
        <div class="spacer"></div>
    </section>
</asp:Content>

