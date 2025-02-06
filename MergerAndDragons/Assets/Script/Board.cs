using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Board : MonoBehaviour
{
    public GameObject boardParent;
    public Vector2 boardSize = new Vector2(5, 40);
    public GameObject tilePrefab;

    public List<Mergables> mergeablesLow = new List<Mergables>();
    public List<Mergables> mergeablesMid = new List<Mergables>();
    public List<Mergables> mergeablesHigh = new List<Mergables>();
    public List<Cell> allCells = new List<Cell>();

    // Start is called before the first frame update
    void Start()
    {
        initialiseBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            for (int i = 0; i < allCells.Count; i++)
            {
                if (!allCells[i].mergeItem)
                {
                    Debug.Log("null");
                }
                else
                {
                    Debug.Log(allCells[i].mergeItem.name);

                }
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            PopulateBoardAtRandom();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            PopulateLowT();
        }
    }

    public void initialiseBoard()
    {
        for (int i = 0; i < boardSize.x; i++)
        {
            for (int j = 0; j < boardSize.y; j++)
            {
                NewTileSpawning(new Vector2(i, j));
            }
        }
    }

    public void NewTileSpawning(Vector2 pos)
    {
        int rand;
        GameObject tile = Instantiate(tilePrefab);
        tile.GetComponentInChildren<Cell>().pos = pos;
        rand = Random.Range(0, 1);
        tile.transform.parent = boardParent.transform;
        tile.transform.localPosition = pos;
        string name = "Tile:  " + pos.x + "," + pos.y;
        tile.transform.name = name;
        allCells.Add(tile.GetComponent<Cell>());
    }

    void PopulateBoardAtRandom()
    {
        List<Cell> emptyCells = new List<Cell>();
        int itemRarityChance;
        Mergables item;
        Cell position;

        for (int i = 0; i < allCells.Count; i++)
        {
            if (!allCells[i].mergeItem)
            {
                emptyCells.Add(allCells[i]);

            }
        }
        if (emptyCells.Count == 0) return;

        position = emptyCells[Random.Range(0, emptyCells.Count)];


        itemRarityChance = Random.Range(0, 10);
        if (itemRarityChance < 7)
        {
            item = mergeablesLow[Random.Range(0, mergeablesLow.Count)];
            //spawn low level item
        }
        else if (itemRarityChance < 10)
        {
            item = mergeablesMid[Random.Range(0, mergeablesMid.Count)];
            //spawn mid level item
        }
        else
        {
            item = mergeablesHigh[Random.Range(0, mergeablesHigh.Count)];
            //spawn high level item
        }

        SpawnItem(position, item);
        emptyCells.Clear();
        itemRarityChance = 0;
        item = null;
        position = null;
    }

    void PopulateLowT()
    {
        List<Cell> emptyCells = new List<Cell>();
        Mergables item;
        Cell position;

        for (int i = 0; i < allCells.Count; i++)
        {
            if (!allCells[i].mergeItem)
            {
                emptyCells.Add(allCells[i]);
            }
        }
        if (emptyCells.Count == 0) return;

        item = mergeablesLow[0];

        position = emptyCells[Random.Range(0, emptyCells.Count)];


        SpawnItem(position, item);
        emptyCells.Clear();
        item = null;
        position = null;
    }

    public void SpawnItem(Cell pos, Mergables item)
    {
        if (pos == null)return;

        if (pos.mergeItem != null) //redundant
        {
            Debug.Log("Position not empty");
            return;
        }

        foreach (Cell cell in allCells)
        {
            if (cell.name == pos.name)
            {
                cell.mergeItem = item;
                cell.UpdateCell();
            }
        }
    }

    public Cell GetClosestCell(Cell origin)
    {
        Cell closestCell = null;
        float minDistance = float.MaxValue;

        foreach (Cell cell in allCells)
        {
            if (cell.mergeItem == null)
            {
                float distance = Vector2.Distance(origin.pos, cell.pos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCell = cell;
                }
            }
        }

        return closestCell;
    }
}


