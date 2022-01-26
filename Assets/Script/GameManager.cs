using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int day=0;
    public static GameManager gameManager;
    public float diffuseTime = 16;
    public ChessGrid[,] gridArray = new ChessGrid[10, 8];
    public List<ChessGrid> infectList = new List<ChessGrid>();
    public Queue<ChessGrid> pendingQueue = new Queue<ChessGrid>();
 	IEnumerator passedDay()
    {
        while(true)
        {
            yield return new WaitForSeconds(5); 
            day++;
        }
    }
    public void pendingPush(ChessGrid grid)
    {
        if (pendingQueue.Count >= 3)
            return;
        if (infectList.FindIndex(x => x == grid) == -1)
            return;
        pendingQueue.Enqueue(grid);
    }

    public void pendingPush(int x, int y)
    {
        pendingPush(gridArray[x, y]);
    }

    public ChessGrid pendingPop(bool isSuccess)
    {
        if (pendingQueue.Count == 0)
            return null;
        if (isSuccess)
        {
            int ind = infectList.FindIndex(x => x == pendingQueue.Peek());

        }
        return pendingQueue.Dequeue();
    }

    public void setStatus(int x, int y, Status newStatus)
    {

    }
    
    public void clickGrid()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1); ;
            if (hit.collider != null)
            {
                ChessGrid hitGrid = hit.collider.GetComponent<ChessGrid>();
                if (hitGrid  != null && hitGrid.status == Status.infect)
                {
                    pendingPush(hitGrid);
                    hitGrid.time=0;
                    hitGrid.timeUIText.text = "";
                }  
            }
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
        StartCoroutine(passedDay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
