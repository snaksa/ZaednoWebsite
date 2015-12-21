<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddAndRemoveFile.aspx.cs"
    Inherits="AdminPanel.AddFileToArchive" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Администраторски панел - Добавяне на файл към архив</title>
</asp:Content>

<asp:Content ID="Name" ContentPlaceHolderID="page_name" runat="server">
    <h2 class="section_title">Добавяне на файл към архив</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <section id="main" class="column">

        <h4 class="alert_warning" runat="server" visible="false" id="AlertBox">A Warning Alert</h4>

        <article class="module width_full">
            <header>
                <h3>Добавяне на файл към архив</h3>
            </header>

            <div class="module_content">

                <fieldset>
                    <label>Заглавие</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxTitle" />
                </fieldset>

                <fieldset>
                    <label>Описание</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="TextBoxDescription" />
                </fieldset>

                <fieldset style="width: 48%; margin-right: 3%; float: left;">
                    <label>Линк към файл</label>
                    <asp:TextBox runat="server" CssClass="smallTextBox" ID="FilePathTextBox" />
                </fieldset>

                <fieldset style="width: 48%; float: left;">
                    <label>Снимка - 285x200</label>
                    <asp:FileUpload runat="server" ID="FileImageFileUpload" Style="margin-left: 10px;" />
                </fieldset>

                <div class="clear"></div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="ButtonAddFile" Text="Добави" CssClass="alt_btn" OnClick="ButtonAddFile_Click" />
                </div>
            </footer>
        </article>
        <!-- end of post new article -->

        <article class="module width_full" id="newsArticle">
            <header>
                <h3 class="tabs_involved" id="sectionName">Изтриване на файл</h3>
            </header>
            <div class="tab_container" style="max-height: 400px; overflow-y: scroll;">
                <div id="tab1" class="tab_content">
                    <table class="tablesorter" cellspacing="0">
                        <thead>
                            <tr>
                                <th style="width: 85%; text-align: center;">Заглавие</th>
                                <th style="width: 15%; text-align: center;">Изтрии</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="AllFilesRepeater">
                                <ItemTemplate>
                                    <tr style="background-color: #F1F1F1;">
                                        <td><%#Eval("Name") %></td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton runat="server" ID="EditSingleFileWithID" ImageUrl="images/icn_edit.png" CssClass="imageActions" CommandName='<%#Eval("ID") %>' OnCommand="EditSingleFileWithID_Command" />
                                            <img id='<%#Eval("ID") %>' src="images/icn_trash.png" class="imageActions" onclick="PaintBackgroundRed(this.id, 'filesToBeDeleted')" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
            <footer>
                <div class="submit_link">
                    <asp:Button runat="server" ID="ButtonDeleteFiles" CssClass="alt_btn" OnClick="ButtonDeleteFiles_Click" Text="Изтрий маркираните файлове" />
                </div>
            </footer>
        </article>
        <asp:HiddenField runat="server" ID="filesToBeDeleted" />
        <!-- end of content manager article -->
    </section>

</asp:Content>

