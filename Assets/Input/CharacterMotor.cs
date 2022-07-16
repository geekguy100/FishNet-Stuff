using FishNet.Object;
using KpattGames.Input;
using UnityEngine;

namespace KpattGames.Movement
{
    [RequireComponent(typeof(PlayerMotorInput))]
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMotor : NetworkBehaviour
    {
        private CharacterController controller;
        private PlayerMotorInput inputController;

        [SerializeField] private float speed;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            inputController = GetComponent<PlayerMotorInput>();
        }

        private void Update()
        {
            // Don't run this if we are not the owner.
            if (!base.IsOwner)
                return;
            
            var movement = inputController.Input;
            controller.SimpleMove(movement * speed);
        }
    }
}