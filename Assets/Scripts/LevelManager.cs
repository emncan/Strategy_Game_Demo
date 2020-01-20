using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> //to access easily, singleton is used.
{
    [SerializeField]
    private GameObject tile; // a prefab for creating a single tile
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private Transform map; 
    private int mapX = 25;
    private int mapY = 25;    
    private Point mapSize;
    private Stack<Node> path;
    public Dictionary<Point,TileScript> Tiles { get; set; } // a dictionary that contains all tiles in our map
    public TileScript start { get; set; }
    public TileScript goal { get; set; }

    public Stack<Node> Path
    {
        get
        {
            if (path == null)
            {
                GeneratePath();
            }
            return new Stack<Node>(new Stack<Node>(path));
        }
    }
    /// <summary>
    /// Property
    /// calculates how big our tile to place tiles on the correct position
    /// </summary>
    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        SetStartAndGaolPoint();
    }
    /// <summary>
    /// creates our map
    /// </summary>
    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        Vector3 maxTile = Vector3.zero;
        mapSize = new Point(22,22);

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); //camera position (top-left corner)
        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
               PlaceTile(x, y ,worldStart);
            }
        }
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;
        cameraController.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize)); // sets the camera limits to the max tile position
    }
    /// <summary>
    /// place our tile
    /// </summary>
    /// <param name="x"> map x position</param>
    /// <param name="y">map y position</param>
    /// <param name="worldStart"> camera position (top-left corner) </param>
    private void PlaceTile(int x , int y , Vector3 worldStart)
    {
        TileScript newTile = Instantiate(tile).GetComponent<TileScript>();

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0),map);       
    }
    /// <summary>
    /// check Is it within the map border
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool InBounds(Point position)
    {
        return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
    }
    /// <summary>
    /// create path after selecting start and goal position
    /// </summary>
    public void GeneratePath()
    {
        path = AStar.GetPath(start.GridPosition, goal.GridPosition);
    }
    /// <summary>
    /// Set start and goal position for pathfinding
    /// </summary>
    private void SetStartAndGaolPoint()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider.name == "soldier1")
            {
                TileScript tmp = hit.collider.transform.parent.GetComponent<TileScript>();

                if (tmp != null)
                {
                    if (start == null)
                    {
                        start = tmp;
                        //Debug.Log(start.GridPosition.X);
                    }
                }
            }           
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.tag == "ground")
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();

                if (tmp != null)
                {
                    goal = null;
                    if (goal == null)
                    {
                        goal = tmp;
                        //Debug.Log(goal.GridPosition.X);
                    }
                }
                if (goal != null && start != null)
                {
                    GeneratePath();
                    UnitMovement.Instance.SetPath(Path);
                    GameController.Instance.soldier.transform.SetParent(LevelManager.Instance.Tiles[goal.GridPosition].transform);
                    goal = null;
                    start = null;
                }
            }
        }
        path = null;
    }
}
