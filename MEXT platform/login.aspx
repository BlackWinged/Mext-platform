<%@ Page Title="Home Page" Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.Equals("POST"))
        {
            checkUser();
        }
    }

    private void checkUser()
    {
        UserFactory uf = new UserFactory();
        Users loggedUser = uf.authenticateUser(Request.Form["username"], Request.Form["password"]);
        if (loggedUser != null)
        {
            Session[CollectionKeys.CurrentUser] = loggedUser;
            setCookie();
            if (Request.QueryString[CollectionKeys.signInParameters] != null)
            {
                Response.Redirect((Request.QueryString[CollectionKeys.signInParameters]));
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void setCookie()
    {
        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
        Response.Cookies["UserName"].Value = Request.Form["username"].Trim();
        Response.Cookies["Password"].Value = Request.Form["password"].Trim();
    }

</script>

<!DOCTYPE html>
<html>


<!-- Mirrored from webapplayers.com/inspinia_admin-v2.5/login.html by HTTrack Website Copier/3.x [XR&CO'2014], Wed, 20 Apr 2016 20:32:41 GMT -->
<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>INSPINIA | Login</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
</head>

<body class="gray-bg">

    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>

                <h1 class="logo-name">IN+</h1>

            </div>
            <h3>Welcome to IN+</h3>
            <p>
                Perfectly designed and precisely prepared admin theme with over 50 pages with extra new web app views.
                <!--Continually expanded and constantly improved Inspinia Admin Them (IN+)-->
            </p>
            <p>Login in. To see it in action.</p>
            <form class="m-t" role="form" method="post">
                <div class="form-group">
                    <input type="text" name="username" class="form-control" placeholder="Username" required="">
                </div>
                <div class="form-group">
                    <input type="password" name="password" class="form-control" placeholder="Password" required="">
                </div>
                <button type="submit" class="btn btn-primary block full-width m-b">Login</button>

                <a href="#"><small>Forgot password?</small></a>
                <p class="text-muted text-center"><small>Do not have an account?</small></p>
                <a class="btn btn-sm btn-white btn-block" href="register.html">Create an account</a>
            </form>
            <p class="m-t"><small>Inspinia we app framework base on Bootstrap 3 &copy; 2014</small> </p>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>

</body>


<!-- Mirrored from webapplayers.com/inspinia_admin-v2.5/login.html by HTTrack Website Copier/3.x [XR&CO'2014], Wed, 20 Apr 2016 20:32:41 GMT -->
</html>