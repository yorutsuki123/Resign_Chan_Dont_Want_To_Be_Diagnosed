using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Grid[,] gridArray = new Grid[10, 8];
    List<Grid> infectList = new List<Grid>();
    Queue<Grid> pendingQueue = new Queue<Grid>();

    public void pendingPush(Grid grid)
    {
        if (pendingQueue.Count >= 3)
            return;
        if (infectList.FindIndex(grid) == -1)
            return;
        pendingQueue.Enqueue(grid);
    }

    public void pendingPush(int x, int y)
    {
        pendingPush(gridArray[x, y]);
    }

    public Grid pendingPop(bool isSuccess)
    {
        if (pendingQueue.Count == 0)
            return null;
        if (isSuccess)
        {
            int ind = infectList.FindIndex(pendingQueue.Peek());

        }
        return pendingQueue.Dequeue();
    }

    public void setStatus(int x, int y, Status newStatus)
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
