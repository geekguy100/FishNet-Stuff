using UnityEngine;

namespace KpattGames.Input
{
    /// <summary>
    /// Handles invoking tasks of the core player behaviour.
    /// </summary>
    [RequireComponent(typeof(RaycastInteractor))]
    public class PlayerRaycastInteractorController : MonoBehaviour
    {
        private PlayerControls controls;
        private RaycastInteractor raycastInteractor;
        private Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
        }

        /// <summary>
        /// The position of the cursor in world space.
        /// </summary>
        private Vector3 CursorPos
        {
            get
            {
                Vector3 screenPos = controls.Actions.CursorPosition.ReadValue<Vector2>();
                Vector3 worldPos = mainCam.ScreenToWorldPoint(screenPos);
                worldPos.z = -10;

                return worldPos;
            }
        }

        private void Awake()
        {
            raycastInteractor = GetComponent<RaycastInteractor>();
            
            controls = new PlayerControls();
            controls.Actions.Interact.performed += _ => raycastInteractor.PerformRaycast(CursorPos);
            
            controls.Actions.Enable();
        }
    }   
}