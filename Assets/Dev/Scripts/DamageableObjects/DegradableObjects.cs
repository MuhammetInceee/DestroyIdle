using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Collectables;
using Scripts.Interfaces;
using TMPro;
using UnityEngine;

namespace Scripts.DegradableObject
{
    public class DegradableObjects : MonoBehaviour, IDegradable
    {
        private const float ResetTime = 3;
        private float _tempSpeed;
        
        [SerializeField] private List<RockPart> children;
        [SerializeField] private Transform explodeTr;
        [SerializeField] private TextMeshProUGUI counterText;

        private void Awake() => InitVariables();

        private void InitVariables()
        {
            foreach (Transform tr in transform.GetChild(0))
            {
                var comp = tr.GetComponent<RockPart>();
                children.Add(comp);
            }
        }

        public void Execute(Transform playerTransform)
        {
            var target = GetAvailablePart();
            if (target == null) return;
            
            target.coll.enabled = true;
            target.rb.isKinematic = false;
            Explode(target.rb);
            target.canExplode = false;
            Stack(target, playerTransform);
            
            if(!CheckChild()) return;
            StartCoroutine(CounterStart());
        }

        private IEnumerator CounterStart()
        {
            counterText.gameObject.SetActive(true);
            _tempSpeed = ResetTime;
            while (_tempSpeed > 0)
            {
                _tempSpeed -= Time.deltaTime;
                counterText.text = Mathf.RoundToInt(_tempSpeed).ToString();
                yield return null;
            }
            counterText.gameObject.SetActive(false);
        }

        private bool CheckChild()
        {
            var usedCount = children.Count(t => t.canExplode);
            return usedCount >= children.Count;
        }

        private RockPart GetAvailablePart()
        {
            var target = children.FirstOrDefault(m => m.canExplode);
            return target;
        }

        private void Explode(Rigidbody rb)
        {
            rb.AddExplosionForce(500, explodeTr.position, 15);
        }

        private async void Stack(RockPart rockPart, Transform playerTransform)
        {
            await Task.Delay(800);
            rockPart.Stack(playerTransform);
        }

        private void ResetRock()
        {
            foreach (var child in children)
            {
                var transform1 = child.transform;

                child.coll.isTrigger = false;
                child.coll.enabled = false;
                child.rb.isKinematic = true;
                transform1.localPosition = child.firstLocalPos;
                transform1.localRotation = child.firstLocalRotation;
                child.canExplode = true;
            }
        }
    }
}