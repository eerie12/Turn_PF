using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest＿Jewel : MonoBehaviour
{
    //MainQuest_obj設定

    public GameObject pickupEffect;
    public GameObject eventObject;
    public GameObject eventParticle;
    public GameObject warp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.fieldMonName.Add(gameObject.name);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(eventObject);
            Destroy(eventParticle);
            QuestManager.questManager.AddQuestItem("Jewel", 1);//QuestManagerにQuestItemを追加
            QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
            GameManager.instance.eventStartCheck = true;
            foreach (QuestObject obj in currentQuestGuys)
            {
                obj.SetQuestMaker();//QuestMarkを修正
            }
            Invoke("WarpOn", 0.5f);//村までのWarpを開く
            Destroy(gameObject, 1f);
        }
    }

    private void WarpOn()
    {
        warp.SetActive(true);
    }
}
