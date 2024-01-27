using System;
using System.Collections;
using System.Linq.Expressions;
using Character.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.View
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCharacterView : BaseCharacterView
    {
        private readonly WaitForSeconds secondDelay = new(1f);
        private Coroutine holdFartCoroutine = null;
        private float holdFartCounter;
        protected PlayerControls playerControls;

        private void Awake() 
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        protected override ICharacterProperties GetCharacterProperties()
        {
            return CharacterPropertiesFactory.Get(false);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var move = context.ReadValue<Vector2>();
            var direction = new Vector3(move.x, 0, move.y);
            walkAction.Execute(this, direction);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            corkAction.Execute(this, null);
        }

        public void OnFart(InputAction.CallbackContext context)
        {
            if(context.started && holdFartCoroutine == null)
            {
                holdFartCoroutine = StartCoroutine(HoldButtonRoutine());
            }

            return;

            IEnumerator HoldButtonRoutine()
            {
                yield return new WaitUntil(context.ReadValueAsButton);
                while (context.ReadValueAsButton())
                {
                    yield return secondDelay;
                    holdFartCounter += 3f;
                }
                fartAction.Execute(this, holdFartCounter);
                holdFartCoroutine = null;
            }
        }
    }
}