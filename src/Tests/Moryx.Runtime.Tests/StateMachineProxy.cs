using Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates;
using Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates;
using Moryx.StateMachines;
using System;
using System.Collections.Generic;

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

        private List<Action> _actionsToBeDoneAfterTheLock = new List<Action>();
        public virtual void SetState(IState state)
        {
          
            lock (StateLock)
            {
                State = (TState)state;
                State.AddActionToBeDoneAfterLock = AddActionToBeDoneAfterLock;
                if(RaiseStateChangedEvent != null)
                    _actionsToBeDoneAfterTheLock.Add(RaiseStateChangedEvent);
            }
            
        }

        public void AddActionToBeDoneAfterLock(Action action)
        {
            _actionsToBeDoneAfterTheLock.Add(action);
        }

        protected void LockedCall(Action action)
        {
            var actionsToBeDone = new List<Action>();
            lock (StateLock)
            {               
                action();
                actionsToBeDone = new List<Action>(_actionsToBeDoneAfterTheLock);
                _actionsToBeDoneAfterTheLock = new List<Action>();
            }
            foreach(var act in actionsToBeDone)
            {
                act.Invoke();
            }
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
