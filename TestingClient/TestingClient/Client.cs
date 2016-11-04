using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingClient
{
    [TestClass]
    public class Client
    {
        [TestInitialize]
        public void Initialize()
        {
            WebMeetingService.WebMeetingService service = new WebMeetingService.WebMeetingService();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
