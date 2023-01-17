using Moryx.Modules;
using Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates;
using Moryx.StateMachines;
using System;
using System.Threading;

namespace Moryx.Runtime.Tests.ResourcesDrivers
{
    public class TestDriver: IStateContext, IInitializablePlugin
    {
        public event EventHandler<object> Received;
        public event EventHandler<object> Foo2;
        internal DriverBaseState State { get; set; }

        /// <summary>
        /// Lock object to lock state handling
        /// </summary>
        private readonly object StateLock = new object();

        public void SetState(IState state)
        {
            State = (DriverBaseState)state;
        }

        public void Initialize()
        {
            StateMachine.Initialize(this).With<DriverBaseState>();
        }

        public void Start()
        {
         
        }

        public void Stop()
        {
            
        }

        public void AnotherCallToTheDriver()
        {
            lock (StateLock)
            {
                Console.WriteLine("No Deadlock");
            }
        }
        public void CreateDeadlock()
        {
            lock (StateLock)
            {
                State.Receive();
                Received?.Invoke(this,null);
                Thread.Sleep(5000);
                Console.WriteLine("");
                Foo2?.Invoke(this,null);
            }
        }
    }
}
