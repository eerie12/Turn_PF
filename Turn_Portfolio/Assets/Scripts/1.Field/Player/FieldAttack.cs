using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldAttack : MonoBehaviour
{
    //FieldAttack設定

    [SerializeField] private GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FieldEnemy" || other.gameObject.tag == "FieldEnemyB")
        {
            SoundManager.instance.PlaySound("Sturn", 1);
            other.gameObject.GetComponent<FieldEnemy>().Damage(1,transform.position);
        }
    }
}
