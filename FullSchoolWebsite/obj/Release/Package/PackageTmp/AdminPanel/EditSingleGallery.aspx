<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="EditSingleGallery.aspx.cs" Inherits="AdminPanel.EditSingleGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на галерия</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на галерия</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Редактиране на галерия</h3>
            </header>

            <div class="module_content">

                    <fieldset style="width: 48%;float:left;margin-right:3%;">
                        <label>Заглавие</label>
                        <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxTitle" />
                    </fieldset>

                    <fieldset style="width: 48%;float:left;">
                        <label>Описание</label>
                        <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxDescription" />
                    </fieldset>

                    <fieldset style="width: 48%;float:left;margin-right:3%;">
                        <label>Статус</label><br />
                        <asp:DropDownList runat="server" CssClass="dropDownBox" ID="status">
                            <asp:ListItem Text="Видима" Value="1" />
                            <asp:ListItem Text="Невидима" Value="0" />
                        </asp:DropDownList>
                    </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Снимка - 285x200</label>
                    <asp:FileUpload runat="server" ID="GalleryImageFileUpload" style="margin-left:10px;" />
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="EditThisGallery" Text="Редактирай" CssClass="alt_btn" OnClick="EditThisGallery_Click" />
                </div>
            </footer>
        </article>
        <!-- end of post new article -->
    </section>
</asp:Content>
