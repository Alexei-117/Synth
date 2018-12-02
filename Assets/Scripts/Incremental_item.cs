using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incremental_item : MonoBehaviour {

	//================================================================
	// NUMERICAL VALUES
	//================================================================
	public Economy_manager economyManager; //To subtract the total money to buy an object

	public double base_cost       = 1.0;    
	public double actual_cost     = 1.0;    
	public double augment_ratio   = 0.15;  //Increasing ratio of the cost value
	public double wps_per_unit    = 1;     //Waves per second
	public double wps_total       = 1;
	public int number_of_elements = 0;
	public int elements_till_next_item = 100; 	//Items needed to buy the next item on the list

	//================================================================
	// UI VALUES
	//================================================================
	public Button clicking_button; 	//Button to listen to when clicked
	public List<Text> inner_text; 	//Name of the object - current number of objects - value
	public string object_name; 		//Name of the object itself
	public bool clickable = false;	//If it can be clicked

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		wps_total = wps_per_unit * number_of_elements;
	}

	void purchase()
	{
		//If it is clickable, then you can buy it
		if(!clicking_button.isActiveAndEnabled)
			return;

		//First buy the object (if false you can't buy it)
		if (!economyManager.buy_with_cost (actual_cost))
			return;

		//Add number of elements
		number_of_elements++;

		//Augment the price
		actual_cost *= (1 + augment_ratio);

		//Update the text
		inner_text[0].text = object_name;
		inner_text[1].text = "" + actual_cost.ToString("F1");
		inner_text[2].text = "" + number_of_elements;
	}

	public void activate()
	{
		//================================================================
		// GET VARIABLES
		//================================================================
		//Button
		clicking_button = transform.GetComponent<Button>();

		//Inner text of the button
		inner_text = new List<Text>();
		Text[] t = transform.GetComponentsInChildren<Text>();
		foreach(Text t_aux in t)
		{
			inner_text.Add(t_aux);
		}

		//Find economy Manager
		economyManager = GameObject.FindWithTag("EconomyManager").GetComponent<Economy_manager>();

		//================================================================

		//Function to activate when pressed
		clicking_button.onClick.AddListener(purchase);

		//Set personal tag
		gameObject.tag = "IncrementalItem";
		
		//Let it be clicked
		clickable = true;

		//Change visual aspect to "clickable"
		ColorBlock c = clicking_button.colors;
		c.normalColor      = new Color32(255,255,255,255); //Change normal color
		clicking_button.colors = c;

		//Enable button
		clicking_button.enabled = true;
	}


}
