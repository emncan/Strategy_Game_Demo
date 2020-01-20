using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController> //to access easily, singleton is used.
{
    int x = 4, y = 4 ,name = 1;
    public GameObject soldier;
    public Soldier soldierName { get; set; }
    [SerializeField]
    public BuildingButton ClickedBtn { get; set; }
    // Update is called once per frame
    void Start()
    {
       
    }
    void Update()
    {
        HandleEscape();              
    }
   
    public void PickBuilding(BuildingButton buildingBtn)
    {
        this.ClickedBtn = buildingBtn;
        Hover.Instance.Activate(buildingBtn.Sprite);
    }

    public void SetClickedBtnNull()
    {
        Hover.Instance.Deactivate();
       // ClickedBtn = null;
    }
    /// <summary>
    /// Handle escape press
    /// </summary>
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void CreateUnit()
    {        
        Point spawnPoint = new Point(x, y);
        if (LevelManager.Instance.Tiles[spawnPoint].IsEmpty && LevelManager.Instance.InBounds(spawnPoint))
        {
            soldier = (GameObject)Instantiate(new SoldierFactory().CreateUnit(), LevelManager.Instance.Tiles[spawnPoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
            LevelManager.Instance.Tiles[spawnPoint].IsEmpty = false;
            soldier.transform.SetParent(LevelManager.Instance.Tiles[spawnPoint].transform);
            soldierName = soldier.GetComponent<Soldier>();
            soldierName.name = "soldier" + name;
            name++;
            //Debug.Log(soldier.transform.parent.GetComponent<TileScript>().GridPosition.X);
            //Soldier.Instance.GridPosition = new Point(soldier.transform.parent.GetComponent<TileScript>().GridPosition.X,soldier.transform.parent.GetComponent<TileScript>().GridPosition.Y);
        }
        else
        {           
            x++;
            y++;
        }        
        x++;       
    }
}
