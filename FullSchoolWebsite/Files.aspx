<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="Files.aspx.cs" Inherits="FullSchoolWebsite.Files" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Материали - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
        <h1>Материали</h1>
    </div>

    <!-- Gallery holder -->
    <div id="gallery-holder">

        <!-- Thumbnails -->
        <form runat="server" id="contestForm">
            <ul class="work-thumbs">
                <asp:Repeater runat="server" ID="AllFilesRepeater">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="ShowContest" CssClass="plusbg" runat="server" NavigateUrl='<%# Eval("FilePath") %>'><img src='<%#Eval("ImagePath") %>' style="width:285px;height:200px"></asp:HyperLink>
                            <div class="thumb-description">
                                <span class="thumb-title">
                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("FilePath") %>'><%#Eval("Name") %></asp:HyperLink>
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
