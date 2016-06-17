using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace LanguageDictionary
{

    [Dapper.Table("Dictionary")]
    public class LangContent
    {
        [Dapper.Key]
        public int id { get; set; }
        public int langId { get; set; }
        public String contentKey { get; set; }
        public String contentString { get; set; }
    }

    [Dapper.Table("Languages")]
    public class Languages
    {
        [Dapper.Key]
        public int id { get; set; }
        public String langCode { get; set; }
        public String langName { get; set; }
    }

    public class PrintRow
    {
        public string rowKey { get; set; }
        public List<String> strings { get; set; }
    }
}

