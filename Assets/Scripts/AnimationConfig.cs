using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationConfig : MonoBehaviour
{
    public GameObject castObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("CastCreate");
        }
    }

    private IEnumerator CastCreate()
    {
        yield return new WaitForSeconds(0.5f);
        var newCast = Instantiate(castObject, castObject.transform.position,
            castObject.transform.rotation);
        newCast.SetActive(true);   
        Destroy(gameObject, 0.3f);
    }
}
