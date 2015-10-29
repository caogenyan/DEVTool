using Model.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class EnvironmentMnager
    {
        public static EnvironmentSection EnvironmentConfigSection = (EnvironmentSection)ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("EnvironmentSection");
        public static string GetIpAdress(SiteType siteType, TargetType targetType)
        {
            if (EnvironmentConfigSection != null)
            {
                if (siteType == SiteType.ELM)
                {
                    return null;
                }
                else
                {
                    switch (targetType)
                    {
                        case TargetType.QA1:
                            if (siteType == SiteType.TPO)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA1"].TPO;
                            else if (siteType == SiteType.WBC)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA1"].WebCenter;
                            else if (siteType == SiteType.CDW)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA1"].ConsumerDirect;
                            else
                                return null;
                        case TargetType.QA2:
                            if (siteType == SiteType.TPO)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA2"].TPO;
                            else if (siteType == SiteType.WBC)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA2"].WebCenter;
                            else if (siteType == SiteType.CDW)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA2"].ConsumerDirect;
                            else
                                return null;
                        case TargetType.QA3:
                            if (siteType == SiteType.TPO)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA3"].TPO;
                            else if (siteType == SiteType.WBC)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA3"].WebCenter;
                            else if (siteType == SiteType.CDW)
                                return EnvironmentConfigSection.EnvironmentSettingCollection["QA3"].ConsumerDirect;
                            else
                                return null;
                        case TargetType.LOCAL:
                            return "127.0.0.1";
                        default:
                            return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<SiteType, IEnumerable<string>> GetIPSiteTypeDictionary()
        {
            Dictionary<SiteType, IEnumerable<string>> dic = new Dictionary<SiteType, IEnumerable<string>>();
            List<string> tpolist = new List<string>();
            List<string> weclist = new List<string>();
            List<string> cdwlist = new List<string>();
            foreach (EnvironmentSettings a in EnvironmentConfigSection.EnvironmentSettingCollection)
            {
                if (!string.IsNullOrEmpty(a.TPO))
                {
                    tpolist.Add(a.TPO);
                }
                if (!string.IsNullOrEmpty(a.WebCenter))
                {
                    weclist.Add(a.WebCenter);
                }
                if (!string.IsNullOrEmpty(a.ConsumerDirect))
                {
                    cdwlist.Add(a.ConsumerDirect);
                }
            }
            dic.Add(SiteType.TPO, tpolist);
            dic.Add(SiteType.WBC, weclist);
            dic.Add(SiteType.CDW, cdwlist);

            return dic;
        }

        public static Dictionary<TargetType, IEnumerable<string>> GetIPTargetTypeDictionary()
        {
            Dictionary<TargetType, IEnumerable<string>> dic = new Dictionary<TargetType, IEnumerable<string>>();
            List<string> qa1list = new List<string>();
            List<string> qa2list = new List<string>();
            List<string> qa3list = new List<string>();

            TargetType type = TargetType.Other;
            foreach (EnvironmentSettings s in EnvironmentConfigSection.EnvironmentSettingCollection)
            {

                if (s.Name == "QA1")
                {
                    type = TargetType.QA1;
                    if (!string.IsNullOrWhiteSpace(s.TPO))
                        qa1list.Add(s.TPO);
                    if (!string.IsNullOrWhiteSpace(s.WebCenter))
                        qa1list.Add(s.WebCenter);
                    if (!string.IsNullOrWhiteSpace(s.ConsumerDirect))
                        qa1list.Add(s.ConsumerDirect);
                    dic.Add(type, qa1list);
                }
                else if (s.Name == "QA2")
                {
                    type = TargetType.QA2;
                    if (!string.IsNullOrWhiteSpace(s.TPO))
                        qa2list.Add(s.TPO);
                    if (!string.IsNullOrWhiteSpace(s.WebCenter))
                        qa2list.Add(s.WebCenter);
                    if (!string.IsNullOrWhiteSpace(s.ConsumerDirect))
                        qa2list.Add(s.ConsumerDirect);
                    dic.Add(type, qa2list);
                }
                else if (s.Name == "QA3")
                {
                    type = TargetType.QA3;
                    if (!string.IsNullOrWhiteSpace(s.TPO))
                        qa3list.Add(s.TPO);
                    if (!string.IsNullOrWhiteSpace(s.WebCenter))
                        qa3list.Add(s.WebCenter);
                    if (!string.IsNullOrWhiteSpace(s.ConsumerDirect))
                        qa3list.Add(s.ConsumerDirect);
                    dic.Add(type, qa3list);
                }
            }
            dic.Add(TargetType.LOCAL, new List<string> { "127.0.0.1" });
            return dic;
        }
    }
}
