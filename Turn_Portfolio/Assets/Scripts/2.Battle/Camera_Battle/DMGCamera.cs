using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGCamera : MonoBehaviour
{
    //Battle_Player_DamegeCamera(CameraShakeに使用される)設定

    public Camera MainCMA;
 
    // Update is called once per frame
    void Update()
    {
        transform.position = MainCMA.transform.position;
        transform.rotation = MainCMA.transform.rotation;
    }
}
