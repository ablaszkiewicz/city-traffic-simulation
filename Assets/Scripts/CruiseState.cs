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
            engine.SetCanMove(true);
        }

        public override void Tick()
        {
            Debug.Log("Tick cruise state");
            if (proximitySensor.GetDistanceToClosestObjectOnPath() < STOP_THRESHOLD)
            {
                stateMachine.ChangeState(State.STOPPED);
            }
        }
    }
}