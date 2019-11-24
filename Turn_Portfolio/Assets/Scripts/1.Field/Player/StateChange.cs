using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour
{
    //PlayerのAttackとStateChange処理

    private PlayerControllerrbody PlayerController;
    public CapsuleCollider SwordColider;

    [SerializeField] private ParticleSystem fieldATK_Particle;

    void Start()
    {
      PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerrbody>();     
      SwordColider.enabled = false;

    }

    #region StateChange処理

    void PlayerStateChange()
    {
        PlayerController.StateMove();

    }

    #endregion

    #region FieldATK処理

    void FieldATK_ParticleOn()
    {
        fieldATK_Particle.Play();
        SoundManager.instance.PlaySound("Slash1", 1);
    }

    void FieldATK_Start()
    {
        SwordColider.enabled = true;

       
    }

    void FieldATK_End()
    {
        SwordColider.enabled = false;
        fieldATK_Particle.Clear();
    }

    #endregion
}
