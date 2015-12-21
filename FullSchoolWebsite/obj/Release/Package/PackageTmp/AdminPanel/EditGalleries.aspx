<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true"
    CodeBehind="EditGalleries.aspx.cs" Inherits="AdminPanel.EditGalleries" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на галерии</title>

    <script type="text/javascript">
        function uploadImages(id) {
            $(id).click();
        }

        function selectFile(id, galleryID) {
            $(id).click();
            var label = document.getElementById("GalleryID");
            label.value = galleryID;
        }
    </script>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на галерии</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full" id="newsArticle">
            <header>
                <h3 class="tabs_involved" id="sectionName">Редактиране на галерии</h3>
            </header>
            <div class="tab_container" style="max-height: 400px; overflow-y: scroll;">
                <div id="tab1" class="tab_content">
                    <table class="tablesorter" cellspacing="0">
                        <thead>
                            <tr>
                                <th style="width: 55%; text-align: center;">Заглавие</th>
                                <th style="width: 15%; text-align: center;">Брой снимки</th>
                                <th style="width: 11%; text-align: center;">Статус</th>
                                <th style="width: 19%; text-align: center;">Действия</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="AllGalleriesRepeater">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Title") %></td>
                                        <td style="text-align: center;"><%#Eval("NumberOfImages") %></td>
                                        <td style="text-align: center;"><%#(bool)Eval("Status") == true ? "Видима" : "Невидима" %></td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="EditGalleryWithID" runat="server" ImageUrl="images/icn_edit.png"
                                                ToolTip="Редактиране" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="EditGalleryWithID_Command" />
                                            <img id="AddImages" src="images/addImage.png" class="imageActions" onclick="selectFile('#AddImagesFileUpload', '<%#Eval("ID") %>'); return false;" title="Добавете снимки" />
                                            <asp:ImageButton ID="DeleteImage" runat="server" ImageUrl="images/deleteImage.png" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="DeleteImage_Command" ToolTip="Изтриване на снимки по избор" />
                                            <asp:ImageButton ID="DeleteGallery" runat="server" ImageUrl="images/icn_trash.png" ToolTip="Изтриване на галерия" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="DeleteGallery_Command" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </article>
        <asp:FileUpload runat="server" ID="AddImagesFileUpload" AllowMultiple="true" Style="visibility: hidden;" onchange="uploadImages('#UploadImages')" />
        <asp:Button runat="server" ID="UploadImages" Style="visibility: hidden;" OnClick="UploadImages_Click" />
        <asp:HiddenField runat="server" ID="GalleryID" Value="" />
        <!-- end of content manager article -->

        <%--ALL IMAGES--%>
        <div class="testDiv" id="allImages" style="display: none; margin-top: -20%; width: 683px; height: 800px;">
            <article class="module width_full" id="Article1">
                <header>
                    <h3 class="tabs_involved" id="H2">Изберете снимки</h3>
                    <a onclick="CloseArticle('#allImages')">
                        <img src="images/icn_logout.png" style="float: right; margin-top: 6px; margin-right: 6px;" /></a>
                </header>

                <div class="tab_container" style="height: 300px; overflow-y: scroll;">
                    <asp:Repeater runat="server" ID="AllImagesRepeater">
                        <ItemTemplate>
                            <div style="width: 150px; height: 150px; margin-right: 5px; float: left;">
                                <asp:CheckBox runat="server" ID="chkDelete" ToolTip='<%#Eval("ID") %>' Style="margin-top: 6px; margin-left: 10px;" />
                                <img src="<%#Eval("ImagePath") %>" class="imageToGallery" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <footer>
                    <div class="submit_link">
                        <asp:Button runat="server" ID="DeleteSelectedImages" CssClass="alt_btn" OnClick="DeleteSelectedImages_Click" Text="Изтрий маркираните снимки" />
                    </div>
                </footer>
            </article>
        </div>

    </section>
</asp:Content>
