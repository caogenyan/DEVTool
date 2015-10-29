using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public class EnvironmentSection : ConfigurationSection
    {
        private readonly static ConfigurationProperty s_property
            = new ConfigurationProperty(string.Empty, typeof(EnvironmentCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

        public EnvironmentSection()
        {
            base.Properties.Add(s_property);
        }

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public EnvironmentCollection EnvironmentSettingCollection
        {
            get { return base[s_property] as EnvironmentCollection; }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }
}
