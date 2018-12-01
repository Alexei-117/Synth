using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy_manager : MonoBehaviour {
	//================================================================
	// NEEDED OBJECTS
	//================================================================
	public Text waves_text; //Changes every frame

	//================================================================
	// HELPING NUMERICAL VALUES
	//================================================================

	double second_duration 		  = 1.0;
	double time_since_last_second = 1.0;

	//================================================================
	// GLOBAL NUMERICAL VALUES
	//================================================================

	public double total_waves = 0;
	public double total_waves_production = 0.0; //Per second


	// Use this for initialization
	void Start () {

		//Inner text
		waves_text = transform.GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		
		//Update production to adjust increment
		update_waves_production ();

		//Add value to the total of waves based on the production per second
		total_waves += total_waves_production*Time.deltaTime;

		//Update text showing it
		waves_text.text = "You have " + total_waves.ToString("F1") + " waves";
	}

	bool second_passed()
	{
		if (time_since_last_second <= 0.0) 
		{
			time_since_last_second = second_duration;
			return true;
		}

		time_since_last_second -= Time.deltaTime;
		return false;
	}

	void update_waves_production()
	{
		//Start from 0
		total_waves_production = 0.0;

		//Find all incremental objects and add the add to the total wps count
		GameObject[] inc_items = GameObject.FindGameObjectsWithTag ("IncrementalItem");
		foreach (GameObject go in inc_items) 
		{
			total_waves_production += go.GetComponent<Incremental_item> ().wps_total;
		}
	}

	public bool buy_with_cost(double cost)
	{
		//Don't buy over what money you have
		if (cost > total_waves)
			return false;

		//Subtract cost
		total_waves -= cost;

		return true;
	}

	public void increment_waves(double input)
	{
		total_waves += input;
	}
}
