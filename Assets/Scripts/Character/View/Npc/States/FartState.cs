using Character.Fart;
using UnityEngine;

namespace Character.View.Npc.States
{
    public class FartState : IState
    {
        private const float PROBABILITY_OF_QUICK_FART = 0.7f;
        private const float MIN_SECONDS_LONG_FART = 2f;
        
        public NextState[] NextStates => new[]
        {
            new NextState(0.7f, new WalkState()),
            new NextState(0.3f, new IdleState())
        };

        private bool shortFart;
        private float longFartLength;
        private float endTime;
        
        public void Start(ICharacterView characterView)
        {
            shortFart = Random.value <= PROBABILITY_OF_QUICK_FART;
            longFartLength = Random.Range(MIN_SECONDS_LONG_FART, GameConstants.MAX_SECONDS_FARTING);
            endTime = Time.realtimeSinceStartup + longFartLength;
            
            if (!shortFart)
            {
                characterView.StartFarting();
            }
        }

        public bool Execute(ICharacterView characterView)
        {
            if (shortFart)
            {
                FartAction.Get().Execute(characterView);
                return true;
            }

            if (endTime >= Time.realtimeSinceStartup)
            {
                FartAction.Get().Execute(characterView, longFartLength);
                return true;
            }

            return false;
        }

        public void End()
        {
            
        }
    }
}