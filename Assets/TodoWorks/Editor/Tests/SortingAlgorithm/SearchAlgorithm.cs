using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAlgorithm : MonoBehaviour
{
    public static int LinearSearch(int[] x, int valueToFind)
    {
        for (int i = 0; i < x.Length; i++)
        {
            if (valueToFind == x[i])
            {
                return i;
            }
        }
        return -1;
    }
}
