<%@ Page Title="Home Page" Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.Equals("POST"))
        {
            createUser();
        }
    }

    private void createUser()
    {
        UserFactory uf = new UserFactory();
        uf.createUser(Request.Form["username"], null, Request.Form["username"], Request.Form["password"], Request.Form["email"], 2, 1);

        Response.Redirect("Default.aspx");
    }
</script>

<!DOCTYPE html>
<html>
<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Project Athena</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
</head>

<body class="gray-bg">

    <div class="middle-box text-center loginscreen   animated fadeInDown">
        <div>
            <div>

                <h1 class="logo-name">PA</h1>

            </div>
            <h3>Register to Project Athena</h3>
            <p>We have cookies</p>
            <form class="m-t" role="form" method="post">
                <div class="form-group">
                    <input type="text" class="form-control" name="username" placeholder="Name" required="">
                </div>
                <div class="form-group">
                    <input type="email" class="form-control" name="email" placeholder="Email" required="">
                </div>
                <div class="form-group">
                    <input type="password" class="form-control" name="password" placeholder="Password" required="">
                </div>

                <button type="submit" class="btn btn-primary block full-width m-b">Register</button>

                <p class="text-muted text-center"><small>Already been here?</small></p>
                <a class="btn btn-sm btn-white btn-block" href="login.aspx">Login</a>
            </form>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

</body>


</html>
