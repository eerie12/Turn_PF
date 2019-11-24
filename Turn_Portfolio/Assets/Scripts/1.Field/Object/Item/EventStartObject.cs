using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStartObject : MonoBehaviour
{
    //Event開始設定

    MapEvent map_Event;//MapEventの処理を参照
    public GameObject pickupEffect;
    public GameObject eventObject;
    public GameObject eventParticle;
    public GameObject eventObject_Save;

    // Start is called before the first frame update
    void Start()
    {
        map_Event = FindObjectOfType<MapEvent>();
    }

    //Playerが触れたらEventをStart
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {           
            if(pickupEffect && eventObject && eventParticle)
            {
                Instantiate(pickupEffect, transform.position, transform.rotation);
                if (eventObject.activeSelf)
                {
                    SoundManager.instance.PlaySound("Event_Jewel", 1);
                }                
                eventObject.SetActive(false);
                eventParticle.SetActive(false);
                
            }                       
            if(gameObject.tag == "EventA")
            {           
                eventObject_Save = gameObject;
                Invoke("mapEvent_Start", 0.5f);              
                Invoke("SetActive_Obj", 1f);
                GameManager.instance.eventReset = true;
            }
            else if (gameObject.tag == "EventA_Reset")
            {
                if (GameManager.instance.eventReset)
                {
                    Invoke("mapEvent_Start_Off", 0.2f);
                    Destroy(gameObject, 1f);
                    GameManager.instance.eventReset = false;
                }
                
            }
            else if (gameObject.tag == "EventB")
            {                
                GameManager.instance.fieldMonName.Add(gameObject.name);
                Invoke("mapEventB_Start", 0.5f);
                Destroy(gameObject, 1f);

            }
            else if (gameObject.tag == "EventC")
            {                
                Invoke("mapEventC_Start", 0.5f);
                Destroy(gameObject, 1f);
            }
            else if (gameObject.tag == "EventBoss")
            {
                GameManager.instance.fieldMonName.Add(gameObject.name);                
                Invoke("mapEvent_Boss_Start", 0.2f);
                Destroy(gameObject, 1f);
            }
        }
    }

    //触れた後の設定
    private void SetActive_Obj()
    {
        gameObject.SetActive(false);
    }

    #region MapEventからの参照

    private void mapEvent_Start()
    {
        map_Event.Map_Event_Camera();
    }

    private void mapEvent_Start_Off()
    {
        map_Event.Map_Event_Camera_Reset();
    }

    private void mapEventB_Start()
    {
        map_Event.Map_Event_CameraB();
    }

    private void mapEventC_Start()
    {
        map_Event.Map_Event_CameraC();
    }

    private void mapEvent_Boss_Start()
    {
        map_Event.Map_Event_Camera_Boss();
    }

    #endregion
}
