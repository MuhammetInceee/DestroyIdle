using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.HandleObjects
{
    public class HeldObjectsBase : MonoBehaviour
    {
        private BoxCollider _collider;

        internal virtual void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Trigger(other);
        }

        internal virtual void Trigger(Collider other) { }
        
        internal async void ColliderToggle()
        {
            _collider.enabled = false;
            await Task.Delay(2257);
            _collider.enabled = true;
        }
    }
}
