using System.Collections;
using System.Collections.Generic;
using Character.Properties;
using Character.View.Npc;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.View
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCharacterView : BaseCharacterView
    {
        private static int UserIdCounter = 0;

        [SerializeField] private float detectionRange = 2f;
        [SerializeField] private float sphereRadius = 2f;
        [SerializeField] private LayerMask characterLayer;
        
        private readonly WaitForSeconds secondDelay = new(1f);
        private Coroutine holdFartCoroutine = null;
        private float holdFartTimer = 1f;
        private Vector3 moveDirection;
        private bool isFarting = false;
        
        private PlayerControls playerControls;

        private int userId;
        
        private void Awake() 
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
            
            userId = UserIdCounter++;

            CharacterProperties.InjectUI(PlayersUI.Instance.GetUIBehaviourFor(userId));
        }

        private void Update()
        {
            if (moveDirection != Vector3.zero && !isFarting)
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

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + transform.forward * detectionRange, sphereRadius);
        }

        private ICharacterView[] GetCharactersToCork()
        {
            List<ICharacterView> npcCharacterViewList = new();
            var overlappingNPCs = Physics.OverlapSphere(transform.position + transform.forward * detectionRange, sphereRadius, characterLayer);

            foreach (var npcCollider in overlappingNPCs)
            {
                if (npcCollider.gameObject == gameObject)
                {
                    continue;
                }

                NpcCharacterView npcCharacterView = npcCollider.gameObject.GetComponent<NpcCharacterView>();
                if (npcCharacterView)
                {
                    npcCharacterViewList.Add(npcCharacterView);
                }
            }

            return npcCharacterViewList.ToArray();
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
            if (context.performed)
            {
                corkAction.Execute(this, GetCharactersToCork());
            }
        }

        public void OnFart(InputAction.CallbackContext context)
        {
            if (context.started && holdFartCoroutine == null)
            {
                isFarting = true;
                holdFartCoroutine = StartCoroutine(HoldButtonRoutine());
            }
            else if (context.canceled && holdFartCoroutine != null)
            {
                StopCoroutine(holdFartCoroutine);
                holdFartCoroutine = null;
                fartAction.Execute(this, holdFartTimer);
                holdFartTimer = 1f;
                isFarting = false;
            }

            IEnumerator HoldButtonRoutine()
            {
                yield return new WaitUntil(context.ReadValueAsButton);
                while (context.ReadValueAsButton())
                {
                    if (holdFartTimer == 2f)
                    {
                        StartFarting();
                    }
                    yield return secondDelay;
                    holdFartTimer++;
                }
            }
        }
        
#endregion
    }
}