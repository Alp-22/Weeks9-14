using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile grass;
    public Tile stone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int gridPos = tilemap.WorldToCell(mousePos);

            if (tilemap.GetTile(gridPos) == stone)
            {
                tilemap.SetTile(gridPos, grass);
            }
            else
            {
                tilemap.SetTile(gridPos, stone);
            }
        }
    }
}
