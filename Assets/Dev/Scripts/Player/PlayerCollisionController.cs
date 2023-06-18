using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        private PlayerHeldObjectsController _playerHeldObjectsController;
        private PlayerController _playerController;

        private void Awake() => InitVariables();

        private void InitVariables()
        {
            _playerHeldObjectsController = GetComponent<PlayerHeldObjectsController>();
            _playerController = GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                _playerHeldObjectsController.ChangeHeldObject(HeldObjectsTypes.weapon);
                _playerController.SetTarget(1);
                collectable.Execute();
            }
        }
    }
}