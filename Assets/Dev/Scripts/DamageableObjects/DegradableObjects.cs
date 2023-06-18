using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Scripts.Collectables;
using Scripts.Interfaces;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Time.timeScale = Time.timeScale <= 1.2f ? 2 : 1;
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
        }

        private IEnumerator CounterStart()
        {
            ResetRock();
            counterText.gameObject.SetActive(true);
            _tempSpeed = ResetTime;
            while (_tempSpeed > 0f)
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
            return usedCount <= 0;
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
            rockPart.Stack(playerTransform, () =>
            {
                if(!CheckChild()) return;
                StartCoroutine(CounterStart());
            });
        }

        private async void ResetRock()
        {
            var sequence = DOTween.Sequence();
            float duration = ResetTime / children.Count;
            
            foreach (var child in children)
            {
                child.transform.localPosition = child.firstLocalPos + Vector3.up * Random.Range(3f, 5f);
                
                child.coll.enabled = false;
                child.rb.isKinematic = true;

                child.gameObject.SetActive(true);
                
                var rotateTween = child.transform.DOLocalRotate(Vector3.zero, 0.3f);
                var moveTween = child.transform.DOLocalMove(child.firstLocalPos, 0.3f);
                
                sequence.Append(rotateTween);
                sequence.Append(moveTween);

                sequence.Play()
                    .OnComplete(() =>
                    {
                        sequence.Kill();
                    });
                
                await Task.Delay((int)((duration + 0.3f) * 1000));
            }

            foreach (var child in children)
            {
                child.canExplode = true;
            }
        }
    }
}