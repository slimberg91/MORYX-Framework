using Moryx.Modules;
using Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates;
using Moryx.StateMachines;
using System;

namespace Moryx.Runtime.Tests.ResourcesDrivers
{
    public class TestResource : IStateContext, IInitializablePlugin
    {
        public event EventHandler<object> ResourceEvent;
        internal ResourceBaseState State { get; set; }
        public TestDriver Driver { get; set; }

        /// <summary>
        /// Lock object to lock state handling
        /// </summary>
        private readonly object StateLock = new object();

        public void Initialize()
        {
            StateMachine.Initialize(this).With<ResourceBaseState>();
        }  
       
        public void Start()
        {
            Driver.Received += OnDriverMessageReceived;           
        }

        public void Stop()
        {
            Driver.Received -= OnDriverMessageReceived;
        }

        private void OnDriverMessageReceived(object sender, object e)
        {
            lock (StateLock)
            {
                State.OnDriverMessageReceived();
            }
        }

        public void SetState(IState state)
        {
            State = (ResourceBaseState)state;
        }

        public void StartProduction()
        {
            lock (StateLock)
            {
                State.StartProducing();
            }
        }     
    }
}
