using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Status
{
    safe, // 安全
    infect, // 感染
    pending,   // 待處理
    none = -1  // 邊界外 (查詢座標用)
}

public class ChessGrid : MonoBehaviour
{
    public Sprite[] spriteArray = new Sprite[3];
    public Text timeUIText;
    public float time=0;
    public Status status;
    public int locateX;
    public int locateY;

    void Start()
    {
        timeUIText = transform.GetChild(0).GetComponent<Text>();
        StartCoroutine(passedDiffuseTime());
    }
    public void changeColor(Status color)
    {
        if (color == Status.safe)
        {
            GetComponent<Image>().sprite = spriteArray[(int)Status.safe];
        }
        else if (color == Status.infect)
        {
            GetComponent<Image>().sprite = spriteArray[(int)Status.infect];
        }
        else
        {
            GetComponent<Image>().sprite = spriteArray[(int)Status.pending];
        }
    }

    struct Locate
    {
        public int x;
        public int y;

        public Locate(int nX, int nY)
        {
            x = nX;
            y = nY;
        }
    };

    public void diffuse()
    {
        List<Locate> neighbour = new List<Locate>();
        List<Locate> safeNeighbour = new List<Locate>();
        neighbour.Add(new Locate(locateX, locateY + 1));
        neighbour.Add(new Locate(locateX, locateY - 1));
        neighbour.Add(new Locate(locateX - 1, locateY));
        neighbour.Add(new Locate(locateX + 1, locateY));
        neighbour.Add(new Locate(locateX - 1, locateY + (locateX % 2 == 0 ? -1 : 1)));
        neighbour.Add(new Locate(locateX + 1, locateY + (locateX % 2 == 0 ? -1 : 1)));
        foreach (Locate loc in neighbour)
        {
            if (GameManager.gameManager.getStatus(loc.x, loc.y) == Status.safe)
                safeNeighbour.Add(loc);
        }
        int diffusePoint = Random.Range(1, 3);
        for (int i = 0; i < diffusePoint && safeNeighbour.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, safeNeighbour.Count);
            int ranX = safeNeighbour[randomIndex].x;
            int ranY = safeNeighbour[randomIndex].y;
            GameManager.gameManager.setStatus(ranX, ranY, Status.infect);
            safeNeighbour.RemoveAt(randomIndex);
        }
    }

    public IEnumerator passedDiffuseTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); 
            if(status == Status.infect)
            {
                time--;
                if(time <= 0 )
                {
                    time=GameManager.gameManager.diffUseTime;
                    diffuse();
                }
                timeUIText.text = "" + time;
            }
        }
    }

    public void init(int x, int y)
    {
        status = Status.safe;
        locateX = x;
        locateY = y;
    }



}
