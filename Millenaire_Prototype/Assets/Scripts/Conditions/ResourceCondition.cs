using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

[Category("Quest")]

public class ResourceCondition : ConditionTask
{
	public ResourcesScriptable resource;
	public int nbResources;


	protected override bool OnCheck()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player.GetComponent<PlayerResources>().GetSpecificResources(resource) >= nbResources) { return true; }

		return false;
	}
}
