using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    public enum DrawLayer
    {
        DistantBackground,
        NearBackground,
        GameObjects1,
        GameObjects2,
        UI
    }
    internal class DrawLayerUtils
    {
        private static Dictionary<DrawLayer, float> layerToDepth = new Dictionary<DrawLayer, float>();
        private static Dictionary<DrawLayer, int> objectsPerLayer = new Dictionary<DrawLayer, int>();

        /// <summary>
        /// Function intended to be called once at the beginning of the game to popate the dictionary
        /// </summary>
        public static void InitializeDrawLayerUtils()
        {
            layerToDepth.Add(DrawLayer.DistantBackground, 1 / 5f);
            layerToDepth.Add(DrawLayer.NearBackground, 2 / 5f);
            layerToDepth.Add(DrawLayer.GameObjects1, 3 / 5f);
            layerToDepth.Add(DrawLayer.GameObjects2, 4 / 5f);
            layerToDepth.Add(DrawLayer.UI, 5 / 5f);
        }

        /// <summary>
        /// Resets the objectsPerLayer array, making future layer depth assignments more reliable
        /// </summary>
        public static void ResetObjectsPerLayer()
        {
            for(int i = 0; i < objectsPerLayer.Keys.Count; i++)
            {
                objectsPerLayer.Values.ToList<int>()[i] = 0;
            }
        }

        /// <summary>
        /// Gets a uniqe float as a draw layer for a game object. 
        /// </summary>
        /// <param name="drawLayer">which broad layer should this object be on</param>
        /// <param name="incrementNumObjects"></param>
        /// <returns></returns>
        public static float GetUniqueDepth(DrawLayer drawLayer, bool incrementNumObjects = true)
        {
            if (incrementNumObjects) objectsPerLayer[drawLayer]++;
            return layerToDepth[drawLayer] + objectsPerLayer[drawLayer] * .0001f;
        }

    }
}
