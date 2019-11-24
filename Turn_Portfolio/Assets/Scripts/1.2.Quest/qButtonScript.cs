using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class qButtonScript : MonoBehaviour
{
    public int questID;//questを区分する番号
    public Text questTitle;//questのTitle

    #region questButton

    public Transform acceptButton;
    public Transform giveUpButton;
    public Transform completeButton;

    private qButtonScript acceptButtonScript;
    private qButtonScript giveUpButtonScript;
    private qButtonScript completeButtonScript;

    #endregion

    #region questButtonの外部

    private Image acceptImage;
    private Image giveUpButtonImage;
    private Image completeButtonImage;
    private Button acceptButtonOnOff;
    private Button giveUpButtonOnOff;
    private Button completeButtonOnOff;
    private Text acceptText;
    private Text giveUpText;
    private Text completeText;

    #endregion

    private void Awake()
    {
        
    }

    //Show all info
    void Start()
    {
        #region acceptButtonの初期設定

        acceptButton =  GameObject.Find("QuestButton_Obj").transform.GetChild(0);
        acceptButtonScript = acceptButton.GetComponent<qButtonScript>();
        acceptImage = acceptButton.GetComponent<Image>();
        acceptButtonOnOff = acceptButton.GetComponent<Button>();
        acceptText = acceptButton.transform.Find("Text").GetComponent<Text>();
        acceptImage.enabled = false;
        acceptButtonOnOff.enabled = false;
        acceptText.enabled = false;

        #endregion

        #region giveUpButtonの初期設定

        giveUpButton = GameObject.Find("QuestButton_Obj").transform.GetChild(1);
        giveUpButtonScript = giveUpButton.GetComponent<qButtonScript>();
        giveUpButtonImage = giveUpButton.GetComponent<Image>();
        giveUpButtonOnOff = giveUpButton.GetComponent<Button>();
        giveUpText = giveUpButton.transform.Find("Text").GetComponent<Text>();
        giveUpButtonImage.enabled = false;
        giveUpButtonOnOff.enabled = false;
        giveUpText.enabled = false;

        #endregion

        #region completeButtonの初期設定

        completeButton = GameObject.Find("QuestButton_Obj").transform.GetChild(2);
        completeButtonScript = completeButton.GetComponent<qButtonScript>();
        completeButtonImage = completeButton.GetComponent<Image>();
        completeButtonOnOff = completeButton.GetComponent<Button>();
        completeText = completeButton.transform.Find("Text").GetComponent<Text>();
        completeButtonImage.enabled = false;
        completeButtonOnOff.enabled = false;
        completeText.enabled = false;

        #endregion

        //最初全てOffにしておく
        Invoke("QuestButtonSet", 0.01f);

    }

    #region 最初ButtonのOff設定

    public void QuestButtonSet()
    {
        acceptImage.enabled = true;
        acceptButtonOnOff.enabled = true;
        acceptText.enabled = true;

        giveUpButtonImage.enabled = true;
        giveUpButtonOnOff.enabled = true;
        giveUpText.enabled = true;

        completeButtonImage.enabled = true;
        completeButtonOnOff.enabled = true;
        completeText.enabled = true;

        acceptButton.gameObject.SetActive(false);
        giveUpButton.gameObject.SetActive(false);
        completeButton.gameObject.SetActive(false);
    }

    #endregion

    #region buttonの情報を表示

    public void ShowAllInfos()
    {
        SoundManager.instance.PlaySound("Button", 1);
        QuestUIManager.uiManager.showSelectedQuest(questID);
        
        if (QuestManager.questManager.RequestAvailableQuest(questID))
        {
            
            
            acceptButton.gameObject.SetActive(true);
            acceptButtonScript.questID = questID;
        }
        else
        {
            acceptButton.gameObject.SetActive(false);
        }
        //give up button
        if (QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            giveUpButton.gameObject.SetActive(true);
            giveUpButtonScript.questID = questID;
        }
        else
        {
            giveUpButton.gameObject.SetActive(false);
        }
        //complete button
        if (QuestManager.questManager.RequestCompleteQuest(questID))
        {
            completeButton.gameObject.SetActive(true);
            completeButtonScript.questID = questID;
        }
        else
        {
            completeButton.gameObject.SetActive(false);
        }
    }

    public void AcceptQuest()
    {
        QuestManager.questManager.AcceptQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        //Update All Npc 
        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach(QuestObject obj in currentQuestGuys)
        {
            obj.SetQuestMaker();
        }

    }

    public void GiveUpQuest()
    {
        QuestManager.questManager.GIveUpQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        //Update All Npc 
        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject obj in currentQuestGuys)
        {
            obj.SetQuestMaker();
        }

    }

    public void CompleteQuest()
    {
        QuestManager.questManager.CompleteQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        //Update All Npc 
        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject obj in currentQuestGuys)
        {
            obj.SetQuestMaker();
        }

        

    }

    #endregion

    #region ClosePanelの時の設定

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestPanel();
        acceptButton.gameObject.SetActive(false);
        giveUpButton.gameObject.SetActive(false);
        completeButton.gameObject.SetActive(false);
    }

    #endregion

}
