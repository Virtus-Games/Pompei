using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
     #region  private fields
     private Animator m_anim;

     private const string isJump = "isJump";
     private const string isSit = "isSit";

     #endregion

     private void Start()
     {
          m_anim = GetComponent<Animator>();
     }

     private void OnEnable()
     {
          GameManager.OnGameStateChanged += UpdateGameState;
     }

     private void UpdateGameState(GAMESTATE obj)
     {

     }

     private void OnDisable()
     {
          GameManager.OnGameStateChanged -= UpdateGameState;
     }

     public void PlayFallingAnimClip()
     {

          m_anim.SetTrigger("falling");
     }

     public void PlayDraggerAnimClip()
     {
          m_anim.SetTrigger("dragger");
     }

     public void JumpAnimClip()
     {
          m_anim.SetTrigger(isJump);
     }

     public void PlayToSit()
     {
          m_anim.SetBool(isSit, true);
     }
     public void PlayRunning()
     {
          m_anim.SetBool("running", true);
     }

     public void Idle()
     {
          m_anim.SetBool("running", false);
          m_anim.SetBool("idle", true);
     }
}
