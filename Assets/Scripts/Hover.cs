using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover> //to access easily, singleton is used.
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }       
    }
    /// <summary>
    /// show building image 
    /// </summary>
    /// <param name="sprite"> building image</param>
    public void Activate(Sprite sprite)
    {
        this.spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
    }
    /// <summary>
    /// hide building image 
    /// </summary>   
    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        GameController.Instance.ClickedBtn = null;
    }
}
