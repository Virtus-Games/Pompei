using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyDetectedSide
{
     right, left, none
}
public class AiController : Singleton<AiController>
{
     #region Serialize Field

     [SerializeField] float effectRange;
     [SerializeField] float raycastDistance;

     [SerializeField] Vector3 meteorColliderOffset;
     [SerializeField] PlayerData playerData;
     #endregion


     #region Private Fields 

     public float jumpSpeed = 10.0f;
     public bool isSit;
     private bool isBreak;
     private CharacterController cc;
     private float sidewaySpeed;
     private Vector3 forwardForwardMove;
     private PetrifactionControl petrifaction;

     private Coroutine reduceSpeed;
     private PlayerAttack playerAttack;
     private string lastStone;
     private string lastBarrel;
     private PlayerAnimationControl animationControl;
     private EnemyDetectedSide enemyDetectedSide;
     private bool moveToPlayer = false;
     public bool TotalStop;


     #endregion


     private void Start()
     {
          TotalStop = true;
          cc = GetComponent<CharacterController>();
          animationControl = GetComponent<PlayerAnimationControl>();
          playerAttack = GetComponent<PlayerAttack>();
          petrifaction = GetComponent<PetrifactionControl>();
          enemyDetectedSide = EnemyDetectedSide.none;
          StartCoroutine(MoveToPlayer());
     }

     private void Update()
     {

          if (!TotalStop)
          {
               MeteorDetector();
               EnviromentCheck();

               MoveToPlayerFuc();
               if (!isBreak)
               {
                    ApplyGravity();
                    ForwardMove();
               }

               LavaCrush();

               if (isBreak)
               {
                    Jumping();
               }
          }

     }
     private void ForwardMove()
     {
          forwardForwardMove = (transform.forward * playerData.runSpeed) * Time.deltaTime;
          cc.Move(forwardForwardMove);
     }

     private void SideWayMove(int side)
     {
          Vector3 sidewayVector = transform.right * playerData.swipeSpeed * side * Time.deltaTime;
          cc.Move(sidewayVector);
     }
     private void ApplyGravity()
     {
          Vector3 gravity = new Vector3(0, -playerData.gravityForce, 0) * Time.deltaTime;
          cc.Move(gravity);
     }
     private void OnControllerColliderHit(ControllerColliderHit hit)
     {


          if (hit.gameObject.tag.Equals("barrel") && !(hit.gameObject.name.Equals(lastBarrel)))
          {
               string hitObjectName = hit.gameObject.name;
               playerAttack.AddForceAtObject(hit.gameObject.transform.forward, hit.gameObject);

               animationControl.PlayDraggerAnimClip();
               lastBarrel = hitObjectName;
          }
          if (hit.gameObject.tag.Equals("stone") && !(hit.gameObject.name.Equals(lastStone)))
          {
               animationControl.PlayFallingAnimClip();
               lastStone = hit.gameObject.name;

               if (reduceSpeed != null)
               {
                    StopCoroutine("ReduceRunningSpeed");
               }

               reduceSpeed = StartCoroutine(ReduceRunningSpeed(10.2f, playerData.runSpeed - 5, 2));

          }
          if (hit.collider.CompareTag("PointToJump"))
          {

               animationControl.JumpAnimClip();
               isBreak = true;
               PlayerController.Instance.TotalStop = true;
               GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);
          }

          if (hit.collider.CompareTag("tekne"))
          {
               isSit = true;


          }


     }



     private void OnEnable()
     {
          GameManager.OnGameStateChanged += OnGameStart;
     }

     private void OnGameStart(GAMESTATE obj)
     {
          if (obj == GAMESTATE.START)
          {

               TotalStop = true;
          }
          if (obj == GAMESTATE.PLAY)
          {
               animationControl.PlayRunning();
          }
     }

     private void OnDestroy()
     {
          GameManager.OnGameStateChanged -= OnGameStart;
     }


     private void LavaCrush()
     {
          Collider[] colliders = Physics.OverlapSphere(transform.position, playerData.lavaContactDistance);
          foreach (var item in colliders)
          {
               if (item.gameObject.layer == 7)
               {
                    petrifaction.StartPetrifaction();
               }
               if (item.gameObject.layer == 9)
               {
                    Destroy(item.gameObject);
                    animationControl.PlayFallingAnimClip();
                    if (reduceSpeed != null)
                    {
                         StopCoroutine("ReduceRunningSpeed");
                    }

                    reduceSpeed = StartCoroutine(ReduceRunningSpeed(10.2f, playerData.runSpeed - 5, 2));
               }
          }

     }
     public void Jumping()
     {
          if (!isSit)
               transform.Translate(Vector3.forward * jumpSpeed * Time.deltaTime);

          if (isSit)
               ApplyGravity();
     }

     public void ToSit()
     {
          isSit = true;
          animationControl.PlayToSit();
          GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);

     }

     public void ToTekneRun()
     {
          cc.enabled = false;
          transform.SetParent(Tekne.Instance.GetComponent<Transform>());
          Tekne.Instance.Play();



     }

     private void MeteorDetector()
     {
          Collider[] colliders = Physics.OverlapSphere(transform.position + meteorColliderOffset, effectRange);
          if (colliders.Length > 0)
          {
               foreach (var item in colliders)
               {

                    //  SideWayMove(ObjectSideCheck(item.gameObject));
               }
          }
     }

     private void EnviromentCheck()
     {
          Collider[] colliders = Physics.OverlapSphere(transform.position, raycastDistance);

          if (colliders.Length > 0)
          {
               foreach (var item in colliders)
               {
                    if (item.gameObject.layer == 11)
                         SideWayMove(ObjectSideCheck(item.gameObject));
               }
          }
     }

     private int ObjectSideCheck(GameObject obj)
     {
          Vector3 objPos = obj.transform.position;
          Vector3 differenceVect = transform.position - objPos;

          if (differenceVect.x > 0)
          {
               differenceVect = Vector3.zero;
               enemyDetectedSide = EnemyDetectedSide.left;
               return 1;
          }
          enemyDetectedSide = EnemyDetectedSide.right;

          return -1;
     }

     private void MoveToPlayerFuc()
     {

          if (moveToPlayer)
          {
               Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
               float distanceX = transform.position.x - pos.x;

               if (Vector3.Distance(transform.position, pos) < 10f)
               {
                    return;
               }
               if (distanceX > 0)
               {
                    SideWayMove(-1);
               }
               else if (distanceX < 0)
               {
                    SideWayMove(1);
               }
          }


     }


     IEnumerator ReduceRunningSpeed(float value, float newValue, float duration)
     {
          WaitForSeconds wait = new WaitForSeconds(duration);
          float currentValue = value;
          playerData.runSpeed = newValue;
          yield return wait;
          playerData.runSpeed = currentValue;

     }

     IEnumerator MoveToPlayer()
     {
          while (true)
          {
               moveToPlayer = false;
               float rand1 = Random.Range(1, 3.5f);
               WaitForSeconds wait1 = new WaitForSeconds(rand1);
               yield return wait1;

               moveToPlayer = true;
               float rand2 = Random.Range(0, 0.8f);
               WaitForSeconds wait2 = new WaitForSeconds(rand2);
               yield return wait2;



          }
     }
     private void OnDrawGizmos()
     {
          Gizmos.color = Color.green;
          Gizmos.DrawWireSphere(transform.position + meteorColliderOffset, effectRange);

          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(transform.position, playerData.lavaContactDistance);

          Gizmos.color = Color.magenta;
          Gizmos.DrawWireSphere(transform.position, raycastDistance);

     }


}
