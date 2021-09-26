using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
[Category("Text")]
public class ChangeTextTask : ActionTask
{
    public BBParameter<GameObject> textGameobject;
	public string newText;

	protected override void OnExecute()
	{
		textGameobject.value.GetComponent<TextMesh>().text = newText;

		EndAction();
	}
}
