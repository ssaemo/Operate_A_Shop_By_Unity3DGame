﻿using UnityEngine;
using System.Collections;

public class Order_Button : MonoBehaviour {

	int bt_width, bt_height;
	//Dictionary

	// Use this for initialization
	void Start () {
		bt_width = 100;
		bt_height = 50;

		if(Order.Order_Init() == false)
		{
			print("Order_Init Error");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}


	
	void OnGUI()
	{
		if(GUI.Button (new Rect(10,
		                        10,
		                        this.bt_width, 
		                        this.bt_height), "Portion"))
		{
			int result = Object_Management.OrderItem("Portion");

			if (result == 0)
				audio.Play ();
			else if (result == 1)
				Debug.Log ("All Item Slot is using");
			else if (result == 2)
				Debug.Log ("There is not a Desk");
			else if (result == -1)
				Debug.Log ("Error Occured");
			else if (result == -2)
				Debug.Log ("Invalid Item Name");
				
			

			/*
			if(Money_Management.GetGold()>=Order.GetPrice(0) && Order.meth_Item_Order("Portion"))
			{
				audio.Play();
			}
			else
			{
				print("Not Enough Money");
			}
			*/
		}

		if(GUI.Button (new Rect(10+this.bt_width, 
		                        10, 
		                        this.bt_width,
		                        this.bt_height), "Basic_Desk"))
		{
			int result = Object_Management.OrderDesk("Basic_Desk");
			if(result == 1)
				Debug.Log ("All Desk Slot is using");
			else if(result == -1)
				Debug.Log ("Error Occured");
			else if(result == 0) //success
				audio.Play ();
				


			//if(Order.meth_Desk_Order("Basic_Desk"))
			//	audio.Play();
			//else
				//audio.Play();
		}
	}
}