using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime;
using System.Configuration;
using System.Security.Cryptography;
using Dapper;

    class UserFactory
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connString"]);

        public void createSuperUser(string username, string password)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] csid = new byte[257];
            rng.GetBytes(csid);
            byte[] hashedPass = null;
            using (SHA512 shaM = new SHA512Managed())
            {
                hashedPass = shaM.ComputeHash(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(csid) + Encoding.UTF8.GetString((Encoding.UTF8.GetBytes(password)))));
            }

            Users user = new Users();
            user.username = username;
            user.email = "lovro.gamulin@globaldizajn.hr";
            user.password = hashedPass;
            user.csid = csid;
            conn.Insert(user);
            user.active = 0;
        }

        public List<Users> getNonActivatedUsers()
        {
            return conn.GetList<Users>("where active=0").ToList();
        }

        public int? createUser(string firstname, string lastname, string username, string password, string email, int companyId, int role, int active)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] csid = new byte[257];
            rng.GetBytes(csid);


            byte[] hashedPass = null;
            using (SHA512 shaM = new SHA512Managed())
            {
                hashedPass = shaM.ComputeHash(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(csid) + Encoding.UTF8.GetString((Encoding.UTF8.GetBytes(password)))));
            }

            Users user = new Users();
            user.firstName = firstname;
            user.lastName = lastname;
            user.username = username;
            user.email = email;
            user.password = hashedPass;
            user.csid = csid;
            user.active = active;
            user.role = role;
            return conn.Insert(user);
        }

    }

