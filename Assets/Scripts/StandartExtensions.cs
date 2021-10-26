using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StandartExtensions
{
    static public T GetRandom<T>(this List<T> list)
    {
        if (list.Count > 0)
        {
            return list[Random.Range(0, list.Count)];
        }
        return default(T);
    }

    /*static public void Compact<T>(this List<T> list) where T: class
    {
        int i = 0, j = 1;
        while(i < list.Count)
        {
            if(list[i] == null)
            {
                if(j < i)
                {

                }
                else
                {

                }
            }
            else
            {
                ++i;
                ++j;
            }
        }
    }*/

    static public T GetRandom<T>(this System.Array arr)
    {
        if (arr.Length > 0)
        {
            return (T)arr.GetValue(Random.Range(0, arr.Length));
        }
        return default(T);
    }
}
