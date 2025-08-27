using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SneakingTeacherController : MonoBehaviour
{
    public static SneakingTeacherController instance;

    public Node startNode;
    public Node currentNode;
    public List<Node> path = new List<Node>();

    public SneakingPlayerController player;
    public SneakingManager manager;
    public float speed = 5f;

    private Node targetNode;
    public bool isMoving = false;

    private void Awake()
    {
        instance = this;
        player = FindFirstObjectByType<SneakingPlayerController>();
        manager = FindFirstObjectByType<SneakingManager>();
        startNode = currentNode;
    }

    private void Update()
    {
        if (isMoving)
        {
            CreatePath();
        }
    }

    public void TeacherMove()
    {
        GoToPlayer();
        isMoving = true;
    }

    void GoToPlayer()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
            if (path.Count == 2)
            {
                Debug.Log("found player");
                manager.Fail();
            }
            else if (path.Count >= 3)
            {
                targetNode = path[1];
            }
            else
            {
                targetNode = null;
            }
            
        }
    }

    public void CreatePath()
    {
        if (path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetNode.transform.position.x, targetNode.transform.position.y, -2), speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetNode.transform.position) < 0.01f)
            {
                currentNode = targetNode;
                if (path.Count - 1 == 2)
                {
                    manager.Fail();
                }
                path.Clear();
                isMoving = false;
                player.playerTurn = true;
            }
        }
    }
}
