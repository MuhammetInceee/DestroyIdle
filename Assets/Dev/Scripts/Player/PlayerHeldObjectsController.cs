using System.Diagnostics.CodeAnalysis;
using Scripts.SOArchitecture;
using UnityEngine;
#pragma warning disable CS8524

namespace Scripts.Player
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PlayerHeldObjectsController : MonoBehaviour
    {
        [SerializeField] private HeldObjectsTypes Type;
        [SerializeField] private HeldObjectList ObjectList;
        
        internal HeldObjects type => new(Type, ObjectList.heldObjects);

        private void Awake() => InitVariables();

        private void InitVariables()
        {
            type.gObject.SetActive(true);
        }

        private void ChangeHeldObject(HeldObjectsTypes targetType)
        {
            type.gObject.SetActive(false);
            Type = targetType;
            type.gObject.SetActive(true);
        }
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum HeldObjectsTypes { destroyer, weapon, cleaner}

    [SuppressMessage("ReSharper", "InconsistentNaming"), System.Serializable]
    public struct HeldObjectList
    {
        public GameObject[] heldObjects;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class HeldObjects
    {
        internal static HeldObjects destroyer => new(HeldObjectsTypes.destroyer, new HeldObjectList().heldObjects);
        internal static HeldObjects weapon => new(HeldObjectsTypes.weapon, new HeldObjectList().heldObjects);
        internal static HeldObjects cleaner => new(HeldObjectsTypes.cleaner, new HeldObjectList().heldObjects);

        private readonly HeldObjectsTypes heldObjectsTypes;
        private readonly GameObject[] heldObjectList;

        internal HeldObjects(HeldObjectsTypes heldObjectsTypes, GameObject[] heldObjectList)
        {
            this.heldObjectsTypes = heldObjectsTypes;
            this.heldObjectList = heldObjectList;
        }
        
        internal GameObject gObject => heldObjectsTypes switch
        {
            HeldObjectsTypes.destroyer => heldObjectList[0],
            HeldObjectsTypes.weapon => heldObjectList[1],
            HeldObjectsTypes.cleaner => heldObjectList[2]
        };

        internal float threshold => heldObjectsTypes switch
        {
            HeldObjectsTypes.destroyer => Resources.Load<FloatVariable>("HeldObjects/Thresholds/DestroyerThreshold").value,
            HeldObjectsTypes.weapon => Resources.Load<FloatVariable>("HeldObjects/Thresholds/WeaponThreshold").value,
            HeldObjectsTypes.cleaner => Resources.Load<FloatVariable>("HeldObjects/Thresholds/CleanerThreshold").value
        };
    }
}