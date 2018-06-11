using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class BasicController : NetworkBehaviour
{

    public enum PlayerState { Idle, Ability, Run, Jump, SpecialJump, Fall };

    //--------------------------------------------------- COMPONENTS ---------------------------------------------------//
    protected Animator anim;
    protected Rigidbody rb;
    protected CapsuleCollider coll;
    private Vector3 dir;
    public  GameObject SpawnPoint;

    public Camera cam;
    public LayerMask GroundLayer;
    public LayerMask MoleLayer;

    //--------------------------------------------------- CONTROL PARAMETERS ---------------------------------------------------//
    protected float MovementSpeed = 4.5f;
    protected float RotationSpeed = 0.15f;
    public float JumpForce = 500f;
    protected float JumpSlow = 1.25f;
    protected float MaxDistance = 50f;
    public float AbilityRange = 0f;

    //--------------------------------------------------- INTERNAL PARAMETERS ---------------------------------------------------//
    public PlayerState State;
    protected bool IsGrounded;
    protected float InputX;
    protected float InputZ;
    protected bool SpecialJumped = false;

    private bool isMole = true;
    private int molePoints = 50;

    private int lives = 3;

    //NETWORK VARS
    [SyncVar(hook = "updateTime")]
    public int remainingTime = 300;

    private float remainingTimeF = 300;



    //Initial setup, gets the components
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();

        Sprite s1 = null;
        Sprite s2 = null;
        Sprite s3 = null;
        Sprite s4 = null;

        Sprite s5 = null;
        Sprite s6 = null;
        Sprite s7 = null;

        if (isLocalPlayer)
        {
            DontDestroyOnLoad(gameObject);

            if (transform.gameObject.tag == "Bean")
            {
                s1 = Resources.Load<Sprite>("bean");
                s2 = Resources.Load<Sprite>("beanName");
                s3 = Resources.Load<Sprite>("beanJump");
                s4 = Resources.Load<Sprite>("beanAbility");
                SpawnPoint = GameObject.Find("sir_bean_spwan");
                DontDestroyOnLoad(GameObject.Find("sir_bean_spwan"));
            }
            else if (transform.gameObject.tag == "Eal")
            {
                s1 = Resources.Load<Sprite>("eal");
                s2 = Resources.Load<Sprite>("ealName");
                s3 = Resources.Load<Sprite>("ealJump");
                s4 = Resources.Load<Sprite>("ealAbility");
                SpawnPoint = GameObject.Find("sir_eal_spawn");
                DontDestroyOnLoad(GameObject.Find("sir_eal_spawn"));
            }
            else if (transform.gameObject.tag == "Loin")
            {
                s1 = Resources.Load<Sprite>("loin");
                s2 = Resources.Load<Sprite>("loinName");
                s3 = Resources.Load<Sprite>("loinJump");
                s4 = Resources.Load<Sprite>("loinAbility");
                SpawnPoint = GameObject.Find("sir_loin_spawn");
                DontDestroyOnLoad(GameObject.Find("sir_loin_spawn"));

            }
            else if (transform.gameObject.tag == "Sage")
            {
                s1 = Resources.Load<Sprite>("sage");
                s2 = Resources.Load<Sprite>("sageName");
                s3 = Resources.Load<Sprite>("sageJump");
                s4 = Resources.Load<Sprite>("sageAbility");
                SpawnPoint = GameObject.Find("sir_sage_spawn");
                DontDestroyOnLoad(GameObject.Find("sir_sage_spawn"));
            }

            GameObject.Find("PlayerIcon").GetComponent<Image>().sprite = s1;
            GameObject.Find("PlayerName").GetComponent<Image>().sprite = s2;
            GameObject.Find("JumpIcon").GetComponent<Image>().sprite = s3;
            GameObject.Find("AbilityIcon").GetComponent<Image>().sprite = s4;

            if (GameObject.Find("BossUI"))
            {
                int mate1lives = 0;
                int mate2lives = 0;
                int mate3lives = 0;

                GameObject.Find("PlayerLives").GetComponent<Text>().text = lives.ToString();
                if (transform.gameObject.tag == "Bean")
                {
                    s5 = Resources.Load<Sprite>("eal");
                    s6 = Resources.Load<Sprite>("loin");
                    s7 = Resources.Load<Sprite>("sage");

                    GameObject x;
                    if (x = GameObject.FindWithTag("Eal")) mate1lives = x.GetComponent<BasicController>().GetLives(); 
                    if (x = GameObject.FindWithTag("Loin")) mate2lives = x.GetComponent<BasicController>().GetLives(); 
                    if (x = GameObject.FindWithTag("Sage")) mate3lives = x.GetComponent<BasicController>().GetLives(); 
                }
                else if (transform.gameObject.tag == "Eal")
                {
                    s5 = Resources.Load<Sprite>("loin");
                    s6 = Resources.Load<Sprite>("sage");
                    s7 = Resources.Load<Sprite>("bean");

                    GameObject x;
                    if (x = GameObject.FindWithTag("Loin")) mate1lives = x.GetComponent<BasicController>().GetLives();
                    if (x = GameObject.FindWithTag("Sage")) mate2lives = x.GetComponent<BasicController>().GetLives();
                    if (x = GameObject.FindWithTag("Bean")) mate3lives = x.GetComponent<BasicController>().GetLives();
                }
                else if (transform.gameObject.tag == "Loin")
                {
                    s5 = Resources.Load<Sprite>("sage");
                    s6 = Resources.Load<Sprite>("bean");
                    s7 = Resources.Load<Sprite>("eal");

                    GameObject x;
                    if (x = GameObject.FindWithTag("Sage")) mate1lives = x.GetComponent<BasicController>().GetLives();
                    if (x = GameObject.FindWithTag("Bean")) mate2lives = x.GetComponent<BasicController>().GetLives();
                    if (x = GameObject.FindWithTag("Eal")) mate3lives = x.GetComponent<BasicController>().GetLives();
                }
                else if (transform.gameObject.tag == "Sage")
                {
                    s5 = Resources.Load<Sprite>("bean");
                    s6 = Resources.Load<Sprite>("eal");
                    s7 = Resources.Load<Sprite>("loin");

                    GameObject x;
                    if (x = GameObject.FindWithTag("Bean")) mate1lives = x.GetComponent<BasicController>().GetLives();
                    if (x = GameObject.FindWithTag("Eal")) mate2lives = x.GetComponent<BasicController>().GetLives();
                    if (x = GameObject.FindWithTag("Loin")) mate3lives = x.GetComponent<BasicController>().GetLives();
                }

                GameObject.Find("MateIcon1").GetComponent<Image>().sprite = s5;
                GameObject.Find("MateIcon2").GetComponent<Image>().sprite = s6;
                GameObject.Find("MateIcon3").GetComponent<Image>().sprite = s7;

                GameObject.Find("MateLives1").GetComponent<Text>().text = mate1lives.ToString();
                GameObject.Find("MateLives2").GetComponent<Text>().text = mate2lives.ToString();
                GameObject.Find("MateLives3").GetComponent<Text>().text = mate3lives.ToString();
            }
        }
    }

    //Updates the internal parameters
    void Update()
    {
        if (this.isLocalPlayer)
        {
            InputX = Input.GetAxisRaw("Horizontal");
            InputZ = Input.GetAxisRaw("Vertical");
            float CurrentInput = Mathf.Sqrt(InputX * InputX + InputZ * InputZ);

            IsGrounded = GroundDetection();
            anim.SetBool("IsGrounded", IsGrounded);

            StatusUpdate(CurrentInput);
        }
        else
        {
            this.cam.enabled = false;
            this.cam.GetComponent<AudioListener>().enabled = false;
        }
        if (isServer)
        {
            remainingTimeF -= Time.deltaTime;
            remainingTime = (int)remainingTimeF;
        }
    }

    //Decides the character's status
    protected virtual void StatusUpdate(float CurrentInput)
    {
        if (Input.GetMouseButtonDown(1))
        {
            CmdMoleAbility();
        }

        //The rest is implemented in every character

    }

    //Detects whether the character is on the ground or not
    protected bool GroundDetection()
    {
        return Physics.CheckCapsule(coll.bounds.center, new Vector3(coll.bounds.center.x, coll.bounds.min.y, coll.bounds.center.z), coll.radius * 0.9f, GroundLayer) 
            ||  Physics.CheckCapsule(coll.bounds.center, new Vector3(coll.bounds.center.x, coll.bounds.min.y, coll.bounds.center.z), coll.radius * 0.9f, MoleLayer);
    }

    //Shoots a raycast and returns the object hit id any
    protected Vector3 AimRayCast()
    {
        RaycastHit CameraHit;
        Ray CameraRay = new Ray(cam.transform.position, cam.transform.forward * MaxDistance);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * MaxDistance, Color.red);
        if (Physics.Raycast(CameraRay, out CameraHit, MaxDistance))
        {
            return CameraHit.point;
        }
        else return transform.position;
    }

    protected GameObject ProximityRayCast()
    {
        RaycastHit AbilityHit;
        Ray AbilityRay = new Ray(transform.position + new Vector3(0, 0.1f, 0), new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized);
        if (Physics.Raycast(AbilityRay, out AbilityHit, AbilityRange))
        {
            Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized * AbilityRange, Color.green);
            return AbilityHit.collider.gameObject;
        }
        else {
            Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized * AbilityRange, Color.red);
            return null;
        }
    }

    //Calculates the movement direction given the input and the camera direction
    protected Vector3 GetDirection()
    {
        var CamForward = cam.transform.forward;
        var CamRight = cam.transform.right;

        CamForward.y = CamRight.y = 0;

        return (CamForward * InputZ + CamRight * InputX).normalized;
    }

    //When on the ground moves the character
    protected void Run()
    {
        State = PlayerState.Run;

        anim.SetBool("Moving", true);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GetDirection()), RotationSpeed);
        transform.position += GetDirection() * MovementSpeed * Time.deltaTime;
    }

    //When on the ground stay still
    protected void Idle()
    {
        State = PlayerState.Idle;
        anim.SetBool("Moving", false);
    }

    //Whether in Idle or Run apply a vertical force to Jump
    protected void Jump()
    {
        State = PlayerState.Jump;
        if ((dir = GetDirection()) != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GetDirection()), RotationSpeed);
            transform.position += GetDirection() * MovementSpeed / JumpSlow * Time.deltaTime;

        }
    }

    protected virtual void CmdUseAbility()
    {
        State = PlayerState.Ability;
        //AimRayCast();
        //AimDifference();
    }

    protected virtual void SpecialJump()
    {
        State = PlayerState.SpecialJump;
        SpecialJumped = true;
    }

    private void updateTime(int t)
    {
        GameObject x;
        if (x = GameObject.Find("TimeText")) x.GetComponent<Text>().text = (int)t/60 + ":" + t%60;
    }

    protected void CmdMoleAbility()
    {

        RaycastHit AbilityHit;
        Ray AbilityRay = new Ray(transform.position + new Vector3(0, 0.68f, 0), AimRayCast() - (transform.position + new Vector3(0, 0.68f, 0)));
        Debug.DrawRay(transform.position + new Vector3(0, 0.68f, 0), AimRayCast() - (transform.position + new Vector3(0, 0.68f, 0)), Color.green);
		if (Physics.Raycast (AbilityRay, out AbilityHit, AbilityRange)) {
			GameObject target = AbilityHit.collider.gameObject;

			if (target.layer == 10)                                 //DA CAMBIARE, USARE IL LAYER MASK
 {            //if (MoleLayer == (MoleLayer | (1 << target.layer)))
				
				target.GetComponent<RespawnWithDelay>().MoleAbility();
			} else if (target.layer == 9) {
				Rigidbody TargetRb = target.GetComponent<Rigidbody> ();
				TargetRb.AddForce (-target.transform.forward * 20, ForceMode.Impulse);
			}
		} else 
		{
			rb.AddForce (-transform.forward * 20, ForceMode.Impulse);
		}
    }

    
    public void Respawn(Vector3 Checkpoint)
    {
       // Debug.Log(Checkpoint);
        if (Checkpoint != Vector3.zero)
        {
           // Debug.Log("sono nell'if");
            gameObject.transform.position = Checkpoint;
        }
        else
        {
           // Debug.Log(SpawnPoint.transform.position);
            gameObject.transform.position = SpawnPoint.transform.position;
        }
    }

    public bool CheckIsGrounded()
    {
        return IsGrounded;
    }

    public int GetLives()
    {
        return lives;
    }
    
    public void DecreaseLives()
    {
        lives--;
    }

    public bool checkIsMole()
    {
        return isMole;
    }
}