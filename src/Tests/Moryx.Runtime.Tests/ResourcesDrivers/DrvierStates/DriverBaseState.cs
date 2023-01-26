using Moryx.StateMachines;
using System;
using System.Threading;

namespace Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates
{
    internal abstract class DriverBaseState : StateBase<TestDriver>
    {
       
      
        protected DriverBaseState(TestDriver context, StateMap stateMap) : base(context, stateMap)
        {
                     
        }

        internal virtual void Receive()
        {
            NextState(StateConnecting);
            Thread.Sleep(5000);
            AddActionToBeDoneAfterLock?.Invoke(() => { Context.RaiseReceivedEvent(null); });
        }

        internal virtual void AnotherCall()
        {
            Console.WriteLine("NoDeadlock");
        }

        [StateDefinition(typeof(DriverState1), IsInitial = true)]
        protected const int StateDisconnected = 10;

        [StateDefinition(typeof(DriverState2))]
        protected const int StateConnecting = 20;

        [StateDefinition(typeof(DriverState3))]
        protected const int StateConnected = 30;
    }
}
