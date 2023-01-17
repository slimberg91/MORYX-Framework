using Moryx.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates
{
    internal abstract class DriverBaseState : StateBase<TestDriver>
    {

        private TestDriver Driver;
        protected DriverBaseState(TestDriver context, StateMap stateMap) : base(context, stateMap)
        {
            Driver = context;
        }

        internal virtual void Receive()
        {
            NextState(StateConnecting);
        }

        internal virtual void Foo()
        {

        }

        [StateDefinition(typeof(DriverState1), IsInitial = true)]
        protected const int StateDisconnected = 10;

        [StateDefinition(typeof(DriverState2))]
        protected const int StateConnecting = 20;

        [StateDefinition(typeof(DriverState3))]
        protected const int StateConnected = 30;
    }
}
