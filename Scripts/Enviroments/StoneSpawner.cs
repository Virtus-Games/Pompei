using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
     #region Serialized Fields
     [Header("Gizmos Settings")]
     [SerializeField] Vector3 cubeSize;

     [Header("Spawn Settings")]
     [SerializeField] List<GameObject> stoneRefs;


     [Tooltip("Düşen kayaların yere çarpma hızı")]
     [SerializeField] float throwingForce;


     [SerializeField] Transform parentPoint;
     #endregion

     #region  Private Fields
     private GameObject stone;
     #endregion

     private void Start()
     {

     }
     private void OnDestroy()
     {
          GameManager.OnGameStateChanged -= OnGameStart;
     }
     private void OnEnable() => GameManager.OnGameStateChanged += OnGameStart;

     private void OnGameStart(GAMESTATE obj)
     {

          if (obj == GAMESTATE.PLAY)
          {
               StartSpawner();
          }
     }

     public void StartSpawner()
     {
          StartCoroutine(SpawnstoneRef());
     }


     private IEnumerator SpawnstoneRef()
     {
          while (true)
          {
               WaitForSeconds wait = new WaitForSeconds(Random.Range(4, 6));

               yield return wait;
               ExplosionEffect.TrigExplosionParticle();

               Vector3 randomPoint = new Vector3(Random.Range(-cubeSize.x / 2, cubeSize.x / 2), 0, Random.Range(-cubeSize.z / 2, cubeSize.z / 2));

               stone = Instantiate(stoneRefs[Random.Range(0, stoneRefs.Count)], transform.position + randomPoint, Quaternion.identity, parentPoint);
               stone.GetComponent<Rigidbody>().AddForce(Vector3.down * throwingForce, ForceMode.Impulse);

          }

     }



     private void OnDrawGizmos()
     {
          Gizmos.color = Color.white;
          Gizmos.DrawWireCube(transform.position, cubeSize);
     }
}
