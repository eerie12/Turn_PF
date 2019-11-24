using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseAttacks : MonoBehaviour
{
    #region  Attackの基本変数

    public string attackName;
    public string attackDiscription;
    public float attackDamage;//基本ダメージ 
    public float attackCost;
    public float attackBuff;
    public float attackHeal;

    #endregion
}
