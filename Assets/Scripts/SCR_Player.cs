using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Player : MonoBehaviour
{
    public Animator animator;
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
    //Klimbing
    public bool Klimbing = false;
    public Transform KlimbingCheck;
    public float KlimbingCheckRadius = 0.2f;//end of Klimbing

    public GameObject arrow; 
    public Transform ArrowPoint;
   public float delayTime;//задержка выстрела
    bool canShoot = true;
    public int lives=5;
    public GameObject respawn;
    new private Rigidbody2D rigidbody;

    [SerializeField] AudioSource DamageSound;
    bool MusDamage;
    [SerializeField] AudioSource ArrowShootSound;
    bool MusShoot;
    [SerializeField] AudioSource StepsSound;
    [SerializeField] AudioSource HealSound;
    bool walkingsound=true;
    public bool MusMove;
    public GameObject DeathScreen;
    public GameObject Player;
    public int Lives{
        get {return lives;}
        set {
            if(value < 5) lives = value;
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
        Klimbing = Physics2D.OverlapCircle(KlimbingCheck.position, KlimbingCheckRadius, whatIsGround);//Klimbing cheking
        
        Grounded = Physics2D.OverlapCircle(GroundedCheck.position, GroundedCheckRadius, whatIsGround);//Grounded cheking
        
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
      
        
         if (Math.Abs(Input.GetAxis("Horizontal")) > 0.1f){
            MusMove = true;
        }
        if (Math.Abs(Input.GetAxis("Horizontal")) < 0.1f) MusMove = false;

        animator.SetFloat("Speed", Math.Abs(Speed));
        //Jumping
        if ((Grounded||Klimbing) && Math.Abs(Input.GetAxis("Jump"))>0.0f && canJump == true)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < 0.1f)
            {
                canJump = false;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce));
                StartCoroutine (NoJump());
                
            }
        }
        IEnumerator NoJump () 
        {
        yield return new WaitForSeconds (delayTime);
        canJump = true;
        }
        if (!Grounded&&!Klimbing){animator.SetBool("Jump",true);animator.SetBool("Klimb",false);}
        else if (!Grounded&&Klimbing){animator.SetBool("Jump",false);animator.SetBool("Klimb",true);}
        else {animator.SetBool("Jump",false);animator.SetBool("Klimb",false);}
        //End of Jumping

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
    
   


    //бляцкий звук ходьбы
   if( animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Move") && walkingsound==true){
        StepsSound.Play();
        StartCoroutine (PlayOnWalking()); 
        walkingsound=false;
   }
   else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Move")){
       StepsSound.Stop();
   }

    }

    IEnumerator PlayOnWalking(){
        yield return new WaitUntil(() => StepsSound.isPlaying == false);
       walkingsound=true;

    }//конец бляцкого звука ходьбы



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


    void OnCollisionEnter2D(Collision2D col){

    if (col.gameObject.CompareTag("Enemy")){
     Damage();
    
        }

    }

   
      void OnTriggerEnter2D(Collider2D other) {
         if (other.gameObject.CompareTag("Heart"))
          {
              Debug.Log("сердечко");
              Lives++;
              Destroy(other.gameObject);
              HealSound.Play();
              
          }
       
   }  
    


     void Damage(){

        Lives--;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 3.0F, ForceMode2D.Impulse);
        DamageSound.Play();
    }

    void ReSpawn()
    {
        transform.position = respawn.transform.position;
        Lives = 5;
        livesBar.Refresh();
    }



}