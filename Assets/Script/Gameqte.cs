using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameqte : MonoBehaviour {  
    public Queue<char> Qtekey = new Queue<char>();
    public string chara = "wsad";
    public int rand = Random.Range(0, 4);
    public int tmp = 0;
    public int tf = 0;
    public void Qtemethod() {
        
        if(tmp != GameManager.gameManager.pendingCount){
            if(tmp > GameManager.gameManager.pendingCount && GameManager.gameManager.pendingCount != 0 || (tmp == 0 && GameManager.gameManager.pendingCount == 1)){
                for (int i = 0; i <= 3; i++) {
                    Qtekey.Enqueue(chara[rand]);
                }
            }
            
            tmp = GameManager.gameManager.pendingCount;
        }
        


        if (GameManager.gameManager.pendingCount != 0) {
            

            if(Qtekey.Count != 0) {   
                if (Input.GetKeyDown(KeyCode.W)) {
                    if (Qtekey.Peek().Equals('w')) {
                        tf = 1;
                        Qtekey.Dequeue();
                        //continue;
                    }
                    else{
                        tf = 0;
                        //break;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.S)) {
                    if (Qtekey.Peek().Equals('s')) {
                        tf = 1;
                        Qtekey.Dequeue();
                        //continue;
                    }
                    else{
                        tf = 0;
                        //break;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.A)) {
                    if (Qtekey.Peek().Equals('a')) {
                        tf = 1;
                        Qtekey.Dequeue();
                        //continue;
                    }
                    else{
                        tf = 0;
                        //break;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.D)) {
                    if (Qtekey.Peek().Equals('d')) {
                        tf = 1;
                        Qtekey.Dequeue();
                        //continue;
                    }
                    else{
                        tf = 0;
                        //break;
                    }
                }
                else{
                    tf = 0;
                    //break;
                }
                
            }

            if(tf == 0){
                while(Qtekey.Count != 0) {
                    Qtekey.Dequeue();
                }
                GameManager.gameManager.pendingPop(false);
                return;
            }
            else if(Qtekey.Count == 0 && tf == 1){
                GameManager.gameManager.pendingPop(true);
            }
            
        }
    }
    void Update() {
        Qtemethod();
    }
}