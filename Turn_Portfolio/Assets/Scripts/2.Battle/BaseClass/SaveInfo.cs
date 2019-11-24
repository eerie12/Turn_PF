﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInfo
{
    //Status_Save設定

    public static void SaveAllInfo()
    {
        PlayerPrefs.SetString("PLAYERNAME", GameManager.instance.PlayerName);
        PlayerPrefs.SetFloat("BASEATK", GameManager.instance.BaseATK);
        PlayerPrefs.SetFloat("BASEDEF", GameManager.instance.BaseDEF);
        PlayerPrefs.SetFloat("BASEHEAL", GameManager.instance.BaseHeal);
    }
}
