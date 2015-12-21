<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="FullSchoolWebsite.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Грешка - Клуб по журналистика - СОУ "Христо Ботев" гр. Кубрат</title>
    <style>
        a, a:visited {
            color: gray;
            font-style: italic;
            text-decoration: none;
        }

            a:hover {
                text-decoration: underline;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="img/error404.png" style="width:100%;position:absolute;" />
            <span runat="server" id="MessageTextSpan" style="position:absolute; width:450px;margin-top:330px;margin-left:20%;">Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text </span>
        </div>
    </form>
</body>
</html>
