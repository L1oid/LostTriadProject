using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Arrow : MonoBehaviour
{
    private Rigidbody2D ar;
	public float speed = 15f;
	public int damage = 20;
	public int life = 0;
	public int lifeMax = 500;





    // Start is called before the first frame update
    void Start()
    {
        ar = GetComponent<Rigidbody2D>();
		ar.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
       life++;

		if (life >= lifeMax)
		{
			Destruction(); 
		} 

    }

//void OnTriggerEnter2D(Collider2D hitInfo) 
	//{

//if (transform.gameObject.tag == "Enemy"){
//	hitInfo.Hit(damage);
//}

		//Destruction();
	//}



//void OnCollisionEnter2D(Collision2D collision)
//{ SCR_Enemy hp = collision.GetComponent<SCR_Enemy>();
  //  if (collision.gameObject.tag == "Enemy") 
   // {
       
      
       
     //  hp.Hit(damage);
       
       //Destruction();
    //}     
//}







void OnTriggerEnter2D(Collider2D hitInfo)
{
	

	Destruction();

}


void Destruction(){

Destroy(gameObject);

}


}
