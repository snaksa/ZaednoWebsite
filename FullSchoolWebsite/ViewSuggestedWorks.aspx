<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="ViewSuggestedWorks.aspx.cs" Inherits="FullSchoolWebsite.ViewSuggestedWorks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title id="pageTitle" runat="server">- Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title"></div>

    <!-- side content -->
    <div id="side-content">

        <!-- single -->
        <div class="single-post">
            <div class="post">
                <div class="post-feature-img">
                    <img id="NewsImage" src="" runat="server" width="590" height="260" />
                </div>
                <img src="/files/feature-post-shadow.png" alt="shadow">

                <h4><span id="TitleSpan" runat="server"></span></h4>
                <div class="meta">Публикувано от <span id="AuthorNameSpan" runat="server"></span>, <span id="DateSpan" runat="server"></span></div>

                <div class="content">
                    <span id="TextSpan" runat="server"></span>
                </div>
            </div>

        </div>
        <!-- ENDS single -->

    </div>
    <!-- ENDS side content -->

    <!-- sidebar -->
    <div id="sidebar">
        <div class="sideblock">
            <h6 class="side-title">Детайли</h6>
            <ul class="cat-list">
                <li>
                    <img src="/files/images/authorPic.png" title="Автор" style="margin-right: 3px; width: 20px; height: 20px;" /><span runat="server" id="SidebarAuthorName">asfasf</span></li>
                <li>
                    <img src="/files/images/datePic.png" title="Дата на публикуване" style="margin-right: 3px; width: 20px; height: 20px;" /><span runat="server" id="SidebarDateOfPublication">asfasf</span></li>
                <li runat="server" id="authorEmailListElement" visible="false">
                    <img title="Email на автора" src="/files/images/emailPic.png" style="margin-right: 3px; width: 20px; height: 20px;" /><span runat="server" id="SidebarAuthorEmail">asfasf</span></li>

            </ul>
        </div>
    </div>
    <!-- ENDS sidebar -->

    <div class="clear"></div>
</asp:Content>