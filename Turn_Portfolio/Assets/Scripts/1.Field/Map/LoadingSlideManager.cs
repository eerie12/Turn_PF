using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSlideManager : MonoBehaviour
{
    //LoadingSlideの設定

    public Animation anim;
    AlarmSlideManager alarmManager;

    public static bool loadingFinished = true;
    public static bool loadingOnOff;


    void Start()
    {
        anim = GetComponent<Animation>();
        alarmManager = FindObjectOfType<AlarmSlideManager>();
        loadingOnOff = false;
    }

    #region Loading設定

    public IEnumerator LoadingAppearSlide()
    {
        anim.Play("LoadingSlideAppear");
        if (SoundManager.instance)
        {
            SoundManager.instance.StopBGM();
        }
        
        yield return new WaitForSeconds(0.5f);
        
        loadingOnOff = true;
        loadingFinished = true;
    }

    public IEnumerator LoadingDisappearSlide(string bgmName, int number)
    {
        
        if(Time.timeScale < 1f)
        {
            Time.timeScale = 1f;
        }

        anim.Play("LoadingSlideDisappear");
        if (GameManager.instance.eventFlags[0])
        {
            SoundManager.instance.PlaySound(bgmName, number);
        }       
        yield return new WaitForSeconds(0.2f);

        if (GameManager.instance.advancedBattle)
        {
            AlarmSlideManager.alarmInt = 2;
            AlarmSlideManager.alarmFinished = false;
            StartCoroutine(alarmManager.BattleAlarmAppear());
        }
        

        loadingOnOff = false;
        loadingFinished = true;

    }

    void LoadingAppearSound()
    {
        if (SoundManager.instance)
        {
            SoundManager.instance.PlaySound("LoadingAppear", 1);
        }
        else
        {
            Title title = FindObjectOfType<Title>();
            title.SE_Title[0].Play();
        }
        
    }

    void LoadingDisappearSound()
    {
        SoundManager.instance.PlaySound("LoadingDisappear", 1);
    }

    #endregion

}
