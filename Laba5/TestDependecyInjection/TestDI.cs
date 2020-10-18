using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyInjectionContainer;
using System.Collections;
using System.Collections.Generic;

namespace TestDependecyInjection
{
    [TestClass]
    public class TestDI
    {

        [TestMethod]
        public void TestResolver()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IMessage, MailMessage>();

            var provider = new DependencyProvider(configurations);

            var message = provider.Resolver<IMessage>();
            Assert.AreEqual("Mail message", message.Send());
        }

        [TestMethod]
        public void TestSingleton()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterSingleton<IMessage, MailMessage>();

            var provider = new DependencyProvider(configurations);

            var message = provider.Resolver<IMessage>();
            message.Send();
            message = provider.Resolver<IMessage>();
            message.Send();
            Assert.AreEqual(2, message.Counter);
        }

        [TestMethod]
        public void TestTransient()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IMessage, MailMessage>();

            var provider = new DependencyProvider(configurations);

            var message = provider.Resolver<IMessage>();
            message.Send();
            message = provider.Resolver<IMessage>();
            message.Send();
            Assert.AreEqual(1, message.Counter);
        }

        [TestMethod]
        public void TestRecursiveCreation()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterSingleton<IMessage, SmsMessage>();
            configurations.RegisterSingleton<IPhone, Samsung>();

            var provider = new DependencyProvider(configurations);

            var message = provider.Resolver<IMessage>();
            Assert.AreEqual("Samsung Galaxy A40", ((SmsMessage)message).Phone.Name);
        }

        [TestMethod]
        public void TestIEnumerable()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IMessage, SmsMessage>();
            configurations.RegisterTransient<IMessage, MailMessage>();

            var provider = new DependencyProvider(configurations);

            IEnumerable<IMessage> messages = provider.Resolver<IEnumerable<IMessage>>();

            int i = 0;
            foreach(var message in messages)
            {
                if (i == 0)
                    Assert.AreEqual("Message from sms", message.Send());
                if (i == 1)
                    Assert.AreEqual("Mail message", message.Send());
                i++;
            }

        }

        [TestMethod]
        public void TestConstructor()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<Person, Person>();

            var provider = new DependencyProvider(configurations);
            var person = provider.Resolver<Person>();

            Assert.AreEqual(0, person.Age);
        }

        [TestMethod]
        public void TestGenericImplemetation()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IList, List<int>>();

            var provider = new DependencyProvider(configurations);
            IList list = provider.Resolver<IList>();

            list.Add(10);
            Assert.AreEqual(10, list[0]);
        }

        [TestMethod]
        public void TestGenericService()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IRepository, MySQLRepository>();
            configurations.RegisterTransient<IService<IRepository>, Google<IRepository>>();

            var provider = new DependencyProvider(configurations);

            IRepository repository = provider.Resolver<IRepository>();
            var google = provider.Resolver<IService<IRepository>>();

            Assert.AreEqual("Google: MySQL request", 
                google.UseRepository(provider.Resolver<IRepository>()));
        }

        [TestMethod]
        public void TestOpenGeneric()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IRepository, MySQLRepository>();
            configurations.RegisterTransient<IMongoDB, MongoDB>();
            configurations.Register(typeof(IService<>), typeof(Google<>));

            var provider = new DependencyProvider(configurations);

            var google = provider.Resolver<IService<IMongoDB>>();
            Assert.AreEqual("Google: MongoDB request", google.UseRepository(provider.Resolver<IMongoDB>()));
            Assert.AreEqual("Google: MongoDB request", google.UseLocalRepository());
        }
    }
}
