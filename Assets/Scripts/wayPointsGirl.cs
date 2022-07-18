using UnityEngine;
using System.Collections;

public class wayPointsGirl : MonoBehaviour {
	
	public Transform head;
	public Transform player;
	Animator anim;
	
	string state = "waiting";
	public GameObject[] waypoints;
	public int currentWP = 0;
	public float rotSpeed = 0.8f;
	public float speed = 1.5f;
	float accuracyWP = 5.0f;
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		float angle = Vector3.Angle(direction, head.up);
		
		if(state == "waiting" && waypoints.Length > 0)
		{
			anim.SetBool("isIdle",true);
			anim.SetBool("isWalking",true);
			if(Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
			{
				currentWP++;
				if(currentWP >= waypoints.Length)
				{
					currentWP = 0;
				}	
			}
			
			//rotate towards waypoint
			direction = waypoints[currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp(transform.rotation,
			                                           Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
			this.transform.Translate(0, 0, Time.deltaTime * speed);
		}
		
		if(Vector3.Distance(player.position, this.transform.position) < 10 && (angle < 160 || state == "dancing"))
		{
			
			state = "dancing";
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
			                                           Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
			
			if(direction.magnitude > 8)
			{
				//this.transform.Translate(0,0,Time.deltaTime * speed);
				anim.SetBool("isWalking",true);
				anim.SetBool("isDance",false);
			}
			else
			{
				anim.SetBool("isDance",true);
				anim.SetBool("isWalking",false);
        //StartCoroutine("Esperar");
			}
			
		}
		else 
		{
			anim.SetBool("isWalking", true);
			anim.SetBool("isDance", false);
			state = "waiting";
		}
		
	}
}