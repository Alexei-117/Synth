using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incremental_item : MonoBehaviour {
	//================================================================
	// NEEDED OBJECTS
	//================================================================

	public Button clicking_button; 		   //Button to listen to when clicked
	public Text text; 			  		   //Text to change when clicking
	public Economy_manager economyManager; //To subtract the total money to buy an object

	//================================================================
	// NUMERICAL VALUES
	//================================================================

	public double base_cost       = 1.0;    
	public double actual_cost     = 1.0;    
	public double augment_ratio   = 0.15;  //Increasing ratio of the cost value
	public double wps_per_unit    = 1;    //Waves per second
	public double wps_total       = 1;
	public int number_of_elements = 0;    

	// Use this for initialization
	void Start () {
		//================================================================
		// GET VARIABLES
		//================================================================
		//Button
		clicking_button = transform.GetComponent<Button>();

		//Inner text
		text = transform.GetComponentInChildren<Text>();

		//Find economy Manager
		economyManager = GameObject.FindWithTag("EconomyManager").GetComponent<Economy_manager>();

		//================================================================

		//Button to listen to when pressed
		clicking_button.onClick.AddListener(purchase);

		//Set personal tag
		gameObject.tag = "IncrementalItem";

	}
	
	// Update is called once per frame
	void Update () {
		wps_total = wps_per_unit * number_of_elements;
	}

	void purchase()
	{
		//First buy the object (if false you can't buy it)
		if (!economyManager.buy_with_cost (actual_cost))
			return;
		//Add number of elements
		number_of_elements++;

		//Augment the price
		actual_cost *= (1 + augment_ratio);

		//Update the text
		text.text = "You have "+ number_of_elements + " Synth strings";
	}
}
