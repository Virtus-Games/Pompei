using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LavaMove : Singleton<LavaMove>
{
     #region  Serialized Fields
     [SerializeField] LavaData data;
     [SerializeField] Transform[] followedBones;

     [SerializeField] Vector3 attachOffset;
     public bool totalStop;
     #endregion
     private void Start()
     {
          totalStop = true;
     }

     private void OnEnable() {
          GameManager.OnGameStateChanged += OnGameStart;
     }

      private void OnDestroy() {
          GameManager.OnGameStateChanged -= OnGameStart;
     }

     private void OnGameStart(GAMESTATE obj)
     {
          if(obj == GAMESTATE.START)
          {
               totalStop = true;
          }
          if(obj == GAMESTATE.PLAY)
          {
               totalStop = false;
          }
     }

     private void Update()
     {
          if (!totalStop)
          {
               transform.position -= transform.forward * data.moveSpeed * Time.deltaTime;
               foreach (var bone in followedBones)
               {
                    bone.localPosition = new Vector3(bone.localPosition.x, transform.position.z, bone.localPosition.z) - attachOffset;
               }
          }
     }
}
