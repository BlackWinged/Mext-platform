<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
    </div>

        <script language="javascript" type="text/javascript">

        function resizeIframe(obj) {

            obj.style.height = obj.contentWindow.document.body.scrollHeight+250 + 'px';

        }

    </script>

</asp:Content>
