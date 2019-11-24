using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Camera : MonoBehaviour
{
    //BossEvent用のカメラ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 1.25f);
    }
}
