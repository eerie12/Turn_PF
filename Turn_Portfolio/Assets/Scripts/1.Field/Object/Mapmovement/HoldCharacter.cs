using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour
{

    //MoveMapの接地設定

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = gameObject.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

        


        
}
