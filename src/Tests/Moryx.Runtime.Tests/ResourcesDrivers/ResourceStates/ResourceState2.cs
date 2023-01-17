namespace Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates
{
    internal class ResourceState2 : ResourceBaseState
    {
        public ResourceState2(TestResource context, StateMap stateMap) : base(context, stateMap)
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