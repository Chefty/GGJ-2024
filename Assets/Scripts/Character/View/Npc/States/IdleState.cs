using UnityEngine;

namespace Character.View.Npc.States
{
    public class IdleState : IState
    {
        private const float MIN_SECONDS_IDLING = 1f;
        private const float MAX_SECONDS_IDLING = 3f;
            
        public NextState[] NextStates => new[]
        {
            new NextState(0.95f, new WalkState()),
            new NextState(0.05f, new FartState())
        };

        private float endTime;
        
        public void Start(ICharacterView characterView)
        {
            endTime = Time.realtimeSinceStartup + Random.Range(MIN_SECONDS_IDLING, MAX_SECONDS_IDLING);
            characterView.Walk(Vector3.zero);
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