using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyInfoGetter;
using System.Reflection;

namespace TestAssemblyInfoGetter
{
    [TestClass]
    public class AssemblyInfoResult
    {
        public int Num;

        [TestMethod]
        public void TestAssemblyName()
        {
            AssemblyGetter infoGetter = new AssemblyGetter();

            infoGetter.LoadAssembly("TestAssemblyInfoGetter.dll");
            var result = infoGetter.GetAssemblyInfo();

            Assembly asm = Assembly.GetExecutingAssembly();
            Assert.AreEqual(asm.FullName, result.Name);
        }

        [TestMethod]
        public void TestDataTypeName()
        {
            AssemblyGetter infoGetter = new AssemblyGetter();

            infoGetter.LoadAssembly("TestAssemblyInfoGetter.dll");
            var result = infoGetter.GetAssemblyInfo();

            Assert.AreEqual("AssemblyInfoResult", result.DataTypes[0].Name);
        }

        [TestMethod]
        public void TestMethodsInfoName()
        {
            AssemblyGetter infoGetter = new AssemblyGetter();

            infoGetter.LoadAssembly("TestAssemblyInfoGetter.dll");
            var result = infoGetter.GetAssemblyInfo();

            Assert.AreEqual("TestAssemblyName", result.DataTypes[0].Methods[0].Name);
        }

        [TestMethod]
        public void TestFieldInfoName()
        {
            AssemblyGetter infoGetter = new AssemblyGetter();

            infoGetter.LoadAssembly("TestAssemblyInfoGetter.dll");
            var result = infoGetter.GetAssemblyInfo();

            Assert.AreEqual("Num", result.DataTypes[0].Fields[0].Name);
        }

        [TestMethod]
        public void TestFieldInfoType()
        {
            AssemblyGetter infoGetter = new AssemblyGetter();

            infoGetter.LoadAssembly("TestAssemblyInfoGetter.dll");
            var result = infoGetter.GetAssemblyInfo();

            Assert.AreEqual(typeof(int), result.DataTypes[0].Fields[0].Type);
        }
    }
}
