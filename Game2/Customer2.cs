﻿using UnityEngine;
using System.Collections;

public class Customer2 : MonoBehaviour {

	private GameObject Object_Item;

	private Vector3 src_pos;
	private Vector3 dst_pos;

	private int state;

	private bool move_init;
	private float start_time;
	public  float distance_time = 8;
	private float t;

	int desk_pos;
	int item_pos;

	// Use this for initialization
	void Start () {
		if(Order.GetItemCount()<=0)
			Destroy(this.gameObject);
		state = 0;
		move_init = false;
		Object_Item = Select_Item();
		animation.Play ("Walk"); //Check Anime (1/3)
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
			case 0: //go to the desk
			{
				Move();
				break;
			}
			case 1: //arrived to the desk
			{
				Buy();
				break;
			}
			case 2: //go to the exit
			{
				Move();
				break;
			}
			case 3:
			{
				Exit();
				break;
			}
		}
	}

	GameObject Select_Item()
	{
		BuyList item_value = Order.Getbuyitem();

		GameObject Object_Item = item_value.GetItem();
		desk_pos = item_value.GetDeskIndex();
		item_pos = item_value.GetItemIndex();
		//print (Object_Item);
		//print (desk_pos);
		//print (item_pos);

		//int Item_Index = Random.Range(0,Object_Item.Length); //exception point (out of range)
		//print (Object_Item[Item_Index]);
		//if (Object_Item == null)//if (Object_Item[Item_Index] == null)
		//{
		//	print("Destroy");
		//	Destroy(this.gameObject);
		//	return null;
		//}
		//else
		return Object_Item;
			//return Object_Item[Item_Index];
	}
	
	void Move()
	{
		if(move_init == false)
		{
			src_pos = this.transform.position;
			start_time = Time.time;
			switch(state)
			{
				case 0: //go to the desk
				{
					dst_pos = Order.GetDeskspace(desk_pos).desk_obj.transform.position + new Vector3 (0,0.5f,2);//desk;
					break;
				}
				case 2: //go to the exit
				{
					dst_pos = new Vector3 (50,1,5);//exit;
					break;
				}
			}
			move_init = true;
		}
		else
		{
			t = ( (Time.time - start_time)*distance_time ) / Vector3.Distance(src_pos, dst_pos);
			//print(t);
			//print (src_pos);
			//print (dst_pos);
			//Debug.Break ();
			this.transform.position = Vector3.Lerp(src_pos, dst_pos, t);
			if(t >= 1)
			{
				move_init = false;
				state++;
				//print(state);
				//Debug.Break();
			}
		}
	}
	
	void Buy()
	{
		//print(Object_Item);
		if(Object_Item)
		{
			this.audio.Play(); //sound
			//print(desk_pos);
			//print(item_pos);
			//Debug.Break ();
			Order.SetItemInUse(desk_pos, item_pos,false);
			Order.DecreaseItemCount();
			if(Object_Item == (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/Game2/Portion_Prefab.prefab",typeof(GameObject)))
			{
				Money_Management.SetGold(7);
			}
			Destroy(Object_Item);
			//Button.exists = false;
			//Button.gold += 7;
			animation.Play ("Walk"); //Check Anime(2/3)
		}
		else
			animation.Play("Oops"); //Check Anime (3/3)

		this.transform.rotation = Quaternion.Euler (0,0,0);
		state++;
	}
	
	void Exit()
	{
		Customer_Spawn2.customer_count--;
		Destroy(this.gameObject);
	}
}