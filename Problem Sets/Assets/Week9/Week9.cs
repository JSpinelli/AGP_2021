﻿using System;
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
public class Week9 : MonoBehaviour
{
    public class Node
    {
        public List<Node> children;
        public int value;

        public Node(int value, params Node[] children)
        {
            this.value = value;
            this.children = children.ToList();
        }
    }

    // write a function that returns true if a tree contains a number, false if it doesn't.
    public bool ContainsNumber(Node root, int number)
    {
        if (root.value == number) return true;
        if (root.children.Count == 0) return false;
        foreach (var child in root.children)
        {
            if (ContainsNumber(child, number))
            {
                return true;
            }
        }

        return false;
    }

    // write a function that returns true if the tree contains duplicates, false if not.
    public bool ContainsDuplicates(Node root)
    {
        List<int> myNumbers = new List<int>();
        return ScanForDuplicates(root, myNumbers);
    }

    private bool ScanForDuplicates(Node root, List<int> currentNumbers)
    {
        if (currentNumbers.Contains(root.value))
            return true;
        currentNumbers.Add(root.value);
        if (root.children.Count == 0)
            return false;
        foreach (var child in root.children)
        {
            if (ScanForDuplicates(child, currentNumbers))
            {
                return true;
            }
        }

        return false;
    }

    // write a function to add a new node to the tree as a child of a node w/ value 'toAddTo'
    // return false if you can't find the node to add to, true if you successfully add it.
    public bool AddAsChild(Node root, int toAddTo, int numberToAdd)
    {
        if (root.value == toAddTo)
        {
            root.children.Add(new Node(numberToAdd));
            return true;
        }

        if (root.children.Count == 0)
            return false;
        foreach (var child in root.children)
        {
            if (AddAsChild(child, toAddTo, numberToAdd))
            {
                return true;
            }
        }

        return false;
    }

    // write a function that returns the depth of a number in the tree.  The root should be 0, it's children should
    // be 1, and so on.  Return -1 if it can't find the number in the tree.
    public int DepthOfNumber(Node root, int number)
    {
        return DepthCalculatorByNumber(root, number, 0);
    }

    public int DepthCalculatorByNumber(Node root, int numberToFind, int depthCounter)
    {
        if (root.value == numberToFind)
        {
            return depthCounter;
        }
        int foundNumber = -1;
        foreach (var child in root.children)
        {
            int childDepth = DepthCalculatorByNumber(child, numberToFind, depthCounter+1);
            if (childDepth > foundNumber)
            {
                foundNumber = childDepth;
            }
        }
        return foundNumber;
    }

    // MAde a fucntion that calculates the depth of the subtree below the given number, my bad
    public int DepthOfSubtree(Node root, int numberToFind, int depthCounter)
    {
        if (root.children.Count == 0)
        {
            if (depthCounter == -1 && root.value == numberToFind)
            {
                return 0;
            }
            if (depthCounter >= 0)
                return depthCounter + 1;
            return -1;
        }
        if (depthCounter >= 0 || root.value == numberToFind)
        {
            depthCounter = depthCounter + 1;
        }
        int maxDepth = -1;
        foreach (var child in root.children)
        {
            int childDepth = DepthCalculatorByNumber(child, numberToFind, depthCounter);
            if (childDepth > maxDepth)
            {
                maxDepth = childDepth;
            }
        }
        return maxDepth;
    }

    // =========================== DON'T EDIT BELOW THIS LINE =========================== //

    public TextMeshProUGUI left, right;

    private void Update()
    {
        var simpleTree = new Node(1, new Node(2, new Node(3), new Node(4)), new Node(5, new Node(6), new Node(7)));
        var treeWithDuplicates =
            new Node(1, new Node(2, new Node(3), new Node(1)), new Node(5, new Node(6), new Node(7)));


        left.text = "Contains Number\n<align=left>\n";
        left.text += Success(ContainsNumber(simpleTree, 6)) + " returns true correctly.\n";
        left.text += Success(!ContainsNumber(simpleTree, 8)) + " returns false correctly.\n";
        left.text += "\n</align>Contains Duplicates\n<align=left>\n";
        left.text += Success(!ContainsDuplicates(simpleTree)) + " correct for tree without duplicates.\n";
        left.text += Success(ContainsDuplicates(treeWithDuplicates)) + " correct for tree with duplicates.\n";

        var addAsChild1 = new Node(10);
        right.text = "Add as Child\n<align=left>\n";
        right.text += Success(!AddAsChild(addAsChild1, 9, 1)) + " add as child works correctly if can't find number.\n";

        var correct = AddAsChild(addAsChild1, 10, 1);
        right.text += Success(correct && addAsChild1.children[0].value == 1) + " adds child correctly.\n";


        var depthTree1 = new Node(0, new Node(1));
        right.text += "\n</align>Depth of Number\n<align=left>\n";
        right.text += Success(DepthOfNumber(depthTree1, 0) == 0) + " finds depth 0 correctly.\n";
        right.text += Success(DepthOfNumber(depthTree1, 1) == 1) + " finds depth 1 correctly.\n";
        right.text += Success(DepthOfNumber(depthTree1, 2) == -1) + " returns correctly when number not in tree.\n";
    }

    private string Success(bool test)
    {
        return test ? "<color=\"green\">PASS</color>" : "<color=\"red\">FAIL</color>";
    }
}