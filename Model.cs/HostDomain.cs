using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HostDomain
    {
        public string Ip { get; set; }
        public string DomainName { get; set; }
        public string Remark { get; set; }
        public TargetType Target { get; set; }
        public SiteType SiteType { get; set; }
        public bool IsDisabled { get; set; }
    }
}
