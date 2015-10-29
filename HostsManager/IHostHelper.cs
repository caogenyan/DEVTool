using System;
using Model;
namespace HostsManager
{
    interface IHostHelper
    {
        void ChangeDomain(string ip, string olddomain, string newdomain, bool writetofile);
        //void ChangeDomain(Model.HostDNS dns,string oldDomainName, bool writetofile);

        void ChangeDomainAndRemark(string oldDomain, string ip, string newDomain, Model.HostRemark remark, bool writetofile);
        //void ChangeDomainAndRemark(Model.HostDNS dns,string oldDomain, bool writetofile);

        void ChangeIp(string oldip, string newip, string domain, bool writetofile);
        void ChangeIpAndRemark(string domain, string oldIp, string newIp, Model.HostRemark remak, bool writetofile);
        void ChangeIpAndTargetType(string domain, string newIpAdress, string newTargetTypeStr, bool writetofile);
        void ChangeLineByDomain(string domain, string newip, Model.HostRemark newremark, bool isenable, bool writetofile);
        void ChangeLineByDomain(string domain, string newip, string newremark, string targetTypeStr, bool isenable, bool writetofile);
        void ChangeRemark(string ip, string domain, Model.HostRemark newremark, bool writetofile);
        void ChangeRemark(string ip, string domain, string newremark, bool writetofile);
        void ChangeTargetType(string ip, string domain, string newTargetTypeStr, bool writetofile);
        void DeleteLineByDomain(string domain, bool writetofile);
        void Disableip(string ip, bool writetofile);
        void Enableip(string ip, bool writetofile);
        int FindLinesStartWithip(string ip);
        void FormatHost(Model.Recognizer recognizer, bool writetofile);
        void FormatHost(bool writetofile);
        System.Collections.Generic.IEnumerable<string> GetAllLines();
        System.Text.StringBuilder HostsStr { get; }
        bool IsipDomainEnable(string ip, string domain);
        bool IsipDomainExists(string ip, string domain);
        void WriteToFile();
        void WriteToFile(string[] lines, bool isappend);
    }
}
