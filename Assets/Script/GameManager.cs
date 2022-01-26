using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;


    public Grid[,] gridArray = new Grid[10, 8];
    public List<Grid> infectList = new List<Grid>();
    public List<Grid> pendingQueue = new List<Grid>();
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
