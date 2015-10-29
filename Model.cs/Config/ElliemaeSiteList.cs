using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    [ConfigurationCollection(typeof(SiteSettings))]
    public class ElliemaeSiteList : ConfigurationElementCollection
    {
        public ElliemaeSiteList()
            : base(StringComparer.OrdinalIgnoreCase)
        { }

        new public SiteSettings this[string name]
        {
            get { return base.BaseGet(name) as SiteSettings; }
            set
            {
                if (base.BaseGet(name) != null)
                {
                    base.BaseRemove(name);
                }
                this.BaseAdd(value);
            }
        }

        public SiteSettings this[int index]
        {
            get { return base.BaseGet(index) as SiteSettings; }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SiteSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SiteSettings)element).DomainName;
        }
    }
}
