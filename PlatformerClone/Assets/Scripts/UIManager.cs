using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
 * Symon Belcher
 * 4/22/2024
 * player UI
 */
public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text missileText;

    public GameObject player;

    private PlayerHealth playerHealth;
    private PlayerController missileCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");


        playerHealth = player.GetComponentInChildren<PlayerHealth>();
        missileCount = player.GetComponentInChildren<PlayerController>();
    }

    // Update is called once per frame
    void Update()   
    {
       
       healthText.text = "Health:   " + playerHealth.health;// counts lives

     
       missileText.text = "Super Missiles: "  + missileCount.superMissiles;// counts missles
    }
}
