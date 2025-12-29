using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    TextAsset fileData;

    [System.NonSerialized]
    public int[,] data;
    [System.NonSerialized]
    public int width, height;

    //public string data = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetData();
    }

    void GetData()
    {
        string stringData = fileData.text;
        stringData = PrepareData(stringData);
        string[] splitedData = stringData.Split('\n');
        height = splitedData.Length;
        width = splitedData[0].Length;
        data = new int[splitedData.Length, splitedData[0].Length];
        for (int i = 0; i < splitedData.Length; i++)
            for (int j = 0; j < splitedData[i].Length; j++){
                data[i, j] = splitedData[i][j] - '0';
            }
    }

    string PrepareData(string stringData)
    {
        StringBuilder sb = new StringBuilder();
        for (int j = 0; j < stringData.Length; j++)
        {
            if ((stringData[j] > 47 && stringData[j] < 58) || (stringData[j] == '\n'))
                sb.Append(stringData[j]);
        }
        return sb.ToString();
    }
}
