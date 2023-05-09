using System.Collections.Generic;

namespace Generators.Utils
{
    public static class Extensions
    {

        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = RandomUtils.GetRandomInt(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
    }   
}