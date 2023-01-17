using Moryx.StateMachines;
using System.Threading;

namespace Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates
{
    internal abstract class ResourceBaseState : StateBase<TestResource>
    {
        protected TestResource Resource;
        protected ResourceBaseState(TestResource context, StateMap stateMap) : base(context, stateMap)
        {
            Resource= context;
        }

        internal virtual void OnDriverMessageReceived()
        {
            System.Console.WriteLine("Message was received");
                      
        }

        internal virtual void StartProducing()
        {
            NextState(StateProducing);
            Resource.StartProduction();
        }

        [StateDefinition(typeof(ResourceState1), IsInitial = true)]
        protected const int StateIdle = 10;

        [StateDefinition(typeof(ResourceState2))]
        protected const int StateProducing = 20;

        [StateDefinition(typeof(ResourceState3))]
        protected const int StateConnected = 30;
    }
}
