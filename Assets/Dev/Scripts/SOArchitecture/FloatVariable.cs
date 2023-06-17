using UnityEngine;

namespace Scripts.SOArchitecture
{
    [CreateAssetMenu(menuName = "ScriptableObject/Variables/FloatVariable", fileName = "NewFloatVariable")]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField] private float Value;
        public float value => Value;
    }
}
