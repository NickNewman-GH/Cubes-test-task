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

    Vector2Int currentIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = GetRandomIndex(dataManager.width, dataManager.height);
        cubesRenderer = new Renderer[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
            cubesRenderer[i] = cubes[i].GetComponent<Renderer>();
        Colorize(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           currentIndex.x = (currentIndex.x + 1 + dataManager.height) % dataManager.height;
           Colorize(currentIndex);
        } else if (Input.GetKeyDown(KeyCode.A))
        {
           currentIndex.y = (currentIndex.y - 1 + dataManager.width) % dataManager.width;
           Colorize(currentIndex);
        } else if (Input.GetKeyDown(KeyCode.S))
        {
           currentIndex.x = (currentIndex.x - 1 + dataManager.height) % dataManager.height;
           Colorize(currentIndex);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
           currentIndex.y = (currentIndex.y + 1 + dataManager.width) % dataManager.width;
           Colorize(currentIndex);
        }
    }

    void Colorize(Vector2Int index)
    {
        Debug.Log(index);
        int count = 0;
        for (int i = -1; i < 2; i++)
            for (int j = -1; j < 2; j++){
                int colorNum = dataManager.data[(index.x + i + dataManager.height) % dataManager.height, (index.y + j + dataManager.width) % dataManager.width] - 1;
                cubesRenderer[count].material.color = colors[colorNum];
                count++;
            }
    }

    Vector2Int GetRandomIndex(int width, int height)
    {
        return new Vector2Int(Random.Range(0, width), Random.Range(0, height));
    }
}
