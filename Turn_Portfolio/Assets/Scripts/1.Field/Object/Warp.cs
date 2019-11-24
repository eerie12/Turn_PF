using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    //Warp設定

    public Transform warpPoint;
    public GameObject thePlayer;
    HealthManager health;

    private void Start()
    {
        health = FindObjectOfType<HealthManager>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == thePlayer)
        {
            thePlayer.transform.position = warpPoint.transform.position;
            health.SetSpawnPoint(health.startPoint);
        }
    }
}
