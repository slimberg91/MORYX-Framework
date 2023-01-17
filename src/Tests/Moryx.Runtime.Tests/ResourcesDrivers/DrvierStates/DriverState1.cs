namespace Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates
{
    internal class DriverState1 : DriverBaseState
    {
        public DriverState1(TestDriver context, StateMap stateMap) : base(context, stateMap)
        {
        }

        internal override void Receive()
        {
            base.Receive();
        }

        internal override void Foo()
        {

        }
    }
}