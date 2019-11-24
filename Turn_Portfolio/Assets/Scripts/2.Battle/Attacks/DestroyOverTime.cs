using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    //一定時間後Destroy

    public float lifetime;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,lifetime);
        
        
    }
}
