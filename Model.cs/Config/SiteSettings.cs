using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public class SiteSettings : ConfigurationElement
    {
        private static readonly ConfigurationProperty domainNameProperty =
            new ConfigurationProperty("domainName", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        public SiteSettings()
        {
            base.Properties.Add(domainNameProperty);
        }

        [ConfigurationProperty("domainName", IsRequired = true, IsKey = true)]
        public string DomainName
        {
            get { return this["domainName"].ToString(); }
        }
    }
}
