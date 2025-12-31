using UnityEngine;

public class MainLogic : MonoBehaviour
{
    [SerializeField]
    DataManager dataManager;

    [SerializeField]
    GameObject cubesContainer;
    [SerializeField]
    GameObject cubePrefab;
    GameObject[] cubes;
    [SerializeField]
    Color[] colors;
    Renderer[] cubesRenderer;

    Vector2Int currentIndex;

    float maxDelay = 1f, minDelay = 0.1f, delayTime = 0;
    float currentDelay;

    float fieldSideUnit = 5f;

    Vector3 baseSpawnPoint = new Vector3(-100, -100, 0);

    int minFieldSide = 1, maxFieldSide, fieldSide = 9;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxFieldSide = System.Math.Min(dataManager.width, dataManager.height);
        maxFieldSide -= 1 - (maxFieldSide % 2);  
        SpawnCubes();
        SetCubesActive();
        currentIndex = GetRandomIndex(dataManager.width, dataManager.height);
        cubesRenderer = new Renderer[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
            cubesRenderer[i] = cubes[i].GetComponent<Renderer>();
        Colorize(currentIndex);
        currentDelay = maxDelay;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (delayTime >= currentDelay)
            {
                if (Input.GetKey(KeyCode.W))
                    currentIndex.x = (currentIndex.x - 1 + dataManager.height) % dataManager.height;
                else if (Input.GetKey(KeyCode.S))
                    currentIndex.x = (currentIndex.x + 1 + dataManager.height) % dataManager.height;
                if (Input.GetKey(KeyCode.A))
                    currentIndex.y = (currentIndex.y - 1 + dataManager.width) % dataManager.width;
                else if (Input.GetKey(KeyCode.D))
                    currentIndex.y = (currentIndex.y + 1 + dataManager.width) % dataManager.width;
                Colorize(currentIndex);
                currentDelay = System.Math.Max(currentDelay / 1.5f, minDelay); 
                delayTime = 0;
            }
        } else
        {
            delayTime = maxDelay;
            currentDelay = maxDelay;
        }
        delayTime = System.Math.Min(delayTime + Time.deltaTime, maxDelay);
    }

    void SpawnCubes()
    {
        cubes = new GameObject[maxFieldSide * maxFieldSide];
        for (int i = 0; i < maxFieldSide * maxFieldSide; i++) {
            cubes[i] = Instantiate(cubePrefab, cubesContainer.transform);
            cubes[i].transform.position = baseSpawnPoint;
            cubes[i].SetActive(false);
        }
    }

    void SetCubesActive()
    {
        float scale = fieldSideUnit / fieldSide;
        float gap = scale * 0.2f;
        scale -= gap;
        Debug.Log($"{scale}, {gap}");
        for (int i = 0; i < fieldSide * fieldSide; i++)
        {
            cubes[i].SetActive(true);
            cubes[i].transform.localScale = new Vector3(scale, scale, scale);
            cubes[i].transform.position = new Vector3(i % fieldSide * (scale + gap) - fieldSideUnit / 2 + ((scale + gap) / 2), 0, (fieldSide * fieldSide - i - 1) / fieldSide * (scale + gap) - fieldSideUnit / 2 + ((scale + gap) / 2));
        }
    }

    void Colorize(Vector2Int index)
    {
        Debug.Log(index);
        int count = 0;
        for (int i = -(fieldSide / 2); i < (fieldSide / 2) + 1; i++)
            for (int j = -(fieldSide / 2); j < (fieldSide / 2) + 1; j++){
                int colorNum = dataManager.data[(index.x + i + dataManager.height) % dataManager.height, (index.y + j + dataManager.width) % dataManager.width];
                cubesRenderer[count].material.color = colors[colorNum];
                count++;
            }
    }

    Vector2Int GetRandomIndex(int width, int height)
    {
        return new Vector2Int(Random.Range(0, width), Random.Range(0, height));
    }

    public void DeactivateCubes()
    {
        for (int i = 0; i < maxFieldSide * maxFieldSide; i++) {
            cubes[i].transform.position = baseSpawnPoint;
            cubes[i].SetActive(false);
        }
    }

    public void PlusSize()
    {
        DeactivateCubes();
        fieldSide = System.Math.Min(fieldSide + 2, maxFieldSide);
        SetCubesActive();
        Colorize(currentIndex);
    }

    public void MinusSize()
    {
        DeactivateCubes();
        fieldSide = System.Math.Max(fieldSide - 2, minFieldSide);
        SetCubesActive();
        Colorize(currentIndex);
    }
}