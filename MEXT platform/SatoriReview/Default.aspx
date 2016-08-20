<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/SatoriReview/Site.Master" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        // SatoriReaderConnector.signIn("lovro.gamulin@gmail.com", "5h4d0wnetM");
    }
</script>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="main" ng-controller="MainController as main">
        <div>
            <login-container ng-show="!main.isSignedIn"></login-container>
        </div>
        <div>
            <review-container ng-show="main.isSignedIn"></review-container>
        </div>
    </div>
</asp:Content>
