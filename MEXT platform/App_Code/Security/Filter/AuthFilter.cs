
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using static SecurityFilterSection;

public class AuthFilter : IHttpModule
{

    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    public void Init(HttpApplication context)
    {
        //ntext.post
        context.PostAcquireRequestState += this.Application_EndRequest;
        context.EndRequest += this.Application_EndRequest;
        //AddHandler context.BeginRequest, AddressOf Me.Application_BeginRequest
        //AddHandler context.EndRequest, AddressOf Me.Application_EndRequest
        //AddHandler application.EndRequest, AddressOf Me.Application_EndRequest
    }

    private void Application_BeginRequest(object source, EventArgs e)
    {
    }

    private void Application_EndRequest(object source, EventArgs e)
    {
        SecurityFilterSection settings = (SecurityFilterSection)ConfigurationManager.GetSection("securityFilter");

        CheckRestrictedFiles(settings);
        CheckRestrictedApps(settings);

        //Dim thing = HttpContext.Current.Response
    }

    private static void CheckRestrictedApps(SecurityFilterSection settings)
    {
        if (HttpContext.Current.Session != null)
        {
            List<string> items = new List<string>(HttpContext.Current.Request.FilePath.ToString().Split('/'));
            List<string> lowitems = new List<string>();
            foreach (string item in items)
            {
                lowitems.Add( item.ToLower());
            }
            foreach (restrictedAppElement item in settings.restrictedSubApp)
            {
                if (lowitems.Contains(item.appName.ToLower()))
                {
                    SecurityHelper.authorizeSessionOrCookies();
                }
            }
        }
    }

    private static void CheckRestrictedFiles(SecurityFilterSection settings)
    {

        List<string> items = new List<string>(HttpContext.Current.Request.FilePath.ToString().Split('/'));
        foreach (restrictedFileElement item in settings.restrictedFiles)
        {
            if (items.Contains(item.fileName))
            {
                if ((HttpContext.Current.Session == null))
                {
                    HttpContext.Current.Response.Redirect("login.aspx");
                }
                if (HttpContext.Current.Session[CollectionKeys.CurrentUser] == null)
                {
                    HttpContext.Current.Response.Redirect("login.aspx");
                }
            }
        }
    }
}


