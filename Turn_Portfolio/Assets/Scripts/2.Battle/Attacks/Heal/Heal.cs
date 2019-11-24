using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BaseAttacks
{
    //Battle_Player_Skill_Heal設定

    public Heal()
    {
        attackName = "Heal";
        attackDamage = 0f;
        attackCost = 30f;
        attackHeal = 20f;

    }
}
