using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float jump;

	private float h;
	private float v;
	// Use this for initialization
	void Start () {
		rb=GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {

		GetInput();
		
	}

	void FixedUpdate()
	{
		Move();
		Jump();
	}
	void GetInput(){
		h=Input.GetAxis("Horizontal");
		v=Input.GetAxis("Vertical");
	}

	void Jump(){
		if(Input.GetButton("Jump")){
			rb.velocity=new Vector3(rb.velocity.x,jump,rb.velocity.z);
		}
	}

	void Move(){

		if(h>0){
			rb.velocity=new Vector3(speed,rb.velocity.y,rb.velocity.z);
		}else if(h<0){
			rb.velocity=new Vector3(-speed,rb.velocity.y,rb.velocity.z);
		}

		if(v>0){
			rb.velocity=new Vector3(rb.velocity.x,rb.velocity.y,speed);
		}else if(v<0){
			rb.velocity=new Vector3(rb.velocity.x,rb.velocity.y,-speed);
		}
	}

}
