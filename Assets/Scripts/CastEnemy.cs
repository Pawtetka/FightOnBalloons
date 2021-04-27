using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastEnemy : MonoBehaviour
{
    [SerializeField] private int coolDown;
    [SerializeField] private GameObject cast;
    private Animator animator;
    private bool canAttack;
    void Start()
    {
        animator = GetComponent<Animator>();
        canAttack = true;
        StartCoroutine("Cast");
    }

    IEnumerator Cast()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (UnityEngine.Random.Range(1, coolDown) == 1 && canAttack)
            {
                canAttack = false;
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("VillianSpell");
                }
                
                animator.SetTrigger("Cast");
                yield return new WaitForSeconds(0.75f);

                Vector3[] pos = new Vector3[UnityEngine.Random.Range(5, 15)];
                for (int i = 0; i < pos.Length; i++)
                {
                    pos[i] = RandomPosition(cast.transform);
                    var newLightning = Instantiate(cast, pos[i],
                    Quaternion.identity, gameObject.transform.parent);
                    newLightning.SetActive(true);
                    Invoke("CastAudioEffect", 0.75f);
                    Destroy(newLightning, 2f);
                }

                animator.SetTrigger("BackToIdle");
                canAttack = true;
            }
        }
    }

    private void CastAudioEffect()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Thunder");
        }
    }

    private Vector3 RandomPosition(Transform transform)
    {
        var x = UnityEngine.Random.Range(-16, 16);
        var y = UnityEngine.Random.Range(-5, 7);
        var z = transform.position.z;
        return new Vector3(x, y, z);
    }
}
