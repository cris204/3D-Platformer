using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float speed;

	[SerializeField] private float jump;

	private Rigidbody rb;
	[SerializeField]	private Animator anim;
	private float deltaSpeed;
	private float deltaJump;
	private float h;
	private float v;
	private bool run;
	private bool canJump;

	// Use this for initialization
	void Start () {
		rb=GetComponent<Rigidbody>();	
		anim.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		Debug.Log(canJump);
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
			anim.SetBool("Jump",true);
		}else{
			anim.SetBool("Jump",false);
		}
	}

	void Move(){


		if(h!=0 || v!=0){

		if(run && PlayerStats.Instance.stamina > 0){

			deltaSpeed=speed*2*Time.deltaTime;
			PlayerStats.Instance.LessStamina();
		}else{
			
			deltaSpeed=speed*Time.deltaTime;
			PlayerStats.Instance.MoreStamina();
		}

		rb.velocity=((((v*transform.forward) +( h*transform.right))*deltaSpeed)+Vector3.up*rb.velocity.y);

		}else{
			rb.velocity=Vector3.zero+transform.up*rb.velocity.y;
		}
		anim.SetFloat("HVSpeed",rb.velocity.magnitude);

	}

	void Girar(){
		transform.rotation = Quaternion.Euler (0f, Input.mousePosition.x , 0f);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag=="Terrain" && !canJump){
			StartCoroutine(wait());
		}
	}
	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.tag=="Terrain"){
			canJump=false;
		}
	}
	
	public IEnumerator wait()
	{
		
		yield return new WaitForSeconds(2);
		canJump=true;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Key"){
			Debug.Log("recoger");
		}
	}
}
