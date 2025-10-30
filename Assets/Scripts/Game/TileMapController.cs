using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    [Serializable]
    public class TileData
    {
        public Vector3 m_initial;
        public Vector3 m_final;
    }

    public Tilemap m_tileMap;
    public List<TileData> pos;

    public void ClearTiles(int num)
    {
        Vector3Int start = Vector3Int.RoundToInt(pos[num].m_initial);
Vector3Int end   = Vector3Int.RoundToInt(pos[num].m_final);

// Calcular esquina inferior izquierda
int minX = Mathf.Min(start.x, end.x);
int maxX = Mathf.Max(start.x, end.x);
int minY = Mathf.Min(start.y, end.y);
int maxY = Mathf.Max(start.y, end.y);

int width  = maxX - minX + 1;
int height = maxY - minY + 1;

BoundsInt area = new BoundsInt(minX, minY, 0, width, height, 1);

foreach (Vector3Int tilePos in area.allPositionsWithin)
{
    m_tileMap.SetTile(tilePos, null);
}
        
    }
}
