using Moryx.Modules;
using Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates;
using Moryx.StateMachines;
using System;

namespace Moryx.Runtime.Tests.ResourcesDrivers
{
    public class TestResource : IStateContext, IInitializablePlugin
    {
        public event EventHandler<object> ResourceEvent;
        internal StateContainer<ResourceBaseState> StateContainer = new();
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

            StateContainer.State.OnDriverMessageReceived();

        }

        public void SetState(IState state)
        {
            StateContainer.SetState(state);
        }

        public void StartProduction()
        {

            StateContainer.State.StartProducing();

        }
    }
}
