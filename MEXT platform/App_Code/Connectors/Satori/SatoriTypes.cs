using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Dapper;

public class baseResponse
{

}
[Table("Satori_Review")]
public class SatoriReview
{
    public long id { get; set; }
    [JsonIgnore]
    public long? userId { get; set; }
    public string cardId { get; set; }
    [Editable(false)]
    public string entryId { get; set; }
    [Editable(false)]
    public string senseId { get; set; }
    [Editable(false)]
    public string cardType { get; set; }
    [Editable(false)]
    public string whenCreated { get; set; }
    [Editable(false)]
    public string expression { get; set; }
    [Editable(false)]
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
    [Editable(false)]
    public string definition { get; set; }
    [Editable(false)]
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
    [Editable(false)]
    public int? totalCorrect { get; set; }
    [Editable(false)]
    public int? totalIncorrect { get; set; }
    [Editable(false)]
    public int? consecutiveCorrect { get; set; }
    [Editable(false)]
    public float ef { get; set; }
    [Editable(false)]
    public float q { get; set; }
    [Editable(false)]
    public string whenUpdated { get; set; }
    [Editable(false)]
    public string nextDueDate { get; set; }
    [Editable(false)]
    public List<SatoriReview> contexts { get; set; }
    public string alternateDefinitions;
    public string mnemonics;
}

public class Satori_word
{
    public string kanji;
    public string hiragana;
}