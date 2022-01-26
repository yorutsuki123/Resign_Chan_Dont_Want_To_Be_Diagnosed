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


    public void diffuse()
    {
        

    }


}
