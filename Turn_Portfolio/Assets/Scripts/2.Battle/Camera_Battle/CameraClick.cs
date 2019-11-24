using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
    //Battle_Player_MainCamera設定

    private BattleStateMachine BSM;
    
    private EnemyStateMachine ESM;
    public Animator cameraAnim;
    public bool rayCast;
    public bool playerTurn = false;

    //攻撃の時のcamera移動
    public float cameraSpeed;
    private Vector3 disValue;

    public Transform pivot;

    [SerializeField] private GameObject[] battleUI;


    // Start is called before the first frame update
    void Start()
    {
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        
        
        for(int i = 0; i< battleUI.Length; i++)
        {
            battleUI[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //RayCastの処理
        if (Input.GetMouseButtonDown(0) && BSM.click && BSM.enemyClicked)//Playerのターンの時だけRayCastを使用
        {
            rayCast = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit) || hit.collider.tag == "DeadEnemy")
                return;

            var targetCharacter = hit.collider.GetComponent<EnemyStateMachine>();
            if (targetCharacter != null)
            {
                foreach (GameObject enemy in BSM.EnemysInBattle) //enemysinbattleの中のenemyprefabsを呼ぶ
                {
                    BSM.Input2(targetCharacter.gameObject);
                    rayCast = false;
                }
            }

        }
        Anim();
    }

    void Anim()//MainCamera_Animation
    {
        cameraAnim.SetBool("PlayerTurn", playerTurn);
    }
    
    void BattleUIStart()//Battle開始にUIの処理に使用
    {
        for (int i = 0; i < battleUI.Length; i++)
        {
            battleUI[i].SetActive(true);          
        }
    }
    
}
