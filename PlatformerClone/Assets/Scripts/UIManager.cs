using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
 * Symon Belcher
 * 4/16/2024
 * player UI
 */
public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;

    public GameObject player;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponentInChildren<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
       healthText.text = "health: " + playerHealth.health;// counts lives
    }
}
