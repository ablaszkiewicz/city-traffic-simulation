using UnityEngine;

namespace Assets.Scripts
{
    public class StateMachine : MonoBehaviour
    {
        private AState state;
        private ProximitySensor proximitySensor;
        private Engine engine;
        private State currentStateEnum;
        
        private SettingsScriptableObject settingsScriptableObject;

        public State CurrentState => currentStateEnum;
        private CruiseState cruiseState;
        private StoppedState stoppedState;

        private void Start()
        {
            settingsScriptableObject = FindObjectOfType<JsonSerializer>().SettingsScriptableObject;
            proximitySensor = GetComponent<ProximitySensor>();
            engine = GetComponent<Engine>();
            InitializeStates();
            ChangeState(State.CRUISE);
        }

        public void Update()
        {
            state.Tick();
        }

        private void InitializeStates()
        {
            cruiseState = new CruiseState(proximitySensor, this, engine);
            stoppedState = new StoppedState(proximitySensor, this, engine, settingsScriptableObject.GetDelay());
        }

        public void ChangeState(State newState)
        {
            switch (newState)
            {
                case State.CRUISE:
                    state = cruiseState;
                    break;
                case State.STOPPED:
                    state = stoppedState;
                    break;
            }

            state.OnEnter();
            currentStateEnum = newState;
        }
    }
}