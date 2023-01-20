using Moryx.Modules;
using Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates;
using Moryx.StateMachines;
using System;

namespace Moryx.Runtime.Tests.ResourcesDrivers
{
    public class TestResource : IStateContext, IInitializablePlugin
    {
        public event EventHandler<object> ResourceEvent;
        internal ResourceStateBaseProxy State = new ResourceStateBaseProxy();
        public TestDriver Driver { get; set; }

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

            State.OnDriverMessageReceived();

        }

        public void SetState(IState state)
        {
            State.SetState(state);
        }

        public void StartProduction()
        {

            State.StartProducing();

        }
    }
}
