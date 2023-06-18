using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Collectables
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RockPart : MonoBehaviour
    {
        internal MeshCollider coll;
        internal Rigidbody rb;
        
        internal bool canExplode = true;
        internal Vector3 firstLocalPos;
        internal Quaternion firstLocalRotation;

        private void Awake()
        {
            var transform1 = transform;
            
            firstLocalPos = transform1.localPosition;
            firstLocalRotation = transform1.localRotation;
            coll = GetComponent<MeshCollider>();
            rb = GetComponent<Rigidbody>();
        }

        internal void Stack(Transform playerTransform)
        {
            coll.isTrigger = true;
            
            var sequence = DOTween.Sequence();
            var position = playerTransform.position;
            var targetPos = new Vector3(position.x, position.y + 1,
                position.z);
            
            var moveTween = transform.DOMove(targetPos, 0.3f)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    print("stack");
                });

            sequence.Append(moveTween);
            sequence.Play();
        }
        
    }
}
