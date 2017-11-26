using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {


	[SerializeField] private int heal;


	public void TakeDamage(){
		heal--;
		if(heal<=0){
			gameObject.SetActive(false);
		}
	}

}
