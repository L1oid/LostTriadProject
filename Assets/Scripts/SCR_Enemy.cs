using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Enemy : MonoBehaviour
{
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    bool moveingRight;

    Transform player;
    public float stoppingDistance;

    bool chill = false;
    bool angry = false;
    bool goBack = false;
    public int hp=100;
    public int maxhp=100;
    public int enemydamage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


public void Hit(int damage)
	{
		hp -= damage;
	}


void Die()
	{
		Destroy(gameObject);
	}





    // Update is called once per frame
    void Update()
    {


if (hp > maxhp)
		{
			hp = maxhp;
		}

		if (hp <= 0)
		{
			Die();
		}



        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }
        if(Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }

    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            moveingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            moveingRight = true;
        }

        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }



void OnTriggerEnter2D(Collider2D hitInfo)
{
	SCR_Arrow damage = hitInfo.GetComponent<SCR_Arrow>();
	if (damage.gameObject.tag=="Arrow")
	{
		Hit(damage.damage);
	}


}





}
