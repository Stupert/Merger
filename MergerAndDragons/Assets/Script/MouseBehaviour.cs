using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MouseBehaviour : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField]
    private bool isDragging = false;
    [SerializeField] Cell clickedCell;
    public Cell droppedCell;
    [HideInInspector] public Mergables temp;
    public float lerpSpeed = 5f;


    #region clicked variables
    public Cell selectedCell;
    public float movementTheshhold;
    public float timethreshhold;
    private Vector2 initialMousePosition;
    public bool hasClicked = false;
    #endregion

    [SerializeField] UIManager uiManager;


    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uiManager.ClearText();
            CheckForSpriteClick();
        }

        if (hasClicked && Vector2.Distance(mainCamera.ScreenToWorldPoint(Input.mousePosition), initialMousePosition) > movementTheshhold)
        {
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            hasClicked = false;
            DragSprite();
        }

        if (Input.GetMouseButtonUp(0) && !isDragging && hasClicked)
        {
            selectedCell = clickedCell;
            clickedCell.EnableColliders();
            clickedCell = null;
            hasClicked = false;
            HandleCellClick();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            hasClicked = false;
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

            clickedCell.spriteRenderer.sortingOrder = 1;
            droppedCell = null;
            clickedCell = null;
        }
    }

    private void CheckForSpriteClick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if (hitCollider == null) return;

        Cell hitCell = hitCollider.GetComponent<Cell>();

        if (hitCell.mergeItem != null)
        {

            initialMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("Clicked " + hitCollider.gameObject.GetComponent<Cell>().pos);
            clickedCell = hitCollider.gameObject.GetComponent<Cell>();
            clickedCell.spriteRenderer.sortingOrder = 2;
            clickedCell.DisableColliders();
            hasClicked = true;

        }
        hitCell = null;
    }

    private bool CheckIfSpriteIsAlreadySelected(Cell cell)
    {
        if (selectedCell != null && cell.pos == selectedCell.pos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DragSprite()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        clickedCell.transform.position = Vector3.Lerp(clickedCell.transform.position, mousePosition, Time.deltaTime * lerpSpeed);
    }

    private void HandleCellClick()
    {
        if (selectedCell.mergeItem.isGenerator)
        {
            selectedCell.Generate();
        }
        selectedCell.SelectAnimation();
        uiManager.UpdateText(selectedCell.mergeItem.itemDescription);
    }

}