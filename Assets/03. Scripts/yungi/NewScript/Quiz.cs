using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Quiz : ScriptableObject
{
	public List<QuizEntity> Entities; 
	// Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> Sheet2; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> Sheet3; // Replace 'EntityType' to an actual type that is serializable.
}
