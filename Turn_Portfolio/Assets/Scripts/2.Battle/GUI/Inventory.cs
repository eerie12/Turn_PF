using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    //Battle_Player_Inventory設定

    BattleStateMachine BSM;

    public static bool inventoryActivated = false;
    public static bool blackActivated = false;
    public static bool helpActiveated = false;
   
    [SerializeField]
    private GameObject go_InventoryBase;//Base_obj
    [SerializeField]
    private GameObject go_SlotsParebt;//slot_obj
    public GameObject gameOverText;
    public GameObject titleButton;//UI_TitleButton_obj
    public GameObject endButton;//UI_ExitButton_obj
    public GameObject help;//UI_Help_obj
    private Slot[] slots;
    public GameObject blackScreenObject_Battle;//battle_Screen_Color設定用_obj
    public Image blackScreen_Battle;//battle_Screen_Color設定用_image
    public bool EndBattleAction;
    public bool isFadeToDialogueBackground_Battle;//helpがOnになっている時Off判定
    public bool isFadeToBlack_Battle;//GameOver判定
    public bool isFadeFromBlack_Battle;//helpがOffになっている時On判定
    public float fadeSpeed_Battle;//Screen_Color_Fade_Speed

    [SerializeField] private string sceneName = "Title";

    // Start is called before the first frame update
    void Start()
    {
        inventoryActivated = false;
        blackActivated = false;
        helpActiveated = false;
        slots = go_SlotsParebt.GetComponentsInChildren<Slot>();
        BSM = FindObjectOfType<BattleStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        Setting();

        if (isFadeToBlack_Battle)
        {
            blackScreenObject_Battle.SetActive(true);
            StartCoroutine(blackScreen_EndBattle());
        }

        if (isFadeToDialogueBackground_Battle)
        {
            blackScreenObject_Battle.SetActive(true);
            blackScreen_Battle.color = new Color(blackScreen_Battle.color.r, blackScreen_Battle.color.g, blackScreen_Battle.color.b, Mathf.MoveTowards(blackScreen_Battle.color.a, 0.3f, fadeSpeed_Battle * Time.deltaTime));
            if (blackScreen_Battle.color.a == 0.3f)
            {
                if (helpActiveated)
                {
                    Time.timeScale = 0.0f;
                }
                isFadeToDialogueBackground_Battle = false;
            }
        }




        if (isFadeFromBlack_Battle)
        {
            blackScreen_Battle.color = new Color(blackScreen_Battle.color.r, blackScreen_Battle.color.g, blackScreen_Battle.color.b, Mathf.MoveTowards(blackScreen_Battle.color.a, 0f, fadeSpeed_Battle * Time.deltaTime));

            if (blackScreen_Battle.color.a == 0f)
            {
                blackScreenObject_Battle.SetActive(false);
                isFadeFromBlack_Battle = false;
            }


        }

        
    }

    private void Setting()
    {
        if (blackActivated)
        {
            isFadeToBlack_Battle = true;
        }
        else if (helpActiveated)
        {
            OpenHelp();
        }
        else if(!inventoryActivated)
        {
            CloseHelp();
            CloseInventory();
        }
        else if(inventoryActivated)
        {            
            OpenInventory();

        }


        
    }

    #region Inventory_UI設定

    public void TryOpenInventory()
    {
        SoundManager.instance.PlaySound("Button", 1);

        inventoryActivated = !inventoryActivated;
        if (inventoryActivated)
        {
            titleButton.SetActive(true);
            endButton.SetActive(true);
        }
        else
        {
            titleButton.SetActive(false);
            endButton.SetActive(false);
        }


    }

    private void OpenInventory()
    {
        isFadeToDialogueBackground_Battle = true;
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        
        isFadeFromBlack_Battle = true;
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item_Pr _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itmeName == _item.itmeName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }


        }

        for (int i = 0; i < slots.Length; i++)
        {

            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }


        }
    }

    #endregion

    #region Help_UI設定

    public void TryOpenHelp()
    {
        SoundManager.instance.PlaySound("Button", 1);
        helpActiveated = !helpActiveated;
    }

    private void OpenHelp()
    {

        isFadeToDialogueBackground_Battle = true;
        help.SetActive(true);
    }

    private void CloseHelp()
    {
        if(Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
        }
        
        isFadeFromBlack_Battle = true;
        help.SetActive(false);
    }

    #endregion

    #region GameOver処理

    private IEnumerator blackScreen_EndBattle()
    {



        yield return new WaitForSeconds(2f);
        go_InventoryBase.SetActive(false);
        help.SetActive(false);
        for (int i = 0; i < BSM.EnemysInBattle.Count; i++)
        {

            BSM.EnemysInBattle[i].transform.Find("EnemyUI").gameObject.SetActive(false);

            BSM.EnemysInBattle[i].transform.Find("EnemyUI").transform.Find("Selector").gameObject.SetActive(false);


        }
       
        BSM.enemyBarStop = true;
        blackScreen_Battle.color = new Color(blackScreen_Battle.color.r, blackScreen_Battle.color.g, blackScreen_Battle.color.b, Mathf.MoveTowards(blackScreen_Battle.color.a, 1f, (fadeSpeed_Battle - 1.5f) * Time.deltaTime));
        if (blackScreen_Battle.color.a == 1f)
        {

            
            gameOverText.SetActive(true);
            titleButton.SetActive(true);
            endButton.SetActive(true);





        }



    }

    public void ClickToTitle()
    {

        SoundManager.instance.PlaySound("Button", 1);

        Invoke("MoveTitle", 0.5f);

    }

    public void MoveTitle()
    {
        blackActivated = false;
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("QuestManager"));
        SceneManager.LoadScene(sceneName);
    }

    public void ClickToExit()
    {
        SoundManager.instance.PlaySound("Button", 1);
        Invoke("MoveExit", 0.5f);

    }

    public void MoveExit()
    {
        blackActivated = false;
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("QuestManager"));
        Application.Quit();
    }

    #endregion

}
