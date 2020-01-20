using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// This script is used for all tiles in the game
/// </summary>
public class TileScript : MonoBehaviour
{
    private Color32 fullColor = new Color32(255, 118, 118, 225);
    private SpriteRenderer spriteRenderer;
    private Color32 emptyColor = new Color32(96, 255, 90, 255);
      
    private static Dictionary<Point, Node> nodesBuilding;
    public Point GridPosition { get;  set; } //tiles grid position
    public Point spawnPoint { get; set; }
    public bool IsEmpty { get;  set; }   
    public bool Debugging { get; set; } //just for Astar debugging
    public bool Walkable { get; set; }
 
    /// <summary>
    /// tile's center world position
    /// </summary>
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Sets up the tile
    /// </summary>
    /// <param name="girdPos">tiles grid position</param>
    /// <param name="worldPos"> tiles world position</param>
    /// <param name="parent"></param>
    public void Setup(Point girdPos ,Vector3 worldPos, Transform parent)
    {
        Walkable = true;
        IsEmpty = true;
        this.GridPosition = girdPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(girdPos, this);
    }
    /// <summary>
    /// Mouseover , this is executed when the palyer mouse over the tile
    /// </summary>
    private void OnMouseOver()
    {
       
        if (!EventSystem.current.IsPointerOverGameObject() && GameController.Instance.ClickedBtn != null)
        {           
            if (CheckNeighbourIsEmpty(GridPosition) && !Debugging)
            {
                ColorTile(emptyColor);
            }
            if (!CheckNeighbourIsEmpty(GridPosition) && !Debugging)
            {
                ColorTile(fullColor);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                UIController.Instance.HideInformationPanel();
                //spawnPoint = new Point(GridPosition.X + 3, GridPosition.Y + 4);
                //Debug.Log(spawnPoint.X+" ,"+ spawnPoint.Y);
                PlaceBuilding(GridPosition);             
            }
        }
    }
    /// <summary>
    /// make sure all tiles empty before placing building
    /// </summary>
    /// <param name="gridPos">tile grid position</param>
    /// <returns></returns>
    private bool CheckNeighbourIsEmpty(Point gridPos)
    {
        string buildingName = GameController.Instance.ClickedBtn.BuildingName.text;
        int X = 0;
        int Y = 0;
        if (buildingName == "B") // check building name if it barrack (B) check 4x4 tiles is empty else 2x3 for powerplan (P)
        { X = 3; Y = 3; }
        else
        {  X = 2;  Y = 1; }
      
        for (int y = 0; y <= Y; y++)
        {
            for (int x = 0; x <= X; x++)
            {
                Point neighbourPos = new Point(gridPos.X + x, gridPos.Y + y);
                if (LevelManager.Instance.Tiles[neighbourPos].IsEmpty && LevelManager.Instance.InBounds(gridPos)) // mkae sure empty and within our map
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// After placing building set all tiles full
    /// </summary>
    /// <param name="gridPos">tile grid position</param>
    private void SetNeighbourFull(Point gridPos)
    {
        string buildingName = GameController.Instance.ClickedBtn.BuildingName.text;
        int X = 0;
        int Y = 0;
        if (buildingName == "B") // check building name if it barrack (B) set 4x4 tiles full else 2x3 for powerplan (P)
        { X = 3; Y = 3; }
        else 
        { X = 2; Y = 1; }
        for (int y = 0; y <= Y; y++)
        {
            for (int x = 0; x <= X; x++)
            {
                Point neighbourPos = new Point(gridPos.X + x, gridPos.Y + y);
                LevelManager.Instance.Tiles[neighbourPos].IsEmpty = false;                
                LevelManager.Instance.Tiles[neighbourPos].Walkable = false;                
            }
        }
    }  
    /// <summary>
    /// Places a building on the tile
    /// </summary>
    /// <param name="gridPos">tile grid psorition</param>
    private void PlaceBuilding(Point gridPos)
    {               
        GameObject building = (GameObject) Instantiate(GameController.Instance.ClickedBtn.BuildingPrefab, transform.position, Quaternion.identity);        
        ColorTile(Color.white);
        SetNeighbourFull(gridPos);      
        GameController.Instance.SetClickedBtnNull();       
    }
    /// <summary>
    /// Mouseexit , this is executed when the palyer mouse exit the tile
    /// </summary>
    private void OnMouseExit()
    {
        if (!Debugging)
        {
            ColorTile(Color.white);
        }

    }

    /// <summary>
    /// Sets the color on the tile
    /// </summary>
    /// <param name="newColor"></param>
    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
    /// <summary>
    /// Hide information menu
    /// </summary>
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && GameController.Instance.ClickedBtn == null)
        {
            UIController.Instance.HideInformationPanel();           
        }
        
    }

    //private void OnMouseUp()
    //{
    //    if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && GameController.Instance.ClickedBtn == null)
    //    {
    //        LevelManager.Instance.goal = new Point(GridPosition.X, GridPosition.Y);
    //        //Debug.Log("goal " + LevelManager.Instance.goal.X);
    //        GeneratePath();
    //        UnitMovement.Instance.SetPath(Path);
    //        path = null;
    //    }
    //}

    //public void GeneratePath()
    //{
    //    path = AStar.GetPath(LevelManager.Instance.start, LevelManager.Instance.goal);
    //}

}
