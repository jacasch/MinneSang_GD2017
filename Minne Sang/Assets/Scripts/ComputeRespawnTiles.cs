using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ComputeRespawnTiles : MonoBehaviour {

    private Tilemap collisionMap;
    private Tilemap respawnMap;

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update() {
    }

    [ContextMenu("Generate Colliders")]
    void CalculateCollision() {

        collisionMap = transform.parent.Find("Collider").gameObject.GetComponent<Tilemap>();
        collisionMap.CompressBounds();
        respawnMap = GetComponent<Tilemap>();

        int xMin = collisionMap.cellBounds.xMin;
        int xMax = collisionMap.cellBounds.xMax;
        int yMin = collisionMap.cellBounds.yMin;
        int yMax = collisionMap.cellBounds.yMax;

        for (int x = xMin; x < xMax; x++) {
            for (int y = yMin; y < yMax; y++) {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = collisionMap.GetTile(pos);
                if (tile != null) {
                    Vector3Int newPos = new Vector3Int(x * 3 + 1, y * 3 + 1, 0);
                    respawnMap.SetTile(newPos,tile);
                }
            }
        }
    }
}
