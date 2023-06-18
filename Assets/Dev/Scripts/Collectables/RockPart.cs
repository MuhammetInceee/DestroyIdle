using System;
using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Collectables
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RockPart : MonoBehaviour
    {
        private PlayerStackManager _playerStackManager;
        
        internal MeshCollider coll;
        internal Rigidbody rb;
        
        internal bool canExplode = true;
        internal Vector3 firstLocalPos;

        private void Awake()
        {
            _playerStackManager = PlayerStackManager.Instance;
            
            firstLocalPos = transform.localPosition;
            coll = GetComponent<MeshCollider>();
            rb = GetComponent<Rigidbody>();
        }

        internal void Stack(Transform playerTransform, Action onComplete)
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
                    _playerStackManager.Stack();
                });

            sequence.Append(moveTween);
            sequence.Play()
                .OnComplete(onComplete.Invoke);
        }
        
    }
}
