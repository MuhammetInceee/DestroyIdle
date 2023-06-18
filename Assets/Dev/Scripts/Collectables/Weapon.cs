using Scripts.Interfaces;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Collectables
{
    public class Weapon : MonoBehaviour, ICollectable
    {
        private PlayerHeldObjectsController _playerHeldObjects;

        private void Awake() => InitVariables();

        private void InitVariables()
        {
            _playerHeldObjects = PlayerHeldObjectsController.Instance;
        }

        public void Execute()
        {
            Destroy(gameObject);
            _playerHeldObjects.ChangeHeldObject(HeldObjectsTypes.weapon);
        }
    }
}
