<%@ Page Language="vb" Debug="true" %>

<%@ Import Namespace="System.Configuration.ConfigurationManager" %>
<%@ Import Namespace="Dapper" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Newtonsoft.json" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.collections.generic" %>
<%@ Import Namespace="StackExchange.Profiling" %>
<%@ Import Namespace="LanguageDictionary" %>
<script runat="server">

    Private Shared Function setupCards(cards As List(Of SatoriReview))
        Dim currentUser As Users = HttpContext.Current.Session(CollectionKeys.CurrentUser)
        For Each card As SatoriReview In cards
            Dim dbCard As SatoriReview = SatoriDatabaseHelper.checkIfCardExists(card, currentUser)
            If (dbCard IsNot Nothing) Then
                card.mnemonics = dbCard.mnemonics
                card.alternateDefinitions = dbCard.alternateDefinitions

                Dim altCards As List(Of SatoriReview) = SatoriDatabaseHelper.getSynonims(card, currentUser)
                If altCards.Count > 0 Then
                    card.possibleSynonims = New List(Of String)
                    For Each altCard As SatoriReview In altCards
                        card.possibleSynonims.Add(altCard.expression_text.First().kanji + " -- " + altCard.definition_text)
                    Next
                End If
            End If
        Next

        Return JsonConvert.SerializeObject(cards)
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function getCards(data As Object) As String
        Dim cards As List(Of SatoriReview) = SatoriReaderConnector.getDueCards
        If cards.Count = 0 Then
            cards = SatoriReaderConnector.getAllCards
        End If
        Return setupCards(cards)
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function getAllCards(data As Object) As String
        Dim cards As List(Of SatoriReview) = SatoriReaderConnector.getAllCards
        Return setupCards(cards)
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function setCards(data As String) As String
        Dim result As String = SatoriReaderConnector.sendCardStatus(data)
        Return result
    End Function

    <System.Web.Services.WebMethod>
    Public Shared Function saveCardData(card As SatoriReview) As String
        Dim currentUser As Users = HttpContext.Current.Session(CollectionKeys.CurrentUser)
        SatoriDatabaseHelper.saveCardData(card, currentUser)
    End Function
</script>
