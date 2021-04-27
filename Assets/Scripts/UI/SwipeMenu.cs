using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    [SerializeField] private GameObject scrollBar;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    private float scrollPosition = 0;
    private float[] itemsPosition;
    private int currentItemIndex = 0;

    void Start()
    {

    }

    public void NextItem()
    {
        if (currentItemIndex < itemsPosition.Length - 1)
        {
            currentItemIndex += 1;
            scrollPosition = itemsPosition[currentItemIndex];
        }
    }

    public void PreviousItem()
    {
        if (currentItemIndex > 0)
        {
            currentItemIndex -= 1;
            scrollPosition = itemsPosition[currentItemIndex];
        }
    }

    void Update()
    {
        itemsPosition = new float[transform.childCount];
        float distance = 1f / (itemsPosition.Length - 1f);
        for (int i = 0; i < itemsPosition.Length; i++)
        {
            itemsPosition[i] = distance * i;

        }
        if (Input.GetMouseButton(0))
        {
            scrollPosition = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < itemsPosition.Length; i++)
            {
                if (scrollPosition < itemsPosition[i] + (distance / 2) && scrollPosition > itemsPosition[i] - (distance / 2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, itemsPosition[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < itemsPosition.Length; i++)
        {
            if (scrollPosition < itemsPosition[i] + (distance / 2) && scrollPosition > itemsPosition[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int j = 0; j < itemsPosition.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(0.3f, 0.3f), 0.1f);
                        currentItemIndex = i;
                    }
                }
            }
        }

        SwitchWeaponButtonsController();
    }

    void SwitchWeaponButtonsController()
    {
        if(currentItemIndex == 0)
        {
            previousButton.SetActive(false);
        }
        else
        {
            previousButton.SetActive(true);
        }

        if(currentItemIndex == itemsPosition.Length - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }
}
