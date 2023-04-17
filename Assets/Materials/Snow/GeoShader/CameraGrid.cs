using System;
using UnityEngine;

namespace Materials.Snow.GeoShader
{
    [ExecuteInEditMode]
    public class CameraGrid : MonoBehaviour
    {
        public float gridSize = 10f;
        public Transform playerTransform;
        public event Action<Vector3Int> onPlayerGridChange;
        Vector3Int lastPlayerGrid = new Vector3Int(-99999,-99999,-99999);
    
        void Update () {
            if (playerTransform == null) {
                return;
            }
            Vector3 playerPos = playerTransform.position;
            Vector3Int playerGrid = new Vector3Int(
                Mathf.FloorToInt(playerPos.x / gridSize),
                Mathf.FloorToInt(playerPos.y / gridSize),
                Mathf.FloorToInt(playerPos.z / gridSize)
            );
    
            if (playerGrid != lastPlayerGrid) {
                if (onPlayerGridChange != null) {
                    onPlayerGridChange(playerGrid);
                }
                lastPlayerGrid = playerGrid;       
            }
        }
        public Vector3 GetGridCenter(Vector3Int grid) {
            float halfGrid = gridSize * .5f;
            return new Vector3(
                grid.x * gridSize + halfGrid, 
                grid.y * gridSize + halfGrid,
                grid.z * gridSize + halfGrid
            );
        }
    
        void OnDrawGizmos () {
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    for (int z = -1; z <= 1; z++) {

                        bool isCenter = x == 0 && y == 0 && z == 0;

                        Vector3 gridCenter = GetGridCenter(lastPlayerGrid + new Vector3Int(x, y, z));
                        Gizmos.color = isCenter ? Color.green : Color.red;
                        Gizmos.DrawWireCube(gridCenter, Vector3.one * (gridSize * (isCenter ? .95f : 1.0f)));
                    }
                }
            }   
        }
    }
}