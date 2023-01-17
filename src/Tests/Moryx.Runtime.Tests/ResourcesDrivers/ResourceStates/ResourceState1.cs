namespace Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates
{
    internal class ResourceState1 : ResourceBaseState
    {
        public ResourceState1(TestResource context, StateMap stateMap) : base(context, stateMap)
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