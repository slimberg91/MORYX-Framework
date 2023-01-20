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
        internal DriverStateBaseProxy State = new DriverStateBaseProxy();



        public void SetState(IState state)
        {
           State.SetState(state);
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

            State.AnotherCall();
            
        }
        public void CreateDeadlock()
        {

                State.Receive();              

        }

        internal void RaiseReceivedEvent(object e)
        {
            Received?.Invoke(this, e);
        }

        internal void RaiseFoo2Event(object e)
        {
            Foo2?.Invoke(this, e);
        }
    }
}
