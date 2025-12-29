using UnityEngine;

public class MainLogic : MonoBehaviour
{
    [SerializeField]
    DataManager dataManager;

    [SerializeField]
    GameObject[] cubes;
    [SerializeField]
    Color[] colors;
    Renderer[] cubesRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Vector2Int startIndex = GetRandomIndex(dataManager.height, dataManager.width);
        //cubesRenderer = new Renderer[cubes.Length];
        // for (int i = 0; i < cubes.Length; i++)
        //     cubesRenderer[i] = cubes[i].GetComponent<Renderer>();
        //Colorize(startIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Colorize(Vector2Int index)
    {
        Debug.Log(index);
        int count = 0;
        for (int i = -1; i < 2; i++)
            for (int j = -1; j < 2; j++){
                int colorNum = dataManager.data[(index.y + i) % dataManager.height, (index.x + j) % dataManager.width];
                cubesRenderer[count].material.color = colors[colorNum];
                count++;
            }
    }

    Vector2Int GetRandomIndex(int height, int width)
    {
        return new Vector2Int(Random.Range(0, height), Random.Range(0, width));
    }
}
