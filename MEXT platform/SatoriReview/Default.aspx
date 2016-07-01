<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/SatoriReview/Site.Master"%>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        SatoriReaderConnector.signIn();
    }
    </script>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <review-container></review-container>
    </asp:Content>