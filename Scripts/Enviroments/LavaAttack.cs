using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAttack  : MonoBehaviour
{
    #region Serialized Fields
 
    [Header("ATTACK PARAMETERS")]
    [SerializeField] float attackDelay;
    [SerializeField] AnimationCurve forwardAttack;


    [Header("BONES")]
    [SerializeField] List<Transform> bones;
    [SerializeField] List<Transform> leftAttackBones;
    [SerializeField] List<Transform> rightAttackBones;
    [SerializeField] float updateRandomPosesDelay;

    [Header("MOTION PARAMETERS")]
    [SerializeField] float boneMotionAmplitudeX;
    [SerializeField] float boneMotionAmplitudeY;
    [SerializeField] float boneMotionAmplitudeZ;

    #endregion


    #region Private Fields
    private List<Vector3> randomPoses = new List<Vector3>();
    private List<Vector3> defaultPoses = new List<Vector3>();

    private List<List<Transform>> attackBones = new List<List<Transform>>();

 
    private GameObject player;

    private float time;
    private float counter;

    private Coroutine attackCoroutine;
    #endregion
    private void Start()
    {
        
        attackBones.Add(rightAttackBones);
        attackBones.Add(leftAttackBones);

        for (int i = 0; i < bones.Count; i++)
        {
            defaultPoses.Add(bones[i].localPosition);
        }
        RandomPoses();
        StartCoroutine(UpdateRandomPoses());
      player = FindObjectOfType<PlayerController>().gameObject;

    }
    private void Update()
    {     

        //BoneMotion();

        Counter();

      }


    private void Counter()
    {
        if (time == 0)
            counter += Time.deltaTime;
        if (counter > attackDelay)
        {
            StartCoroutine(Attacker());
            counter = 0;
        }
    }

    private void RandomPoses()
    {
        randomPoses.Clear();

        for (int i = 0; i < bones.Count; i++)
        {
            Vector3 currentBoneDefaultPose = defaultPoses[i];
            randomPoses.Add(new Vector3(Random.Range(currentBoneDefaultPose.x, currentBoneDefaultPose.x + boneMotionAmplitudeX),
            Random.Range(currentBoneDefaultPose.y, currentBoneDefaultPose.y + boneMotionAmplitudeY),
             Random.Range(currentBoneDefaultPose.z, currentBoneDefaultPose.z + boneMotionAmplitudeZ)));
        }

    }


    private void BoneMotion()
    {

        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].localPosition = Vector3.Slerp(bones[i].localPosition, randomPoses[i], Time.deltaTime / 4);
        }

    }


    IEnumerator Attacker()
    {
        int randIndex = Random.Range(0,2);
       
        while (time < forwardAttack[forwardAttack.length - 1].time)
        {
            time += Time.deltaTime;
            foreach (var bone in attackBones[randIndex])
            {
                bone.localPosition = Vector3.Lerp(bone.localPosition,new Vector3(bone.localPosition.x,forwardAttack.Evaluate(time),bone.localPosition.z),Time.deltaTime*10);
            }
            yield return null;

        }
       
        time = 0;
    }
    IEnumerator UpdateRandomPoses()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(updateRandomPosesDelay);

            yield return wait;
            RandomPoses();
        }
    }

}
