using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shuffle 
{
    System.Random r = new System.Random();

    public static Sprite red;

    public void shufflemethod(List<Sprite> arr)
    {
       int n = arr.Count;

        for (int i = n - 1; i > 0; i--)
        {

            int j = r.Next(0, i + 1);

            Sprite temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }

   
}
