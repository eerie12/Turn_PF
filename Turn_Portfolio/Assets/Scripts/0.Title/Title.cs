using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    //TitleManager

    UI_Start start;//Title_UIの設定を参照   
    LoadingSlideManager loading;//LoadingSlideの設定を参照
    private bool loading_Start;//Loading_Coroutine用のBool
    [SerializeField] private string sceneName = "town(pr)";//GameのMainField呼び用のstring

    //TitleのSound
    public AudioSource BGM_Title;
    public AudioSource[] SE_Title;
    

    private void Start()
    {
        start = FindObjectOfType<UI_Start>();
        loading = FindObjectOfType<LoadingSlideManager>();
    }

    #region Title_Button設定

    public void ClickStart()//Start_Button_UI設定の処理
    {
        SE_Title[1].Play();
        start.isFadeToBlack = true;
        StartCoroutine(WaitTitle_BlackScreen());
    }

    public void ClickExit()//Exit_Button_UI設定の処理
    {
        SE_Title[1].Play();
        Application.Quit();
    }

    #endregion

    #region Title_Buttonによる画面のColor設定

    public IEnumerator WaitTitle_BlackScreen()
    {

        yield return new WaitUntil(() => !start.isFadeToBlack);
        BGM_Title.Stop();
        StartCoroutine(Loading());

    }

    IEnumerator Loading()
    {
        if (loading_Start)
        {
            yield break;
        }

        loading_Start = true;

        LoadingSlideManager.loadingFinished = false;
        StartCoroutine(loading.LoadingAppearSlide());

        yield return new WaitUntil(() => LoadingSlideManager.loadingFinished);
        loading_Start = false;
        SceneManager.LoadScene(sceneName);
    }

    #endregion
}
