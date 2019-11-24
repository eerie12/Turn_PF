using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCameraController : MonoBehaviour
{
    //Field_Event用のCamera設定

    PlayerControllerrbody playerMovement;//Field_Playerの動きを呼ぶ

    Vector3 startPos;//EventCameraのStart位置
    Quaternion startRot;//EventCameraのStartRotation
    
    public bool camEvent = false;//corが実行されているか
    public Transform MainCam;//追跡するMainCameraの位置

    Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerrbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.eventFlags[0])//StartEventが終了した後
        {
            if (!playerMovement.eventStart)//event途中ではなかった場合MainCameraと同じ位置を維持する
            {
                transform.position = MainCam.position;
                transform.rotation = MainCam.rotation;
            }
        }


    }


    #region 会話Event用のEventCamera設定

    public void CamStartSetting()//Eventが始まる時、最初のCamera位置をSaveしておく
    {
        startPos = MainCam.position;
        startRot = MainCam.rotation;

    }

    //会話Eventの時のEventCamera設定
    public void CameraTargetting(Transform p_Target, float p_CamSpeed = 0.15f, bool p_isReset = false, bool p_isFinish = false)
    {

        if (!p_isReset)
        {
            if (p_Target != null)
            {
                StopAllCoroutines();
                coroutine = StartCoroutine(CameraTargettingCoroutine(p_Target, p_CamSpeed));
                camEvent = false;

            }
        }
        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            StartCoroutine(CameraResetCoroutine(p_CamSpeed, p_isFinish));
            camEvent = false;

        }



    }

    #endregion

    #region EventCamera_Cor

    IEnumerator CameraTargettingCoroutine(Transform p_Target, float p_CamSpeed = 0.15f)//EventCameraをLerpで移動させるCor
    {
        Vector3 t_TargetPos = p_Target.position;
        Vector3 t_TargetFrontPos = t_TargetPos + p_Target.forward;
        Vector3 t_Direction = (t_TargetPos - t_TargetFrontPos).normalized;

        while (transform.position != t_TargetFrontPos || Quaternion.Angle(transform.rotation, Quaternion.LookRotation(t_Direction)) >= 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t_TargetFrontPos, p_CamSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(t_Direction), p_CamSpeed);
            yield return null;

        }
        camEvent = true;

    }

    IEnumerator CameraResetCoroutine(float p_CamSpeed = 0.1f, bool p_isFinish = false)//EventCameraを元の位置まで移動させるCor
    {

        yield return new WaitForSeconds(0.5f);

        playerMovement.playerBody.SetActive(true);
        while (transform.position != startPos || Quaternion.Angle(transform.rotation, startRot) >= 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, p_CamSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, startRot, p_CamSpeed);
            yield return null;

        }
        transform.position = startPos;

        if (p_isFinish)
        {
            //対話が終了したらリセット
            playerMovement.FieldCamMain();
            playerMovement.eventStart = false;
            if (playerMovement.storyEvent)
            {
                playerMovement.storyEvent = false;
            }

            playerMovement.SettingUI(true);
            if (!GameManager.instance.eventFinish)
            {
                SoundManager.instance.PlaySound("Town", 0);
            }

        }

        camEvent = true;
    }

    #endregion
}
