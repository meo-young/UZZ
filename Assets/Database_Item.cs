using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Database_Item : ScriptableObject
{
	public List<ItemEntity> items; // Replace 'EntityType' to an actual type that is serializable.
}
