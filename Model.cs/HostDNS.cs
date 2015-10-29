using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HostDNS
    {
        public string DomainName { get; set; }
        public bool IsDisabled { get; set; }
        public string IP { get; set; }
        public HostRemark Remark { get; set; }
    }
}
