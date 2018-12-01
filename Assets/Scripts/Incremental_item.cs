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
	bool clickable;					//If it can be clicked

	// Use this for initialization
	void Start () {
		//================================================================
		// GET VARIABLES
		//================================================================
		//Button
		clicking_button = transform.GetComponent<Button>();

		//Inner text of the button
		Text[] t = transform.GetComponentsInChildren<Text>();
		foreach(Text t_aux in t)
		{
			inner_text.Add(t_aux);
		}

		//Find economy Manager
		economyManager = GameObject.FindWithTag("EconomyManager").GetComponent<Economy_manager>();

		//================================================================

		//Button to listen to when pressed
		clicking_button.onClick.AddListener(purchase);

		//Set personal tag
		gameObject.tag = "IncrementalItem";

		//If it is clickable right now
		clickable = false;

		//Change visual aspect to "unclickable"
		ColorBlock c = clicking_button.colors;
		c.normalColor = new Color32(120,120,120,200);
		clicking_button.colors = c;

	}
	
	// Update is called once per frame
	void Update () {
		wps_total = wps_per_unit * number_of_elements;
	}

	void purchase()
	{
		//If it is clickable, then you can buy it
		if(!clickable)
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
		inner_text[1].text = "" + actual_cost;
		inner_text[2].text = "" + number_of_elements;
	}

	public void activate()
	{
		//Start the item to get all the variables
		Start();
		
		//Let it be clicked
		clickable = true;

		//Change visual aspect to "clickable"
		ColorBlock c = clicking_button.colors;
		c.normalColor = new Color32(255,255,255,255);
		clicking_button.colors = c;
	}


}
