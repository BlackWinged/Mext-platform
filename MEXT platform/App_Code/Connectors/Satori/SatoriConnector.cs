using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class SatoriReaderConnector
{

    private static String fireRequest(string @params, string username = "", string password = "")
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@params);
        CookieContainer newContainer = new CookieContainer();
        SatoriDatabaseHelper.authorizeSession();
        if (HttpContext.Current.Session["satori_cookies"] != null)
        {
            newContainer = (CookieContainer)HttpContext.Current.Session["satori_cookies"];
        }
        request.CookieContainer = newContainer;
        if (!username.Equals(""))
        {
            NetworkCredential cred = new NetworkCredential(username, password);
            request.Credentials = cred;
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        HttpContext.Current.Session["satori_cookies"] = request.CookieContainer;
        string resultString = "";
        using (StreamReader dataStream = new StreamReader(response.GetResponseStream()))
        {
            resultString = dataStream.ReadToEnd();
        }
        return resultString;

    }

    //content.ToString(Formatting.None)
    private static String fireRequestWithMethod(string @params, String content, string method = "POST", string contentType = "application/json")
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@params);
            if ((CookieContainer)HttpContext.Current.Session["satori_cookies"] == null)
            {
                SatoriDatabaseHelper.authorizeSession();
            }
            request.CookieContainer = (CookieContainer)HttpContext.Current.Session["satori_cookies"];
            // NetworkCredential cred = new NetworkCredential("lovro.gamulin@gmail.com", "5h4d0wnetM");
            //request.Credentials = cred;
            request.Method = method;
            request.ContentType = "application/" + contentType;
            StreamWriter requestStream = new StreamWriter(request.GetRequestStream());
            requestStream.Write(content);
            requestStream.Flush();
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string resultString = "";
            using (StreamReader dataStream = new StreamReader(response.GetResponseStream()))
            {
                resultString = dataStream.ReadToEnd();
            }
            return resultString;
        }
        catch (WebException ex)
        {
            throw new Exception(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
        }
    }

    public static string signIn(string username, string password)
    {
        string parameters = "https://www.satorireader.com/signin";
        string content = "username=" + HttpUtility.UrlEncode(username) + "&";
        content += "password=" + HttpUtility.UrlEncode(password);
        fireRequest(parameters, username, password);
        string resultJson = fireRequestWithMethod(parameters, content, "POST", "x-www-form-urlencoded");
        Users user = (Users)HttpContext.Current.Session[CollectionKeys.CurrentUser];
        CookieContainer cookieJar = (CookieContainer)HttpContext.Current.Session[CollectionKeys.satoriCookies];
        SatoriDatabaseHelper.saveUser(user.id, cookieJar);
        return resultJson;
    }

    public static string checkIfCookieIsValid()
    {
        string parameters = "https://www.satorireader.com/api/studylist/due";
        JObject resultJson = JObject.Parse(fireRequest(parameters));
        return resultJson.ToString(Formatting.None);
    }

    public static List<SatoriReview> getDueCards()
    {
        string parameters = "https://www.satorireader.com/api/studylist/due";
        JObject resultJson = JObject.Parse(fireRequest(parameters));
        List<JToken> rezultati = resultJson["result"].Children().ToList();
        List<SatoriReview> cards = new List<SatoriReview>();
        foreach (JToken item in rezultati)
        {
            cards.Add(JsonConvert.DeserializeObject<SatoriReview>(item.ToString()));
        }
        return cards;
    }

    public static List<SatoriReview> getAllCards()
    {
        string parameters = "https://www.satorireader.com/api/studylist/all";
        JObject resultJson = JObject.Parse(fireRequest(parameters));
        List<JToken> rezultati = resultJson["result"].Children().ToList();
        List<SatoriReview> cards = new List<SatoriReview>();
        foreach (JToken item in rezultati)
        {
            cards.Add(JsonConvert.DeserializeObject<SatoriReview>(item.ToString()));
        }
        return cards;
    }

    public static string sendCardStatus(string cardId)
    {
        string parameters = "https://www.satorireader.com/api/studylist/" + cardId;
        string result = fireRequestWithMethod(parameters, "", "PUT", "x-www-form-urlencoded");
        return result;
    }



}
