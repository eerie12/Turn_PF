using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWild : BaseAttacks
{
    //Battle_Boss_Skill_Wild(全体攻撃)設定

    public EnemyWild()
    {
        attackName = "EnemyFire";
        attackDamage = 20f;
        attackCost = -25;

    }
    
}
