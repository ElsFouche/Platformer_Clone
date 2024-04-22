using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocked : MonoBehaviour
{
    private FrontBlocked frontBlocked;
    private RearBlocked rearBlocked;

    void Start() 
    {
        frontBlocked = GetComponentInChildren<FrontBlocked>();
        rearBlocked = GetComponentInChildren<RearBlocked>();
    }

    public bool IsFrontBlocked() 
    {
        return frontBlocked.IsFrontBlocked();
    }

    public bool IsRearBlocked() 
    {
        return rearBlocked.IsRearBlocked();
    }

    public void SetFrontBlocked(bool isFrontBlocked)
    {
        frontBlocked.SetFrontBlocked(isFrontBlocked);
    }

    public void SetRearBlocked(bool isRearBlocked)
    {
        rearBlocked.SetRearBlocked(isRearBlocked);
    }
}
