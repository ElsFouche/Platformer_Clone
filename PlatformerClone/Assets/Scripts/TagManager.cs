using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Fouché, Els
 * Last Update: 04/22/2024
 * Notes:       This script handles the various tags used
 *              throughout the game. 
 */

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
        Health,
        Jump,
        HealthMax,
        SuperMissile,
        StrongBullet
    }

    public Tags tagType;
    public Bullet bulletType;
    public Enemies enemyType;
    public Pickups pickupType;
}
