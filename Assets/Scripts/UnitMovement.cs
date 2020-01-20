using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : Singleton<UnitMovement>
{
    [SerializeField]
    private float speed;     
    private Vector3 destination;   
    private Stack<Node> path;
    public Point GridPosition { get; set; }  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Move();
    }
    /// <summary>
    /// move unit
    /// </summary>
    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (transform.position == destination)
        {
            if (path != null && path.Count > 0)
            {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
            }
        }
    }
    /// <summary>
    /// set path for unit movement
    /// </summary>
    /// <param name="newPath"></param>
    public void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }    
}
