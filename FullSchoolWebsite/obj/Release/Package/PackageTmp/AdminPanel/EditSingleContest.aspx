<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" 
    CodeBehind="EditSingleContest.aspx.cs" Inherits="AdminPanel.EditSingleContest" ClientIDMode="Static" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Редактиране на конкурс</title>

    <script type="text/javascript">
        function uploadImage(id) {
            $(id).click();
        }
        function selectFile(id) {
            $(id).click();
        }
    </script>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Редактиране на конкурс</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Редактиране на конкурс</h3>
            </header>

            <div class="module_content">

                <fieldset>
                    <label>Заглавие</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxTitle" />
                </fieldset>

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>Снимка - 285x200</label>
                    <asp:FileUpload runat="server" ID="smallContestImageFileUpload" style="margin-left:10px;" />
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Снимка - 590x260</label>
                    <asp:FileUpload runat="server" ID="largeContestImageFIleUpload" style="margin-left:10px;" />
                </fieldset>

                <fieldset>
                    <CKEditor:CKEditorControl BasePath="/AdminPanel/ckeditor/" runat="server" ID="CKEditor"></CKEditor:CKEditorControl>
                </fieldset>

                <fieldset style="width: 48%; float: left; margin-right: 3%;">
                    <label>Статус</label><br />
                    <asp:DropDownList runat="server" CssClass="dropDownBox" ID="status">
                        <asp:ListItem Text="Видим" Value="1" />
                        <asp:ListItem Text="Невидим" Value="0" />
                    </asp:DropDownList>
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>E-mail за въпроси</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="emailForQuestions" />
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="EditThisContest" Text="Редактирай" CssClass="alt_btn" OnClick="EditThisContest_Click" />
                </div>
            </footer>
        </article>
        <!-- end of post new article -->
    </section>

</asp:Content>