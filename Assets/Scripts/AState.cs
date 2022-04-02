namespace Assets.Scripts
{
    public abstract class AState
    {
        protected ProximitySensor proximitySensor;
        protected StateMachine stateMachine;
        protected Engine engine;
        
        protected AState(ProximitySensor proximitySensor, StateMachine stateMachine, Engine engine)
        {
            this.proximitySensor = proximitySensor;
            this.stateMachine = stateMachine;
            this.engine = engine;
            OnEnter();
        }

        public abstract void OnEnter();
        public abstract void Tick();
    }
}