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
	private bool hit;
	private bool canJump;

	[SerializeField] private Collider leftSword;
	[SerializeField] private Collider rightSword;

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
		Hit();
	}
	void GetInput(){
		h=Input.GetAxis("Horizontal");
		v=Input.GetAxis("Vertical");
		run=Input.GetButton("Run");
		hit=Input.GetButton("Hit");
	}

	void Jump(){

		if(Input.GetButton("Jump")&&canJump&&!hit){
			deltaJump=jump*Time.deltaTime;
			rb.velocity=new Vector3(rb.velocity.x,deltaJump,rb.velocity.z);
			anim.SetBool("Jump",true);
		}else{
			anim.SetBool("Jump",false);
		}
	}

	void Hit(){
		if(hit){
			anim.SetBool("Hit",true);
			leftSword.enabled=true;
			rightSword.enabled=true;
		}else{
			anim.SetBool("Hit",false);
		}
	}

	void Move(){

		PlayerStats.Instance.MoreStamina();
		if(h!=0 || v!=0){

		if(run && PlayerStats.Instance.stamina > 0){

			deltaSpeed=speed*2*Time.deltaTime;
			PlayerStats.Instance.LessStamina();
		}else{
			
			deltaSpeed=speed*Time.deltaTime;
			
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

	public void SwordOff(){ //se llama en un metodo en una animacion
		leftSword.enabled=false;
		rightSword.enabled=false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Enemy"){
			other.GetComponent<Health>().TakeDamage();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag=="Coin"){
			if(Input.GetButtonDown("Recoger")){
				PlayerStats.Instance.MoreCoins();
				other.gameObject.SetActive(false);
			}
		}
	}
}
