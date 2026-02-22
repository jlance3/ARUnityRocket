using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    
    public static TargetManager Instance;


    public int currentTargetIndex = 0;
    public List<Transform> targetList = new List<Transform>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddTarget(Transform newTarget)
    {
        targetList.Add(newTarget);

    }

    public Transform GetTarget()
    {
        if (currentTargetIndex < targetList.Count)
        {
            currentTargetIndex++;
            return targetList[currentTargetIndex - 1];
        }
        else
        {
            //reiterates to beginning of list
            currentTargetIndex = 0;
            return targetList[currentTargetIndex];
        }
    }
}
