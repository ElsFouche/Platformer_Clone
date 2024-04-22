using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearBlocked : MonoBehaviour
{
    private bool isRearBlocked = false;

    private void OnTriggerEnter(Collider other) {
        isRearBlocked = true;
    }

    private void OnTriggerExit(Collider other) {
        isRearBlocked = false;
    }

    public bool IsRearBlocked() {
        return isRearBlocked;
    }

    public void SetRearBlocked(bool rearBlocked)
    {
        isRearBlocked = rearBlocked;
    }
}
