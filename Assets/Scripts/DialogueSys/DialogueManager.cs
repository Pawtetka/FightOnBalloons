using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public TMP_Text nameText;
	public TMP_Text dialogueText;
	public Image image;

	public Animator animator;

	private Queue<string> sentences;
	private Queue<string> names;
	private Queue<Sprite> images;
	private List<GameObject> activeGameObjects;

	void Awake () {
		sentences = new Queue<string>();
		names = new Queue<string>();
		images = new Queue<Sprite>();
	}

	public void StartDialogue (List<Dialogue> dialogues, List<GameObject> gameObjects)
	{
		activeGameObjects = gameObjects;
		animator.SetBool("IsOpen", true);
		foreach (GameObject gameObject in activeGameObjects)
		{
			gameObject.SetActive(false);
		}

		sentences.Clear();
		names.Clear();
		images.Clear();

		foreach (Dialogue dialogue in dialogues)
		{
			sentences.Enqueue(dialogue.sentence);
			names.Enqueue(dialogue.name);
			images.Enqueue(dialogue.image);
		}
		
		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		nameText.text = names.Dequeue();
		image.sprite = images.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		foreach (GameObject gameObject in activeGameObjects)
		{
			gameObject.SetActive(true);
		}
	}

}
