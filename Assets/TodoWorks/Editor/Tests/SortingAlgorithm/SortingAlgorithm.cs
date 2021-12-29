using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortingAlgorithm : MonoBehaviour
{
    private string text = "1";
    private int[] arr = { 12, 41, 51, 1, 2, 5, 20};
    
    private void Start() => Sort();
    private void Sort()
    {
        // displays unsorted array
        const string m = "Unsorted:";
        var x = arr.Aggregate(" ", (current, p) => current + (p + " "));
        Debug.Log($"{m} {x}");
        
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

        // Displays the sorted array
        const string n = "Sorted:";
        var s = arr.Aggregate(" ", (current, p) => current + (p + " "));
        Debug.Log($"{n} {s}");
    }

    private void OnGUI()
    {
        text = GUI.TextField(new Rect(100, 100, 50, 50), text);
        
        var s = arr.Aggregate(" ", (current, p) => current + (p + " "));
        GUI.Label(new Rect(250, 100, 400, 50), $"Sorted list: {s}");
        
        if(!int.TryParse(text, out var num)) return; // makes sure to only parse numbers

        var index = SearchAlgorithm.LinearSearch(arr, num);
        if(index + 1 > arr.Length || index == -1) return; // makes sure you are not out of bounds of the array
        GUI.Label(new Rect(250, 120, 400, 100), $"Position in array:{index}\nnumber:{arr[index]}");
    }
}
