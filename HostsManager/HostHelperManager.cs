using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HostsManager
{
    public class HostHelperManager
    {
        private HostHelper _helper;
        private ILoger _loger;
        public HostHelperManager(HostHelper helper, ILoger loger)
        {
            _helper = helper;
            _loger = loger;
        }

        public bool SaveHostDomain(string oldDomain, HostDomain newDomain, bool isenable, HostRemark remark, bool isWriteToFile)
        {
            try
            {
                _helper.ChangeLineByDomain(oldDomain, newDomain.Ip, remark, isenable, isWriteToFile);
                return true;
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool AddHostDomain(HostDomain domain, bool isenable)
        {
            try
            {
                string[] newline = new string[1] { string.Format("{0}{1}\t\t{2}\t\t{3}", isenable ? "" : "#", domain.Ip, domain.DomainName, domain.Remark) };
                _helper.WriteToFile(newline, true);
                return true;
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }

        }

        public bool DeleteHostDomain(string domainName, bool iswritetofile)
        {
            try
            {
                _helper.DeleteLineByDomain(domainName, iswritetofile);
                return true;
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<HostDomain> GetDomanList(Recognizer rec)
        {
            try
            {
                List<HostDomain> resultList = new List<HostDomain>();
                IEnumerable<string> lines = _helper.GetAllLines();
                Regex reg = new Regex(@"(^[\s#]*)([\d\.]+)\s*([\w\d\.-]+)\s*(#[^#\r\n%]*)?(\s*#%([^%]*)%)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                foreach (string line in lines)
                {
                    Match match = reg.Match(line.Trim());
                    if (match.Success)
                    {
                        HostDomain domain = new HostDomain();
                        domain.IsDisabled = !string.IsNullOrEmpty(match.Groups[1].Value);
                        domain.Ip = match.Groups[2].Value;
                        domain.DomainName = match.Groups[3].Value;
                        domain.Remark = match.Groups[4].Value.Trim();

                        string targetStr = match.Groups[6].Value.Trim();
                        TargetType type = TargetType.Other;
                        if (Enum.TryParse(targetStr, out type))
                            domain.Target = type;
                        else
                            domain.Target = TargetType.Other;
                        domain.SiteType = rec.GetSiteType(domain.DomainName);
                        resultList.Add(domain);
                    }
                }
                return resultList;
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void ChangeDomainAndRemark(string oldDomain, string ip, string newdomain, HostRemark remark, bool iswritetofile)
        {
            try
            {
                 string newIp = EnvironmentMnager.GetIpAdress(remark.SiteType, remark.Target);

                _helper.ChangeDomainAndRemark(oldDomain, ip, newdomain, remark, iswritetofile);
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void ChangeDomainSiteType(HostDomain domain, SiteType newSiteType)
        {
            try
            {
                if (domain.SiteType == newSiteType) return;

                string newIp = EnvironmentMnager.GetIpAdress(newSiteType, domain.Target);
                _helper.ChangeIp(domain.Ip, newIp, domain.DomainName, true);
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
