using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    #region PlayerHealth変数

    public float maxHp;
    public float currentHp;
    public float currentMp;
    public int maxSp;
    public int currentSp;
    private float startHp;

    public PlayerControllerrbody thePlayer;

    public float invincibillityLength;
    private float invincibillityCounter;

    public Renderer[] playerRenderer;

    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public Vector3 startPoint;
    public float respawnLength;

    public GameObject deathEffect;
    public GameObject blackScreenObject;
    public Image blackScreen;
    public bool isFadeToBlack = false;
    public bool isFadeFromBlack = false;
    public bool isFadeToDialogueBackground = false;
    public float fadeSpeed;
    public float waitForFade;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //GameManagerからHpとMpの情報をもらう
        #region HpとMpのStartの処理(Field)

        if (GameManager.instance.hpBattleExit == 0)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp = GameManager.instance.hpBattleExit;
        }
        currentMp = GameManager.instance.mpBattleExit;

        startPoint = new Vector3(-6.7f, 1.5f, -22);
        currentSp = maxSp;

        #endregion

        //respawnPointに触れていない場合、respawnPointはStartPoint
        #region Respawn設定

        if (GameManager.instance.respawnPosition == Vector3.zero)
        {
            respawnPoint = startPoint;
        }
        else
        {
            respawnPoint = GameManager.instance.respawnPosition;
        }

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region PlayerHide処理

        if (invincibillityCounter > 0)
        {
            invincibillityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                for (int h = 0; h < 6; h++)
                {
                    playerRenderer[h].enabled = !playerRenderer[h].enabled;
                }
                flashCounter = flashLength;
            }
            if (invincibillityCounter <= 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    playerRenderer[i].enabled = true;
                }
                thePlayer.knockBack = false;
            }
        }

        #endregion

        #region BlackScreen処理

        if (isFadeToBlack)
        {
            blackScreenObject.SetActive(true);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        
        if (isFadeToDialogueBackground)
        {
            blackScreenObject.SetActive(true);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0.3f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0.3f)
            {
                isFadeToDialogueBackground = false;
            }
        }
        
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            
            if (blackScreen.color.a == 0f)
            {
                blackScreenObject.SetActive(false);
                isFadeFromBlack = false;
            }
            
            
        }

        #endregion

    }


    #region PlayerHp処理

    public void HurtPlayer(int damage)
    {
        if (invincibillityCounter <= 0)
        {
            startHp = currentHp;
            currentHp -= damage;
           

            if (currentHp <= 0)
            {
                SoundManager.instance.PlaySound("Dead_Field", 1);
                currentHp = 0;
                Respawn();
            }

        }





    }

    public void HealthPlayer(int healAmount)
    {
        currentHp += healAmount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

    }

    #endregion

    #region PlayerRespawn処理

    public void Respawn()
    {

        if (!isRespawning)
        {

            StartCoroutine("RespawnCo");
        }
    }
    
    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;
      
        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        currentHp = startHp;

        invincibillityCounter = invincibillityLength;

        for (int k = 0; k < 6; k++)
        {
            playerRenderer[k].enabled = false;
        }


        flashCounter = flashLength;

    }
    
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    #endregion


}
