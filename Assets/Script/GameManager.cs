using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int day = 0;
    public int speedupDay = 5;
    public float diffuseTime = 16.0f;
    public List<ChessGrid> gridSet = new List<ChessGrid>();
    public Queue<ChessGrid> pendingQueue = new Queue<ChessGrid>();

    public int unsafeCount
    {
        get { return gridSet.FindAll(x => x.status == Status.infect || x.status == Status.pending).Count; }
    }

    public float unsafeProportion
    {
        get { return (float)unsafeCount / 80.0f; }
    }

 	IEnumerator passedDay()
    {
        while(true)
        {
            yield return new WaitForSeconds(5); 
            day++;
            if (day % speedupDay == 0) {
                diffuseTime -= 1.0f;
            }
        }
    }

    public void pendingPush(ChessGrid grid)
    {
        if (pendingQueue.Count >= 3)
            return;
        if (grid.status != Status.infect)
            return;
        pendingQueue.Enqueue(grid);
        setStatus(grid, Status.pending);
    }

    public void pendingPush(int x, int y)
    { 
        pendingPush(gridSet.Find(tag => tag.locateX == x && tag.locateY == y));
    }

    public ChessGrid pendingPop(bool isSuccess)
    {
        if (pendingQueue.Count == 0)
            return null;
        if (isSuccess)
        {
            setStatus(pendingQueue.Peek(), Status.safe);
        }
        else
        {
            setStatus(pendingQueue.Peek(), Status.infect);
            pendingQueue.Peek().diffuse();
        }
        return pendingQueue.Dequeue();
    }

    public void setStatus(ChessGrid grid, Status newStatus)
    {
        grid.status = newStatus;
        grid.changeColor(newStatus);
        grid.time = 0.0f;
        if (newStatus == Status.infect)
            grid.time = diffuseTime;
        grid.timeUIText.text = "";
    }

    public void setStatus(int x, int y, Status newStatus)
    {
        setStatus(gridSet.Find(tag => tag.locateX == x && tag.locateY == y), newStatus);
    }

    public Status getStatus(int x, int y)
    {
        if (gridSet.FindAll(tag => tag.locateX == x && tag.locateY == y).Count == 0)
            return Status.none;
        return gridSet.Find(tag => tag.locateX == x && tag.locateY == y).status;
    }

    public void clickGrid()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1); ;
            if (hit.collider != null)
            {
                ChessGrid hitGrid = hit.collider.GetComponent<ChessGrid>();
                if (hitGrid  != null)
                {
                    print(hitGrid.status + " " + hitGrid.locateX + " " + hitGrid.locateY); //TEST
                    if (hitGrid.status == Status.infect)
                    {
                        pendingPush(hitGrid);
                    } 
                    //TEST DO
                    else if (hitGrid.status == Status.safe)
                    {
                        setStatus(hitGrid, Status.infect);
                    }
                    else if (hitGrid.status == Status.pending)
                    {
                        if (pendingQueue.Peek() == hitGrid)
                        {
                            pendingPop(true);
                        }
                    }
                    print("NOW " + hitGrid.status);
                    //TEST END
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
        clickGrid();
        if (pendingQueue.Count != 0)
        {
            // Call QTE if not QTE-ing
        }
    }
}
