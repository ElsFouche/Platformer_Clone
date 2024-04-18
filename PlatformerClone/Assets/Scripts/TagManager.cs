using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{

    public enum Tags
    {
        None,
        Player,
        Enemies,
        Bullet,
        Pickup,
        Platform
    }

    public enum Bullet
    {
        None,
        Normal,
        Heavy,
        SuperMissile
    }

    public enum Enemies
    {
        None,
        Normal, 
        Strong
    }

    public enum Pickups
    {
        None,
        Health
    }

    

    public Tags tagType;
    public Bullet bulletType;
    public Enemies enemyType;
    public Pickups pickupType;
}
