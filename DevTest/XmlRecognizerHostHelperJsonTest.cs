using Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevTest
{
    
    
    /// <summary>
    ///This is a test class for XmlRecognizerHostHelperJsonTest and is intended
    ///to contain all XmlRecognizerHostHelperJsonTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlRecognizerHostHelperJsonTest
    {


        private TestContext testContextInstance;

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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for XmlRecognizer Constructor
        ///</summary>
        [TestMethod()]
        public void XmlRecognizerConstructorTest()
        {
        }

        /// <summary>
        ///A test for GetSiteType
        ///</summary>
        [TestMethod()]
        public void GetSiteTypeTest()
        {
            XmlRecognizer target = new XmlRecognizer();
            string domain = "misc.db.qa.elmae";
            SiteType expected = SiteType.ELM;
            SiteType actual;
            actual = target.GetSiteType(domain);
            Assert.AreEqual(expected, actual);
        }
    }
}
