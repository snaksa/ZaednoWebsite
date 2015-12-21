<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="ShowWork.aspx.cs" Inherits="FullSchoolWebsite.ShowWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title id="pageTitle" runat="server">- Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>

    <script>
        function ButtonClick(id) {
            $(id).click();
        }

    </script>
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
                <div class="meta">
                    <div style="float: left;">Публикувано от <span id="AuthorNameSpan" runat="server"></span>, <span id="DateSpan" runat="server"></span></div>

                    <div style="float: right;" id="voteDiv">
                        <img id="positiveVoteImg" src="img/like.png" class="voteIcon" title="Харесва ми" onclick="ButtonClick('#PositiveVote')" />
                        <img id="negativeVoteImg" src="img/dislike.png" class="voteIcon" title="Не ми харесва" onclick="ButtonClick('#NegativeVote')" />
                    </div>

                </div>

                <div class="content">
                    <span id="TextSpan" runat="server"></span>
                </div>
            </div>

        </div>
        <!-- ENDS single -->

        <!-- Comments switcher -->
        <h6 class="show-comments" id="numberOfComments" runat="server"></h6>
        <div class="comments-switcher">

            <!-- comments list -->
            <div id="comments-wrap">
                <ol class="commentlist">

                    <asp:Repeater runat="server" ID="AllCommentsRepeater">
                        <ItemTemplate>
                            <li class="comment even thread-even depth-1" id="li-comment-1">
                                <div id="comment-1" class="comment-body clearfix">
                                    <img alt="" src="/files/images/userPic.png" class="avatar avatar-35 photo" height="35" width="35">
                                    <div class="comment-author vcard"><%#Eval("AuthorName") %></div>

                                    <div class="comment-meta commentmetadata">
                                        <span class="comment-date"><%#Eval("Date") %>  </span>
                                    </div>

                                    <div class="comment-inner">
                                        <p><%#Eval("Text") %></p>
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                </ol>
            </div>
            <!-- ENDS comments list -->


            <!-- Respond -->
            <div id="respond">
                <form runat="server" id="contactForm">

                    <h6 class="s-title">Оставете коментар</h6>
                    <asp:ScriptManager runat="server" ID="ScriptManager" />
                    <asp:UpdatePanel runat="server" ID="UpdatePanel">
                        <ContentTemplate>

                            <%--buttons for voting--%>
                            <asp:Button runat="server" ID="PositiveVote" Style="display: none" OnClick="PositiveVote_Click" />
                            <asp:Button runat="server" ID="NegativeVote" Style="display: none" OnClick="NegativeVote_Click" />
                            <%--end buttons for voting--%>

                            <fieldset>
                                <div>
                                    <input runat="server" type="text" name="author" id="name" value="" class="form-poshytip" title="Въведете своето име">
                                    <label>Име<small style="color: red;">*</small></label><br>
                                </div>

                                <div>
                                    <input runat="server" type="text" name="email" id="email" value="" class="form-poshytip" title="Въведете своя имейл">
                                    <label>Email<small style="color: red;">*</small> <span>(няма да бъде публикуван)</span></label><br>
                                </div>

                                <div>
                                    <textarea runat="server" name="comment" id="comments" class="form-poshytip" title="Въведете своя коментар"></textarea>
                                </div>

                                <p>
                                    <input runat="server" name="submit" type="submit" id="submit" onserverclick="PostComment_ServerClick" tabindex="5" value="Публикувай" />
                                </p>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </form>
                <p id="success" class="success" style="display: none;">Коментарът е добавен успешно!</p>
            </div>
            <div class="clear"></div>
            <!-- ENDS Respond -->
        </div>
        <!-- ENDS Comments switcher -->

    </div>
    <!-- ENDS side content -->

    <!-- sidebar -->
    <div id="sidebar">
        <div class="sideblock">
            <h6 class="side-title">Детайли</h6>
            <ul class="cat-list">
                <li>
                    <img src="/files/images/authorPic.png" title="Автор" style="margin-right: 3px; width: 20px; height: 20px;" />
                    <span runat="server" id="SidebarAuthorName">asfasf</span>
                </li>

                <li>
                    <img src="/files/images/datePic.png" title="Дата на публикуване" style="margin-right: 3px; width: 20px; height: 20px;" />
                    <span runat="server" id="SidebarDateOfPublication">asfasf</span>
                </li>

                <li runat="server" id="authorEmailListElement" visible="false">
                    <img title="Email на автора" src="/files/images/emailPic.png" style="margin-right: 3px; width: 20px; height: 20px;" />
                    <span runat="server" id="SidebarAuthorEmail">asfasf</span>
                </li>

                <li>
                    <img src="/img/like.png" title="Положителни гласове" style="margin-right: 3px; width: 20px;" />
                    <span runat="server" id="SidebarPositiveVotes">asfasf</span>
                </li>

                <li>
                    <img src="/img/dislike.png" title="Отрицателни гласове" style="margin-right: 3px; width: 20px;" />
                    <span runat="server" id="SidebarNegativeVotes">asfasf</span>
                </li>
                <li>
                    <img src="/img/visitors.png" title="Преглеждания" style="margin-right: 3px; width: 20px;" />
                    <span runat="server" id="SidebarVisitors">asfasf</span>
                </li>

            </ul>
        </div>
    </div>
    <!-- ENDS sidebar -->

    <div class="clear"></div>
</asp:Content>
