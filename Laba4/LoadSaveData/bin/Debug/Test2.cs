using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyInfoGetter;

namespace MyTestClass
{
    [TestClass]
    public class AssemblyGetterTest
    {
        AssemblyGetter testObject;
        [TestInitialize]
        public void Initialize()
        {
            testObject = (new AssemblyGetter());
        }

        [TestMethod]
        public void LoadAssemblyTest()
        {
            string fileName = default(string);
            testObject.LoadAssembly(fileName);
            Assert.Fail("autogenerated");
        }

        [TestMethod]
        public void GetAssemblyInfoTest()
        {
            var actual = testObject.GetAssemblyInfo();
            var expected = 0;
            Assert.AreEqual(actual, expected);
            Assert.Fail("autogenerated");
        }

        [TestMethod]
        public void GetExtensionMethodsTest()
        {
            var actual = testObject.GetExtensionMethods();
            var expected = 0;
            Assert.AreEqual(actual, expected);
            Assert.Fail("autogenerated");
        }

        [TestMethod]
        public void AddExtensionMethodTest()
        {
            DTO.NamespaceInfo namespaceInfo = default(DTO.NamespaceInfo);
            MethodInfo extensionMethod = default(MethodInfo);
            testObject.AddExtensionMethod(namespaceInfo, extensionMethod);
            Assert.Fail("autogenerated");
        }
    }
}