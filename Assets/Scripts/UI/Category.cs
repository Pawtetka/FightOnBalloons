using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    [Header("Category parameters")]
    public string categoryName;
    [SerializeField] public bool isActive;
    [SerializeField] private Vector3 scaleWhenActive;

    [Header("Category components")]
    // [SerializeField] private Image image;
    [SerializeField] private Button button;
    public GameObject categoryHolder;

    void Start()
    {
        // image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetCategoryActive);
        // button.onClick.AddListener(SetWeaponCategoriesUnactive);
    }

    public void SetCategoryActive()
    {
        for (int i = 0; i < GetComponentInParent<ShopManager>().categories.Count; i++)
        {
            gameObject.GetComponentInParent<ShopManager>().categories[i].isActive = false;
            gameObject.GetComponentInParent<ShopManager>().categories[i].categoryHolder.SetActive(false);
            gameObject.GetComponentInParent<ShopManager>().categories[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        gameObject.GetComponentInParent<ShopManager>().ResetSkinsHolder();
        categoryHolder.SetActive(true);
        isActive = true;
        gameObject.GetComponent<RectTransform>().localScale = scaleWhenActive;
    }
}
