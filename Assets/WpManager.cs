using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

// Pega as variaveis necessárias para definir o link
public struct Link
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}
public class WpManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    void Start()
    {
        // Gera o grafo

        if (waypoints.Length > 0)
        {
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            foreach (Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }

    // desenha o grafo
    void Update()
    {
        graph.debugDraw();
    }

}