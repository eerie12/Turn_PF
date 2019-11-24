using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    //Battle_State_Enemy_SelectButton設定
   
    public GameObject EnemyPrefab;
    private bool showSelector;

    #region SelectButton処理

    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(EnemyPrefab);//save input enemy prefab 入力されたprefabをセーブ

    }

    public void HideSelector()
    {

            EnemyPrefab.transform.Find("Selector").gameObject.SetActive(false);
    }

    public void ShowSelector()
    {

        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(true);
    }

    #endregion

}
