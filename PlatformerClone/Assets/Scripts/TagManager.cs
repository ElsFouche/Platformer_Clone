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

    public Tags tagType;
    public Bullet bulletType;
    public Enemies enemyType;
}
