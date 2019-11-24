using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Start : MonoBehaviour
{
    Title title;//Title設定を参照  
    public GameObject blackScreenObject;//blackScreen処理用のObj
    public Image blackScreen;//ObjのColor処理用のImage

    //画面Fade用の変数
    public bool isFadeToBlack;
    public bool isFadeFromBlack;
    [SerializeField] float fadeSpeed;

    // Start is called before the first frame update

    private void Awake()
    {
        Screen.SetResolution(1280, 720, true);//最初の画面解像度設定
    }

    void Start()
    {
        title = FindObjectOfType<Title>();
        isFadeFromBlack = true;//最初の画面Color設定
    }

    // Update is called once per frame
    void Update()
    {
        #region 画面のFade設定

        //画面のFadeToBlackの処理
        if (isFadeToBlack)
        {
            blackScreenObject.SetActive(true);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                
                isFadeToBlack = false;
            }
        }

        //画面のFadeFromBlackの処理
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                title.BGM_Title.Play();
                blackScreenObject.SetActive(false);
                isFadeFromBlack = false;
            }


        }

        #endregion
    }


}

