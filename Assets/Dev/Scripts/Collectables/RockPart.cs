using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Scripts.Collectables
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RockPart : MonoBehaviour
    {
        internal bool canExplode = true;
        internal Vector3 firstLocalPos;
        internal Quaternion firstLocalRotation;

        private void Awake()
        {
            var transform1 = transform;
            
            firstLocalPos = transform1.localPosition;
            firstLocalRotation = transform1.localRotation;
        }

        internal void Stack(Transform playerTransform)
        {
        }
        
    }
}
