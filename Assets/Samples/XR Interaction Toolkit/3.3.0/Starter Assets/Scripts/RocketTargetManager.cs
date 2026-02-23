using System.Collections.Generic;
using UnityEngine;

public class RocketTargetManager : MonoBehaviour
{
    
    public static RocketTargetManager Instance;


    public int currentTargetIndex = 0;
    public int currentTarget;
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
        currentTarget = 0;
        if (currentTargetIndex < targetList.Count)
        {
            currentTarget = currentTargetIndex;
            currentTargetIndex++;
            Debug.Log("Returned Index:" + currentTarget);
            return targetList[currentTarget];
        }
        else
        {
            //reiterates to beginning of list
            int returnTarget = 0;
            currentTargetIndex = 1;
            Debug.Log("Returned Index:" + returnTarget);
            return targetList[returnTarget];
        }
    }

    void Update()
    { //removes null from list when RocketTarget is destroyed
        for (int i = targetList.Count - 1; i >= 0; i--)
        {
            if (targetList[i] == null)
            {
                targetList.RemoveAt(i);
            }
        }
    }
}
