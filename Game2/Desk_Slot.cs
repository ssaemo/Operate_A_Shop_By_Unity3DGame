﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Desk_Slot {
	Vector3 position;
	bool in_use;
	GameObject obj;

	int item_max;
	Item_Slot[] item_list;
	int item_count;

	public Desk_Slot(Vector3 pos)
	{
		this.position = pos;
		this.in_use = false;
		this.item_count = 0;
	}

	//Get Methods
	public Vector3 _GetDeskForward()
	{
		return this.obj.transform.forward * 13;
	}
	public Vector3 GetPosition()
	{
		return this.position;
	}
	public bool GetInUse()
	{
		return this.in_use;
	}
	public GameObject GetDeskObject()
	{
		return this.obj;
	}
	public Item_Slot[] GetItemList()
	{
		return this.item_list;
	}
	public int GetItemCount()
	{
		return this.item_count;
	}
	public string GetItemName(int index)
	{
		return this.item_list[index]._GetItemName();
	}

	//Use/FreeDeskSlot
	public bool UseDeskSlot(string desk_name)
	{
		this.in_use = true;

		GameObject obj = (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/Game2/"+desk_name+"_Prefab.prefab", typeof(GameObject));

		switch(desk_name)
		{
		case "Basic_Desk":
		{
			this.obj = (GameObject)GameObject.Instantiate (obj, this.position, Quaternion.identity);

			this.item_max = 6;
			this.item_list = new Item_Slot[item_max];
			for(int i=0;i<this.item_max;i++)
			{
				Transform obj_tr = this.obj.transform;
				Vector3 item_pos = obj_tr.position + (obj_tr.right * -10) + (obj_tr.transform.right * i * 4) + (obj_tr.up * 6);
				this.item_list[i] = new Item_Slot(item_pos);
			}
			
			break;
		}
		default:
		{
			return false;
		}
		}


		return true;
	}
	public bool FreeDeskSlot()
	{
		return true;
	}

	public int _OrderItem(string item_name)
	{
		int result = 1; //all item slot is using

		for(int j=0;j<item_list.Length;j++)
		{
			if(item_list[j].GetInUse() == false)
			{
				//itemslot_num = j;
				if(item_list[j].UseItemSlot(item_name) == true)
				{
					this.item_count++;
					result = 0;
				}
				else
					result = -2; //invalid item name
				break;
			}
		}

		return result;
	}

	public int _Select_Item()
	{
		List<int> random_list = new List<int>();
		for(int j=0;j<item_list.Length;j++)
		{
			if(item_list[j].GetInUse() == true)
				random_list.Add(j);
		}
		int pick2 = random_list[Random.Range(0,random_list.Count)];
		//Debug.Log (random_list2.Count);
		//Debug.Break ();

		return pick2;
	}

	public bool _BuyItem(int index)
	{
		bool result;

		if(item_list[index].GetInUse() == true)
		{
			this.item_count--;
			result = item_list[index].FreeItemSlot();
		}
		else
			result = false;

		return result;
	}
}
