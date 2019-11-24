using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Potion : MonoBehaviour
{
    //Field_Potion設定

    [SerializeField] private float rot_Speed;
    [SerializeField] private GameObject potion_Obj;
    [SerializeField] float currentCoolTime;
    [SerializeField] float maxCoolTime;
    private float start_Speed;//一般回転速度
    private bool heal_Used;//Potionが使用されたか判別
    private bool rot_speed_back;//元のスピードに戻る判定

    // Start is called before the first frame update
    void Start()
    {
        start_Speed = rot_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (potion_Obj)
        {
            potion_Obj.transform.Rotate(Vector3.up * Time.deltaTime * rot_Speed);
            Rot_ReChargeTime();
            Rot_Recover();
        }
    }

    #region Potion_Objectの回転

    private void Rot_ReChargeTime()//回転のCoolTime
    {
        if (heal_Used)
        {
            if (currentCoolTime < maxCoolTime)
                currentCoolTime++;
            else
                heal_Used = false;

        }
      
    }


    private void Rot_Recover()//CoolTimeがmaxになった時、元のスピードに少しずつ戻す
    {
        if (!heal_Used && rot_Speed > start_Speed)
        {           
            rot_Speed -= 10f;
        }
    }


    public void DecreaseCooltime()//CoolTimeをチェック
    {
        rot_Speed *= 10f;
        heal_Used = true;
        currentCoolTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            if(start_Speed == rot_Speed)
            {
                DecreaseCooltime();//CoolTitmeチェックを始める
            }
            
        }
    }

    #endregion

}
