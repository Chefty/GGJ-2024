using Character.Walk;
using UnityEngine;

namespace Character.View.Npc.States
{
    public class WalkState : IState
    {
        private Vector2 MIN_BOUNDS = new (1.5f, -1.5f);
        private Vector2 MAX_BOUNDS = new (29, -47.5f);

        public NextState[] NextStates => new[]
        {
            new NextState(0.97f, new IdleState()),
            new NextState(0.03f, new FartState())
        };

        private Vector3 endPosition;
        private Vector3 direction;
        
        private WalkAction action = new();

        public void Start(ICharacterView characterView)
        {
            endPosition = new Vector3(
                Random.Range(MIN_BOUNDS.x, MAX_BOUNDS.x),
                characterView.Transform.position.y, 
                Random.Range(MIN_BOUNDS.y, MAX_BOUNDS.y));

            direction = (endPosition - characterView.Transform.position).normalized;
        }

        public bool Execute(ICharacterView characterView)
        {
            if (Vector3.Distance(characterView.Transform.position, endPosition) <= 1f)
            {
                return true;
            }
            
            action.Execute(characterView, direction);
            return false;
        }

        public void End()
        {
            
        }
    }
}