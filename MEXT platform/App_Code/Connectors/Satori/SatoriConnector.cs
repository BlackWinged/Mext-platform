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

    private static String fireRequest(string @params)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@params);
            CookieContainer newContainer = new CookieContainer();
            if (HttpContext.Current.Session["satori_cookies"] != null)
            {
                newContainer = (CookieContainer)HttpContext.Current.Session["satori_cookies"];
            }
            request.CookieContainer = newContainer;
            NetworkCredential cred = new NetworkCredential("lovro.gamulin@gmail.com", "5h4d0wnet");
            request.Credentials = cred;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            HttpContext.Current.Session["satori_cookies"] = request.CookieContainer;
            string resultString = "";
            using (StreamReader dataStream = new StreamReader(response.GetResponseStream()))
            {
                resultString = dataStream.ReadToEnd();
            }
            return resultString;
            //JObject resultJson = JObject.Parse(resultString);
            //return resultJson;
        }
        catch (Exception ex)
        {
            dynamic test = ex;
            return null;
        }
    }

    //content.ToString(Formatting.None)
    private static String fireRequestWithMethod(string @params, String content, string method = "POST", string contentType = "application/json")
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@params);
            request.CookieContainer = (CookieContainer)HttpContext.Current.Session["satori_cookies"];
            // NetworkCredential cred = new NetworkCredential("lovro.gamulin@gmail.com", "5h4d0wnetM");
            //request.Credentials = cred;
            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";
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

    public static void signIn()
    {
        string parameters = "https://www.satorireader.com/signin";
        string content = "username=" + HttpUtility.UrlEncode("lovro.gamulin@gmail.com") + "&";
        content += "password=" + HttpUtility.UrlEncode("5h4d0wnetM");
        fireRequest(parameters);
        fireRequestWithMethod(parameters, content);
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

    //exampul

    //public static List<string> insertPartner(object partnerObject)
    //    {
    //        dynamic @params = "/Partneri/snimi";
    //        List<object> arrayWrapper = new List<object>();
    //        arrayWrapper.Add(partnerObject);
    //        JToken content = JToken.Parse(JsonConvert.SerializeObject(arrayWrapper));
    //        JObject contentArray = new JObject();
    //        contentArray.Add("partner", content);
    //        JObject resultJson = fireRequestWithMethod(@params, contentArray, "POST");
    //        List<JToken> rezultati = resultJson["result"].Children().ToList();
    //        List<string> attributeSet = new List<string>();
    //        List<string> resultSet = new List<string>();
    //        foreach (JToken item in rezultati)
    //        {
    //            resultSet.Add(item.ToString());
    //        }
    //        return resultSet;
    //    }


}
