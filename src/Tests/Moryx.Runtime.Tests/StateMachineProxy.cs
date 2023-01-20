using Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates;
using Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates;
using Moryx.StateMachines;
using System;

namespace Moryx.Runtime.Tests
{
    public abstract class StateBaseProxy<TState> where TState : StateBase
    {
        /// <summary>
        /// Lock object to lock state handling
        /// </summary>
        private readonly object StateLock = new object();

        public TState State { get; private set; }

        public Action RaiseStateChangedEvent { get; set; }

        private bool StateChanged = false;
        public virtual void SetState(IState state)
        {
          
            lock (StateLock)
            {
                State = (TState)state;
                StateChanged = true;
            }
            
        }

        protected void LockedCall(Action action)
        {
            var stateChanged = false;
            lock (StateLock)
            {
                action();
                stateChanged = StateChanged;
                StateChanged = false;
            }

            // statte eines dictionaries eine Eventqueue im proxy erzeugen, der steps events hinzufügen können
            // manche steps werfen mehrere events oder unterschiedliche nach bedingung
            // diser Queue wird auch das stateChanged event hinzugefügt
            // die Queue ist gelocked (genauso wie StateChanged)
            var methodName = action.Method.Name;
            if (State.events.ContainsKey(methodName))
                State.events[methodName]();

            if(stateChanged && RaiseStateChangedEvent != null)
                RaiseStateChangedEvent();
        }

    }

    internal class DriverStateBaseProxy: StateBaseProxy<DriverBaseState>
    {
        public void Receive()
        {
            LockedCall(State.Receive);
        }

        public void AnotherCall()
        {
            LockedCall(() =>
            {
                Console.WriteLine("NoDeadlock");
            });
        }

    }

    internal class ResourceStateBaseProxy: StateBaseProxy<ResourceBaseState>
    {

        internal void StartProducing()
        {
            LockedCall(State.StartProducing);
        }

        internal void OnDriverMessageReceived()
        {
            LockedCall(State.OnDriverMessageReceived);
        }
    }

}
