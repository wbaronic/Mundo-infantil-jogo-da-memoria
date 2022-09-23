using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * Developed by WESoft Soluções
 *  http://www.wesoft.com.br
 * 
 */

public static class Util
{
    public static void Shuffle<T>(this IList<T> list)
    {

        //Knuth shuffle
        for (int i = 0; i < list.Count; i++)
        {
            T tmp =  list[i];
            int r = Random.Range(i, list.Count);

            list[i] = list[r];
            list[r] = tmp;
        }

    }

    

}

