using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{

    public List<int> availableQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();

    public GameObject questMarker;//QuestMark_Obj
    public Image theIamge;//QuestMark_Image

    public Sprite questAvailableSprite;//QuestMark_QuestionMark
    public Sprite questReceivableSprite;//QuestMark_exclamationmark


    // Start is called before the first frame update
    void Start()
    {
        //QuestMark設定
        SetQuestMaker();
    }

    public void SetQuestMaker()
    {
        
        if (QuestManager.questManager.CheckCompleteQuests(this))//QuestCompleteの時のMark設定
        {
            questMarker.SetActive(true);
            theIamge.sprite = questReceivableSprite;
            theIamge.color = Color.yellow;
        }
        else if (QuestManager.questManager.CheckAvailableQuests(this))//Quest受託可能な時のMark設定
        {
            questMarker.SetActive(true);
            theIamge.sprite = questAvailableSprite;
            theIamge.color = Color.yellow;
        }
        else if (QuestManager.questManager.CheckAcceptedQuests(this))//Quest受託したの時のMark設定
        {
            questMarker.SetActive(true);
            theIamge.sprite = questReceivableSprite;
            theIamge.color = Color.gray;
        }
        else
        {
            questMarker.SetActive(false);
        }


    }

}
