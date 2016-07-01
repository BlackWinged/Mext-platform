using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LanguageDictionary { 
 public class WanikaniConnector
    {

        private static JObject fireRequest(string @params)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://83.139.112.62:8080/datasnap/rest" + @params);
                NetworkCredential cred = new NetworkCredential("webshop", "di2Pr5w");
                request.Credentials = cred;
                WebResponse response = request.GetResponse();
                string resultString = "";
                using (StreamReader dataStream = new StreamReader(response.GetResponseStream()))
                {
                    resultString = dataStream.ReadToEnd();
                }
                JObject resultJson = JObject.Parse(resultString);
                return resultJson;
            }
            catch (Exception ex)
            {
                dynamic test = ex;
                return null;
            }
        }


        private static JObject fireRequestWithMethod(string @params, JObject content, string method = "POST")
        {
            try
            {
                WebRequest request = WebRequest.Create("https://www.wanikani.com/api/user/518b51c8fd7e35fe20187505d72f64cf/" + @params);
                NetworkCredential cred = new NetworkCredential("webshop", "di2Pr5w");
                request.Credentials = cred;
                request.Method = method;
                request.ContentType = "application/json";
                StreamWriter requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(content.ToString(Formatting.None));
                requestStream.Flush();
                requestStream.Close();
                
                WebResponse response = request.GetResponse();
                string resultString = "";
                using (StreamReader dataStream = new StreamReader(response.GetResponseStream()))
                {
                    resultString = dataStream.ReadToEnd();
                }
                JObject resultJson = JObject.Parse(resultString);
                return resultJson;
            }
            catch (WebException ex)
            {
                throw new Exception(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return null;
            }
        }

        //exampul

        public static List<string> insertPartner(object partnerObject)
        {
            dynamic @params = "/Partneri/snimi";
            List<object> arrayWrapper = new List<object>();
            arrayWrapper.Add(partnerObject);
            JToken content = JToken.Parse(JsonConvert.SerializeObject(arrayWrapper));
            JObject contentArray = new JObject();
            contentArray.Add("partner", content);
            JObject resultJson = fireRequestWithMethod(@params, contentArray, "POST");
            List<JToken> rezultati = resultJson["result"].Children().ToList();
            List<string> attributeSet = new List<string>();
            List<string> resultSet = new List<string>();
            foreach (JToken item in rezultati)
            {
                resultSet.Add(item.ToString());
            }
            return resultSet;
        }


    }
}