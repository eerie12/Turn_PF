using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldNPC : MonoBehaviour
{
    //NPCの行動設定

    PlayerControllerrbody playerController;

    [SerializeField] private Transform player;//Playerの位置を呼ぶ
    [SerializeField] private GameObject quest_Mark;//該当するquestmarkのobj
    private Quaternion startRotation;//動く前のRotation

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerrbody>();
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.eventStart && playerController.storyEvent && GameManager.instance.eventFlags[0])
        {
            if (quest_Mark)
            {
                if (quest_Mark.activeSelf)
                {
                    quest_Mark.SetActive(false);
                    GameManager.instance.fieldMonName.Add(quest_Mark.name);//questMarkをDestroy_objに追加
                }

            }
            Vector3 dir = player.position - transform.position;//向く方向を求める
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z)), 20f * Time.deltaTime * 5);//Lerpで回転
        }
        else
        {
            StartMoveRotation();//元のRotationに戻る
        }

    }

    private void StartMoveRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, 20f * Time.deltaTime * 5);
    }
}
