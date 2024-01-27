using UnityEngine;

namespace Character.View.Npc.States
{
    public class IdleState : IState
    {
        private const float MIN_SECONDS_IDLING = 0.5f;
        private const float MAX_SECONDS_IDLING = 2.5f;
            
        public NextState[] NextStates => new[]
        {
            new NextState(0.7f, new WalkState()),
            new NextState(0.3f, new FartState())
        };

        private float endTime;
        
        public void Start(ICharacterView characterView)
        {
            endTime = Time.realtimeSinceStartup + Random.Range(MIN_SECONDS_IDLING, MAX_SECONDS_IDLING);
        }

        public bool Execute(ICharacterView characterView)
        {
            return Time.realtimeSinceStartup >= endTime;
        }

        public void End()
        {
            
        }
    }
}