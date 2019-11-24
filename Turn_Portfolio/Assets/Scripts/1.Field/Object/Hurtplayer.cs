using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtplayer : MonoBehaviour
{
    public int damgeToGive;

    #region PlayerにDamage処理

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            FindObjectOfType<HealthManager>().HurtPlayer(damgeToGive);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            FindObjectOfType<HealthManager>().HurtPlayer(damgeToGive);
        }
    }

    #endregion


}
