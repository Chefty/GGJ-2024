using Character.Properties;
using Character.Walk;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.View
{
    public class PlayerCharacterView : BaseCharacterView
    {
        private PlayerControls playerControls;

        private void Start() {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        protected override ICharacterProperties GetCharacterProperties()
        {
            return CharacterPropertiesFactory.Get(false);
        }

        void OnMove(InputValue inputValue)
        {
            var move = inputValue.Get<Vector2>();
            var direction = new Vector3(move.x, 0, move.y);
            Debug.Log("MOVE:" + direction);
            walkAction.Execute(this, direction);
        }

        void OnAttack()
        {
            corkAction.Execute(this, null);
        }

        void OnFart()
        {
            //TODO stack fart percentage on hold, call action on release.
            fartAction.Execute(this, .1f);
        }
    }
}