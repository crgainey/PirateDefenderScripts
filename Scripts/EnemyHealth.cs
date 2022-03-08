using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// when adding this script to a gameobject it will automatically add the enmy script to the object
[RequireComponent(typeof(Enemy))]  
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] int diffiucltyRamp = 1;

    int currentHitPoints = 0;

    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;

        if(currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += diffiucltyRamp;
            enemy.RewardGold();
        }
    }
}
