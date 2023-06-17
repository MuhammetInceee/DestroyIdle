using UnityEngine;

namespace Scripts.HandleObjects
{
    public class HeldObjectsBase : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Trigger();
        }

        internal virtual void Trigger()
        {
            
        }
    }
}
