using System.Linq;
using Character.View.Npc.States;
using UnityEngine;

namespace Character.View.Npc
{
    public class StateMachine
    {
        public bool IsReadyToStart { get; private set; }
        
        private IState state;
        private readonly ICharacterView characterView;
        private WalkState initialState;

        public StateMachine(ICharacterView characterView)
        {
            initialState = new WalkState();
            initialState.Start(characterView);
            
            this.characterView = characterView;
        }

        public void Start()
        {
            initialState = null;
            state.Start(characterView);
        }

        public void Update()
        {
            if (initialState != null)
            {
                if (!IsReadyToStart && initialState.Execute(characterView))
                {
                    IsReadyToStart = true;
                    Debug.LogWarning("READY");
                    state = GetNextState(initialState);
                }
                return;
            }
            
            if (state.Execute(characterView))
            {
                Debug.LogWarning($"State {state.GetType()} done.");
                state = GetNextState(state);
                state.Start(characterView);
                Debug.LogWarning($"State {state.GetType()} is the next state.");
            }
        }

        private IState GetNextState(IState state)
        {
            var rValue = Random.value;
            var counter = 0f;
            var nextStates = state.NextStates;
            foreach (var nextState in nextStates)
            {
                counter += nextState.Probability;
                if (rValue <= counter)
                    return nextState.State;
            }

            return nextStates.Last().State;
        }
    }
}