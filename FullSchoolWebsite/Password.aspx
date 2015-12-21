<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="FullSchoolWebsite.Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Забравена парола - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
    </div>

    <!-- side content -->
    <div id="side-content" style="width:930px;">

        <section class="login"style="height:200px;">
            <div class="titulo"><span id="headerText" runat="server">Въведете своя email</span></div>
            <form id="Form1" runat="server">
                <input runat="server" type="text" placeholder="Email" id="emailTextBox" data-icon="U" style="border-bottom-left-radius: 6px; border-bottom-right-radius: 6px; border-bottom: #353535 1px solid;">
                
                <a runat="server" id="SentPassword" onserverclick="SentPassword_ServerClick" class="enviar" style="margin-top:13px;">Изпрати парола</a>
            </form>
        </section>

    </div>
    <!-- ENDS side content -->

    <div class="clear"></div>
</asp:Content>
