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


    public void diffuse()
    {
        int ranX = locateX + Random.Range(-1,2);
        int ranY = locateY + Random.Range(-1,2);
        GameManager.gameManager.setStatus(ranX, ranY, Status.infect);
    }
    public IEnumerator passedDiffuseTime()
    {
        while(true)
        {
            int count=0;
            yield return new WaitForSeconds(1); 
            if(status == Status.infect)
            {
                time--;
                timeUIText.text = "" + time;
                if(time <= 0 ){
                    time=GameManager.gameManager.diffuseTime;
                    if(count >0){
                        diffuse();
                    }
                    count++;
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



}
