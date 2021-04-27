using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] DialogueTrigger trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger.TriggerDialogue();
    }

}
