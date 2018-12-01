using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buying_manager : MonoBehaviour {
	
	//================================================================
	// GENERIC UI VALUES
	//================================================================
	public Vector2 size_delta;	//Width - height of the button
	public Vector2 position;	//Initial position of the first button
	public float   button_h; 	//Height of the button


	//================================================================
	// USEFUL METADATA
	//================================================================
	List<Incremental_item> items;
	int item_to_appear   = 0;
	int item_to_activate = 0;

	// Use this for initialization
	void Start () {
		//Initialize list
		items = new List<Incremental_item>();

		//Fill generic ui data
		button_h   = 54.4f;
		size_delta = new Vector2(273.9f,button_h);
		position   = new Vector2(164.9f,310.2f);

		//Create first item and activate it
		create_item();
		activate_item();
	}
	
	// Update is called once per frame
	void Update () {
		/* if(items[item_to_appear-1].elements_till_next_item <= items[item_to_appear-1].number_of_elements)
		{
			activate_item();
		}*/
	}

	void create_item()
	{	
		//Create item and all the components
		GameObject item    = new GameObject();
		RectTransform r    = item.AddComponent<RectTransform>() as RectTransform; //Position relative to the canvas;
		Image m 	       = item.AddComponent<Image>() as Image;
		Button b 		   = item.AddComponent<Button>() as Button;;
		Incremental_item i = item.AddComponent<Incremental_item>() as Incremental_item;
		Font f 			   = (Font)Resources.Load("Fonts/operational_amplifier.ttf");


		//Add tag and layer
		item.tag   = "IncrementalItem";
		item.layer = 5; 				//UI Layer
		//Add object created as child of Game_layout (the main canvas)
		GameObject layout = GameObject.FindWithTag("GameLayout");
		item.transform.SetParent(layout.transform);

		//================================================================
		// Rect transform
		//================================================================
		r.sizeDelta = size_delta;
		r.anchorMin = new Vector2(0, 0); 				  //Lower-left anchor values
		r.anchorMax = new Vector2(0, 0);
		r.pivot = new Vector2(0.5f, 0.5f);
		Vector2 actual_position = position;				  //We'll operate starting from the initial position of a button
		actual_position.y -= button_h * item_to_appear;   //ActualPosition.height = baseHeight + button_height * what_button_is_this;
		r.anchoredPosition = actual_position;
		r.localScale = new Vector3(1.0f,1.0f,1.0f);		  	
		
		//================================================================
		// Image
		//================================================================

		//================================================================
		// Button
		//================================================================
		ColorBlock c_b = b.colors;					 
		c_b.normalColor = new Color32(210,93,0,255); //Change normal color
		b.colors = c_b;
		
		Navigation nav_mode = new Navigation(); 	 //Set navigation to none so it's stop highlighting after a click
		nav_mode.mode = Navigation.Mode.None;
		b.navigation = nav_mode;

		//================================================================
		// Text childs
		//================================================================
		for(int x = 0; x < 3; x++)
		{
			GameObject text_to_put = new GameObject();
			//=======================
			// Text component
			//=======================
			Text t = text_to_put.AddComponent<Text>() as Text;
			t.font = f;

			//=======================
			// Rect transform in each text
			//=======================
			RectTransform r_text = text_to_put.AddComponent<RectTransform>() as RectTransform;

			if( x  == 0 )
			{
				t.fontSize = 14;
				r_text.sizeDelta   = size_delta;
				r_text.anchorMin   = new Vector2(0.5f, 1.0f);  //Upper-center anchor values
				r_text.anchorMax   = new Vector2(0.5f, 1.0f);
				r_text.pivot 	   = new Vector2(0.5f, 0.5f);
				r.anchoredPosition = new Vector2(0.0f,-15.98f);			 	
			}

			if( x  == 1 )
			{
				t.fontSize = 12;
				r_text.sizeDelta   = size_delta;
				r_text.anchorMin   = new Vector2(0.0f, 0.0f);  //Lower-left anchor values
				r_text.anchorMax   = new Vector2(0.0f, 0.0f);
				r_text.pivot 	   = new Vector2(0.5f, 0.5f);
				r.anchoredPosition = new Vector2(90.4f,15.0f);			 	
			}

			if( x  == 2 )
			{
				t.fontSize = 12;
				r_text.sizeDelta   = size_delta;
				r_text.anchorMin   = new Vector2(1.0f, 0.0f);  //Lower-right anchor values
				r_text.anchorMax   = new Vector2(1.0f, 0.0f);
				r_text.pivot 	   = new Vector2(0.5f, 0.5f);
				r.anchoredPosition = new Vector2(-51.7f,15.0f);				 	
			}
		}

		//================================================================
		// Incremental_item component
		//================================================================
		if(item_to_appear == 0)
		{

			i.base_cost 		 = 1.0;
			i.actual_cost 		 = 1.0;
			i.augment_ratio 	 = 0.15;
			i.wps_per_unit 		 = 1;
			i.number_of_elements = 0;
			i.elements_till_next_item = 100;
		}

		if(item_to_appear == 1)
		{
			i.base_cost 		 = 100.0;
			i.actual_cost		 = 100.0;
			i.augment_ratio		 = 0.16;
			i.wps_per_unit 		 = 8;
			i.number_of_elements = 0;
			i.elements_till_next_item = 100;
		}

		//Add to the list of items
		items.Add(i);

		//Set next item to appear
		item_to_appear++;
	}

	void activate_item()
	{
		//Activate indexed item
		items[item_to_activate].activate();

		//Set next item to the next round
		item_to_activate++;

		//Create new item
		create_item();
	}
}
