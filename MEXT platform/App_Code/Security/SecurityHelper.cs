
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Web;

public class SecurityHelper
{
    private static UserFactory dbHelper = new UserFactory();
    public static void authorizeSessionOrCookies()
    {
        Users currentUser = null;
        if (HttpContext.Current.Session[CollectionKeys.CurrentUser] == null)
        {
            try
            {
            if (((((HttpContext.Current.Request.Cookies["UserName"]) != null)) && (((HttpContext.Current.Request.Cookies["Password"]) != null))))
            {
                currentUser = dbHelper.authenticateUser(HttpContext.Current.Request.Cookies["UserName"].Value, HttpContext.Current.Request.Cookies["Password"].Value);
                if (currentUser != null)
                {
                    HttpContext.Current.Session.Add(CollectionKeys.CurrentUser, currentUser);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/login.aspx?"+CollectionKeys.signInParameters+"="+HttpContext.Current.Request.RawUrl);
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/login.aspx?" + CollectionKeys.signInParameters + "=" + HttpContext.Current.Request.RawUrl);
            }
            } catch (InvalidOperationException ex)
            {
            }
        }
    }

  
}


