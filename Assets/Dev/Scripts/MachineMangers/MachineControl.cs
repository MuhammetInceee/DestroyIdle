using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Scripts.Manager;

[SuppressMessage("ReSharper", "FunctionRecursiveOnAllPaths")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class MachineControl : MonoBehaviour
{
    private PoolingManager _poolingManager;
    
    internal List<GameObject> stackedList;
    [SerializeField] public Transform firstStackTr;
    
    internal List<GameObject> stackedProductList;
    [SerializeField] public Transform firstProductStackTr;

    [SerializeField] private float waitTime;

    private void Awake() => InitVariables();
    void Start() => StartCoroutine(Work());
    private void InitVariables() => _poolingManager = PoolingManager.Instance;


    private IEnumerator Work()
    {
        yield return new WaitForSeconds(waitTime);

        if (stackedList.Count > 0)
        {
            var stackOffset = 0.22f;
            
            var stack = stackedList[^1].gameObject;
            var targetPos = firstProductStackTr.position + Vector3.up * (stackedProductList.Count * stackOffset);

            stackedList.Remove(stack);
            _poolingManager.ReturnPoolOre(stack);
            stack = _poolingManager.GetAvailableIngot();
            stackedProductList.Add(stack);

            stack.transform.position = targetPos;
        }
        
        StartCoroutine(Work());
    }
}
