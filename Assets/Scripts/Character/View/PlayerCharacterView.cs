using System.Collections;
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
        private Vector3 moveDirection;
        protected PlayerControls playerControls;

        private void Awake() 
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        private void Update()
        {
            if (moveDirection != Vector3.zero)
            {
                MoveRelativeToMainCamera();
            }
        }

        private void MoveRelativeToMainCamera()
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            Vector3 cameraRelativeMoveDirection = (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized;
            walkAction.Execute(this, cameraRelativeMoveDirection);
        }

        protected override ICharacterProperties GetCharacterProperties()
        {
            return CharacterPropertiesFactory.Get(false);
        }

#region PlayerInputActions

        public void OnMove(InputAction.CallbackContext context)
        {
            var moveInputAxis = context.ReadValue<Vector2>();
            moveDirection = new Vector3(moveInputAxis.x, 0, moveInputAxis.y);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            corkAction.Execute(this, null);
        }

        public void OnFart(InputAction.CallbackContext context)
        {
            if(context.started && holdFartCoroutine == null)
            {
                StartFarting();
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
        
#endregion
    }
}