<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="ShowGallery.aspx.cs" Inherits="FullSchoolWebsite.ShowGallery" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title id="pageTitle" runat="server">- Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
        <h1 id="headerTitle" runat="server"></h1>
    </div>

    <!-- Gallery holder -->
    <div id="gallery-holder">

        <!-- Thumbnails -->
        <form runat="server" id="contestForm">
            <div class="baguetteBoxOne gallery">
                <ul id="portfolio-list" class="gallery-thumbs">
                    <asp:Repeater runat="server" ID="AllImagesRepeater">
                        <ItemTemplate>
                            <li class="graphics" id="unique_item0">

                                <div class='<%# Session["PicVote" + Eval("ID")] != null ? "galleryVotingInactive" : "galleryVoting" %>'>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:ImageButton runat="server" ImageUrl="img/like.png" CssClass='<%#Session["PicVote" + Eval("ID")] != null ? Session["PicVote" + Eval("ID")].ToString() == "1" ? "voteIconClicked" : "voteIcon" : "voteIcon" %>' Style="margin-right: 1px;" CommandName='<%# Eval("ID")%>' OnCommand="PositiveVoteClicked_Command" />
                                        </div>
                                        <div style="float: left; margin-top: 20px;margin-left:1px;">
                                            <span runat="server" style="background-color: green; color: white;"><%#Eval("PositiveVotes") %></span>
                                        </div>
                                    </div>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:ImageButton runat="server" ImageUrl="img/dislike.png" CssClass='<%#Session["PicVote" + Eval("ID")] != null ? Session["PicVote" + Eval("ID")].ToString() == "0" ? "voteIconClicked" : "voteIcon" : "voteIcon"  %>' Style="margin-right: 1px;" CommandName='<%# Eval("ID")%>' OnCommand="NegativeVoteClicked_Command" />
                                        </div>
                                        <div style="float: left; margin-top: 20px;">
                                            <span runat="server" style="background-color: red; color: white;margin-left:1px;"><%#Eval("NegativeVotes") %></span>
                                        </div>
                                    </div>
                                </div>

                                <a href='<%#Eval("ImagePath") %>' class="plusbg">
                                    <img class="galleryThumbnails" src='<%#Eval("ImagePath") %>'>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="clear"></div>
        </form>
        <!-- ENDS Thumbnails -->

        <div class="clear"></div>
    </div>
</asp:Content>
