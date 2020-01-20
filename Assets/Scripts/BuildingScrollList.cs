using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;  
    public GameObject prefab;
    public Sprite sprite;
}
public class BuildingScrollList : MonoBehaviour
{
    public List<Item> itemList;
    public Transform contentPanel;
    public BuildingObjectPool buttonObjectPool;
    public ScrollRect scrollbarVertical;

    // Use this for initialization
    void Update()
    {
        RefreshDisplay();
      
    }

    void RefreshDisplay()
    {
        RemoveButtons();
        AddBuildingButtons();
    }
    /// <summary>
    /// Remove building button from scrollview  
    /// </summary>
    private void RemoveButtons()
    {
        if (scrollbarVertical.verticalScrollbar.value == 0)
        {
            if (contentPanel.childCount > 30)
            {
                for (int i = 0; i < 20; i++)
                {
                    GameObject toRemove = transform.GetChild(0).gameObject;
                    buttonObjectPool.ReturnObject(toRemove);
                }
            }
        }
    }
    /// <summary>
    /// Add building button to the scrollview 
    /// </summary>
    private void AddBuildingButtons()
    {        
        if (scrollbarVertical.verticalScrollbar.value == 0)
        {
            scrollbarVertical.verticalScrollbar.value = 0.0000001f;
            int a = 0;
            while (a < 7)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    Item item = itemList[i];
                    GameObject newButton = buttonObjectPool.GetObject();
                    newButton.transform.SetParent(contentPanel);

                    BuildingButton buildingButton = newButton.GetComponent<BuildingButton>();
                    buildingButton.Setup(item);
                }
                a++;
            }            
        }        
    }
}
