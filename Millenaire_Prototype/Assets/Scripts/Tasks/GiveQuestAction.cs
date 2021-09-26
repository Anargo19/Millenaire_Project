using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

[Category("Quest")]
public class GiveQuestAction : ActionTask
{

	public Quest questToGive;

	protected override void OnExecute()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		player.GetComponent<PlayerQuestList>().AddQuest(questToGive);

		EndAction();
	}
}