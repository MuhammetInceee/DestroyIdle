using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
