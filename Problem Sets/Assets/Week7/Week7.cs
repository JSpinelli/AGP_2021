using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using SimpleJSON;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class Week7 : MonoBehaviour
{
    /*
     * Below are a series of problems to solve with recursion.  You may need to make additional functions to do so.
     * Do not solve these problems with loops.
     */

    // Return the reversed version of the input.
    public string ReverseString(string toReverse)
    {
        if (toReverse.Length == 0) return "";
        return toReverse.Substring(toReverse.Length-1,1) + ReverseString(toReverse.Substring(0,toReverse.Length-1));
    }

    // Return whether or not the string is a palindrome
    public bool IsPalindrome(string toCheck)
    {
        if (toCheck.Length-2 <= 0) return toCheck[0] == toCheck[toCheck.Length-1];
        return (toCheck[0] == toCheck[toCheck.Length-1]) && IsPalindrome(toCheck.Substring(1,toCheck.Length-2));
    }

    // Return all strings that can be made from the set characters using all characters.
    public string[] AllStringsFromCharacters(params char[] characters)
    {
        if (characters.Length == 1) return new []{""+characters[0]};
        List<string> permutations = new List<string>();
        for (int i = 0; i < characters.Length; i++)
        {
            List<char> currentChars = new List<char>(characters);
            currentChars.RemoveAt(i);
            List<string> myPermutations = new List<string>(AllStringsFromCharacters(currentChars.ToArray()));
            foreach (var rest in myPermutations)
            {
                permutations.Add(characters[i]+rest);
            }
        }
        return permutations.ToArray();
    }
    
    // Return the sum of all the numbers given.

    public int SumOfAllNumbers(params int[] numbers)
    {
        if (numbers.Length == 1) return numbers[0];
        List<int> subArray = new List<int>(numbers);
        subArray.RemoveAt(0);
        return numbers[0] + SumOfAllNumbers(subArray.ToArray());
    }
    
    /*
     * Solve the following problem with recursion:
     *
     * A new soda company is doing a promotion - they'll buy back cans.  But they're not sure how much to charge per can,
     * or how much to pay out for cans.  Write a function that can determine how many cans someone can purchase for
     * a given amount of money, assuming they always return all the cans and then buy as much soda as they can.
     */

    public int TotalCansPurchasable(float money, float price, float canRefund)
    {
        if ((money - price) == 0) return 1;
        if ((money - price) < 0) return 0;
        return 1 + TotalCansPurchasable(money-price+canRefund,price,canRefund);
    }
    
    // =========================== DON'T EDIT BELOW THIS LINE =========================== //

    public TextMeshProUGUI recursionTest, sodaTest;
    
    private void Update()
    {
        recursionTest.text =  "Recursion Problems\n<align=left>\n";
        recursionTest.text += Success(ReverseString("TEST") == "TSET") + " string reverser worked correctly.\n";
        recursionTest.text += Success(!IsPalindrome("TEST") && IsPalindrome("ASDFDSA") && IsPalindrome("ASDFFDSA")) + " palindrome test worked correctly.\n";
        recursionTest.text += Success(AllStringsFromCharacters('A', 'B').Length == 2 && AllStringsFromCharacters('A').Length == 1 && AllStringsFromCharacters('A', 'B', 'C').Length == 6 && AllStringsFromCharacters('A', 'B', 'C', 'D', 'E', 'F', 'G').Length == 5040) + " all strings test worked correctly.\n";
        recursionTest.text += Success(SumOfAllNumbers(1, 2, 3, 4, 5) == 15 && SumOfAllNumbers(1, 2, 3, 4, 5, 6, 7) == 28) + " sum test worked correctly.\n";

        sodaTest.text = "Soda Problem\n<align=left>\n";
        
        sodaTest.text += Success(TotalCansPurchasable(4, 2, 1) == 3) + " soda test works correctly w/out change.\n";
        sodaTest.text += Success(TotalCansPurchasable(5, 2, 1) == 4) + " soda test works correctly w/change.\n";
    }

    private string Success(bool test)
    {
        return test ? "<color=\"green\">PASS</color>" : "<color=\"red\">FAIL</color>";
    }
}