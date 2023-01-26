using Moryx.Runtime.Tests.ResourcesDrivers;
using Moryx.Runtime.Tests.ResourcesDrivers.DrvierStates;
using Moryx.Runtime.Tests.ResourcesDrivers.ResourceStates;
using Moryx.StateMachines;
using System;
using System.Collections.Generic;

namespace Moryx.Runtime.Tests
{
    // Everything in this class comes into the proxy... So write that once again in IL
    // In order for the proxy being the only one knowing the SetStateMethod, I either need a new interface or the proxy implements
    // IStateContext. If the proxy implments IStateContext, I have to find a way for the steps to still know the Driver...
    // And when the real context doesn't implement IStateContext, I have to change IState<TContext> where TContext:IStateContext to where TContext:object
    public class StateContainer<TState> where TState : StateBase
    {
        /// <summary>
        /// Lock object to lock state handling
        /// </summary>
        private readonly object StateLock = new object();

        public TState State { get; private set; }

        //wie soll das bitteschön gesetzt werden wenn dem context nur der schritt bekannt ist
        public Action RaiseStateChangedEvent { get; set; }

        private List<Action> _actionsToBeDoneAfterTheLock = new List<Action>();
        public virtual void SetState(IState state)
        {
          
            lock (StateLock)
            {
                var castedState = (TState)state;
                castedState.AddActionToBeDoneAfterLock = AddActionToBeDoneAfterLock;
                State = StateMachineProxyBuilder.BuildStateProxy<TState>(castedState, LockedCall);
               
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

    internal class DriverStateBaseProxy: DriverBaseState, IStateContext
    {
        public DriverStateBaseProxy(TestDriver context, StateMap stateMap) : base(context, null)
        {
        }

        /// <summary>
        /// Lock object to lock state handling
        /// </summary>
        private readonly object StateLock = new object();

        private DriverBaseState _state;

        // This cannot be set by the Context anymore, if the context knows the Proxy as DriverBaseState
        public Action RaiseStateChangedEvent { get; set; }

        private List<Action> _actionsToBeDoneAfterTheLock = new List<Action>();

        // only if a proxy is used, this method should be called during NextState()
        public virtual void SetState(IState state)
        {

            lock (StateLock)
            {
                var castedState = (DriverBaseState)state;
                //this has to be done when building the state machine
                //castedState.AddActionToBeDoneAfterLock = AddActionToBeDoneAfterLock;

                _state = castedState;
                if (RaiseStateChangedEvent != null)
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
            foreach (var act in actionsToBeDone)
            {
                act.Invoke();
            }
        }

        internal override void AnotherCall()
        {
            LockedCall(_state.AnotherCall);
        }

        internal override void Receive()
        {
            LockedCall(_state.Receive);
        }
    }

    internal class ResourceStateBaseProxy: ResourceBaseState
    {
        public ResourceStateBaseProxy(TestResource context, StateMap stateMap) : base(context, stateMap)
        {
        }

        private Action<Action> _lockedCall;

        private ResourceBaseState _state;

        public ResourceStateBaseProxy(ResourceBaseState state, Action<Action> lockedCall) : base(null, null)
        {
            _state = state;
            _lockedCall = lockedCall;
        }

        internal override void OnDriverMessageReceived()
        {
            _lockedCall(_state.OnDriverMessageReceived);
        }

        internal override void StartProducing()
        {
            _lockedCall(_state.StartProducing);
        }
    }

}
