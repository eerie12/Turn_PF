using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //参照
    PlayerControllerrbody playerMovement;
    HealthManager HM;
    FieldCameraController theEventCam;

    [SerializeField] GameObject dialogueBar;//Dialogue_UI_obj
    [SerializeField] GameObject dialogueNameBar;//Dialogue_Name_UI_obj

    [SerializeField] Text text_Dialogue;
    [SerializeField] Text text_Name;

    Dialogue[] dialogues;
   
    bool isDialogue = false; //対話中の場合trueに変換
    bool isNext = false;//入力待機

    [Header("textの速度")]
    [SerializeField] float textDelay;//textの速度

    int lineCount = 0;//対話のカウントダウン用
    int contextCount = 0;//セリフのカウントダウン用
    

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerrbody>();
        HM = FindObjectOfType<HealthManager>();
        theEventCam = FindObjectOfType<FieldCameraController>();
    }

    void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isNext = false;
                    text_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            CameraTargettingType();
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }                   
                }
            }
        }        
    }

    #region Dialogue設定

    public void ShowDialogue(Dialogue[] p_dialogue)//会話の時のUI設定
    {              
        playerMovement.SettingUI(false);
        isDialogue = true;
        text_Dialogue.text = "";
        text_Name.text = "";
        dialogues = p_dialogue;
        theEventCam.CamStartSetting();
        CameraTargettingType();     
    }

    void CameraTargettingType()//会話の時のCamera設定
    {
        switch (dialogues[lineCount].cameraType)
        {
            case CameraType.ObjectFront: theEventCam.CameraTargetting(dialogues[lineCount].tf_Target);break;//会話開始case
            case CameraType.Reset: theEventCam.CameraTargetting(null,0.1f,true,false); break;//会話終了case
        }
        StartCoroutine(eventFinish());
    }

    void EndDialogue()//会話終了の時のUI設定
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        theEventCam.CameraTargetting(null, 0.1f, true, true);

        HM.isFadeFromBlack = true;
        SettingUI(false);
        dialogueNameBar.SetActive(false);
    }

    void PlaySound()//会話Sound
    {
        if(dialogues[lineCount].VoiceName[contextCount] != "")
        {
            SoundManager.instance.PlaySound(dialogues[lineCount].VoiceName[contextCount],2);
        }
    }
    
    public IEnumerator eventFinish()
    {
        yield return new WaitUntil(() => theEventCam.camEvent);

        StartCoroutine(TypeWriter());

    }
    
    IEnumerator TypeWriter()
    {
        
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", "、");//'を、に置換
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");//'を、に置換

        bool t_white = false, t_red = false; 
        bool t_ignore = false, t_green = false;

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case 'ⓦ': t_white = true; t_red = false; t_green = false; t_ignore = true; break;
                case 'ⓡ': t_white = false; t_red = true; t_green = false; t_ignore = true; break;
                case 'ⓖ': t_white = false; t_red = false; t_green = true; t_ignore = true; break;
            }

            string t_letter = t_ReplaceText[i].ToString();

            if (!t_ignore)
            {
                if (t_white) { t_letter = "<color=#ffffff>" + t_letter + "</color>"; }
                else if (t_red) { t_letter = "<color=#FFA22A>" + t_letter + "</color>"; }
                else if (t_green) { t_letter = "<color=#C8EF76>" + t_letter + "</color>"; }
                text_Dialogue.text += t_letter;
            }
            t_ignore = false;

            yield return new WaitForSeconds(textDelay);
        }
        isNext = true;       
    }

    void SettingUI(bool p_flag)//会話の時のDialogues_UI設定
    {
        dialogueBar.SetActive(p_flag);
        if (p_flag)
        {
            if(dialogues[lineCount].name == "")
            {
                dialogueNameBar.SetActive(false);
            }
            else
            {
                dialogueNameBar.SetActive(true);
                text_Name.text = dialogues[lineCount].name;
            }
        }
        else
        {
            dialogueNameBar.SetActive(false);
        }       
    }

    #endregion
}
