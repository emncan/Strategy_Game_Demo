using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control UI element
/// </summary>
public class UIController : Singleton<UIController>
{
    [SerializeField]
    private Transform informationMenu;
    [SerializeField]
    private Button unitButton;
    [SerializeField]
    private Image buildingImage;

    public Image BuildingImage
    {
        get { return buildingImage; }
    }
    void Start()
    {
        unitButton.onClick.AddListener(UnitButtonOnClick);
    }

    public void UnitButtonOnClick()
    {
        GameController.Instance.CreateUnit();
    }
    public void ShowUnitButton()
    {      
        unitButton.gameObject.SetActive(true);
    }
    public void HideUnitButton()
    {
        unitButton.gameObject.SetActive(false);
    }
    public void ShowInformationPanel()
    {
        informationMenu.gameObject.SetActive(true);
    }
    public void HideInformationPanel()
    {
        informationMenu.gameObject.SetActive(false);
    }
}
