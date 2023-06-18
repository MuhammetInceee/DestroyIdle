using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Collectables;
using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.DegradableObject
{
    public class DegradableObjects : MonoBehaviour, IDegradable
    {
        [SerializeField] private List<Transform> children;
        [SerializeField] private Transform explodeTr;

        private void Awake() => InitVariables();

        private void InitVariables()
        {
            foreach (Transform tr in transform.GetChild(0))
            {
                children.Add(tr);
            }
        }
        
        public void Execute(Transform playerTransform)
        {
            var target = GetAvailablePart();
            var meshCol = target.GetComponent<MeshCollider>();
            var rb = target.GetComponent<Rigidbody>();
            var rockComp = target.GetComponent<RockPart>();

            meshCol.enabled = true;
            rb.isKinematic = false;
            Explode(rb);
            rockComp.canExplode = false;
            Stack(rockComp, playerTransform);
        }

        private Transform GetAvailablePart()
        {
            var target = children.FirstOrDefault(m => m.GetComponent<RockPart>().canExplode);
            return target;
        }

        private void Explode(Rigidbody rb)
        {
            rb.AddExplosionForce(500, explodeTr.position, 15);
        }

        private async void Stack(RockPart rockPart, Transform playerTransform)
        {
            await Task.Delay(1000);
            rockPart.Stack(playerTransform);
        }

        private void ResetRock()
        {
            foreach (var child in children)
            {
                var comp = child.GetComponent<RockPart>();
                var rb = child.GetComponent<Rigidbody>();
                var meshCol = child.GetComponent<MeshCollider>();
                var transform1 = child.transform;

                meshCol.enabled = false;
                rb.isKinematic = true;
                transform1.localPosition = comp.firstLocalPos;
                transform1.localRotation = comp.firstLocalRotation;
                comp.canExplode = true;
            }
        }
    }
}
