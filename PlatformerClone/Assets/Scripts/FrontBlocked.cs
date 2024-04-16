using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBlocked : MonoBehaviour
{
    private bool isFrontBlocked = false;

    private void OnTriggerEnter(Collider other) {
        isFrontBlocked = true;
    }

    private void OnTriggerExit(Collider other) {
        isFrontBlocked = false;
    }

    public bool IsFrontBlocked() {
        return isFrontBlocked;
    }
}
