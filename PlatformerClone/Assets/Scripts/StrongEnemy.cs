using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrongEnemy : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject playerTarget;
    public float findPlayerDelay = 0.5f;
    public bool rightBlocked = false;
    public bool leftBlocked = false;

    private Vector3 playerPos;
    private MoveBlocked moveBlocked;

    void Awake()
    {
        InvokeRepeating("TrackPlayerPos", 0.0f, findPlayerDelay);
        moveBlocked = GetComponent<MoveBlocked>();
    }

    void Update()
    {
        if (!moveBlocked.IsFrontBlocked() && transform.position.x < playerPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        } else if (!moveBlocked.IsRearBlocked() && transform.position.x > playerPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        } 
    }

    private void TrackPlayerPos()
    {
        playerPos = playerTarget.transform.position;
        playerPos.y = transform.position.y;
        playerPos.z = 0.0f;
    }
}
