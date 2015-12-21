<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddWork.aspx.cs" Inherits="FullSchoolWebsite.AdminPanel.AddWork" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Добавяне на творба</title>

    <script>
        function disableElements() {
            if (document.getElementById("enableEmail").checked) {
                document.getElementById("TextBoxEmail").disabled = false;
            }
            else {
                document.getElementById("TextBoxEmail").disabled = true;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Добавяне на творба</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Добавяне на творба</h3>
            </header>

            <div class="module_content">

                <fieldset>
                    <label>Заглавие</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxTitle" />
                </fieldset>

                <fieldset>
                    <ckeditor:ckeditorcontrol runat="server" basepath="/AdminPanel/ckeditor/" id="CKEditor"></ckeditor:ckeditorcontrol>
                </fieldset>

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>Снимка - 590x260</label>
                    <asp:FileUpload runat="server" ID="NewsImageFileUpload" Style="margin-left: 10px;" />
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Статус</label><br />
                    <asp:DropDownList runat="server" CssClass="dropDownBox" ID="status">
                        <asp:ListItem Text="Видима" Value="1" />
                        <asp:ListItem Text="Невидима" Value="0" />
                    </asp:DropDownList>
                </fieldset>

                <fieldset style="width: 48%; float: left; margin-right: 3%;">
                    <label>Автор</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxAuthor" />
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>
                        <input type="checkbox" id="enableEmail" runat="server" onclick="disableElements()" />E-mail на автора</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxEmail" Enabled="false" />
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="AddNewNews" Text="Публикувай" CssClass="alt_btn" OnClick="AddNewNews_Click" />
                </div>
            </footer>
        </article>
        <!-- end of post new article -->
    </section>

</asp:Content>