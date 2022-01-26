using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    enum Status
    {
        safe, // 安全
        infect, // 感染
        pending   // 待處理
    }

    // 宣告你的列舉變數 遊戲狀態
    Status status;


    public void diffuse()
    {
        

    }


}
