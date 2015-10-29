using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HostsManager
{
    public class HostHelperJson
    {
        public const string HostPath = @"C:\Windows\System32\drivers\etc\hosts";

        public StringBuilder HostsStr { get; private set; }
        public HostHelperJson()
        {
            using (FileStream stream = new FileStream(HostPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(stream))
            {
                string hostStr = reader.ReadToEnd();
                HostsStr = new StringBuilder(hostStr);
            }
        }

        /// <summary>
        /// caculate the count of lines which start with the specific ip
        /// </summary>
        /// <param name="ip">the ip to find</param>
        /// <returns></returns>
        public int FindLinesStartWithip(string ip)
        {
            Regex reg = new Regex("^" + ip + @"(?=\s)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(HostsStr.ToString());
            return mc.Count;
        }

        /// <summary>
        /// Disable the line which start with specific ip address
        /// </summary>
        /// <param name="ip">ip address</param>
        /// <param name="writetofile">a bool value indicate whether to write changes to file immediately</param>
        public void Disableip(string ip, bool writetofile)
        {
            if (FindLinesStartWithip(ip) > 0)
            {
                string pattern = @"^\s*" + ip + @"(?=\s)";
                string result = Regex.Replace(HostsStr.ToString(), pattern, "#$&", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                HostsStr = new StringBuilder(result);
                if (writetofile)
                    WriteToFile();
            }
        }

        /// <summary>
        /// Disable the lines which contains the ip address
        /// </summary>
        /// <param name="ip">ip address</param>
        /// <param name="writetofile">a bool value indicate whether to write changes to file immediately</param>
        public void Enableip(string ip, bool writetofile)
        {
            string pattern = @"^[#\s]*" + @"(?=" + ip + ")";
            string result = Regex.Replace(HostsStr.ToString(), pattern, "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(result);
            if (writetofile)
                WriteToFile();
        }

        /// <summary>
        /// Disable the lines which contains the specific domain name
        /// </summary>
        /// <param name="domainName">Domain name</param>
        /// <param name="writetoFile">a bool value indicate whether to write changes to file immediately</param>
        public void DisableDomain(string domainName, bool writetoFile)
        {
            //(^[#\s]*)(10.81.152.31)\s*(edm.db.qa.elmae)[^\S\n]*(#[^\n]*)?$
            Regex reg = new Regex(@"(^[#\s]*)([\d\.]+)\s*(" + domainName + @")[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string result = reg.Replace(HostsStr.ToString(), "#${2}\t\t${3}\t\t${4}");
            HostsStr = new StringBuilder(result);
            if (writetoFile)
                WriteToFile();
        }

        /// <summary>
        /// Enable the lines which contains the specific domain name
        /// </summary>
        /// <param name="domainName">Domain name</param>
        /// <param name="writetoFile">a bool value indicate whether to write changes to file immediately</param>
        public void EnableDomain(string domainName, bool writetoFile)
        {
            //(^[#\s]*)(10.81.152.31)\s*(edm.db.qa.elmae)[^\S\n]*(#[^\n]*)?$
            Regex reg = new Regex(@"^([#\s]*)([\d\.]+)\s*(" + domainName + @")[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string result = reg.Replace(HostsStr.ToString(), "${2}\t\t${3}\t\t${4}");
            HostsStr = new StringBuilder(result);
            if (writetoFile)
                WriteToFile();
        }

        /// <summary>
        /// Disable the lines which has the specific ip and specific domain name
        /// </summary>
        /// <param name="ip">Ip address</param>
        /// <param name="domainName">Domain name</param>
        /// <param name="writetoFile"></param>
        public void DisableByIpAndDomain(string ip, string domainName, bool writetoFile)
        {
            if (string.IsNullOrWhiteSpace(domainName))
                throw new ArgumentException("The domain name can not be empty or whiteSpace.");
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("The new IpAdress can not be empty or whiteSpace.");
            else if (!Regex.Match(ip, @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)", RegexOptions.IgnoreCase).Success)
            {
                throw new ArgumentException("Invalid IpAdress.");
            }

            //(^[#\s]*)(10.81.152.31)\s*(edm.db.qa.elmae)[^\S\n]*(#[^\n]*)?$
            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + domainName + @")[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string result = reg.Replace(HostsStr.ToString(), "#${2}\t\t${3}\t\t${4}");
            HostsStr = new StringBuilder(result);
            if (writetoFile)
                WriteToFile();
        }

        /// <summary>
        /// Enable the lines which has the specific ip and specific domain name
        /// </summary>
        /// <param name="ip">ip address</param>
        /// <param name="domainName">domain name</param>
        /// <param name="writetoFile">a bool value indicate whether to write changes to file immediately</param>
        public void EnableByIpAndDomain(string ip, string domainName, bool writetoFile)
        {
            if (string.IsNullOrWhiteSpace(domainName))
                throw new ArgumentException("The domain name can not be empty or whiteSpace.");
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("The new IpAdress can not be empty or whiteSpace.");
            else if (!Regex.Match(ip, @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)", RegexOptions.IgnoreCase).Success)
            {
                throw new ArgumentException("Invalid IpAdress.");
            }

            //(^[#\s]*)(10.81.152.31)\s*(edm.db.qa.elmae)[^\S\n]*(#[^\n]*)?$
            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + domainName + @")[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string result = reg.Replace(HostsStr.ToString(), "${2}\t\t${3}\t\t${4}");
            HostsStr = new StringBuilder(result);
            if (writetoFile)
                WriteToFile();
        }

        /// <summary>
        /// return a bool value indicate whether the specific ip and domain is enabled
        /// </summary>
        /// <param name="ip">IP address</param>
        /// <param name="domain">domainName</param>
        /// <returns></returns>
        public bool IsipDomainEnable(string ip, string domain)
        {
            Regex reg = new Regex(@"^\s*" + ip + @"\s*" + domain, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return reg.Match(HostsStr.ToString()).Success;
        }

        public bool IsipDomainExists(string ip, string domain)
        {
            Regex reg = new Regex(@"^[\s#]*" + ip + @"\s*" + domain, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return reg.Match(HostsStr.ToString()).Success;
        }

        public void ChangeIp(string oldip, string newip, string domain, bool writetofile)
        {
            if (string.IsNullOrWhiteSpace(domain))
                throw new ArgumentException("The new domain name can not be empty or whiteSpace.");
            if (string.IsNullOrWhiteSpace(newip))
                throw new ArgumentException("The new IpAdress can not be empty or whiteSpace.");
            else if (!Regex.Match(newip, @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)", RegexOptions.IgnoreCase).Success)
            {
                throw new ArgumentException("Invalid IpAdress.");
            }

            Regex reg = new Regex(@"(^[#\s]*)(" + oldip + @")\s*(" + domain + ")", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}" + newip + "\t\t${3}"));
            if (writetofile)
                WriteToFile();
        }

        public void ChangeDomain(string ip, string olddomain, string newdomain, bool writetofile)
        {
            if (string.IsNullOrWhiteSpace(olddomain))
                throw new ArgumentException("The old domain name can not be empty or whiteSpace.");
            else if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("The old ip address can not be empty or whiteSpace.");
            else if (!Regex.Match(ip, @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)", RegexOptions.IgnoreCase).Success)
            {
                throw new ArgumentException("Invalid old ip adress.");
            }
            else if (string.IsNullOrWhiteSpace(newdomain))
                throw new ArgumentException("The new domain name can not be empty or whiteSpace.");

            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + olddomain + ")", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}${2}\t\t" + newdomain));
            if (writetofile)
                WriteToFile();
        }

        private HostRemark GetRemark(string ip, string domain)
        {
            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + domain + @")[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match match = reg.Match(HostsStr.ToString());
            string remark = match.Groups[4].Value.Trim('#');
            return JSONHelper.JsonDeserialize<HostRemark>(remark);
        }

        public void ChangeRemark(string ip, string domain, HostRemark newremark, bool writetofile)
        {
            if (newremark == null) return;
            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentException("Invalid ip or domain name.");
            }

            string remark = JSONHelper.JsonSerializer<HostRemark>(newremark);
            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + domain + @")[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}${2}\t\t${3}\t\t#" + remark));
            if (writetofile)
                WriteToFile();
        }

        public void ChangeIpAndRemark(string domain, string oldIp, string newIp, HostRemark remak, bool writetofile)
        {
            ChangeIp(oldIp, newIp, domain, false);
            ChangeRemark(newIp, domain, remak, writetofile);
        }

        public void ChangeDomainAndRemark(string oldDomain, string ip, string newDomain, HostRemark remark, bool writetofile)
        {
            ChangeDomain(ip, oldDomain, newDomain, false);
            ChangeRemark(ip, newDomain, remark, writetofile);
        }

        /// <summary>
        /// modify the host by delete the line first and append new host line
        /// </summary>
        /// <param name="domain">Domain name</param>
        /// <param name="newip">ip address</param>
        /// <param name="newremark">remark for the domain</param>
        /// <param name="isenable">a value indicate whether the new host is enabled</param>
        /// <param name="writetofile">a value indicate whether to apply changes to the hosts file immediately</param>
        public void ChangeLineByDomain(string domain, string newip, HostRemark newremark, bool isenable, bool writetofile)
        {
            if (string.IsNullOrWhiteSpace(newip))
                throw new ArgumentException("The new IpAdress can not be empty or whiteSpace.");
            else if (!Regex.Match(newip, @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)", RegexOptions.IgnoreCase).Success)
            {
                throw new ArgumentException("Invalid IpAdress.");
            }

            string remark = "#";
            if (newremark != null) remark += JSONHelper.JsonSerializer<HostRemark>(newremark);
            string enableStr = isenable ? "" : "#";
            string newline = string.Format("{0}{1}\t\t{2}\t\t{3}\r\n", enableStr, newip, domain, remark);

            ReplaceLineByDomain(domain, newline, false);

            if (writetofile)
                WriteToFile();
        }

        /// <summary>
        /// Delete all the host by the domain name
        /// </summary>
        /// <param name="domain">Domain name</param>
        /// <param name="writetofile">a value indicate whether to apply changes to the hosts file immediately</param>
        public void DeleteLineByDomain(string domain, bool writetofile)
        {
            if (string.IsNullOrWhiteSpace(domain))
                return;
            Regex reg = new Regex(@"^[#\s\.\d]*" + domain + ".*\r?\n", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), ""));
            if (writetofile)
                WriteToFile();
        }

        public void DeleteLine(string ip, string domain, bool isdisabled, bool writetofile)
        {
            string disableStr = isdisabled ? @"[#\s]" : @"[\s]";
            Regex reg = new Regex(@"(^" + disableStr + "*)(" + ip + @")\s*(" + domain + @")[^\S\n]*(#[^\n]*)?(\r?\n)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), ""));
            if (writetofile)
                WriteToFile();
        }

        /// <summary>
        /// Replace a line by specific replacement
        /// </summary>
        /// <param name="domain">Domain Name</param>
        /// <param name="replacement">Replacement</param>
        /// <param name="writetofile">a value indicate whether to apply changes to the hosts file immediately</param>
        private void ReplaceLineByDomain(string domain, string replacement, bool writetofile)
        {
            Regex reg = new Regex(@"^[#\s\.\d]*" + domain + ".*\r?\n", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), replacement));
            if (writetofile)
                WriteToFile();
        }

        public void FormatHost(Recognizer recognizer, bool writetofile)
        {
            IEnumerable<string> HostDnses = GetAllLines();
            StringBuilder hostStr = new StringBuilder();

            Regex reg = new Regex(@"(^[#\s]*)((\d{1,3}\.){3}\d{1,3})\s*([\d\w\.-]+)[^\S\n]*(#[^\n]*)?$", RegexOptions.IgnoreCase);
            foreach (var host in HostDnses)
            {
                Match match = reg.Match(host.ToString());
                string enable = match.Groups[1].Value;
                string ip = match.Groups[2].Value;
                string domainName = match.Groups[4].Value;
                string remark = match.Groups[5].Value;

                if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(domainName)) continue;
                string formatRemark;
                if (string.IsNullOrWhiteSpace(remark.Trim('#')))
                {
                    formatRemark = "";
                    HostRemark newmark = new HostRemark() { SiteType = recognizer.GetSiteType(domainName, ip), Target = recognizer.GetTargetType(domainName, ip), Comment = "" };
                    formatRemark = "#" + JSONHelper.JsonSerializer<HostRemark>(newmark);
                }
                else
                {
                    try
                    {
                        HostRemark hostRemark = JSONHelper.JsonDeserialize<HostRemark>(remark.Trim('#'));
                        if (hostRemark.SiteType == SiteType.UNK)
                        {
                            hostRemark.SiteType = recognizer.GetSiteType(domainName, ip);
                        }
                        if (hostRemark.Target == TargetType.Other || hostRemark.Target == TargetType.DEFAULT)
                        {
                            hostRemark.Target = recognizer.GetTargetType(domainName, ip);
                        }
                        formatRemark = "#" + JSONHelper.JsonSerializer<HostRemark>(hostRemark);
                    }
                    catch (Exception e)
                    {
                        HostRemark newmark = new HostRemark() { SiteType = recognizer.GetSiteType(domainName, ip), Target = recognizer.GetTargetType(domainName, ip), Comment = remark.Trim('#') };
                        formatRemark = "#" + JSONHelper.JsonSerializer<HostRemark>(newmark);
                    }
                }

                hostStr.AppendLine(reg.Replace(host, "${1}${2}" + GetFormatEmpyStr(enable + ip) + "\t\t${4}\t\t" + formatRemark));
            }
            HostsStr = hostStr;
            if (writetofile)
                WriteToFile();
        }

        public IEnumerable<string> GetAllLines()
        {
            return File.ReadLines(HostPath);
        }

        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(HostPath))
            {
                writer.Write(HostsStr.ToString());
            }
        }

        public void WriteToFile(string[] lines, bool isappend)
        {
            string content = string.Concat(lines);
            using (StreamWriter writer = new StreamWriter(content, isappend))
            {
                writer.Write(HostsStr.ToString());
            }
        }

        private string GetFormatEmpyStr(string ip)
        {
            int emptyCount = 15 - ip.Length;
            StringBuilder resultEmtpy = new StringBuilder();
            for (int i = 0; i < emptyCount; i++)
                resultEmtpy.Append(" ");
            return resultEmtpy.ToString();
        }
    }
}
