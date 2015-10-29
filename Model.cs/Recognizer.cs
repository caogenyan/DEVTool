using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Recognizer
    {
        public SiteType SiteType { get; protected set; }
        public TargetType TargetType { get; protected set; }

        public abstract SiteType GetSiteType(string domain, string ip);

        public abstract TargetType GetTargetType(string domain, string ip);
    }
}
