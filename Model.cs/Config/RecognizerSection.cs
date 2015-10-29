using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public class RecognizerSection : ConfigurationSection
    {
        private readonly static ConfigurationProperty s_property
            = new ConfigurationProperty("EllieMaeList", typeof(ElliemaeSiteList), null, ConfigurationPropertyOptions.IsDefaultCollection);

        public RecognizerSection()
        {
            base.Properties.Add(s_property);
        }

        [ConfigurationProperty("EllieMaeList", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public ElliemaeSiteList ElliemseSiteList
        {
            get { return this[s_property] as ElliemaeSiteList; }
        }
    }
}
