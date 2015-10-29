using Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HostsManager
{
    public class HostHelperJsonManager
    {
        private HostHelperJson _helper;
        private ILoger _loger;
        public HostHelperJsonManager(HostHelperJson helper, ILoger loger)
        {
            _helper = helper;
            _loger = loger;
        }

        public bool SaveHostDomain(string oldDomain, HostDNS host, bool isWriteToFile)
        {
            try
            {
                _helper.ChangeLineByDomain(oldDomain, host.IP, host.Remark, !host.IsDisabled, isWriteToFile);
                return true;
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool AddHostDomain(HostDNS domain, bool isenable)
        {
            try
            {
                string remark = "";
                if (domain.Remark != null)
                    remark = JSONHelper.JsonSerializer<HostRemark>(domain.Remark);
                string[] newline = new string[1] { string.Format("{0}{1}\t\t{2}\t\t{3}", isenable ? "" : "#", domain.IP, domain.DomainName, "#" + remark) };
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

        public bool DeleteHostLine(HostDNS host, bool iswritetofile)
        {
            try
            {
                _helper.DeleteLine(host.IP, host.DomainName, host.IsDisabled, iswritetofile);
                return true;
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<HostDNS> GetDomanList(Recognizer rec)
        {
            try
            {
                List<HostDNS> resultList = new List<HostDNS>();
                IEnumerable<string> lines = _helper.GetAllLines();
                Regex reg = new Regex(@"(^[#\s]*)((\d{1,3}\.){3}\d{1,3})\s*([\d\w\.-]+)[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                foreach (string line in lines)
                {
                    Match match = reg.Match(line.Trim());
                    if (match.Success)
                    {
                        HostDNS domain = new HostDNS();
                        domain.IsDisabled = !string.IsNullOrEmpty(match.Groups[1].Value.Trim());
                        domain.IP = match.Groups[2].Value;
                        domain.DomainName = match.Groups[4].Value;
                        string remarkStr = match.Groups[5].Value;

                        if (string.IsNullOrWhiteSpace(domain.IP) || string.IsNullOrWhiteSpace(domain.DomainName)) continue;
                        if (string.IsNullOrWhiteSpace(remarkStr.Trim('#')))
                        {
                            HostRemark remark = new HostRemark() { SiteType = rec.GetSiteType(domain.DomainName,domain.IP), Target = TargetType.Other, Comment = "" };
                            domain.Remark = remark;
                        }
                        else
                        {
                            try
                            {
                                domain.Remark = JSONHelper.JsonDeserialize<HostRemark>(remarkStr.Trim('#'));
                            }
                            catch (Exception e)
                            {
                                HostRemark newmark = new HostRemark() { SiteType = rec.GetSiteType(domain.DomainName,domain.IP), Target = TargetType.Other, Comment = remarkStr.Trim('#') };
                                domain.Remark = newmark;
                            }
                        }

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

        public void ChangeRemark(HostDNS domain, HostRemark newRemark, bool isWriteToFile)
        {
            try
            {
                if (newRemark.SiteType != domain.Remark.SiteType || newRemark.Target != domain.Remark.Target)//siteType or targetType has changed
                {
                    string newIp = EnvironmentMnager.GetIpAdress(newRemark.SiteType, newRemark.Target);
                    if (String.IsNullOrWhiteSpace(newIp))
                    {
                        _helper.ChangeRemark(domain.IP, domain.DomainName, newRemark, true);

                    }
                    else
                    {
                        _helper.ChangeIpAndRemark(domain.DomainName, domain.IP, newIp, newRemark, true);
                    }
                }
                else//only comment is changed
                {
                    _helper.ChangeRemark(domain.IP, domain.DomainName, newRemark, true);
                }
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

        public void ChangeIP(HostDNS host, string newip)
        {
            try
            {
                if (host == null || host.Remark == null)
                {
                    throw new ArgumentException("Invald domain argument.");
                }
                _helper.ChangeIp(host.IP, newip, host.DomainName, true);
                DNSManager.FulshDNS();
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void ChangeDomainName(HostDNS host, string newDomainName)
        {
            try
            {
                if (host == null || host.Remark == null)
                {
                    throw new ArgumentException("Invald domain argument.");
                }
                _helper.ChangeDomain(host.IP, host.DomainName, newDomainName, true);
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void ChangeSiteType(HostDNS host, SiteType newSiteType)
        {
            try
            {
                if (host == null || host.Remark == null)
                {
                    throw new ArgumentException("Invald domain argument.");
                }
                else if (host.Remark.SiteType == newSiteType)
                    return;
                HostRemark newRemark = new HostRemark() { SiteType = newSiteType, Target = host.Remark.Target, Comment = host.Remark.Comment };
                string newIp = EnvironmentMnager.GetIpAdress(newSiteType, host.Remark.Target);
                if (!string.IsNullOrEmpty(newIp))
                {

                    _helper.ChangeIpAndRemark(host.DomainName, host.IP, newIp, newRemark, true);
                    DNSManager.FulshDNS();
                }
                else
                {
                    _helper.ChangeRemark(host.IP, host.DomainName, newRemark, true);
                }
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void ChangeTargetType(HostDNS host, TargetType newTargetType)
        {
            try
            {
                if (host == null || host.Remark == null)
                {
                    throw new ArgumentException("Invald domain argument.");
                }
                else if (host.Remark.Target == newTargetType)
                {
                    return;
                }
                else
                {
                    HostRemark newRemark = new HostRemark() { SiteType = host.Remark.SiteType, Target = newTargetType, Comment = host.Remark.Comment };
                    string newIp = EnvironmentMnager.GetIpAdress(host.Remark.SiteType, newTargetType);
                    if (!string.IsNullOrEmpty(newIp))
                    {
                        _helper.ChangeIpAndRemark(host.DomainName, host.IP, newIp, newRemark, true);
                        DNSManager.FulshDNS();
                    }
                    else
                    {
                        _helper.ChangeRemark(host.IP, host.DomainName, newRemark, true);
                    }
                }
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void ChangeComment(HostDNS host, string newcomment)
        {
            try
            {
                if (host == null || host.Remark == null)
                {
                    throw new ArgumentException("Invald domain argument.");
                }
                HostRemark remark = new HostRemark() { SiteType = host.Remark.SiteType, Target = host.Remark.Target, Comment = newcomment };
                _helper.ChangeRemark(host.IP, host.DomainName, new HostRemark(), true);
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void DisableHost(HostDNS host)
        {
            try
            {
                if (host == null)
                {
                    throw new ArgumentException("The host can not be null.");
                }
                else if (host.IsDisabled)
                {
                    return;
                }
                _helper.DisableByIpAndDomain(host.IP, host.DomainName, true);
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void EnableHost(HostDNS host)
        {
            try
            {
                if (host == null)
                {
                    throw new ArgumentException("The host can not be null.");
                }
                else if (!host.IsDisabled)
                {
                    return;
                }
                _helper.EnableByIpAndDomain(host.IP, host.DomainName, true);
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }

        public void FormatHost()
        {
            try
            {
                _helper.FormatHost(new XmlRecognizer(), true);
            }
            catch (Exception ex)
            {
                _loger.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
