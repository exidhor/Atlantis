using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class RandomCaseGenerator : RandomGenerator
    {
        public int availables
        {
            get { return _availables.Count; }
        }

        List<Vector2i> _availables = new List<Vector2i>();

        public RandomCaseGenerator(Vector2i size, int seed)
            : base(seed)
        {
            InitAvailables(size);
        }

        public RandomCaseGenerator(int seed)
            : this(Vector2i.zero, seed)
        { }

        public void InitAvailables(Vector2i size)
        {
            _availables.Clear();

            _availables.Capacity = size.x * size.y;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    _availables.Add(new Vector2i(i, j));
                }
            }
        }

        public Vector2i NextCase()
        {
            int i = Next(_availables.Count);
            Vector2i c = _availables[i];
            _availables.RemoveAt(i);

            return c;
        }

        public Vector2i NextCase(Func<Vector2i, float> getWeightFromCoord)
        {
            List<float> weights = new List<float>(_availables.Count);
            float totalWeight = 0f;

            for(int i = 0; i < _availables.Count; i++)
            {
                float w = getWeightFromCoord(_availables[i]);
                totalWeight += w;
                weights.Add(w);
            }

            float f = (float) NextDouble() * totalWeight;
            float cf = 0f;
            int index = weights.Count - 1;

            for (int i = 0; i < weights.Count; i++)
            {
                cf += weights[i];

                if(cf > f)
                {
                    index = i;
                    break;
                }
            }

            Vector2i c = _availables[index];
            _availables.RemoveAt(index);

            return c;
        }
    }
}

