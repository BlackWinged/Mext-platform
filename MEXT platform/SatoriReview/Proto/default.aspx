<%@ Page Language="C#" Debug="true" %>

<%@ Import Namespace="Dapper" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="LanguageDictionary" %>


<script runat="server">
</script>


<!DOCTYPE html>
<html ng-app="Dictionary" lang="en">
<head>
    <script language="javascript" type="text/javascript">
        function resizeIframe(obj) {
            obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';
        }
    </script>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">


    <title>HGshop</title>

    <!-- Bootstrap core CSS -->
    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet">



    <!-- Custom styles for this template -->
    <link href="css/flash.css" rel="stylesheet" />
    <link href="css/sticky-footer-navbar.css" rel="stylesheet">
    <link rel="stylesheet" href="css/easyTree.css">
    <link rel="stylesheet" href="css/select2.css">
    <link href="https://raw.githubusercontent.com/t0m/select2-bootstrap-css/bootstrap3/select2-bootstrap.css" rel="stylesheet" />
    <link href="css/select2-bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/jquery.fileupload.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/css/bootstrap-select.min.css">


    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>

<body>

    <!-- Fixed navbar -->
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/admin/default.aspx">HGshop admin</a>
   
            </div>
            <ul class="nav navbar-nav navbar-right">
                <li class="active"><a href="/admin/login.aspx">Odjavi se </a></li>
            </ul>

            <!--/.nav-collapse -->
        </div>
    </nav>

    <!-- Begin page content -->
    <div class="container">
        <h2><%Response.Write(LangDict.current().getString("hr-hr", "testKey2")); %></h2>
   
            
    </div>
    <footer class="footer">
        <div class="container">
            <p class="text-muted">HGShop</p>
        </div>
    </footer>


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="src/pace.min.js"></script>
    <script src="src/angular.min.js"></script>
    <script src="src/dictLogic/app.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/js/bootstrap-select.min.js"></script>
    
    <!-- The Load Image plugin is included for the preview images and image resizing functionality -->

    <script lang="JavaScript">
        //var iframes = $('iframe');
        //iframes.each(function () {
        //    var src = $(this).attr('src');
        //    $(this).data('src', src).attr('src', '');
        //});


    </script>
    <script src="bootstrap-3.3.6-dist/js/bootstrap.min.js"></script>

</body>
</html>
