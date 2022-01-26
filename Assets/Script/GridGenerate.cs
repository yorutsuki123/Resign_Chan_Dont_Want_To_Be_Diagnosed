using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    public GameObject gridPrefab;
    public RectTransform startPos;
    public Vector2 gridSize = new Vector2(559,-650);
    void mapCreate()
    {
        for(int x=0;x<10;x++)
        {
            Vector2 createPos = startPos.anchoredPosition3D;
            createPos.x += gridSize.x * x;
            if((x+1)%2 == 0)
            {
                createPos.y += gridSize.y/2;
            }
            for(int y=0; y<8; y++)
            {
                createPos.y += gridSize.y ;
                GameObject prefab = Instantiate(gridPrefab);
                prefab.transform.parent = transform;
                prefab.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(createPos.x, createPos.y);
                prefab.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
                prefab.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
                GameManager.gameManager.gridArray[x, y] = prefab.GetComponent<ChessGrid>();
                GameManager.gameManager.gridArray[x, y].init(x, y);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mapCreate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
