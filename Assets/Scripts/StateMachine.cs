using UnityEngine;

namespace Assets.Scripts
{
    public class StateMachine : MonoBehaviour
    {
        private AState state;
        private ProximitySensor proximitySensor;
        private Engine engine;
        
        private void Start()
        {
            proximitySensor = GetComponent<ProximitySensor>();
            engine = GetComponent<Engine>();
            ChangeState(State.CRUISE);
        }

        public void Update()
        {
            state.Tick();
        }

        public void ChangeState(State newState)
        {
            Debug.Log($"Changing state to {newState}");
            
            switch (newState)
            {
                case State.CRUISE:
                    state =  new CruiseState(proximitySensor, this, engine);
                    break;
                case State.STOPPED:
                    state = new StoppedState(proximitySensor, this, engine);
                    break;
            }
        }
    }
}