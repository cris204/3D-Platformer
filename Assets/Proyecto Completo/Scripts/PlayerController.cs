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
	Vector3 rotar;
	[SerializeField]
	private Vector3 movement;
	private float deltaSpeed;
	private float deltaJump;
	private float h;
	private float v;
	private bool run;
	private bool canJump;

	// Use this for initialization
	void Start () {
		rb=GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}


	void FixedUpdate(){
		Girar();
		Move();
		Jump();
	}
	void GetInput(){
		h=Input.GetAxis("Horizontal");
		v=Input.GetAxis("Vertical");
		run=Input.GetButton("Run");
	}

	void Jump(){

		if(Input.GetButton("Jump")&&canJump){
			deltaJump=jump*Time.deltaTime;
			rb.velocity=new Vector3(rb.velocity.x,deltaJump,rb.velocity.z);
		}
	}

	void Move(){
		if(run){
			deltaSpeed=speed*2*Time.deltaTime;
		}else{
			deltaSpeed=speed*Time.deltaTime;
		}

		if(h!=0 || v!=0){
			
			rb.velocity=(((v*transform.forward) +( h*transform.right))*deltaSpeed)+(transform.up*rb.velocity.y);
			
		}
	}

	void Girar(){
		transform.rotation = Quaternion.Euler (0f, Input.mousePosition.x , 0f);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag=="Terrain"){
			canJump=true;
		}
	}
	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.tag=="Terrain"){
			canJump=false;
		}
	}
}
