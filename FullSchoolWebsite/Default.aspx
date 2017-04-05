<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FullSchoolWebsite.Index" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>Начало - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!-- Front slider -->
    <div id="front-slides">
        <div class="slides_container" style="overflow: hidden; position: relative; display: block;">

            <asp:Repeater runat="server" ID="MainImagesRepeater">
                <ItemTemplate>
                    <div id='Div<%#Eval("Position") %>' class="slide">
                        <img src='<%#Eval("ImagePath") %>' width="940" height="360">
                        <div class="caption">Клуб по журналистика при СУ "Христо Ботев" град Кубрат</div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>

        <div id="front-slides-cover"></div>
        <!-- Headline -->
        <div id="headline">
            <h6>Клуб по журналистика при СУ "Христо Ботев" град Кубрат</h6>
        </div>
        <!-- ENDS Headline -->

    </div>
    <!-- ENDS Front slider -->


    <!-- Reel slider -->
    <div id="reel">
        <div class="slides_container" style="overflow: hidden; position: relative; display: block;">

            <div class="slide-box" style="position: absolute; top: 0px; left: 918px; z-index: 5;">
                <asp:Repeater runat="server" ID="FirstStatementsRepeater">
                    <ItemTemplate>
                        <div class="box-container">
                            <img src='<%#Eval("ImagePath") %>' class="box-icon">
                            <h6><%#Eval("Title") %></h6>
                            <div class="box-divider"></div>
                            <%#Eval("Text") %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="slide-box" style="position: absolute; top: 0px; left: 918px; z-index: 0; display: none;">
                <asp:Repeater runat="server" ID="SecondStatementsRepeater">
                    <ItemTemplate>
                        <div class="box-container">
                            <img src='<%#Eval("ImagePath") %>' class="box-icon">
                            <h6><%#Eval("Title") %></h6>
                            <div class="box-divider"></div>
                            <%#Eval("Text") %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>
        <a href="#" class="prev"></a>
        <a href="#" class="next"></a>
    </div>
    <!-- ENDS Reel slider -->



    <!-- Featured -->
    <div style="width: 100%;">
        <div class="featured-title">
            <div class="ribbon"><span>ДОБРЕ ДОШЛИ!</span></div>
        </div>
        <br />

        <div>
            <p style="font-size: 17px;">
                Радваме се , че в огромното виртуално пространство попаднахте на нас и се надяваме, че тук ще 
            намерите интересна за вас информация.Разгледайте нашите секции, качете новина, направете коментар и 
            споделете впечатленията си от сайта - какво ви хареса, върху кое още да поработим, къде се затруднихте...
            Ще бъдем щастливи, ако станете наш редовен посетител!
            </p>

            <p style="font-size: 17px; text-align: center;">
                Приятни и полезни занимания при нас!
            </p>

            <p style="text-align:center;">
                <img src="./img/defaultPageGirl.jpg" width="590" height="260" style="border: 3px solid #3A3A3A;" />
            </p>
        </div>
    </div>
    <div class="clear"></div>
    <!-- ENDS Featured -->
</asp:Content>
