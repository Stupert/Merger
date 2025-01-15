using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public Mergables mergeItem;
    public SpriteRenderer spriteRenderer;
    public Vector2 pos;
    public BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCell();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCell()
    {
        if(mergeItem != null)
        {
            spriteRenderer.sprite = mergeItem.image;
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }

    public void DisableColliders()
    {
        collider.enabled = false;
    }

    public void EnableColliders()
    {
        collider.enabled = true;
    }

    public void Merge()
    {
        mergeItem = mergeItem.mergeOutcome;
        UpdateCell();
    }

}
