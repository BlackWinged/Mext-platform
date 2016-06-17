
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

[Dapper.Table("userRoles")]
public class UserRoles
{
    [Dapper.Key()]
    [Dapper.ReadOnly(true)]
    public int id { get; set; }
    public string roleName { get; set; }
    public int roleBitMask { get; set; }

}

[Dapper.Table("users")]
public class Users
{
    [Dapper.Key()]
    [Dapper.ReadOnly(true)]
    public int id { get; set; }
    public string username { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public byte[] password { get; set; }
    public string email { get; set; }
    public byte[] csid { get; set; }
    public int active { get; set; }
    public int role { get; set; }

}
