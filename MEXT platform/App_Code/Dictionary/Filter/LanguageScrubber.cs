
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Web;
using System.IO;

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
        //AddHandler application.EndRequest, AddressOf Me.Application_EndRequest
    }

    private void Application_BeginRequest(object source, EventArgs e)
    {
    }

    private void Application_EndRequest(object source, EventArgs e)
    {
        //StreamReader reader = new StreamReader(HttpContext.Current.Response.OutputStream, System.Text.Encoding.GetEncoding("utf-8"));
        HttpResponse response = HttpContext.Current.Response;
        //reamReader read = new StreamReader(response.con);
        HttpContext.Current.Response.Write("testyface");

        ResponseFilterStream filter = new ResponseFilterStream(HttpContext.Current.Response.Filter);
        var thing = HttpContext.Current.Response.Filter;
        HttpContext.Current.Response.Filter = filter;


    }

}
