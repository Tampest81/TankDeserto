using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowPath : MonoBehaviour
{
    //Variaveis
    Transform goal;
    public float speed = .25f;
    public float accuracy = 1.0f;
    public float rotSpeed = 20f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        wps = wpManager.GetComponent<WpManager>().waypoints;
        g = wpManager.GetComponent<WpManager>().graph;

        currentNode = wps[0];
    }

    // método para ir até o ponto, pega o ponto e aplica ele no goal
    public void GoToPoint(GameObject point)
    {
        g.AStar(currentNode, point);
        currentWP = 0;
    }

    void Update()
    {
        // Termina o método se estiver no ponto.
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
        {
            return;
        }

        //O nó que estará mais próximo neste momento
        currentNode = g.getPathPoint(currentWP);

        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position,transform.position) < accuracy)
        {
            currentWP++;
        }
        // se ele não estiver no destino
        if (currentWP < g.getPathLength())
        {
            // pega o transform do destino
            goal = g.getPathPoint(currentWP).transform;
            // recebe a posição do destino em vector3, ignorando o valor y do destino
            Vector3 lookAtGoal = new Vector3(goal.position.x,this.transform.position.y,goal.position.z);
            // recebe a direção do destino
            Vector3 direction = lookAtGoal - this.transform.position;
            // rotaciona o tank
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);
            // faz o tank andar a partir do transform
            this.transform.Translate(Vector3.forward * speed, Space.Self);

        }
    }
}