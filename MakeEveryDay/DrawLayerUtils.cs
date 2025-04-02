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

        public static void InitializeDrawLayerUtils()
        {
            layerToDepth.Add(DrawLayer.DistantBackground, 1 / 5f);
            layerToDepth.Add(DrawLayer.NearBackground, 2 / 5f);
            layerToDepth.Add(DrawLayer.GameObjects1, 3 / 5f);
            layerToDepth.Add(DrawLayer.GameObjects2, 4 / 5f);
            layerToDepth.Add(DrawLayer.UI, 5 / 5f);
        }


        public static float GetUniqueDepth(DrawLayer drawLayer, bool incrementNumObjects = true)
        {
            if (incrementNumObjects) objectsPerLayer[drawLayer]++;
            return layerToDepth[drawLayer] + objectsPerLayer[drawLayer] * .0001f;
        }

    }
}
