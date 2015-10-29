using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HostsManager
{
    public class HostHelper : HostsManager.IHostHelper
    {
        public const string HostPath = @"C:\Windows\System32\drivers\etc\hosts";
        public StringBuilder HostsStr { get; private set; }
        public HostHelper()
        {
            using (FileStream stream = new FileStream(HostPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(stream))
            {
                string hostStr = reader.ReadToEnd();
                HostsStr = new StringBuilder(hostStr);
            }
        }

        public int FindLinesStartWithip(string ip)
        {
            Regex reg = new Regex("^" + ip + @"(?=\s)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(HostsStr.ToString());
            return mc.Count;
        }

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

        public void Enableip(string ip, bool writetofile)
        {
            string pattern = @"^[#\s]*" + @"(?=" + ip + ")";
            string result = Regex.Replace(HostsStr.ToString(), pattern, "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(result);
            if (writetofile)
                WriteToFile();
        }

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
            Regex reg = new Regex(@"(^[#\s]*)(" + oldip + @")\s*(" + domain + ")", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}" + newip + "\t\t${3}"));
            if (writetofile)
                WriteToFile();
        }

        public void ChangeDomain(string ip, string olddomain, string newdomain, bool writetofile)
        {
            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + olddomain + ")", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}${2}\t\t" + newdomain));
            if (writetofile)
                WriteToFile();
        }

        public void ChangeRemark(string ip, string domain, string newremark, bool writetofile)
        {
            if (newremark == null) return;
            else if (newremark.LastIndexOf("%") >= 0)
            {
                throw new ArgumentException("the new remark can't contain '%'");
            }
            //(^[#\s]*)(70.42.161.91)\s*(www.epassbusinesscenter.com)\s*(#[^#\r\n%]*)?\s*(#%([^%]*)%)?\s*$
            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + domain + @")\s*(#[^#\r\n%]*)?\s*(#%([^%]*)%)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}${2}\t\t${3}" + (string.IsNullOrWhiteSpace(newremark) ? "" : "\t\t#" + newremark) + "\t\t${5}"));
            if (writetofile)
                WriteToFile();
        }

        /// <summary>
        /// modify the Host record by change its targetType
        /// </summary>
        /// <param name="ip">domain ip</param>
        /// <param name="domain">domain</param>
        /// <param name="newTargetTypeStr">targetType without '%' and '#',eg."QA1"</param>
        /// <param name="writetofile">bool value indicate whether to apply changes to hosts file immediately.</param>
        public void ChangeTargetType(string ip, string domain, string newTargetTypeStr, bool writetofile)
        {
            if (newTargetTypeStr == null) newTargetTypeStr = string.Empty;
            newTargetTypeStr = newTargetTypeStr.Trim(new char[] { '%', '#' });
            if (newTargetTypeStr.LastIndexOf("%") >= 0)
            {
                throw new ArgumentException("invalid new targetType string.");
            }
            //[(<2>(<1>)70.42.161.91)		(<3>www.epassbusinesscenter.com)		(<4>#00000000 	)(<5>#%(<6>QA1)%)]
            Regex reg = new Regex(@"(^[#\s]*)(" + ip + @")\s*(" + domain + @")\s*(#[^#\r\n%]*)?(\s*#%([^%]*)%)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string newTargetStr = string.IsNullOrWhiteSpace(newTargetTypeStr) ? "" : "\t\t" + newTargetTypeStr;
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}${2}\t\t${3}\t\t${4} " + "#%" + newTargetTypeStr + "%"));
            if (writetofile)
                WriteToFile();
        }

        public void ChangeIpAndTargetType(string domain, string newIpAdress, string newTargetTypeStr, bool writetofile)
        {
            if (string.IsNullOrWhiteSpace(newIpAdress))
                throw new ArgumentException("The new IpAdress can not be empty or whiteSpace.");
            else if (!Regex.Match(newIpAdress, @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)", RegexOptions.IgnoreCase).Success)
            {
                throw new ArgumentException("Invalid IpAdress.");
            }

            if (newTargetTypeStr == null) newTargetTypeStr = string.Empty;
            newTargetTypeStr = newTargetTypeStr.Trim(new char[] { '%', '#' });
            if (newTargetTypeStr.LastIndexOf("%") >= 0)
            {
                throw new ArgumentException("invalid new targetType string.");
            }

            //[(<2>(<1>)70.42.161.91)		(<3>www.epassbusinesscenter.com)		(<4>#00000000 	)(<5>#%(<6>QA1)%)]
            Regex reg = new Regex(@"(^[#\s]*)((\d{1,3}\.){3}\d{1,3})\s*(" + domain + @")\s*(#[^#\r\n%]*)?(\s*#%([^%]*)%)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string newTargetStr = string.IsNullOrWhiteSpace(newTargetTypeStr) ? "" : "\t\t" + newTargetTypeStr;
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), "${1}"+newIpAdress+"\t\t${4}\t\t${5} " + "#%" + newTargetTypeStr + "%"));
            if (writetofile)
                WriteToFile();
        }

        /// <summary>
        /// modify the host by delete the line first and append new host line
        /// </summary>
        /// <param name="domain">Domain name</param>
        /// <param name="newip">ip address</param>
        /// <param name="newremark">remark for the domain</param>
        /// <param name="targetTypeStr">the target type of the domain</param>
        /// <param name="isenable">a value indicate whether the new host is enabled</param>
        /// <param name="writetofile">a value indicate whether to apply changes to the hosts file immediately</param>
        public void ChangeLineByDomain(string domain, string newip, string newremark, string targetTypeStr, bool isenable, bool writetofile)
        {
            if (newremark == null) newremark = string.Empty;
            newremark = newremark.Trim('#');
            if (newremark.IndexOf('#') >= 0)
                throw new ArgumentException("Invalid remark string, remark string can not contain '#'");
            if (targetTypeStr == null) targetTypeStr = string.Empty;
            targetTypeStr = targetTypeStr.Trim(new char[] { '%', '#' });

            if (targetTypeStr.LastIndexOf("%") >= 0)
            {
                throw new ArgumentException("invalid new targetType string");
            }
            DeleteLineByDomain(domain, false);
            if (!string.IsNullOrWhiteSpace(newip))
            {
                string enableStr = isenable ? "" : "#";
                string newline = string.Format("{0}{1}\t\t{2}\t\t{3}\t\t{4}", enableStr, newip, domain, "#"+newremark, "#%"+targetTypeStr+"%");
                HostsStr.AppendLine(newline);
            }
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
            Regex reg = new Regex(@"^[#\s\.\d]*" + domain + ".*\r?\n", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            HostsStr = new StringBuilder(reg.Replace(HostsStr.ToString(), ""));
            if (writetofile)
                WriteToFile();
        }

        public IEnumerable<string> GetAllLines()
        {
            return File.ReadLines(HostPath);
        }

        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(HostPath, false))
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

        public void ChangeIpAndRemark(string domain, string oldIp, string newIp, Model.HostRemark remak, bool writetofile)
        {
            throw new NotImplementedException();
        }

        public void ChangeLineByDomain(string domain, string newip, Model.HostRemark newremark, bool isenable, bool writetofile)
        {
            throw new NotImplementedException();
        }

        public void ChangeRemark(string ip, string domain, Model.HostRemark newremark, bool writetofile)
        {
            throw new NotImplementedException();
        }

        public void FormatHost(bool writetofile)
        {
            throw new NotImplementedException();
        }

        public void ChangeDomainAndRemark(string oldDomain, string ip, string newDomain, Model.HostRemark remark, bool writetofile)
        {
            throw new NotImplementedException();
        }

        public void FormatHost(Model.Recognizer recognizer, bool writetofile)
        {
            throw new NotImplementedException();
        }
    }
}
