<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="FullSchoolWebsite.AboutUs" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <title>За нас - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-title">
        <h1>За нас</h1>
    </div>

    <!-- side content -->
    <div id="side-content" style="text-align:justify;">

        <h4 style="text-align: center;">КОИ СМЕ НИЕ?</h4>
        <p>
            Ние сме няколко момичета и момчета, които имат желание и се опитват да правят журналистика.
            Иска ни  се да мислим, че ние, това сте вие самите. 
            Че изразяваме вас, че мислим като вас, че чрез нас намирате възможност да изкажете  гражданската си позиция. 
            Искаме да  вярваме, че като вас сме будни, активни, непримирими, отзивчиви…
            Надяваме се, че успяваме във всичко това, а ако не – обещаваме да  се стараем  повече, за да го постигнем.
        </p>

        <p>
            Но няма да се справим бе вас! Подкрепяйте ни, поправяйте ни, мотивирайте ни и честичко ни похвалвайте. 
            Защото и ние като вас очакваме нашите  усилия да се забелязват. Вярвайте в нас, защото ние искаме да се учим и можем да кажем, 
            без да скромничим излишно, че се развиваме и  порастваме.

        </p>

        <p>
            Появихме се преди 4 години. Създадохме свой вестник – „Заедно“. 
            Нарекохме го така, защото вярваме, че  двама и трима е по-добре от един,  че светът се 
            променя с общите усилия на всички, че заедно е по-весело отколкото  сам. Започнахме като 
            истинските журналисти с бележник и химикал в ръка да  отразяваме всички впечатлили ни 
            събития от училищния живот. Пишехме  дописки, коментари, вземахме  интервюта . А после на 
            училищните компютри подреждахме материалите и оформяхме рубриките и страниците на вестника. 
            Така  навъртяхме вече 8 броя.
        </p>

        <p>
            Престрашихме се  да предложим свои материали на регионалните вестници.  Те ни харесаха и започнахме активно сътрудничество с 
            „Екип7“ и „Гледища“. Но, както е казал народът: “С яденето идва апетитът“  и ние опитахме да направим  и училищна телевизия. 
            Излъчихме  репортажи, които популяризираха различни събития в нашето училище.

        </p>

        <p>
            Признание за нашите усилия и придобитите умения е спечелването на първото място в Седмия международния конкурс
            „Тудор Мушатеску“, организиран от Националния комитет на образованието в Румъния, от Училищния инспекторат в гр. Арджеш   
            и  от Двореца на децата в гр. Питешти  в Румъния. Това ни дава самочувствие да продължим да работим и да се развиваме в тази област.  
            Този сайт е следващата  стъпка в развитието ни. Искаме да сме по-достъпни за вас, да сме по-бързи в отразяването на новините и да имаме 
            гореща връзка със своите читатели. Каним всички, които имат мнение или желание да пишат, да се включват в нашия сайт и да покажем ЗАЕДНО, 
            че не сме едно изгубено поколение, а сме млади хора , които не са равнодушни в какъв свят живеят.
        </p>




        <h2 class="title-divider">Свържете се с нас</h2>
        <!-- form -->
        <form runat="server" id="contactForm">
            <asp:ScriptManager ID="MainScriptManager" runat="server" />
            <asp:UpdatePanel runat="server" ID="UpdatePanel">
                <ContentTemplate>
                    <fieldset>
                        <div>
                            <input runat="server" name="name" id="name" type="text" class="form-poshytip" title="Въведете своето име">
                            <label>Име</label>
                        </div>
                        <div>
                            <input runat="server" name="email" id="email" type="text" class="form-poshytip" title="Въведете своя Email">
                            <label>Email</label>
                        </div>
                        <div>
                            <textarea runat="server" name="comments" id="comments" rows="5" cols="20" class="form-poshytip" title="Въведете своето съобщение"></textarea>
                        </div>
                        <p>
                            <input runat="server" name="submit" type="submit" id="submit" value="Изпрати" onserverclick="submit_ServerClick" />
                        </p>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
        <p id="success" class="success" style="display: none;">Съобщението е изпратено успешно!</p>
        <!-- ENDS form -->



    </div>
    <!-- ENDS side content -->






    <!-- sidebar -->
    <div id="sidebar">
        <h4>Местоположение на картата</h4>
        <!-- Google map -->

        <!-- GOOGLE MAPS -->
        <script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAAyXu_1Zw3-DbyonSxgLICyxSWQUvSd76__Y3fi9Kog3e7ZrY_3BSXzMhasJq2gZLNOWT1yWR8ut-FDA"></script>
        <script type="text/javascript">google.load("maps", "2.x");</script>

        <script type="text/javascript">

            jQuery(document).ready(function ($) {

                //##########################################
                // Google maps
                //##########################################

                // You can get the latitud and Longitude values at http://itouchmap.com/latlong.html

                var latitude = 43.797723;
                var longitude = 26.501641;

                // center map
                var map = new GMap2(document.getElementById("map"));
                var point = new GLatLng(latitude, longitude);
                map.setCenter(point, 16);

                // Set marker
                marker = new GMarker(point);
                map.addOverlay(marker);

                // controls
                map.addControl(new GLargeMapControl());

            });
						</script>
        <!-- ENDS GOOGLE MAPS -->


        <div id="map"></div>



        <%--<h4>Address</h4>
        <p>
            1234 Hammond Street<br>
            Bangor Maine USA<br>
            Zip Code 445567<br>
            Tel. (33) 4347 26235, (33) 4347 26211<br>
            <a href="http://luiszuno.com/">www.luiszuno.com</a><br>
        </p>--%>


    </div>
    <!-- ENDS sidebar -->

    <div class="clear"></div>
</asp:Content>
