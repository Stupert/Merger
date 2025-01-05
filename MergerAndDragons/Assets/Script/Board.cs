using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject boardParent;
    public Vector2 boardSize = new Vector2(5, 40);
    public GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        initialiseBoard();
    }

    // Update is called once per frame
    void Update()
    {

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
        GameObject tile = Instantiate(tilePrefab);
        tile.GetComponentInChildren<Cell>().pos = pos;
        tile.transform.parent = boardParent.transform;
        tile.transform.localPosition = pos;
        string name = "Tile:  " + pos.x + "," + pos.y;
        tile.transform.name = name;
    }
}
