using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenKing : MonoBehaviour
{
    [SerializeField] DialogueTrigger dialogueTrigger;
    [SerializeField] GameObject companion;
    private GameObject player;
    private bool canDialog = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 1.5 && canDialog)
        {
            companion.transform.position = gameObject.transform.position;
            dialogueTrigger.TriggerDialogue();
            canDialog = false;
        }
        else if (Vector2.Distance(gameObject.transform.position, player.transform.position) > 1.5)
        {
            LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, 3 * Time.deltaTime);
        }

        if (companion.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void LookAt(Vector3 target)
    {
        if (target.x > transform.position.x)
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
        }
        else
        {
            transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
        }
    }
}
