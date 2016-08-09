﻿using UnityEngine;
using System.Collections;

public class Item_Slot {
	Vector3 position;
	bool in_use;
	GameObject obj;
	string item_name;

	public Item_Slot(Vector3 item_pos)
	{
		this.position = item_pos;
		this.in_use = false;
	}

	public Vector3 GetPosition()
	{
		return this.position;
	}
	public bool GetInUse()
	{
		return this.in_use;
	}
	public GameObject GetItemObject()
	{
		return this.obj;
	}
	public string _GetItemName()
	{
		return this.item_name;
	}

	public bool UseItemSlot(string item_name)
	{
		this.in_use = true;
		
		GameObject obj = (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/Game2/"+item_name+"_Prefab.prefab", typeof(GameObject));

		switch(item_name)
		{
		case "Portion":
		{
			this.obj = (GameObject)GameObject.Instantiate (obj, this.position, Quaternion.identity);

			break;
		}
		default:
		{
			return false;
		}
		}

		this.item_name = item_name;

		return true;
	}
	public bool FreeItemSlot()
	{
		this.in_use = false;
		GameObject.Destroy(this.obj);
		//gold
		return true;
	}
}
