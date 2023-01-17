namespace Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates
{
    internal class ResourceState3 : ResourceBaseState
    {
        public ResourceState3(TestResource context, StateMap stateMap) : base(context, stateMap)
        {
        }

        internal override void OnDriverMessageReceived()
        {
            base.OnDriverMessageReceived();
        }

        internal override void Whatever()
        {

        }
    }
}