using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyTestClass
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void getAgeTest()
        {
            var actual = testObject.getAge();
            var expected = 0;
            Assert.AreEqual(actual, expected);
        }
    }
}