using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Dialogs_MainQuest : ScriptableObject
{
	public List<Dialog_MQEntity> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
