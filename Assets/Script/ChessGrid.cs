using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Status
{
    safe, // 安全
    infect, // 感染
    pending   // 待處理
}

public class ChessGrid : MonoBehaviour
{
    public Sprite[] spriteArray = new Sprite[3];
    
    
    // 宣告你的列舉變數 遊戲狀態
    public Status status;
    public int locateX;
    public int locateY;

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
    
    


    


    public void init(int x, int y)
    {
        status = Status.safe;
        locateX = x;
        locateY = y;
    }

    public void diffuse()
    {
        

    }


}
