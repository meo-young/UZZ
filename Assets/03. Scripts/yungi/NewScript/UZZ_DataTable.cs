using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

[ExcelAsset]
public class UZZ_DataTable : ScriptableObject
{
	//public List<EntityType> 데이터 테이블 항목 목차; // Replace 'EntityType' to an actual type that is serializable.
	public List<DialogueTextTableEntity> DialogueTextTable; // Replace 'EntityType' to an actual type that is serializable.
	public List<LetterTextTableEntity> LetterTextTable; // Replace 'EntityType' to an actual type that is serializable.
	public List<RewardTableEntity> RewardTable; // Replace 'EntityType' to an actual type that is serializable.
	public List<SaintMinigameEntity> SaintMinigameTable; // Replace 'EntityType' to an actual type that is serializable.
	public List<OhgueraeMinigameTableEntity> OhgueraeMinigameTable; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> StoryTable; // Replace 'EntityType' to an actual type that is serializable.
	public List<PrayTableEntity> PrayTable; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> FurnitureMakingTable; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> FurnitureTable; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> CharacterTable; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> AchievementTable; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> ResourceTable; // Replace 'EntityType' to an actual type that is serializable.
	public List<FlowerTableEntity> FlowerTable;

}
