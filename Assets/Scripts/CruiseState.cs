using UnityEngine;

namespace Assets.Scripts
{
    public class CruiseState : AState
    {
        private const float STOP_THRESHOLD = 1;
        
        public CruiseState(ProximitySensor proximitySensor, StateMachine stateMachine, Engine engine) : base(proximitySensor,
            stateMachine, engine)
        {
        }

        public override void OnEnter()
        {
            engine.SetMoveSpeed(4);
        }

        public override void Tick()
        {
            if (proximitySensor.GetDistanceToClosestObjectOnPath() < STOP_THRESHOLD)
            {
                stateMachine.ChangeState(State.STOPPED);
            }
        }
    }
}