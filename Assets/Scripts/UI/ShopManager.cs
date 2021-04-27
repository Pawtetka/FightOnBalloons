using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public List<Category> categories;
    // public List<GameObject> gunSubcategories;

    [Header("SkinsHolderResetter")]
    [SerializeField] private GameObject[] componentsToSwitchOff;
    [SerializeField] private GameObject[] componentsToSwitchOn;

    void Start()
    {
        for (int i = 0; i < categories.Count; i++)
        {
            categories[i].categoryHolder.SetActive(false);
        }
        categories.Find(c => c.categoryName == "Weapons").SetCategoryActive();
    }

    public void ResetSkinsHolder()
    {
        for (int i = 0; i < componentsToSwitchOff.Length; i++)
        {
            componentsToSwitchOff[i].SetActive(false);
        }
        for (int i = 0; i < componentsToSwitchOn.Length; i++)
        {
            componentsToSwitchOn[i].SetActive(true);
        }
    }
}
