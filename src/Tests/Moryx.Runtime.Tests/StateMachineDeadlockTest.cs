using Moq;
using Moryx.Configuration;
using Moryx.Runtime.Kernel;
using Moryx.Runtime.Tests.Mocks;
using Moryx.Runtime.Tests.Modules;
using Moryx.Runtime.Tests.ResourcesDrivers;
using NUnit.Framework;

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

            _driver.CreateDeadlock();

            _resource.Stop();
            _driver.Stop();

        }
    }
}
