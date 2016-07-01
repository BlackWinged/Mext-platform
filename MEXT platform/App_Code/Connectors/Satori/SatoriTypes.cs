using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;

public class baseResponse
{

}

public class SatoriReview
{
    public long id { get; set; }
    [JsonIgnore]
    public long? userId { get; set; }
    public string cardId { get; set; }
    public string entryId { get; set; }
    public string senseId { get; set; }
    public string cardType { get; set; }
    public string whenCreated { get; set; }
    public string expression { get; set; }
    public List<Satori_word> expression_text
    {
        get
        {
            List<Satori_word> result = new List<Satori_word>();
            foreach (Match match in Regex.Matches(expression, "text\\\":\\\"(.*?)\\\"|reading\\\":\\\"(.*?)\\\"", RegexOptions.IgnoreCase))
            {
                Satori_word newWord = new Satori_word();
                if (!match.Groups[1].Value.Equals(String.Empty))
                {
                    newWord.kanji = match.Groups[1].ToString();
                    result.Add(newWord);
                }
                if (match.Groups[2] != null)
                {
                    result.Last().hiragana = match.Groups[2].ToString();
                }
            }
            return result;
        }
    }
    public string definition { get; set; }
    public string definition_text
    {
        get
        {
            String result = "";
            if (definition == null)
            {
                foreach (Match match in Regex.Matches(expression, "glosses\\\":\\\"(.*?)\\\"", RegexOptions.IgnoreCase))
                {
                    result += " " + match.Groups[1];
                }

            } else {
                foreach (Match match in Regex.Matches(definition, "glosses\\\":\\\"(.*?)\\\"", RegexOptions.IgnoreCase))
                {
                    result += " " + match.Groups[1];
                }

            }
            return result;
        }
    }
    public int? totalCorrect { get; set; }
    public int? totalIncorrect { get; set; }
    public int? consecutiveCorrect { get; set; }
    public float ef { get; set; }
    public float q { get; set; }
    public string whenUpdated { get; set; }
    public string nextDueDate { get; set; }
    public List<SatoriReview> contexts { get; set; }
}

public class Satori_word
{
    public string kanji;
    public string hiragana;
}