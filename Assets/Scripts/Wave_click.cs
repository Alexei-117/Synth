using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave_click : MonoBehaviour {

	//================================================================
	// NEEDED OBJECTS
	//================================================================
	public Button clicking_button; 		   //Button to listen to when clicked
	public Economy_manager economyManager; //To subtract the total money to buy an object

	//================================================================
	// NUMERICAL VALUES
	//================================================================
	public double waves_per_click = 1.0; 

	// Use this for initialization
	void Start () {
		//================================================================
		// GET VARIABLES
		//================================================================
		//Button
		clicking_button = transform.GetComponent<Button>();

		//Find economy Manager
		economyManager = GameObject.FindWithTag("EconomyManager").GetComponent<Economy_manager>();
		//================================================================

		//Function to listen to when button is pressed
		clicking_button.onClick.AddListener(add_wave);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void add_wave()
	{
		economyManager.increment_waves (waves_per_click);
	}
}
