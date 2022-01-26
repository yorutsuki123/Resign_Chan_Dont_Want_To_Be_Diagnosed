using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Grid[,] gridArray = new Grid[10, 8];
    public List<Grid> infectList = new List<Grid>();
    public Queue<Grid> pendingQueue = new Queue<Grid>();

    void pendingPush(Grid grid)
    {
        if (pendingQueue.Count >= 3)
            return;
        pendingQueue.Enqueue(grid);
    }

    Grid pendingPop()
    {
        if (pendingQueue.Count == 0)
            return null;
        return pendingQueue.Dequeue();
    }

    void setStatus(int x, int y, Status newStatus)
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
