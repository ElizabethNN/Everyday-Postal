using System;

namespace Generators.Utils
{
    public class RandomUtils
    {
        private static Random _random = new();

        public static int GetRandomInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static int GetRandomInt(int max)
        {
            return _random.Next(max);
        }
    }   
}