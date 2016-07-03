using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;

public class SecurityFilterSection : ConfigurationSection
{

    [ConfigurationProperty("restrictedFiles", IsRequired = false)]
    [ConfigurationCollection(typeof(restrictedFileElementCollection), AddItemName = "restrictedFile")]
    public restrictedFileElementCollection restrictedFiles
    {
        get { return (restrictedFileElementCollection)this["restrictedFiles"]; }
        set { this["restrictedFiles"] = value; }
    }

    [ConfigurationProperty("restrictedSubApps", IsRequired = false)]
    [ConfigurationCollection(typeof(restrictedSubApplicationCollection), AddItemName = "restrictedSubApp")]
    public restrictedSubApplicationCollection restrictedSubApp
    {
        get { return (restrictedSubApplicationCollection)this["restrictedSubApps"]; }
        set { this["restrictedSubApps"] = value; }
    }

    public class restrictedFileElementCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new restrictedFileElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((restrictedFileElement)element).fileName;
        }
    }

    public class restrictedSubApplicationCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new restrictedAppElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((restrictedAppElement)element).appName;
        }
    }

    public class restrictedFileElement : ConfigurationElement
    {
        [ConfigurationProperty("fileName", IsRequired = false)]
        public string fileName
        {
            get { return Convert.ToString(this["fileName"]); }
            set { this["fileName"] = value; }
        }
        [ConfigurationProperty("allowedRole", IsRequired = false)]
        public string allowedRole
        {
            get { return Convert.ToString(this["allowedRole"]); }
            set { this["allowedRole"] = value; }
        }

    }

    public class restrictedAppElement : ConfigurationElement
    {
        [ConfigurationProperty("appNaame", IsRequired = false)]
        public string appName
        {
            get { return Convert.ToString(this["appNaame"]); }
            set { this["appNaame"] = value; }
        }


        [ConfigurationProperty("allowedRoles", IsRequired = false)]
        [ConfigurationCollection(typeof(allowedRolesCollection), AddItemName = "allowedRole")]
        public allowedRolesCollection allowedRoles
        {
            get { return (allowedRolesCollection)this["allowedRoles"]; }
            set { this["allowedRoles"] = value; }
        }

    }


    public class allowedRolesCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new allowedRoleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((allowedRoleElement)element).allowedRole;
        }
    }

    public class allowedRoleElement : ConfigurationElement
    {
        [ConfigurationProperty("allowedRole", IsRequired = false)]
        public string allowedRole
        {
            get { return Convert.ToString(this["allowedRole"]); }
            set { this["allowedRole"] = value; }
        }

    }

}
