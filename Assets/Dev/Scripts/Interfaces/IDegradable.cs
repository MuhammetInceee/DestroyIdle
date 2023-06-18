using UnityEngine;

namespace Scripts.Interfaces
{
    public interface IDegradable
    {
        public void Execute(Transform playerTransform);
    }
}