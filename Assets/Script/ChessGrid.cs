using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    safe, // 安全
    infect, // 感染
    pending   // 待處理
}

public class ChessGrid : MonoBehaviour
{
    
    // 宣告你的列舉變數 遊戲狀態
    public Status status;
    public int locateX;
    public int locateY;

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
