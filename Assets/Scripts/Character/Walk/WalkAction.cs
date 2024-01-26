using Character.View;
using UnityEngine;

namespace Character.Walk
{
    public class WalkAction
    {
        public void Execute(ICharacterView characterView, Vector2 direction)
        {
            characterView.Walk(direction);
        }
    }
}