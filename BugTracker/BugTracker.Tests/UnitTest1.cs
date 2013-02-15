using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace BugTracker.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var n = new Regex(@"[A-Za-z0-9]{0,100}");
            var res = n.Match("admin");
        }
    }
}
