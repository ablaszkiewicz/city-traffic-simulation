using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class StoppedState : AState
    {
        private const float START_THRESHOLD = 1;

        private bool isBlocked = true;
        private float initialDelay = 0.5f;
        private float currentDelay = 0f;

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
            CheckCondition();
            PossibleStart();
        }

        private void CheckCondition()
        {
            if (proximitySensor.GetDistanceToClosestObjectOnPath() > START_THRESHOLD)
            {
                isBlocked = false;
            }
            else
            {
                isBlocked = true;
                currentDelay = initialDelay;
            }
        }

        private void PossibleStart()
        {
            if (!isBlocked)
            {
                currentDelay -= Time.deltaTime;
                if (currentDelay <= 0)
                {
                    stateMachine.ChangeState(State.CRUISE);
                }
            }
        }
    }
}