using System.Collections.Generic;
using DG.Tweening;
using Scripts.Manager;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerStackManager : Singleton<PlayerStackManager>
    {
        private PoolingManager _poolingManager;
        
        public List<GameObject> stackedList;
        [SerializeField] private Transform firstStackTr;

        private void Awake() => InitVariables();

        private void InitVariables()
        {
            _poolingManager = PoolingManager.Instance;
        }

        internal void Stack()
        {
            var stackObj = _poolingManager.GetAvailableOre();
            var stackCount = stackedList.Count;
            var stackOffset = 0.22f;
            
            stackObj.transform.SetParent(firstStackTr);

            if (stackedList.Count == 0) stackObj.transform.localPosition = Vector3.zero;
            else stackObj.transform.localPosition = Vector3.zero + Vector3.up * (stackCount * stackOffset);
            
            stackObj.SetActive(true);
            stackedList.Add(stackObj);
        }

        internal void GiveStack(MachineControl machineControl)
        {
            Vector3 startPos = machineControl.firstStackTr.position;
            var stackOffset = 0.22f;

            while (stackedList.Count > 0)
            {
                Vector3 targetPos = startPos + Vector3.up * machineControl.stackedList.Count * stackOffset;
                GameObject stack = stackedList[^1].gameObject;
                stack.transform.parent = null;
                
                stackedList.Remove(stack);
                machineControl.stackedList.Add(stack);

                stack.transform.DOMove(targetPos, 0.5f);
            }
            
            
            

        }
    }
}
