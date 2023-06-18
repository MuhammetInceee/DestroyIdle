using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.HandleObjects
{
    public class HeldWeapon : HeldObjectsBase
    {
        internal override void Awake()
        {
            base.Awake();
        }

        internal override void Trigger(Collider other)
        {
            base.Trigger(other);
            if (other.TryGetComponent(out IKillable killable))
            {
                killable.Execute();
                ColliderToggle();
            }
        }
    }
}
