using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusAttack : MonoBehaviour
{
     #region Serialize Fields                
     [SerializeField] float attackFrequency;
     [SerializeField] float throwingForce;
     [SerializeField] GameObject lightningPrefab;
     [SerializeField] Vector3 targetOffset;
     [SerializeField] Transform spawnPoint;
     #endregion

     #region Private Fields
     private Transform player;
     private Animator anim;

     private CameraShake cs;
     #endregion

     private bool totalStop;

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
               StartCoroutine(AttackTimer());
          }
     }
     private void Start()
     {
          player = FindObjectOfType<PlayerController>().transform;
          anim = GetComponent<Animator>();
          cs = FindObjectOfType<CameraShake>();
     }


     IEnumerator AttackTimer()
     {
          while (true)
          {

               WaitForSeconds wait = new WaitForSeconds(attackFrequency);

               yield return wait;

               Vector3 direction = (player.position + targetOffset - transform.position).normalized;

               anim.SetTrigger("shoot");

               yield return new WaitForSeconds(.3f);
               GameObject lightning = Instantiate(lightningPrefab, spawnPoint.position, lightningPrefab.transform.rotation,transform.parent);

               Rigidbody rb = lightning.GetComponent<Rigidbody>();

               rb.AddForce(direction * throwingForce, ForceMode.Impulse);
               cs.VolcanoExplosionShaker();


          }
     }
}
