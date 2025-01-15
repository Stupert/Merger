using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MouseBehaviour : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    [SerializeField]Cell clickedCell;
    public Cell droppedCell;
    public Mergables temp;
    public float lerpSpeed = 5f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForSpriteClick();
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            DragSprite();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            
            if (hitCollider != null)
            {
                droppedCell = hitCollider.GetComponent<Cell>();
                if (droppedCell.mergeItem != null)
                {
                    if (droppedCell.mergeItem == clickedCell.mergeItem)//if the items are the same
                    {
                        if (clickedCell.mergeItem.mergeOutcome != null) //see if merger items is compatible for another merge
                        {
                            droppedCell.Merge();
                            clickedCell.transform.localPosition = clickedCell.pos;
                            clickedCell.mergeItem = null;
                            clickedCell.EnableColliders();
                            clickedCell.UpdateCell();
                        }
                        else //item list has no more merge outcomes
                        {
                            //TODO
                            //Animation for end of tree merging, a wiggle or vibrate from both items
                            clickedCell.transform.localPosition = clickedCell.pos;
                            clickedCell.EnableColliders();
                        }
                    }
                    else
                    {
                        clickedCell.transform.localPosition = clickedCell.pos;
                        clickedCell.EnableColliders();
                    }
                }
                else //if you have dropped the item on a blank square, this will swap the cells merge items so the item dragged is now blank and the blank item is now the draged item
                {
                    //Cell temp = new Cell();
                    temp = droppedCell.mergeItem;
                    droppedCell.mergeItem = clickedCell.mergeItem;
                    clickedCell.mergeItem = temp;

                    clickedCell.UpdateCell();
                    droppedCell.UpdateCell();
                    clickedCell.transform.localPosition = clickedCell.pos;
                    clickedCell.EnableColliders();
                    temp = null;
                }
            }
            else //user dropped the item on an in compatible object or no object
            {
                clickedCell.transform.localPosition = clickedCell.pos;
                clickedCell.EnableColliders();
            }

            clickedCell.spriteRenderer.sortingOrder = 0;
            droppedCell = null;
            clickedCell = null;
        }
    }

    private void CheckForSpriteClick()
    {
        // Convert mouse position to world position
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse position overlaps with the sprite's collider
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider != null && hitCollider.gameObject.GetComponent<Cell>().mergeItem != null)
        {
            Debug.Log("Clicked " + hitCollider.gameObject.GetComponent<Cell>().pos);
            clickedCell = hitCollider.gameObject.GetComponent<Cell>();
            clickedCell.spriteRenderer.sortingOrder = 1;
            clickedCell.DisableColliders();
            isDragging = true;
        }

    }

    private void DragSprite()
    {
        // Update sprite position to follow the mouse
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //clickedCell.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        clickedCell.transform.position = Vector3.Lerp(clickedCell.transform.position, mousePosition, Time.deltaTime * lerpSpeed);
    }
}
