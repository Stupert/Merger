using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public Mergables mergeItem;
    public SpriteRenderer spriteRenderer;
    public Vector2 pos;
    public BoxCollider2D collider;
    public ParticleSystem particleSystem;
    Board board;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCell();

    }

    // Update is called once per frame
    void Update()
    {
        
        board  = GameObject.Find("BoardManager").GetComponent<Board>();
    }

    public void UpdateCell()
    {
        if(mergeItem != null)
        {
            spriteRenderer.transform.localScale = new Vector3(mergeItem.scaleFactor, mergeItem.scaleFactor, mergeItem.scaleFactor);
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
        particleSystem.Play();
        gameObject.GetComponent<Animator>().SetTrigger("Merged");
        UpdateCell();
    }

    public void Generate()
    {
        if (mergeItem.isGenerator)
        {
            Cell designatedCell = board.GetClosestCell(this);
            if (designatedCell == null) return;
            board.SpawnItem(designatedCell, mergeItem.generativeItems[Random.Range(0, mergeItem.generativeItems.Count())]);
            designatedCell.SpawnAnimation();
            designatedCell = null;
        }
    }

    public void SelectAnimation()
    {
        if (mergeItem.isGenerator) return;
        gameObject.GetComponent<Animator>().SetTrigger("Selected");
    }

    public void SpawnAnimation()
    {
        //Debug.Log("Cell: " + pos + " animating");
        gameObject.GetComponent<Animator>().SetTrigger("Spawned");
    }

}
