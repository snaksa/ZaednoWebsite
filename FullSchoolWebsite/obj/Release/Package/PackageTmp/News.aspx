<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="FullSchoolWebsite.News" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Новини - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
        <h1>Новини</h1>
    </div>

    <!-- side content -->
    <div id="side-content">

        <!-- Blog loop -->
        <div class="blog-loop">

            <asp:Repeater runat="server" ID="NewsToBeShownRepeater">
                <ItemTemplate>
                    <div class="post">
                        <a href='<%#"ShowNews.aspx?id=" + Eval("ID").ToString() %>' class="post-feature-img">
                            <img src='<%#Eval("ImagePath") %>' width="590" height="260" alt="Pic">
                        </a>
                        <img src="./files/feature-post-shadow.png" alt="shadow">

                        <h4><%#Eval("Title") %></h4>
                        <div class="excerpt" style="width: 590px;"><%# HttpUtility.HtmlDecode(Eval("Text").ToString().Length > 270 ? Eval("Text").ToString().Substring(0, 265) + "..." : Eval("Text").ToString()) %></div>
                        <div class="meta"> Публикувано от <%#Eval("Author") %>, <%#Eval("Date") %><a href='<%#"ShowNews.aspx?id=" + Eval("ID").ToString() %>' class="read-more">Прочети още...</a></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- pager -->
            <ul class="pager">
                <li>‹</li>
                <asp:Repeater runat="server" ID="PagerRepeater">
                    <ItemTemplate>
                        <li class='<%#Container.DataItem.ToString() == Request.QueryString["page"] ? "active" : ""%>'><a href='News.aspx?page=<%#Container.DataItem %>'><%#Container.DataItem %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                <li>›</li>
            </ul>
            <div class="clear"></div>
            <!-- ENDS pager -->


        </div>
        <!-- ENDS Blog loop -->

    </div>
    <!-- ENDS side content -->

    <div class="clear"></div>
</asp:Content>
