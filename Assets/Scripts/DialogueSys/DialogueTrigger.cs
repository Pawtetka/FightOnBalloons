using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public List<Dialogue> dialogues;
	[SerializeField] List<GameObject> activeGameObjects;
	private void Start()
	{
		//Invoke("TriggerDialogue", 0.25f);
	}
	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogues, activeGameObjects);
	}

}
