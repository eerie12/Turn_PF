using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QLogButtonScript : MonoBehaviour
{

    public int questID;
    public Text questTitle;

    #region Logbutton設定

    public void ShowAllInfos()
    {      
        QuestManager.questManager.ShowQuestLog(questID);
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestLogPanel();
    }

    #endregion
}
