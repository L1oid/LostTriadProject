using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Enemy : MonoBehaviour
{
    public Animator animator;
    public float maxspeed;
    float speed;
    public int DistOfPatrol;
    public Transform point;
    public bool moveingRight = false;
    public Transform Target;
    Transform player;
    public float stoppingDistance;
    public int hp=100;
    public int maxhp=100;
    public int enemydamage;
    bool angry=false;
    [SerializeField] AudioSource HitPavuk;
    [SerializeField] AudioSource DiePavuk;
    [SerializeField] AudioSource PlayerDetect;
    public GameObject DieSound;
    //public AudioClip pavukzametil;
    public AudioClip PlayerDetected;
    AudioSource audioSource;
    bool detectingsound=true;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       audioSource = GetComponent<AudioSource>();
    }

    public void Hit(int damage)
	{
        hp -= damage;
    }

    void Die()
	{
       
       Instantiate(DieSound, gameObject.transform.position, gameObject.transform.rotation);
       Destroy(gameObject);
    

    }

   

    

    // Update is called once per frame
    void Update()
    {   animator.SetFloat("Speed", Math.Abs(speed));
        if ( transform.position.x < Target.position.x && !moveingRight)
            Flip();
        else if (transform.position.x > Target.position.x && moveingRight)
            Flip();

         if(Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {angry = true;}
        else {angry = false;}
        
        if (hp > maxhp)
        {hp = maxhp;}

        if (hp == 0)
        {Die();}

        if (angry)
        {Detect();Target.transform.position = player.transform.position;}
        else{Target.transform.position = point.transform.position;}

        if (Vector2.Distance(transform.position, Target.position) > 0.1f)
        {speed = maxspeed;
        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);}
        else {speed=0;}
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
	SCR_Arrow damage = hitInfo.GetComponent<SCR_Arrow>();

	if (damage.gameObject.tag=="Arrow")
	    {
		Hit(damage.damage);
        HitPavuk.Play();
	    }
    }

    void Detect(){
        if (detectingsound==true){
        PlayerDetect.Play();
        StartCoroutine (PlayOnDetecting()); 
        //audioSource.PlayOneShot(PlayerDetected, 0.1f);
        detectingsound=false;
        }
    }
 
    IEnumerator PlayOnDetecting(){
        yield return new WaitUntil(() => PlayerDetect.isPlaying == false);
        detectingsound=true;

    }







    void Flip()
    {
        moveingRight = !moveingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}