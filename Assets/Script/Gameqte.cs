using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gameqte : MonoBehaviour {  
    public Queue<char> Qtekey = new Queue<char>();
    public string chara = "wsad";
    public char qteCompare='?';
    public int rand = 0;
    public int tmp = 0;
    public int tf = 3;
    public string qteStr;
    public Text[] qteText= new Text[4];
    public Sprite[] qteSprite= new Sprite[4];
    public Image[] qteImage= new Image[4];

    public int wsadToImage(char chara)
    {
        int imageNum = 0;
        if(chara == 'w')
            imageNum = 0;
        else if(chara == 's')
            imageNum = 1;
        else if(chara == 'a')
            imageNum = 2;
        else if(chara == 'd')
            imageNum = 3;
        else 
            imageNum = 4;
        return imageNum;
    }
    public void Qtemethod() {
        
        if(Qtekey.Count==0)
        {
            for (int i = 0; i <= 3; i++) {
                rand = Random.Range(0, 4);
                Qtekey.Enqueue(chara[rand]);
            }
            foreach (char q in Qtekey)
            {
                qteStr += q;
            }
        }
        if (GameManager.gameManager.pendingCount != 0) {
            if(Qtekey.Count != 0) {
                for(int i=0;i<4;i++)
                {
                    int imageNum = wsadToImage(qteStr[i]);
                    if(imageNum == 4)
                    {
                        qteImage[i].color = Color.black;
                        imageNum = 3;
                    }
                    else
                    {
                        qteImage[i].color = Color.white;
                    }
                    qteImage[i].sprite = qteSprite[imageNum];
                    // qteText[i].text = "" + qteStr[i];
                }
                if (Input.GetKeyDown(KeyCode.W)) {
                    qteCompare = 'w';
                }
                else if (Input.GetKeyDown(KeyCode.S)) {
                    qteCompare = 's';
                }
                else if (Input.GetKeyDown(KeyCode.A)) {
                    qteCompare = 'a';
                }
                else if (Input.GetKeyDown(KeyCode.D)) {
                    qteCompare = 'd';
                }
                if(qteCompare == Qtekey.Peek())
                {
                    SoundManager.soundManager.playSound(SoundType.qte);
                    tf=1;
                    Qtekey.Dequeue();
                    qteCompare = '?';
                }
                else 
                {
                    if(qteCompare != '?')
                    {
                        Qtekey.Clear();
                        tf=0;
                        for(int i=0;i<4;i++)
                        {
                            qteText[i].text = "" ;
                            qteImage[i].color = Color.black;
                        }
                        GameManager.gameManager.pendingPop(false);
                        qteStr="";
                        qteCompare = '?';
                    }
                }
            }
            if(Qtekey.Count == 0 && tf == 1){
                GameManager.gameManager.pendingPop(true);
                //Qtekey.Clear();
                qteStr="";
                qteCompare = '?';
                for(int i=0;i<4;i++)
                {
                    qteText[i].text = "" ;
                    qteImage[i].color = Color.black;
                }
            }
        }
    }
    void Update() {
        Qtemethod();
    }
}