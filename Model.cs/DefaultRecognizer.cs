using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DefaultRecognizer : Recognizer
    {
        private static Dictionary<SiteType, IEnumerable<string>> Sitedic;
        private static Dictionary<TargetType, IEnumerable<string>> Targetdic;

        public override SiteType GetSiteType(string domain, string ip)
        {
            if (Sitedic == null)
            {
                Sitedic = EnvironmentMnager.GetIPSiteTypeDictionary();
            }
            foreach (var entry in Sitedic)
            {
                IEnumerable<string> list = entry.Value;
                foreach (var p in list)
                {
                    if (p.Trim() == ip)
                    {
                        SiteType = entry.Key;
                        return entry.Key;
                    }
                }
            }
            SiteType = SiteType.UNK;
            return SiteType;
        }

        public override TargetType GetTargetType(string domain, string ip)
        {
            if (Targetdic == null)
            {
                Targetdic = EnvironmentMnager.GetIPTargetTypeDictionary();
            }
            foreach (var entry in Targetdic)
            {
                IEnumerable<string> list = entry.Value;
                foreach (var p in list)
                {
                    if (p.Trim() == ip)
                    {
                        TargetType = entry.Key;
                        return entry.Key;
                    }
                }
            }
            TargetType = TargetType.Other;
            return TargetType;
        }
    }
}
