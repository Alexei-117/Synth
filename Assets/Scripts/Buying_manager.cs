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
	public Font    item_font;	//Font to assign to incremental items
	public Sprite  item_sprite; //Background image of the item
	public Sprite  item_highlighted_sprite;
	public Sprite  item_disabled_sprite;
	public Sprite  item_pressed_sprite;


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

		//Check if we activated all items
		if(item_to_activate>=items.Count)
			return;

		//If the previous item (last activated) reached the number of elements to activate the next one, then do so
		if(items[item_to_activate-1].number_of_elements >= items[item_to_activate-1].elements_till_next_item)
		{
			activate_item();
		}
	}

	void create_item()
	{	
		//Create item and all the components
		GameObject item    = new GameObject();
		RectTransform r    = item.AddComponent<RectTransform>() as RectTransform; //Position relative to the canvas;
		Image m 	       = item.AddComponent<Image>() as Image;
		Button b 		   = item.AddComponent<Button>() as Button;;
		Incremental_item i = item.AddComponent<Incremental_item>() as Incremental_item;

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
		m.sprite = item_sprite;

		//================================================================
		// Button
		//================================================================
		SpriteState s_state = new SpriteState(); 			 //Change sprites to make the transition happen
		s_state.highlightedSprite = item_highlighted_sprite;
		s_state.pressedSprite     = item_pressed_sprite;
		s_state.disabledSprite    = item_disabled_sprite;
		b.spriteState = s_state;
		
		Navigation nav_mode = new Navigation(); 	 		 //Set navigation to none so it's stop highlighting after a click
		nav_mode.mode = Navigation.Mode.None;
		b.navigation  = nav_mode;

		b.transition  = Selectable.Transition.SpriteSwap;	 //Let it transition between sprites

		b.interactable= false;								 //Not active in the beginning (appears grey)
		b.enabled     = true;								 //Not active in the beginning (appears grey)

		//================================================================
		// Text childs
		//================================================================
		for(int x = 0; x < 3; x++)
		{
			//Create new text
			GameObject text_to_put  = new GameObject();
			RectTransform r_text    = text_to_put.AddComponent<RectTransform>() as RectTransform;
			Text t 					= text_to_put.AddComponent<Text>() as Text;

			//Set parent the item
			text_to_put.transform.SetParent(item.transform);

			//=======================
			// Text component
			//=======================
			t.font = item_font;

			//=======================
			// Rect transform in each text
			//=======================

			if( x  == 0 )
			{
				t.fontSize  = 30;
				t.alignment = TextAnchor.UpperLeft;
				t.color 	= new Color32(89,23,129,255);	   //Color: #591781
				r_text.sizeDelta   = new Vector2(273.9f,54.4f);
				r_text.anchorMin   = new Vector2(0.5f, 1.0f);  //Upper-center anchor values
				r_text.anchorMax   = new Vector2(0.5f, 1.0f);
				r_text.pivot 	   = new Vector2(0.5f, 0.5f);
				r_text.anchoredPosition = new Vector2(83.0f,-27.2f);
				r_text.localScale  = new Vector3(1.0f,1.0f,1.0f);		 	
			}

			if( x  == 1 )
			{
				t.fontSize  = 26;
				t.alignment = TextAnchor.MiddleLeft;
				t.color     = new Color32(69,177,200,255);     //Color: #45B1C8
				r_text.sizeDelta   = new Vector2(100.12f,33.18f);
				r_text.anchorMin   = new Vector2(0.0f, 0.0f);  //Lower-left anchor values
				r_text.anchorMax   = new Vector2(0.0f, 0.0f);
				r_text.pivot 	   = new Vector2(0.5f, 0.5f);
				r_text.anchoredPosition = new Vector2(133.06f,12.5f);
				r_text.localScale  = new Vector3(1.0f,1.0f,1.0f);			 	
			}

			if( x  == 2 )
			{
				t.fontSize  = 26;
				t.alignment = TextAnchor.MiddleRight;
				t.color 	= new Color32(89,23,129,255);	   //Color: #591781
				r_text.sizeDelta   = new Vector2(82.41f,35.73f);
				r_text.anchorMin   = new Vector2(1.0f, 0.0f);  //Lower-right anchor values
				r_text.anchorMax   = new Vector2(1.0f, 0.0f);
				r_text.pivot 	   = new Vector2(0.5f, 0.5f);
				r_text.anchoredPosition = new Vector2(-47.3f,12.5f);	
				r_text.localScale  = new Vector3(1.0f,1.0f,1.0f);			 	
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
			i.object_name        = "Oscillator";
			i.base_txt_color     = new Color32(89,23,129,255);
			i.price_txt_color    = new Color32(69,177,200,255);
		}

		if(item_to_appear == 1)
		{
			i.base_cost 		 = 100.0;
			i.actual_cost		 = 100.0;
			i.augment_ratio		 = 0.16;
			i.wps_per_unit 		 = 8;
			i.number_of_elements = 0;
			i.elements_till_next_item = 100;
			i.object_name        = "Synth-ons";
			i.base_txt_color     = new Color32(89,23,129,255);
			i.price_txt_color    = new Color32(69,177,200,255);
		}

		if(item_to_appear == 2)
		{
			i.base_cost 		 = 2000.0;
			i.actual_cost		 = 2000.0;
			i.augment_ratio		 = 0.165;
			i.wps_per_unit 		 = 30;
			i.number_of_elements = 0;
			i.elements_till_next_item = 200;
			i.object_name        = "Synth Strings";
			i.base_txt_color     = new Color32(89,23,129,255);
			i.price_txt_color    = new Color32(69,177,200,255);
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
