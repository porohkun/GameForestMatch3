using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForestMatch3.Core
{
    public class SortingLayer
    {
        private static Dictionary<string, SortingLayer> _layers = new Dictionary<string, SortingLayer>();

        public static SortingLayer GetLayer(string name)
        {
            return _layers[name];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order">[0]-(100)</param>
        public static void RegisterLayer(string name, int order)
        {
            if (order < 0) throw new ArgumentException($"order index {order} must be >= 0");
            if (order >= 100) throw new ArgumentException($"order index {order} must be < 100");
            if (_layers.Values.Any(l => l._order == order))
                throw new ArgumentException($"order index {order} already exists");
            _layers.Add(name, new SortingLayer(name, order));
        }

        public string Name { get; }
        private int _order { get; }
        public float Order { get; }

        private SortingLayer(string name, int order)
        {
            Name = name;
            _order = order;
            Order = order / 100f;
        }

        public float GetDepth(int orderInLayer)
        {
            if (orderInLayer <= -50) throw new ArgumentException($"inlayer order index {orderInLayer} must be > -50");
            if (orderInLayer >= 50) throw new ArgumentException($"inlayer order index {orderInLayer} must be < 50");
            return Order + orderInLayer / 10000f;
        }
    }
}
