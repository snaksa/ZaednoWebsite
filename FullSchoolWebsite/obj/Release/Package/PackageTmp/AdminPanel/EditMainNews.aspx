<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true"
    CodeBehind="EditMainNews.aspx.cs" Inherits="AdminPanel.EditHomePage" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на началната страница</title>


</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на слайдшоу</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Редактиране на слайдшоу</h3>
            </header>
            <div class="module_content">

                <div style="height:350px;">
                    <fieldset style="width: 48%; float: left;">
                        <label>1. Снимка - 940x370</label>
                        <button class="alt_btn" style="float: right; margin-right: 10px;" onclick="SelectFile('#FileUpload1'); return false;">Промени</button>
                        <asp:FileUpload runat="server" onchange="SubmitButton('#Button1')" ID="FileUpload1" Style="margin-left: 10px; visibility: hidden;" />
                        <asp:Button runat="server" ID="Button1" Style="visibility: hidden;" OnClick="Button1_Click" />
                        <img id="Image1" runat="server" src="images/940x370.jpg" style="margin-left: 8px; width: 95%;" />
                    </fieldset>

                    <fieldset style="width: 48%; float: left; margin-left: 10px;">
                        <label>2. Снимка - 940x370</label>
                        <button class="alt_btn" style="float: right; margin-right: 10px;" onclick="SelectFile('#FileUpload2'); return false;">Промени</button>
                        <asp:FileUpload runat="server" onchange="SubmitButton('#Button2')" ID="FileUpload2" Style="margin-left: 10px; visibility: hidden;" />
                        <asp:Button runat="server" ID="Button2" Style="visibility: hidden;" OnClick="Button2_Click" />
                        <img id="Image2" runat="server" src="images/940x370.jpg" style="margin-left: 8px; width: 95%;" />
                    </fieldset>
                </div>

                <div>
                    <fieldset style="width: 48%; float: left;">
                        <label>3. Снимка - 940x370</label>
                        <button class="alt_btn" style="float: right; margin-right: 10px;" onclick="SelectFile('#FileUpload3'); return false;">Промени</button>
                        <asp:FileUpload runat="server" onchange="SubmitButton('#Button3')" ID="FileUpload3" Style="margin-left: 10px; visibility: hidden;" />
                        <asp:Button runat="server" ID="Button3" Style="visibility: hidden;" OnClick="Button3_Click" />
                        <img id="Image3" runat="server" src="images/940x370.jpg" style="margin-left: 8px; width: 95%;" />
                    </fieldset>

                    <fieldset style="width: 48%; float: left; margin-left: 10px;">
                        <label>4. Снимка - 940x370</label>
                        <button class="alt_btn" style="float: right; margin-right: 10px;" onclick="SelectFile('#FileUpload4'); return false;">Промени</button>
                        <asp:FileUpload runat="server" onchange="SubmitButton('#Button4')" ID="FileUpload4" Style="margin-left: 10px; visibility: hidden;" />
                        <asp:Button runat="server" ID="Button4" Style="visibility: hidden;" OnClick="Button4_Click" />
                        <img id="Image4" runat="server" src="images/940x370.jpg" style="margin-left: 8px; width: 95%;" />
                    </fieldset>
                </div>
                <div class="clear"></div>
            </div>
        </article>
    </section>
</asp:Content>
