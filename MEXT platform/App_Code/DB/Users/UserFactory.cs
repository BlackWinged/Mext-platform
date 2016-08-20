using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using Dapper;

public class UserFactory
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connString"]);

    public void createSuperUser(string username, string password)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] csid = new byte[256];
        rng.GetBytes(csid);
        byte[] hashedPass = null;
        using (SHA512 shaM = new SHA512Managed())
        {
            hashedPass = shaM.ComputeHash(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(csid) + Encoding.UTF8.GetString((Encoding.UTF8.GetBytes(password)))));
        }

        Users user = new Users();
        user.username = username;
        user.email = "lovro.gamulin@gmail.com";
        user.password = hashedPass;
        user.csid = csid;
        conn.Insert(user);
        user.active = 1;
    }

    public int? createUser(string firstname, string lastname, string username, string password, string email, int role, int active)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] csid = new byte[256];
        rng.GetBytes(csid);
        byte[] hashedPass = null;

        using (SHA512 shaM = new SHA512Managed())
        {
            hashedPass = shaM.ComputeHash(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(csid) + Encoding.UTF8.GetString((Encoding.UTF8.GetBytes(password)))));
        }
        if (checkIfUserExists(username))
        {
            return 0;
        }

        Users user = new Users();
        user.firstname = firstname;
        user.lastname = lastname;
        user.username = username;
        user.email = email;
        user.password = hashedPass;
        user.csid = csid;
        user.active = active;
        user.role = role;
        return conn.Insert(user);
    }

    public bool checkIfUserExists(string username)
    {
        return conn.Query<Users>("select * from users where username = @username", new { username = username }).Count<Users>()>0 ? true:false ;
    }

    public List<Users> getNonActivatedUsers()
    {
        return conn.GetList<Users>("where active=0").ToList();
    }

    public Users authenticateUser(string username, string password)
    {
        Users user = conn.Query<Users>("select * from users where username = @username", new { username = username }).SingleOrDefault();
        if (user == null)
        {
            return null;
        }
        byte[] hashedPass = null;
        using (SHA512 shaM = new SHA512Managed())
        {
            hashedPass = shaM.ComputeHash(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(user.csid) + password));
        }
        if (user.password.SequenceEqual(hashedPass) & user.active > 0)
        {
            return user;
        }
        return null;
    }

}

