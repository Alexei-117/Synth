using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_manager : MonoBehaviour {

	//List of 6 first upgrades are shown
	//By now upgrades only grant "twice as powerful" or "they produce 1% of your total clicks"
	//Add 3d model as object and an onclick button that changes the appearance of the 3D holographic shader instead of button appearance

	//================================================================
	// GENERIC UI VALUES
	//================================================================
	public Vector2 size_delta;	//Width - height of the button
	public Vector2 position;	//Initial position of the first button
	public float   button_h; 	//Height and width of the button
	public float   button_w;
	public Font    upg_font;	//Font to assign to upgrades
	public Sprite  upg_back_sprite; //Background image of the upgrade
	public Sprite  upg_back_highlighted_sprite;
	public Sprite  upg_back_disabled_sprite;
	public Sprite  upg_back_pressed_sprite;


	//================================================================
	// USEFUL METADATA
	//================================================================
	List<GameObject>   slots;	 	//Slots used in
	List<Upgrade_item> upgrades; 	//Upgrade objects

	// Use this for initialization
	void Start () {
		//Initialize list
		upgrades = new List<Upgrade_item>();

		//Fill generic ui data
		button_h   = 100.0f;
		button_w   = 100.0f;
		size_delta = new Vector2(button_w,button_h);
		position   = new Vector2(-54.4f,-139.6f);

		//Create all upgrades
		createSlots();
		createUpgrades();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void createSlots()
	{
		for(int x = 0; x < 6; x++){
			//Create item and all the components
			GameObject slot    = new GameObject();
			RectTransform r    = slot.AddComponent<RectTransform>() as RectTransform; //Position relative to the canvas;
			Image m 	       = item.AddComponent<Image>() as Image;
			Button b 		   = item.AddComponent<Button>() as Button;;

			//Add tag and layer
			slot.layer = 5; 				//UI Layer
			slot.name  = "Upgrade_slot";	//Set name to differentiate from objects

			//Add object created as child of Game_layout (the main canvas)
			GameObject layout = GameObject.FindWithTag("GameLayout");
			item.transform.SetParent(layout.transform);

			//================================================================
			// Rect transform
			//================================================================
			r.sizeDelta = size_delta;
			r.anchorMin = new Vector2(1.0f, 1.0f); 		//Lower-left anchor values
			r.anchorMax = new Vector2(1.0f, 1.0f);
			r.pivot = new Vector2(0.5f, 0.5f);
			Vector2 actual_position = position;			//We'll make a grid starting in the top-right corner of them all
			actual_position.x -= button_w * (x%2);   	//pos.x -= button_w * x%2; 0 - 0, 1 - 1, 2 - 0, 3 - 1 ...
			actual_position.y -= button_h * (x/2);   	//pos.y -= button_w * x/2; 0 - 0, 1 - 0, 2 - 1, 3 - 1 ...
			r.anchoredPosition = actual_position;
			r.localScale = new Vector3(1.0f,1.0f,1.0f);		  	
			
			//================================================================
			// Button
			//================================================================

			b.interactable= false;								 //Not active in the beginning (appears grey)

			//================================================================
			// Text childs
			//================================================================
			//Create new text
			GameObject upgrade_name_txt   = new GameObject();
			RectTransform r_name    = upgrade_name_txt.AddComponent<RectTransform>() as RectTransform;
			Text t_name 			= upgrade_name_txt.AddComponent<Text>() as Text;

			GameObject upgrade_value_txt  = new GameObject();
			RectTransform r_val     = upgrade_value_txt.AddComponent<RectTransform>() as RectTransform;
			Text t_val 				= upgrade_value_txt.AddComponent<Text>() as Text;

			//Set parent the item
			upgrade_name_txt.transform.SetParent(slot.transform);
			upgrade_value_txt.transform.SetParent(slot.transform);

			//=======================
			// Text component in each text
			//=======================
			t_name.font = item_font;
			t_name.fontSize  = 30;
			t_name.alignment = TextAnchor.UpperLeft;
			t_name.color 	= new Color32(89,23,129,255);	   //Color: #591781


			t_val.font  = item_font;
			t_val.fontSize  = 26;
			t_val.alignment = TextAnchor.MiddleLeft;
			t_val.color     = new Color32(69,177,200,255);     //Color: #45B1C8

			//=======================
			// Rect transform in each text
			//=======================

			if( x  == 0 )
			{
				r_text.sizeDelta   = new Vector2(273.9f,54.4f);
				r_text.anchorMin   = new Vector2(0.5f, 1.0f);  //Upper-center anchor values
				r_text.anchorMax   = new Vector2(0.5f, 1.0f);
				r_text.pivot 	   = new Vector2(0.5f, 0.5f);
				r_text.anchoredPosition = new Vector2(83.0f,-27.2f);
				r_text.localScale  = new Vector3(1.0f,1.0f,1.0f);		 	
			}

			if( x  == 1 )
			{
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


			//Add to the list of items
			slots.Add(slot);
		}
	}

	void createUpgrades()
	{	
		GameObject upgrade  = new GameObject();
		Upgrade_item u = upgrade.AddComponent<Upgrade_item>() as Upgrade_item;

		//================================================================
		// Upgrade_item component
		//================================================================
		i.base_cost 		 = 1.0;
		i.actual_cost 		 = 1.0;
		i.augment_ratio 	 = 0.15;
		i.wps_per_unit 		 = 1;
		i.number_of_elements = 0;
		i.elements_till_next_item = 100;
		i.object_name        = "Oscillator";
		i.base_txt_color     = new Color32(89,23,129,255);
		i.price_txt_color    = new Color32(69,177,200,255);

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
	}
}
