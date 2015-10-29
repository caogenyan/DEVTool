using Model.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class XmlRecognizer : DefaultRecognizer
    {
        public static RecognizerSection RecognizerConfigSection = (RecognizerSection)ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("RecognizerSecton");

        public override SiteType GetSiteType(string domain, string ip)
        {
            if (RecognizerConfigSection != null)
            {
                foreach (SiteSettings site in RecognizerConfigSection.ElliemseSiteList)
                {
                    if (site.DomainName == domain)
                    {
                        SiteType = SiteType.ELM;
                        return SiteType;
                    }
                }
            }
            base.GetSiteType(domain,ip);
            return SiteType;
        }
    }
}
