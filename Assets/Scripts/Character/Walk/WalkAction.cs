using Character.View;
using UnityEngine;

namespace Character.Walk
{
    public class WalkAction
    {
        public void Execute(ICharacterView characterView, Vector3 direction)
        {
            var newPosition = characterView.Transform.position + direction * characterView.CharacterProperties.Speed;
            if (AreaManager.Instance.IsPositionInBounds(newPosition))
            {
                characterView.Walk(direction);    
            }
        }
    }
}