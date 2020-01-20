using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Building button feature
/// </summary>
public class BuildingButton : MonoBehaviour
{
    [SerializeField]
    private Button buildingButton;
    [SerializeField]
    private Text buildingName;
    private Item item;
    private GameObject buildingPrefab;
    private Sprite sprite;
    public Text BuildingName
    {
        get { return buildingName; }

    }
    public Sprite Sprite
    {
        get { return sprite; }
    }
    
    public GameObject BuildingPrefab
    {
        get { return buildingPrefab; }
    }

    // Use this for initialization
    void Start()
    {
        buildingButton.onClick.AddListener(ButtonClick);
    }

    public void Setup(Item currentItem)
    {
        item = currentItem;
        buildingPrefab = item.prefab;
        sprite = item.sprite;
        buildingName.text = item.itemName;        
    }
    public void ButtonClick()
    {       
        GameController.Instance.PickBuilding(this);       
    }
}
