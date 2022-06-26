namespace Assets.Scripts
{
    public class StoppedState : AState
    {
        private const float START_THRESHOLD = 1;
        public StoppedState(ProximitySensor proximitySensor, StateMachine stateMachine, Engine engine) : base(proximitySensor,
            stateMachine, engine)
        {
        }

        public override void OnEnter()
        {
            engine.SetCanMove(false);
        }

        public override void Tick()
        {
            if (proximitySensor.GetDistanceToClosestObjectOnPath() > START_THRESHOLD)
            {
                stateMachine.ChangeState(State.CRUISE);
            }
        }
    }
}