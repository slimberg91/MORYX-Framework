namespace Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates
{
    internal class DriverState3 : DriverBaseState
    {
        public DriverState3(TestDriver context, StateMap stateMap) : base(context, stateMap)
        {
        }

        internal override void Receive()
        {
            base.Receive();
        }

        internal override void AnotherCall()
        {

        }
    }
}