<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FullSchoolWebsite.Login" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Вход - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
    </div>

    <!-- side content -->
    <div id="side-content" style="width:930px;">

        <section class="login">
            <div class="titulo"><span runat="server" id="headerText">Вход в системата</span></div>
            <form runat="server">
                <input runat="server" type="text" placeholder="Email" id="emailTextBox" data-icon="U">
                <input runat="server" type="password" placeholder="Password" id="passwordTextBox" data-icon="x">
                <div class="olvido">
                    <div class="col"><a href="Password.aspx">Забравена парола?</a></div>
                </div>
                <a runat="server" id="AdminLogin" onserverclick="AdminLogin_ServerClick" class="enviar">Вход</a>
            </form>
        </section>

    </div>
    <!-- ENDS side content -->

    <div class="clear"></div>
</asp:Content>
