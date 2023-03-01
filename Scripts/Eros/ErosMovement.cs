using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErosMovement : Singleton<ErosMovement>
{
     #region Private Fields
     private Transform player;
     private float xAxis;
     private float zAxis;

     private float yAxis;
     #endregion

     #region  Serialize Fields
     [SerializeField] float followSpeed;
     [SerializeField] Vector3 offset;
     [SerializeField] float strafeAmplitude;

     [SerializeField] float strafeSpeed;
     public bool totalStop;
     #endregion

     private void OnEnable()
     {
          GameManager.OnGameStateChanged += OnGameStart;
     }

     private void OnDestroy()
     {
          GameManager.OnGameStateChanged -= OnGameStart;
     }

     private void OnGameStart(GAMESTATE obj)
     {
          if (obj == GAMESTATE.START)
          {
               totalStop = true;
          }
          if (obj == GAMESTATE.PLAY)
          {
               totalStop = false;
          }
     }
     private void Start()
     {
          player = FindObjectOfType<PlayerController>().transform;
          xAxis = transform.position.x;
          zAxis = transform.position.z;
          yAxis = transform.position.y;
     }

     private void Update()
     {
          if (!totalStop)
          {
               xAxis = Mathf.Lerp(xAxis, player.position.x + offset.x, Time.deltaTime * followSpeed);
               zAxis = Mathf.Lerp(zAxis, player.position.z + offset.z, Time.deltaTime * followSpeed);
               yAxis = Mathf.Sin(Time.time * strafeSpeed) * strafeAmplitude + offset.y;

               transform.position = new Vector3(xAxis, yAxis, zAxis);
          }
     }
}
