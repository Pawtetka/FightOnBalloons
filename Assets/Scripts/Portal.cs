using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    public bool IsActive { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //IsActive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsActive && other.gameObject.transform.parent == null)
        {
            //portal.GetComponent<Portal>().IsActive = false;
            TransformObject(other.gameObject);
        }
        else if (IsActive && (other.gameObject.tag == "PlayerInfo" || other.gameObject.tag == "EnemyInfo"))
        {
            var list = new List<Transform>();
            /*
            foreach(var item in layers)
            {
                list.Add(item.startPosition);
            }
            */
            TransformObject(other.gameObject.transform.parent.gameObject);
            /*
            for(int i =0; i < layers.Count; i++)
            {
                layers[i].transform.position = list[i].position;
            }
            */
        }
        else if (IsActive && other.gameObject.tag == "Companion")
        {
            TransformObject(other.gameObject);
        }
    }

    private void TransformObject(GameObject obj)
    {
        float position;
        if (portal.transform.position.x > this.transform.position.x)
        {
            position = portal.transform.position.x - 1;
        }
        else
        {
            position = portal.transform.position.x + 1;
        }
        obj.gameObject.transform.position = new Vector2(position, obj.transform.position.y);
    }
}
