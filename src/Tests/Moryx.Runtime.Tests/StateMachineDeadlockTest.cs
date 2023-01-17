using Moryx.Runtime.Tests.ResourcesDrivers;
using NUnit.Framework;
using System.Threading;

namespace Moryx.Runtime.Tests
{
    [TestFixture]
    public class StateMachineDeadlockTest
    {

        private TestResource _resource;
        private TestDriver _driver;

        [SetUp]
        public void SetUp() {
            _driver = new TestDriver();
            _resource = new TestResource();
            _resource.Driver = _driver;       
        }
        
        [Test]
        public void Deadlock() {
            _resource.Initialize();
            _resource.Start();
            _driver.Initialize();
            _driver.Start();
            Thread trd = new Thread(new ThreadStart(_driver.CreateDeadlock));
            trd.Start();
            Thread.Sleep(500);
            Thread trd2 = new Thread(new ThreadStart(_resource.StartProduction));
            trd2.Start();

            _resource.Stop();
            _driver.Stop();

        }
    }
}
