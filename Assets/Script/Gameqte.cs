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
    public Text qteText;
    public void Qtemethod() {
        
        if(Qtekey.Count==0)
        {
            for (int i = 0; i <= 3; i++) {
                rand = Random.Range(0, 4);
                Qtekey.Enqueue(chara[rand]);
            }
            foreach (char q in Qtekey)
            {
                qteText.text += q;
            }
        }
        if (GameManager.gameManager.pendingCount != 0) {
            if(Qtekey.Count != 0) {
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
                        GameManager.gameManager.pendingPop(false);
                        qteText.text="";
                        qteCompare = '?';
                    }

                }
            }
            if(Qtekey.Count == 0){
                GameManager.gameManager.pendingPop(true);
                Qtekey.Clear();
                qteText.text="";
            }
        }
    }
    void Update() {
        Qtemethod();
    }
}