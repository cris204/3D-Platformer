﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	private static PlayerStats instance;
	public static PlayerStats Instance{
		 get {
			return instance;
		 }
	}

	[SerializeField] private Image staminaImage;

	public float stamina;

	[SerializeField] private Text coinsTxt;

	private int coins;

	void  Awake()
	{
		if(instance==null){
			instance=this;
		}else{
			Destroy(this.gameObject);
		}
	}
	void Update () {
		staminaImage.fillAmount=stamina;
		coinsTxt.text=coins.ToString();
	}
	
	public void MoreCoins(){
		coins++;
	}
	public void LessStamina(){
		if(stamina>=0){
			stamina-=0.01f;
		}
	}
	public void MoreStamina(){
		if(stamina<=1){
			stamina+=0.005f;
		}
	}

}
