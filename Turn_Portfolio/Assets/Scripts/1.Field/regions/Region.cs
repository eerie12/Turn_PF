using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    //BattleScene移動の変数

    public int maxAmountEnemys = 4;//モンスターの数
    public string battleScene;//Sceneの名前
    public List<GameObject> possibleEnemmys = new List<GameObject>();//配置するモンスターの種類

}
