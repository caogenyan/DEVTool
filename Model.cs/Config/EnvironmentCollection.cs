using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    [ConfigurationCollection(typeof(EnvironmentSettings))]
    public class EnvironmentCollection : ConfigurationElementCollection
    {
        public EnvironmentCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        { }

        new public EnvironmentSettings this[string name]
        {
            get { return base.BaseGet(name) as EnvironmentSettings; }
            set { base.BaseAdd(value as ConfigurationElement); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnvironmentSettings)element).Name;
        }

        public EnvironmentSettings this[int index]
        {
            get { return base.BaseGet(index) as EnvironmentSettings; }
        }


        public void Add(EnvironmentSettings setting)
        {
            this.BaseAdd(setting);
        }

        public void Clear()
        {
            base.BaseClear();
        }

        public void Remove(string name)
        {
            base.BaseRemove(name);
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }
}
