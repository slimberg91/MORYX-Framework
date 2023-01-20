using Moryx.StateMachines;
using System.Threading;

namespace Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates
{
    internal abstract class ResourceBaseState : StateBase<TestResource>
    {
        
        protected ResourceBaseState(TestResource context, StateMap stateMap) : base(context, stateMap)
        {
          
        }

        internal virtual void OnDriverMessageReceived()
        {
            System.Console.WriteLine("Message was received");
                      
        }

        internal virtual void StartProducing()
        {
            NextState(StateProducing);
            Context.Driver.AnotherCallToTheDriver();
        }

        [StateDefinition(typeof(ResourceState1), IsInitial = true)]
        protected const int StateIdle = 10;

        [StateDefinition(typeof(ResourceState2))]
        protected const int StateProducing = 20;

        [StateDefinition(typeof(ResourceState3))]
        protected const int StateConnected = 30;
    }
}
