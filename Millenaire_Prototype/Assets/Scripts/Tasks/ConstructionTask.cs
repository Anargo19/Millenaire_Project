using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
[Category("Quest")]
public class ConstructionTask : ActionTask
{
    public GameObject building;

	protected override void OnExecute()
	{
		building.GetComponent<ConstructionScript>().Construct();

		EndAction();
	}
}
