using UnityEngine;

namespace Scripts.HandleObjects
{
    public class HeldCleaner : HeldObjectsBase
    {
        internal override void Awake()
        {
            base.Awake();
        }

        internal override void Trigger(Collider other)
        {
            base.Trigger(other);
        }
    }
}
