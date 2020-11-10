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
            configurations.RegisterTransient<IRepository, MySQLRepository>();
            configurations.RegisterTransient<IPhone, Samsung>();

            var provider = new DependencyProvider(configurations);

            var phone = provider.Resolver<IPhone>();
            Assert.AreEqual("Samsung Galaxy A40", phone.Name);
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
        public void TestConstructorWithUsualParameter()
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
            configurations.RegisterTransient<IPhone, GenericPhone<int>>();

            var provider = new DependencyProvider(configurations);
            var phone = provider.Resolver<IPhone>();

            Assert.AreEqual("Generic Phone", phone.Name);
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

        [TestMethod]
        public void TestAnotherOpenGeneric()
        {
            var configurations = new DependenciesConfiguration();
            configurations.Register(typeof(GenericPhone<>), typeof(GenericPhone<>));

            var provider = new DependencyProvider(configurations);

            var phone = provider.Resolver<GenericPhone<string>>();
            phone.Users.Add("Eugene");

            Assert.AreEqual("Eugene", phone.Users[0]);
        }


        [TestMethod]
        public void TestDependencyNaming()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IRepository, MySQLRepository>(ImplementationName.First);
            configurations.RegisterTransient<IRepository, MongoDB>(ImplementationName.Second);

            var provider = new DependencyProvider(configurations);

            var mySQL = provider.Resolver<IRepository>(ImplementationName.First);
            var mongoDB = provider.Resolver<IRepository>(ImplementationName.Second);

            Assert.AreEqual("MySQL request", mySQL.SendRequest(""));
            Assert.AreEqual("MongoDB request", mongoDB.SendRequest(""));

        }

        [TestMethod]
        public void TestDependecnyNamingAttribute()
        {
            var configurations = new DependenciesConfiguration();
            configurations.RegisterTransient<IRepository, MySQLRepository>(ImplementationName.Second);
            configurations.RegisterTransient<IRepository, MongoDB>(ImplementationName.First);
            configurations.RegisterTransient<IService<IRepository>, Yandex<IRepository>>();

            var provider = new DependencyProvider(configurations);

            var yandex = provider.Resolver<IService<IRepository>>();

            Assert.AreEqual("Yandex: MongoDB request", yandex.UseLocalRepository());
            
        }
    }
}
