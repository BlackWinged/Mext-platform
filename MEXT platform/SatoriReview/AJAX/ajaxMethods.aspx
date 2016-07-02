<%@ Page Language="vb" debug="true" %>
<%@ Import Namespace="System.Configuration.ConfigurationManager"%>
<%@ Import Namespace="Dapper"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ Import Namespace="Newtonsoft.json"%>
<%@ Import Namespace="System.Linq"%>
<%@ Import Namespace="System.collections.generic"%>
<%@ Import Namespace="StackExchange.Profiling" %>
<%@ Import Namespace="LanguageDictionary" %>
<script runat="server">

    <System.Web.Services.WebMethod>
    Public Shared Function getCards(data As Object) As String
        Dim cards As List(Of SatoriReview) = SatoriReaderConnector.getDueCards
        Dim result As String = JsonConvert.SerializeObject(cards)
        Return result
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function setCards(data As String) As String
        Dim result As String = SatoriReaderConnector.sendCardStatus(data)
        Return result
    End Function

</script>