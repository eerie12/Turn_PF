﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //FieldのSavePoint設定

    public HealthManager health;
    public Renderer theRend;

    public Material cpOff;
    public Material cpOn;

    
    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
    }

    #region SavePoint処理

    public void CheckpointOn()
    {
        CheckPoint[] checkPoints = FindObjectsOfType<CheckPoint>();
        foreach(CheckPoint cp in checkPoints)
        {
            if (cp.theRend)
            {
                cp.CheckpointOff();
            }
            
        }
        if(gameObject.tag != "CheckPoint_unvisible")
        {
            theRend.material = cpOn;
        }
        else
        {
            GameObject check_unvisible = GameObject.FindWithTag("CheckPoint_unvisible");
            Destroy(check_unvisible);
        }


        
    }

    public void CheckpointOff()
    {
        theRend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            health.SetSpawnPoint(transform.position);
            //GameManagerにSaveし、Sceneが移動しても情報を残す。
            GameManager.instance.respawnPosition = transform.position;
            CheckpointOn();
        }
    }

    #endregion

}
