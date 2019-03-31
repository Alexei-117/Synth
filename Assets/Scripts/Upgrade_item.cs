using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_item : MonoBehaviour {

	//================================================================
	// NUMERICAL VALUES
	//================================================================
	public Economy_manager economyManager; //To subtract the total money to buy an object

	public double base_cost         = 1.0;  //Waves needed to buy this upgrade
	public double numeric_upgrade   = 0;    //Numeric value of the upgrade (% reduction cost, % production augmentation, etc)
	public int item_to_affect       = 0;  	//Item in the item list that'll affect this upgrade (-1 is the Click)
	public int elements_till_appear = 100; 	//Items needed to unlock this upgrade

	//================================================================
	// UI VALUES
	//================================================================
	public string upg_name; 		//Name of the upgrade 
	public string upg_desc; 		//Description of the upgrade 
	public Sprite upg_image; 		//Image used in the upgrade slot

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
