using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Manage : MonoBehaviour
{
    [SerializeField] private int space = 2;

    [SerializeField] GameObject Dots;
    public List<GameObject> DotsList;
    public Pathfinding path;
    void Start()
    {
        CreateDots(10);
        path = new Pathfinding(space);

    }


    private void CreateDots(int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Instantiate(Dots).transform.position = new Vector3((i - size / 2) * space, 10, (j - size / 2) * space);

            }
        }
    }
    
   


}

public class Pathfinding
{
    private List<GameObject>[] neighbours;
    public List<GameObject> Nodes;
    private int space;//a changer et prendre la valeur du manage ;
    private float buffer = 0.0001f;

    public Pathfinding(int spacez)
    {
        space = spacez;
        NeighbourList();
    }

    private void NeighbourList()
    {

        neighbours = new List<GameObject>[Nodes.Count];
        for (int i = 0; i < Nodes.Count; i++)
        {
            neighbours[i] = new List<GameObject>();
        }

        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = 0; i < Nodes.Count - 1; j++)
            {
                if (i != j)
                {
                    Vector3 currentNodeVec = new Vector3(Nodes[i].transform.position.x, 0, Nodes[i].transform.position.z);
                    Vector3 otherNodeVec = new Vector3(Nodes[j].transform.position.x, 0, Nodes[j].transform.position.z);
                    float distanceBetween = Vector3.SqrMagnitude(currentNodeVec - otherNodeVec);
                    if (distanceBetween < buffer + space)
                    {
                        neighbours[i].Add(Nodes[j]);
                    }
                }
            }
        }

    }
    private List<GameObject> GetNeighbours(GameObject go)
    {
        int nodeIndex = Nodes.IndexOf(go);
        if (nodeIndex < 0)
        {
            return null;
        }
        return neighbours[nodeIndex];
    }


    public static List<GameObject> GetPath(Pathfinding graph, GameObject start, GameObject end)
    {
        // Create dictionaries to keep track of the cost to go (as float), the previous node in the path, and the priority queue
        Dictionary<GameObject, float> cost2Go = new Dictionary<GameObject, float>();
        Dictionary<GameObject, GameObject> cameFrom = new Dictionary<GameObject, GameObject>();
        PriorityQueue<GameObject> frontier = new PriorityQueue<GameObject>();
        List<GameObject> path = new List<GameObject>();

        // Initialize all nodes as unvisited
        foreach (var node in graph.Nodes)
        {
            cost2Go[node] = float.MaxValue; // Max distance initially
            cameFrom[node] = null; // No parent initially
        }

        // Set start node's cost to 0
        cost2Go[start] = 0f;

        frontier.Enqueue(start, 0f); // Start with the start node having a priority of 0

        while (frontier.Count > 0)
        {
            GameObject currentNode = frontier.Dequeue(); // Get the node with the least cost

            // If we reach the end node, reconstruct the path
            if (currentNode == end)
            {
                while (currentNode != start)
                {
                    path.Add(currentNode); // Add node to path
                    currentNode = cameFrom[currentNode]; // Move to parent node
                }

                path.Add(start); // Add the start node at the end of the path
                path.Reverse(); // Reverse the path to show from start to end
                return path; // Return the path
            }

            // Get all neighboring nodes of the current node
            List<GameObject> neighbours = graph.GetNeighbours(currentNode);

            foreach (var neighbour in neighbours)
            {
                float newDistance = cost2Go[currentNode] + Vector3.SqrMagnitude(currentNode.transform.position - neighbour.transform.position);

                // If a shorter path to this neighbour is found, update the priority queue and path
                if (newDistance < cost2Go[neighbour])
                {
                    frontier.Enqueue(neighbour, newDistance);
                    cost2Go[neighbour] = newDistance;
                    cameFrom[neighbour] = currentNode;
                }
            }
        }

        return null; // Return null if no path was found
    }









}
public class PriorityQueue<T>
{
    private List<(T Item, float Priority)> data;

    public PriorityQueue()
    {
        this.data = new List<(T, float)>();
    }

    public void Enqueue(T item, float priority)
    {
        data.Add((item, priority));
        int childIndex = data.Count - 1;

        // Percolate up
        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (data[childIndex].Priority >= data[parentIndex].Priority)
                break;
            Swap(childIndex, parentIndex);
            childIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        if (data.Count == 0) throw new InvalidOperationException("Priority queue is empty.");

        int lastIndex = data.Count - 1;
        (T Item, float Priority) frontItem = data[0];

        // Replace root with the last element
        data[0] = data[lastIndex];
        data.RemoveAt(lastIndex);

        // Percolate down
        int parentIndex = 0;
        lastIndex = data.Count - 1;

        while (true)
        {
            int leftChildIndex = 2 * parentIndex + 1;
            int rightChildIndex = 2 * parentIndex + 2;
            if (leftChildIndex > lastIndex)
                break;

            int smallerChildIndex = (rightChildIndex > lastIndex || data[leftChildIndex].Priority < data[rightChildIndex].Priority)
                ? leftChildIndex
                : rightChildIndex;

            if (data[parentIndex].Priority <= data[smallerChildIndex].Priority)
                break;

            Swap(parentIndex, smallerChildIndex);
            parentIndex = smallerChildIndex;
        }

        return frontItem.Item;
    }

    public T Peek()
    {
        if (data.Count == 0)
            throw new InvalidOperationException("Priority queue is empty.");
        return data[0].Item;
    }

    public int Count => data.Count;

    private void Swap(int index1, int index2)
    {
        (data[index2], data[index1]) = (data[index1], data[index2]);
    }
}



