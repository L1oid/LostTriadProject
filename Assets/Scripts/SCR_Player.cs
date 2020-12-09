using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Player : MonoBehaviour
{
    //Runing
    public float MaxSpeed = 44f;
    public float Acceleration = 10f;
    public float DeAcceleration = 10f;
    public float Speed;
    bool facingRight = true;//end Runing
    //Jumping
    public float JumpForce = 190f;
    public bool Grounded = false;
    public Transform GroundedCheck;
    public float GroundedCheckRadius = 0.2f;
    bool canJump = true;
    public LayerMask whatIsGround;// end Jumpig
    public GameObject arrow; 
    public Transform ArrowPoint;
   public float delayTime;//задержка выстрела
    bool canShoot = true;
    public int lives=4;
    public GameObject respawn;
    new private Rigidbody2D rigidbody;
    [SerializeField] AudioSource DamageSound;
    [SerializeField] AudioSource ArrowShootSound;
    [SerializeField] AudioSource StepsSound;
    public GameObject DeathScreen;
    public GameObject Player;
    
    public int Lives{
        get {return lives;}
        set {
            if(value < 4) lives = value;
            livesBar.Refresh();
            }
    }
    private SCR_LivesBar livesBar;
    

    void Start()
    {

    }
    private void Awake(){

     livesBar  = FindObjectOfType<SCR_LivesBar>();
      rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(GroundedCheck.position, GroundedCheckRadius, whatIsGround);//Ground cheking
        if (Grounded) MaxSpeed = 44f;
        else
        {
            if (MaxSpeed > 22f) MaxSpeed = MaxSpeed - 22f * (1f * Time.deltaTime);
            else MaxSpeed = 22f;
        }

        if (Math.Abs(Input.GetAxis("Horizontal")) > 0.1f && Math.Abs(Speed) < MaxSpeed)//Speed checking
            Speed = Input.GetAxis("Horizontal") * (Acceleration * Time.deltaTime);
        else
        {
            if (Math.Abs(Speed) > DeAcceleration * Time.deltaTime)
                Speed = Speed * (DeAcceleration * Time.deltaTime);
            else Speed = 0;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D)){
            StepsSound.Play();
        }

        if (Input.GetKeyUp(KeyCode.D)){
            StepsSound.Stop();
        }

        if (Input.GetKeyDown(KeyCode.A)){
            StepsSound.Play();
        }

        if (Input.GetKeyUp(KeyCode.A)){
            StepsSound.Stop();
        }




        //Jumping
        if (Grounded && Math.Abs(Input.GetAxis("Jump"))>0.0f && canJump == true)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < 0.1f)
            {
                canJump = false;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce));
                StartCoroutine (NoJump());
                StepsSound.Stop();
            }
        }
        IEnumerator NoJump () 
        {
        yield return new WaitForSeconds (delayTime);
        canJump = true;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (Speed > 0 && !facingRight)
            Flip();
        else if (Speed < 0 && facingRight)
            Flip();

        if (Lives == 0)
        {
           DeathScreen.SetActive(true);
           Player.SetActive(false);
        }


    if (Math.Abs(Input.GetAxis("Fire1"))>0.0f && canShoot==true) 
		{
			canShoot = false;
			Shoot();
        StartCoroutine (NoFire());    
        }
    
            
    }


    IEnumerator NoFire () {

    yield return new WaitForSeconds (delayTime);

    canShoot = true;
    }

    

    void Shoot()
    {
    
	Instantiate(arrow, ArrowPoint.position, ArrowPoint.rotation);
    ArrowShootSound.Play();
    }



    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        ArrowPoint.Rotate(0f, 180f, 0f);//поворот стрелы

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")){
         Damage();
 
    }
 }




    public void Damage(){

        Lives-=1;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 3.0F, ForceMode2D.Impulse);
        DamageSound.Play();
    }

    



}