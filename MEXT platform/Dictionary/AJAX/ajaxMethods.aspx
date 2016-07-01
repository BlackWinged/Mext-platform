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
    Public Shared Function getDictionary(data As Object) As String
        Return LangDict.current.parseToJson()
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function getDictionaryHeader(data As Object) As String
        Return LangDict.current.getJsonHeader()
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function validateChange(value As Object) As String
        Return value
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function setDictionaryData(data As List(Of PrintRow)) As String
        Return LangDict.current.parseToJson()
    End Function
</script>