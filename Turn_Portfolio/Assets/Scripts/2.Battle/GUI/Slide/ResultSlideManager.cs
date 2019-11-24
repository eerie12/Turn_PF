using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSlideManager : MonoBehaviour
{
    //ResultSlide設定

    public Animation anim;

    public static bool resultFinished = true;
    public bool ResultSlideOnOff = false;
    private Text EnemyText;
    private Text CoinText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    #region 結果画面

    public IEnumerator BattleResultAppear()
    {
        anim.Play("ResultSlideAppear");

        yield return new WaitForSeconds(0.5f);


        resultFinished = true;
    }

    public void ResultAppear()
    {
        resultFinished = false;
        StartCoroutine(BattleResultAppear());
    }

    #endregion






}
