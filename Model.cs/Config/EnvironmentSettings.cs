using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public class EnvironmentSettings : ConfigurationElement
    {
         private static readonly ConfigurationProperty environmentName =
            new ConfigurationProperty("Name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationProperty environmentTPO =
            new ConfigurationProperty("TP", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationProperty environmentWebCenter =
            new ConfigurationProperty("WC", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationProperty environmentConsumerDirect =
            new ConfigurationProperty("CD", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        public EnvironmentSettings()
        {
            base.Properties.Add(environmentName);
            base.Properties.Add(environmentTPO);
            base.Properties.Add(environmentWebCenter);
            base.Properties.Add(environmentConsumerDirect);
        }

        [ConfigurationProperty("Name", IsRequired = true,IsKey=true)]
        public string Name
        {
            get { return this["Name"].ToString(); }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("TP", IsRequired = true)]
        public string TPO
        {
            get { return this["TP"].ToString(); }
            set { this["TP"] = value; }
        }

        [ConfigurationProperty("WC", IsRequired = true)]
        public string WebCenter
        {
            get { return this["WC"].ToString(); }
            set { this["WC"] = value; }
        }

        [ConfigurationProperty("CD", IsRequired = true)]
        public string ConsumerDirect
        {
            get { return this["CD"].ToString(); }
            set { this["CD"] = value; }
        }
    }
}
