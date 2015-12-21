<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="Galleries.aspx.cs" Inherits="FullSchoolWebsite.Galleries" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Галерии - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
        <h1>Галерии</h1>
    </div>

    <!-- Gallery holder -->
    <div id="gallery-holder">

        <!-- Thumbnails -->
        <form runat="server" id="contestForm">
            <ul class="work-thumbs">
                <asp:Repeater runat="server" ID="AllContestsRepeater">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="ShowGallery" CssClass="plusbg" runat="server" NavigateUrl='<%# "ShowGallery.aspx?id=" + Eval("ID") %>'><img src='<%#Eval("ImagePath") %>' style="width:285px;height:200px"></asp:HyperLink>
                            <div class="thumb-description">
                                <span class="thumb-title">
                                    <asp:HyperLink runat="server" NavigateUrl='<%# "ShowGallery.aspx?id=" + Eval("ID") %>'><%#Eval("Title") %></asp:HyperLink>
                                </span>
                                <p><%#Eval("Description") %></p>
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

