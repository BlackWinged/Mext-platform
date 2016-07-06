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
using System.Text;
using Dapper;
using System.Runtime.Serialization.Formatters.Binary;

public class SatoriDatabaseHelper
{
    static SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connString"]);

    public static void saveUser(int userId, CookieContainer cookie)
    {
        Satori_user user = authenticateUser(userId);
        if (user != null)
        {
            user.cookie = serializeCookie(cookie);
            conn.Update(user);
        }
        else
        {
            user = new Satori_user();
            user.cookie = serializeCookie(cookie);
            user.userId = userId;
            conn.Insert(user);
        }
        ///return conn.Insert(user);
    }

    public static Satori_user authenticateUser(int userId)
    {
        return conn.GetList<Satori_user>("where userId = " + userId).SingleOrDefault<Satori_user>();
    }

    public static void authorizeSession()
    {
        Users user = (Users)HttpContext.Current.Session[CollectionKeys.CurrentUser];
        Satori_user satUser = conn.GetList<Satori_user>("where userId = " + user.id).SingleOrDefault<Satori_user>();
        HttpContext.Current.Session[CollectionKeys.satoriCookies] = deserializeCooke(satUser.cookie);
    }

    public static SatoriReview checkIfCardExists(SatoriReview card, Users user)
    {
        return conn.GetList<SatoriReview>("where userId=" + user.id  + " and entryId = '" + card.entryId + "'").SingleOrDefault<SatoriReview>();
    }

    public static void saveCardData(SatoriReview card, Users user)
    {
        SatoriReview newCard = checkIfCardExists(card, user);
        if (newCard != null)
        {
            card.id = newCard.id;
            card.userId = user.id;
            conn.Update(card);
        } else
        {
            card.userId = user.id;
            conn.Insert(card);
        }

    }

    public static byte[] serializeCookie(CookieContainer cookieJar)
    {
        byte[] result = { };
        using (Stream stream = new MemoryStream())
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, cookieJar);
                stream.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(stream);
                result = ReadFully(stream);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Problem reading cookies: " + e.GetType());
            }

        }
        return result;
    }

    public static CookieContainer deserializeCooke(byte[] cookie)
    {

        try
        {
            using (Stream stream = new MemoryStream())
            {
                stream.Write(cookie, 0, cookie.Length);
                stream.Seek(0, SeekOrigin.Begin);
                BinaryFormatter formatter = new BinaryFormatter();
                return (CookieContainer)formatter.Deserialize(stream);
            }
        }
        catch (Exception e)
        {
            Console.Out.WriteLine("Problem reading cookies from disk: " + e.GetType());
            return new CookieContainer();
        }
    }

    public static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    //public static byte[] WriteFully(byte[] input)
    //{
    //    byte[] buffer = new byte[16 * 1024];
    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        int read;
    //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
    //        {
    //            ms.Write(buffer, 0, read);
    //        }
    //        return ms.ToArray();
    //    }
    //}

}
