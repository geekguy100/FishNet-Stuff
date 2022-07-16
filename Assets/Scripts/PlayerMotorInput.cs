using UnityEngine;

namespace KpattGames.Input
{
    public class PlayerMotorInput : MonoBehaviour
    {
        private PlayerControls controls;

        public Vector3 Input
        {
            get
            {
                var inputCtx = controls.Movement.Move.ReadValue<Vector2>();
                return new Vector3(inputCtx.x, 0, inputCtx.y);
            }
        }

        private void Awake()
        {
            controls = new PlayerControls();
        }

        #region Event Handling
        private void OnEnable()
        {
            controls.Movement.Enable();
        }

        private void OnDisable()
        {
            controls.Movement.Disable();
        }
        #endregion
    }
}