using HostsManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Model;
using System.Collections.Generic;

namespace DevTest
{


    /// <summary>
    ///This is a test class for HostHelperJsonHostHelperJsonTest and is intended
    ///to contain all HostHelperJsonHostHelperJsonTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HostHelperJsonHostHelperJsonTest
    {


        private TestContext testContextInstance;
        private static HostHelperJson target;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            target = new HostHelperJson();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            //to reset the filecontent
        }

        #endregion

        /// <summary>
        ///A test for ChangeDomain
        ///</summary>
        [TestMethod()]
        public void ChangeDomainTest()
        {
            string ip = "10.81.152.31";
            string olddomain = "emims.db.qa.elmae";
            string newdomain = "www.tpoyemol.com";
            bool writetofile = true;
            target.ChangeDomain(ip, olddomain, newdomain, writetofile);
        }

        /// <summary>
        ///A test for ChangeDomainAndRemark
        ///</summary>
        [TestMethod()]
        public void ChangeDomainAndRemarkTest()
        {
            string oldDomain = "backoffice.db.qa.elmae";
            string ip = "10.81.152.31";
            string newDomain = "emims.db.qa.elmae";
            HostRemark remark = new HostRemark() { SiteType = SiteType.UNK, Target = TargetType.QA1, Comment = "emims data base." };
            bool writetofile = true;
            target.ChangeDomainAndRemark(oldDomain, ip, newDomain, remark, writetofile);
        }

        /// <summary>
        ///A test for ChangeIp
        ///</summary>
        [TestMethod()]
        public void ChangeIpTest()
        {
            string oldip = "127.0.0.1";
            string newip = "70.42.161.97";
            string domain = "compufund.db.qa.elmae";
            bool writetofile = true;
            target.ChangeIp(oldip, newip, domain, writetofile);
        }

        /// <summary>
        ///A test for ChangeIpAndRemark
        ///</summary>
        [TestMethod()]
        public void ChangeIpAndRemarkTest()
        {
            string domain = "compufund.db.qa.elmae";
            string oldIp = "222.222.222.222";
            string newIp = "127.0.0.1";
            HostRemark remak = new HostRemark() { SiteType = SiteType.TPO, Target = TargetType.QA1, Comment = "www.tpoyemol.com" };
            bool writetofile = true;
            target.ChangeIpAndRemark(domain, oldIp, newIp, remak, writetofile);
        }

        /// <summary>
        ///A test for ChangeLineByDomain
        ///</summary>
        [TestMethod()]
        public void ChangeLineByDomainTest()
        {
            string domain = "docsengine.elliemae.com";
            string newip = "222.222.222.222";
            HostRemark newremark = new HostRemark() { SiteType = SiteType.TPO, Target = TargetType.QA1, Comment = "www.tpoyemol.com" };
            bool isenable = false;
            bool writetofile = true;
            target.ChangeLineByDomain(domain, newip, newremark, isenable, writetofile);
        }

        /// <summary>
        ///A test for ChangeRemark
        ///</summary>
        [TestMethod()]
        public void ChangeRemarkTest()
        {
            string ip = "10.81.152.31";
            string domain = "misc.db.qa.elmae";
            HostRemark newremark = new HostRemark() { SiteType = SiteType.TPO, Target = TargetType.QA1, Comment = "localhost" };
            bool writetofile = true;
            target.ChangeRemark(ip, domain, newremark, writetofile);
        }

        /// <summary>
        ///A test for DeleteLineByDomain
        ///</summary>
        [TestMethod()]
        public void DeleteLineByDomainTest()
        {
            string domain = "encompass.elliemae.com";
            bool writetofile = true;
            target.DeleteLineByDomain(domain, writetofile);
        }

        /// <summary>
        ///A test for DisableDomain
        ///</summary>
        [TestMethod()]
        public void DisableDomainTest()
        {
            string domainName = "encompass.elliemae.com";
            bool writetoFile = true;
            target.DisableDomain(domainName, writetoFile);
        }

        /// <summary>
        ///A test for DisableIpAndDomain
        ///</summary>
        [TestMethod()]
        public void DisableIpAndDomainTest()
        {
            string ip = "10.81.152.31";
            string domainName = "edm.db.qa.elmae";
            bool writetoFile = true;
            target.DisableByIpAndDomain(ip, domainName, writetoFile);
        }

        /// <summary>
        ///A test for Disableip
        ///</summary>
        [TestMethod()]
        public void DisableipTest()
        {
            string ip = "10.81.152.31";
            bool writetofile = true;
            target.Disableip(ip, writetofile);
        }

        /// <summary>
        ///A test for EnableDomain
        ///</summary>
        [TestMethod()]
        public void EnableDomainTest()
        {
            string domainName = "encompass.elliemae.com";
            bool writetoFile = true;
            target.EnableDomain(domainName, writetoFile);
        }

        /// <summary>
        ///A test for EnableIpAndDomain
        ///</summary>
        [TestMethod()]
        public void EnableIpAndDomainTest()
        {
            string ip = "10.81.152.31";
            string domainName = "web.db.qa.elmae";
            bool writetoFile = true;
            target.EnableByIpAndDomain(ip, domainName, true);
        }

        /// <summary>
        ///A test for Enableip
        ///</summary>
        [TestMethod()]
        public void EnableipTest()
        {
            string ip = "10.81.152.31";
            bool writetofile = true;
            target.Enableip(ip, writetofile);
        }

        /// <summary>
        ///A test for FindLinesStartWithip
        ///</summary>
        [TestMethod()]
        public void FindLinesStartWithipTest()
        {
            string ip = "70.42.168.48";
            int expected = 1;
            int actual;
            actual = target.FindLinesStartWithip(ip);
            Assert.AreEqual(expected, actual);
            ip = "10.111.100.60";
            expected = 0;
            actual = target.FindLinesStartWithip(ip);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FormatHost
        ///</summary>
        [TestMethod()]
        public void FormatHostTest()
        {
            Recognizer recognizer = new DefaultRecognizer();
            bool writetofile = true;
            target.FormatHost(recognizer, writetofile);
        }

        /// <summary>
        ///A test for IsipDomainEnable
        ///</summary>
        [TestMethod()]
        public void IsipDomainEnableTest()
        {
            string ip = "70.42.168.48";
            string domain = "tq1vsctest01.corp.elmae";
            bool expected = true;
            bool actual;
            actual = target.IsipDomainEnable(ip, domain);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsipDomainExists
        ///</summary>
        [TestMethod()]
        public void IsipDomainExistsTest()
        {
            string ip = "127.0.0.1";
            string domain = "www.topyemol.com";
            bool expected = false;
            bool actual;
            actual = target.IsipDomainExists(ip, domain);
            Assert.AreEqual(expected, actual);
            ip = "70.42.168.48";
            domain = "tq1vsctest01.corp.elmae";
            expected = true;
            actual = target.IsipDomainExists(ip, domain);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteLineTest()
        {
            string ip = "127.0.0.1";
            string domain = "localhost";
            bool isdiabled = false;
            target.DeleteLine(ip, domain, isdiabled, true);
        }
    }
}
