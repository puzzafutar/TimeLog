using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeLog.Service.Factory;

namespace TimeLog.Service.Factory.Tests
{
    [TestClass()]
    public class TimeLogFactoryTests
    {
        [TestMethod()]
        public void GetDeleteInstanceAsyncTest()
        {
            TimeLogFactory timeLogFactory = new TimeLogFactory();
            var obj = timeLogFactory.GetDeleteInstanceAsync(1, "abc").Result;
            Assert.IsTrue(obj != null && obj.Action == Domain.Action.Delete && obj.TimerId == 1);
        }

        [TestMethod()]
        public void GetModifiedInstanceAsyncTest()
        {
            TimeLogFactory timeLogFactory = new TimeLogFactory();
            var obj = timeLogFactory.GetModifiedInstanceAsync(2, "abc").Result;
            Assert.IsTrue(obj != null && obj.Action == Domain.Action.Modify && obj.TimerId == 2);
        }

        [TestMethod()]
        public void GetInActiveInstanceAsyncTest()
        {
            TimeLogFactory timeLogFactory = new TimeLogFactory();
            var obj = timeLogFactory.GetInActiveInstanceAsync(3, "abc").Result;
            Assert.IsTrue(obj != null && obj.Action == Domain.Action.Stop && obj.TimerId == 3);
        }
    }
}