using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater.Terrain
{
    [CreateAssetMenu(fileName = "TerrainSpawnConfig", menuName = "ScriptableObjects/TerrainSpawnConfig", order = 1)]
    public class TerrainSpawnConfig : ScriptableObject
    {
        public int minGridWidth;
        public int maxGridWidth;

        public int minGridHeight;
        public int maxGridHeight;

        public float minXOffset;
        public float maxXOffset;

        public float minYOffset;
        public float maxYOffset;

        public float minXZScale;
        public float maxXZScale;

        public float minYScale;
        public float maxYScale;

        public float spawnXPositionOffset;
        public float spawnYPositionOffset;
        public float minZPosition;
        public float maxZPosition;

        public float destroyXPositionInterval;

        public float minXZMultiply = 1f;
        public float maxXZMultiply = 2f;

        public float depthCorrection = 0f;
        public float centerCorrection = 5f;
        public float heightCorrection = 5f;
        public float minYScaleThresholed = 1f;

        public float minSpawnXPosInterval;
        public float maxSpawnXPosInterval;
    }

}
