using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading;

public class PlateGetter : MonoBehaviour
{
    private const string key = "9B7E4A2F1F09C028BA2DF9B1B4F3C9E1A3F3F718";
    public string url = "https://test.carjam.co.nz/a/vehicle:abcd?&key=";
    public string plate;
    InputField outputBox;
    public GameObject inputBox;
    public string input;
    public GameObject Panel;
    public string a;

    void Start()
    {
        outputBox = GameObject.Find("OutputBox").GetComponent<InputField>();
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }

    public void GetData() => StartCoroutine(GetData_Coroutine());
    
        public void GetInput()
    {
        input = inputBox.GetComponent<Text>().text;
        plate = input;
    }

    IEnumerator GetData_Coroutine()
    {
        outputBox.text = "Loading...";
        GetInput();
        string uri = (url + key + "&plate=" + plate);
        using(UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
                if( request.isNetworkError|| request.isHttpError)
                {   
                    outputBox.text = request.error;
                    outputBox.text = "TEST";
                }
                else
                {   
                
                   outputBox.text = request.downloadHandler.text;
                   outputBox.text = "PLEASE WORK PLZ";
                }
            
                
            
        }   
    }
}