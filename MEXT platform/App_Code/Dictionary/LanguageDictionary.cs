using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
/// <summary>
/// Provide methods for connecting and parsing dictionary data.
/// </summary>

namespace LanguageDictionary
{
    public class LangDict
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.AppSettings["connString"]);
        public List<Languages> languages = new List<Languages>();
        public Dictionary<String, Dictionary<String, string>> dict = new Dictionary<string, Dictionary<string, string>>();

        public LangDict()
        {
            flush();
            HttpContext.Current.Application.Contents["LangDict"] = this;
        }

        private void setupDictionary(List<LangContent> content, List<Languages> languages)
        {
            foreach (Languages lang in languages)
            {
                Dictionary<string, string> resultCont = new Dictionary<string, string>();
                foreach (LangContent cont in (from c in content where c.langId == lang.id select c))
                {
                    resultCont.Add(cont.contentKey.ToLower(), cont.contentString);
                }
                dict.Add(lang.langCode, resultCont);
            }
        }

        private void validateDictStructure(List<LangContent> content, List<Languages> languages)
        {
            List<string> keyCollection = (from c in content select c.contentKey).ToList<string>();
            foreach (Languages lang in languages)
            {
                List<string> keyCollectionInlang = (from c in content where c.langId == lang.id select c.contentKey).ToList<string>();

                foreach (string key in keyCollection)
                {
                    if (!keyCollectionInlang.Contains(key))
                    {
                        LangContent newCont = new LangContent();
                        newCont.langId = lang.id;
                        newCont.contentKey = key;
                        conn.Insert(newCont);
                    }
                }
            }
        }

        public void flush()
        {
            List<LangContent> content = new List<LangContent>();
            content = conn.GetList<LangContent>().ToList();
            languages = conn.GetList<Languages>().ToList<Languages>();
            validateDictStructure(content, languages);
            setupDictionary(content, languages);
        }

        public string getString(string langCode, string keyString)
        {
            string result = "";
            string sanitizedLangCode = langCode.ToLower(), sanitizedKeyString = keyString.ToLower();
            try
            {
                result = dict[sanitizedLangCode][sanitizedKeyString];
            }
            catch (Exception e)
            {

            }

            if (!result.Equals(""))
            {
                return result;
            }
            else
            {
                return keyString;
            }
        }

        public static LangDict current()
        {
            return (LangDict)HttpContext.Current.Application.Contents["LangDict"];
        }

        public string parseToJson()
        {
            List<PrintRow> result = new List<PrintRow>();

            foreach (string key in dict[dict.Keys.First()].Keys)
            {
                PrintRow row = new PrintRow();
                row.strings = new List<string>();
                row.rowKey = key;
                foreach (string lang in dict.Keys)
                {
                    row.strings.Add(dict[lang][key]);
                }

                result.Add(row);
            }
            return JsonConvert.SerializeObject(result);
        }

        public string getJsonHeader()
        {
            PrintRow result = new PrintRow();
            result.strings = new List<string>();
            result.rowKey = "Key";
            foreach (Languages lang in languages)
            {
                result.strings.Add(lang.langName);

            }
            
            return JsonConvert.SerializeObject(result);
        }

    }
}