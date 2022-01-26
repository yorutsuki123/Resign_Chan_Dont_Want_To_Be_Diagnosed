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

    public IEnumerator passedDiffuseTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); 
            if(status == Status.infect)
            {
                time--;
                timeUIText.text = "" + time;
                if(time <= 0 ){
                    time=GameManager.gameManager.diffUseTime;
                    diffuse();
                }
            }
        }
    }

    public void init(int x, int y)
    {
        status = Status.safe;
        locateX = x;
        locateY = y;
    }

    public void diffuse()
    {
        print("diffuse");

    }


}
