using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortingAlgorithm : MonoBehaviour
{
    private void Start() => Sort();
    private void Sort()
    {
        int[] arr = { 12, 41, 51, 1, 2, 5, 20};

        // loops through the array until it cannot sort anymore
        for (var j = 0; j <= arr.Length - 2; j++) {
            for (var i = 0; i <= arr.Length - 2; i++)
            {
                // compares the value 1 to value 2
                if (arr[i] > arr[i + 1])
                {
                    // if value 2 is less than value one
                    // store value 2
                    var temp = arr[i + 1];
                    arr[i + 1] = arr[i]; // set value 2 to equal value 1
                    arr[i] = temp; // set value 1 to the stored value 2
                }
            }
        }

        const string n = "Sorted:";
        var s = arr.Aggregate(" ", (current, p) => current + (p + " "));
        Debug.Log($"{n} {s}");
    }
}
