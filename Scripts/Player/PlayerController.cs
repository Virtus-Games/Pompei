using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : Singleton<PlayerController>
{

    #region Private Fields

    private CharacterController m_cc;
    private InputManager IM;
    private Vector3 moveVector;

    public float jumpSpeed = 10.0f;
    public bool isSit;
    private bool isBreak;
    private string lastStone;
    private string lastBarrel;
    private PlayerAnimationControl animationControl;

    private PlayerAttack playerAttack;


    private PetrifactionControl petrifaction;

    private Coroutine reduceSpeed;
    private CameraController cameraC;

    #endregion

    #region Public Fields
    public PlayerData playerData;
    #endregion
    private void Start()
    {
        TotalStop = true;
        m_cc = GetComponent<CharacterController>();
        IM = FindObjectOfType<InputManager>();
        animationControl = GetComponent<PlayerAnimationControl>();
        playerAttack = GetComponent<PlayerAttack>();
        cameraC = FindObjectOfType<CameraController>();

        petrifaction = GetComponent<PetrifactionControl>();
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

    public bool TotalStop;

    private void Update()
    {
        if (!TotalStop)
        {
            if (!isBreak)
            {
                ApplyGravity();
                Movement();
            }

            LavaCrush();
        }

        if (isBreak)
        {
            Jumping();
        }
    }


    public void StartGravity()
    {
        m_cc.enabled = true;
        //StartCoroutine(AppGravityEnum());
        animationControl.Idle();
        Tekne.Instance.Play();
        ErosMovement.Instance.totalStop = true;
    }
    IEnumerator AppGravityEnum()
    {
        while (true)
        {
            transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void Movement()
    {

        moveVector = (transform.forward * playerData.runSpeed - transform.right * IM.SideWay * playerData.swipeSpeed) * Time.deltaTime;
        m_cc.Move(moveVector);
    }
    public void QuickButtonJump()
    {
        animationControl.JumpAnimClip();
        isBreak = true;
    }

    private void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, -playerData.gravityForce, 0) * Time.deltaTime;
        m_cc.Move(gravity);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag.Equals("barrel") && !(hit.gameObject.name.Equals(lastBarrel)))
        {
            string hitObjectName = hit.gameObject.name;
            playerAttack.AddForceAtObject(hit.gameObject.transform.forward, hit.gameObject);
            if (reduceSpeed != null)
            {
                StopCoroutine("ReduceRunningSpeed");
            }

            reduceSpeed = StartCoroutine(ReduceRunningSpeed(11, playerData.runSpeed - 5, 2));
            animationControl.PlayDraggerAnimClip();
            lastBarrel = hitObjectName;
        }
        if (hit.gameObject.tag.Equals("stone") && !(hit.gameObject.name.Equals(lastStone)))
        {
            string hitObjectName = hit.gameObject.name;

            animationControl.PlayFallingAnimClip();
            lastStone = hitObjectName;

            if (reduceSpeed != null)
            {
                StopCoroutine("ReduceRunningSpeed");
            }

            reduceSpeed = StartCoroutine(ReduceRunningSpeed(11, playerData.runSpeed - 5, 2));
        }

        if (hit.collider.CompareTag("PointToJump") )
        {
            TotalStop = true;
            UIManager.Instance.ShowQuickPanel(true);
            AiController.Instance.TotalStop = true;
        }

        if (hit.collider.CompareTag("tekne"))
        {
            if (gameObject.CompareTag("playerai"))
            {
                UIManager.Instance.ShowQuickPanel(false);
                GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);
                isSit = true;
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
        GameManager.Instance.UpdateGameState(GAMESTATE.VICTORY);
        cameraC.Target = GameObject.FindGameObjectWithTag("LeavePoint").transform;
    }

    public void ToTekneRun()
    {
        m_cc.enabled = false;
        transform.SetParent(Tekne.Instance.GetComponent<Transform>());
        Tekne.Instance.Play();

    }

    private void LavaCrush()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, playerData.lavaContactDistance);

        if (colliders.Length > 0)
        {
            foreach (var item in colliders)
            {
                if (item.gameObject.layer == 7)
                {
                    petrifaction.StartPetrifaction();
                    GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);

                }

                if (item.gameObject.layer == 9)
                {
                    Destroy(item.gameObject);
                    animationControl.PlayFallingAnimClip();
                    if (reduceSpeed != null)
                    {
                        StopCoroutine("ReduceRunningSpeed");
                    }

                    reduceSpeed = StartCoroutine(ReduceRunningSpeed(11, playerData.runSpeed - 5, 2));
                }


            }
        }


    }

    IEnumerator ReduceRunningSpeed(float value, float newValue, float duration)
    {
        WaitForSeconds wait = new WaitForSeconds(duration);        
        playerData.runSpeed = newValue;
        yield return wait;
        playerData.runSpeed = value;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(transform.position, playerData.lavaContactDistance);
    }

}
