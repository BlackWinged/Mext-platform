using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Runtime;
using System.Configuration;
using System.Security.Cryptography;

public class SatoriDatabaseHelper
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connString"]);


}
