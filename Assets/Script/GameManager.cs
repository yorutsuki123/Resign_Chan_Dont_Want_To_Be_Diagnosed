using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum Girl
{
    safe, // 安全
    infect, // 感染
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int day = 0;
    public int speedupDay = 5;
    public float diffUseTime = 16.0f;
    public float diffSpeedupTime = 1.0f;
    public float diffInfTime = 3.0f;
    public float infectUseTime = 8.0f;
    public float infectSpeedupTime = 0.8f;
    public float infectInfTime = 2.0f;
    public List<ChessGrid> gridSet = new List<ChessGrid>();
    public Queue<ChessGrid> pendingQueue = new Queue<ChessGrid>();
    public Text dayText;
    public Text unsafeProportionText;
    public GameObject gameOverObj;
    public GameObject[] girlObj = new GameObject[2];
    public bool isGameOver;
    public int unsafeCount
    {
        get { return gridSet.FindAll(x => x.status == Status.infect || x.status == Status.pending).Count; }
    }

    public float unsafeProportion
    {
        get { return (float)unsafeCount / 80.0f; }
    }

    public int pendingCount
    {
        get { return pendingQueue.Count; }
    }

 	IEnumerator passedDay()
    {
        while(true)
        {
            yield return new WaitForSeconds(5); 
            day++;
            if (day % speedupDay == 0) 
            {
                if (diffUseTime > diffInfTime)
                    diffUseTime -= diffSpeedupTime;
                if (infectUseTime > infectInfTime)
                    infectUseTime -= infectSpeedupTime;
            }
            dayText.text = "存活天數: " + day;
        }
    }

    IEnumerator passedToInfect()
    {
        yield return new WaitForSeconds(1); 
        while(true)
        {
            List<ChessGrid> safeGrid = new List<ChessGrid>();
            foreach (ChessGrid grid in gridSet)
            {
                if (grid.status == Status.safe)
                    safeGrid.Add(grid);
            }
            int randomIndex = Random.Range(0, safeGrid.Count);
            setStatus(safeGrid[randomIndex], Status.infect);
            print("INFECT " + safeGrid[randomIndex].locateX + " " + safeGrid[randomIndex].locateY);
            yield return new WaitForSeconds(infectUseTime); 
        }
    }
    public void showUnsafeProportion()
    {
        unsafeProportionText.text = "病毒感染率: " + (unsafeProportion * 100) + "%";
        if(unsafeProportion*100 >= 70)
        {
            if(girlObj[(int)Girl.safe].activeSelf)
                girlObj[(int)Girl.safe].SetActive(false);
            if(!girlObj[(int)Girl.infect].activeSelf)
                girlObj[(int)Girl.infect].SetActive(true);
        }
        else
        {
            if(!girlObj[(int)Girl.safe].activeSelf)
                girlObj[(int)Girl.safe].SetActive(true);
            if(girlObj[(int)Girl.infect].activeSelf)
                girlObj[(int)Girl.infect].SetActive(false);
        }
        if(unsafeProportion*100 >= 100)
        {
            isGameOver = true;
        }
    }
    public void gameOver()
    {
        if(isGameOver)
        {
            if(!gameOverObj.activeSelf)
            {
                SoundManager.soundManager.playSound(SoundType.fail);
                GetComponent<AudioSource>().Stop();
                gameOverObj.SetActive(true);
            }
               
            if(Input.anyKeyDown)
                SceneManager.LoadScene(0);
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
            grid.time = diffUseTime;
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
        if(Input.GetKey(KeyCode.Mouse0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, -1); ;
            if (hit.collider != null)
            {
                ChessGrid hitGrid = hit.collider.GetComponent<ChessGrid>();
                if (hitGrid  != null)
                {
                    //print(hitGrid.status + " " + hitGrid.locateX + " " + hitGrid.locateY); //TEST
                    if (hitGrid.status == Status.infect)
                    {
                        SoundManager.soundManager.playSound(SoundType.button);
                        pendingPush(hitGrid);
                    } 
                    /*
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
                    */
                }
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
        StartCoroutine(passedDay());
        StartCoroutine(passedToInfect());
    }

    void Start()
    {
        //
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        clickGrid();
        if (pendingQueue.Count != 0)
        {
            // Call QTE if not QTE-ing
        }
        showUnsafeProportion();
        gameOver();
    }
}
