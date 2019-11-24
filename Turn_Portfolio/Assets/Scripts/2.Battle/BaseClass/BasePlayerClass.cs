using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerClass 
{
    #region Battle_Player_Status_Update用のBaseClass設定

    public string playerName;
    public BaseClass playerClass;

    private float baseATK;
    private float baseDEF;
    private float baseHeal;

    public float BaseATK
    {
        get { return baseATK; }
        set { baseATK = value; }
    }

    public float BaseDEF
    {
        get { return baseDEF; }
        set { baseDEF = value; }
    }

    public float BaseHeal
    {
        get { return baseHeal; }
        set { baseHeal = value; }
    }

    #endregion
}
