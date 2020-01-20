using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPicker : Singleton<BuildingPicker> //to access easily, singleton is used.
{
    [SerializeField]
    private Sprite buildingB;
    [SerializeField]
    private Sprite buildingPP;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SelectBuilding();
    }
    /// <summary>
    /// Select building with mouse left click and show and hide information menu
    /// </summary>
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && GameController.Instance.ClickedBtn == null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.transform.gameObject.tag == "B")
            {
                //Debug.Log(hit.transform.gameObject.tag);
                UIController.Instance.ShowInformationPanel();
                UIController.Instance.ShowUnitButton();
                UIController.Instance.BuildingImage.sprite = buildingB;
            }
            if(hit.transform.gameObject.tag == "PP")
            {
                UIController.Instance.ShowInformationPanel();
                UIController.Instance.HideUnitButton();
                UIController.Instance.BuildingImage.sprite = buildingPP;
            }
           
            //if (hit.transform ==null)
            //{
            //   // Debug.Log(hit.transform.gameObject.tag);
            //    UIController.Instance.HideInformationPanel();               
            //}
        }
    }
}
