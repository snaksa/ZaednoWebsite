<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="Contests.aspx.cs" Inherits="FullSchoolWebsite.Contests" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Конкурси - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
        <h1>Конкурси</h1>
    </div>

    <!-- Gallery holder -->
    <div id="gallery-holder">

        <!-- Thumbnails -->
        <form runat="server" id="contestForm">
            <ul class="work-thumbs">
                <asp:Repeater runat="server" ID="AllContestsRepeater">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="ShowContest" CssClass="plusbg" runat="server" NavigateUrl='<%# "ShowContest.aspx?id=" + Eval("ID") %>'><img src='<%#Eval("SmallImagePath") %>' style="width:285px;height:200px"></asp:HyperLink>
                            <div class="thumb-description">
                                <span class="thumb-title">
                                    <asp:HyperLink runat="server" NavigateUrl='<%# "ShowContest.aspx?id=" + Eval("ID") %>'><%#Eval("Title") %></asp:HyperLink>
                                </span>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="clear"></div>
        </form>
        <!-- ENDS Thumbnails -->

        <div class="clear"></div>
    </div>
</asp:Content>
