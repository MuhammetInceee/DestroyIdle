using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.HandleObjects
{
    public class HeldDestroyer : HeldObjectsBase
    {
        private Transform _playerTransform;
        internal override void Awake()
        {
            base.Awake();
            InitVariables();
        } 

        private void InitVariables()
        {
            _playerTransform = transform.root;
        }
        internal override void Trigger(Collider other)
        {
            base.Trigger(other);
            if (other.TryGetComponent(out IDegradable destroyable))
            {
                destroyable.Execute(_playerTransform);
                ColliderToggle();
            }
        }
    }
}
