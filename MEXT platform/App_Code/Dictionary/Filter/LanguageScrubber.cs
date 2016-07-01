
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Web;
using System.IO;
using LanguageDictionary;
public class LanguageScrubber : IHttpModule
{

    public void Dispose()
    {
        //disposed
    }

    public void Init(HttpApplication context)
    {
        //context.PostAcquireRequestState += this.Application_EndRequest;
        //AddHandler context.BeginRequest, AddressOf Me.Application_BeginRequest
        context.EndRequest += this.Application_EndRequest;
        context.BeginRequest += this.Application_BeginRequest;
        //AddHandler application.EndRequest, AddressOf Me.Application_EndRequest
    }

    private void Application_BeginRequest(object source, EventArgs e)
    {
        ResponseFilterStream filter = new ResponseFilterStream(HttpContext.Current.Response.Filter);
        filter.TransformString += transformString ;
        HttpContext.Current.Response.Filter = filter;
    }

    private void Application_EndRequest(object source, EventArgs e)
    {
        //StreamReader reader = new StreamReader(HttpContext.Current.Response.OutputStream, System.Text.Encoding.GetEncoding("utf-8"));
        HttpResponse response = HttpContext.Current.Response;
        //reamReader read = new StreamReader(response.con);



    }

    private string transformString(string input)
    {
        string test = input;
        return test;
        //return LangDict.current().
    }

}
