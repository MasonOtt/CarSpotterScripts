using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlateGetter : MonoBehaviour
{
    private const string key = "9B7E4A2F1F09C028BA2DF9B1B4F3C9E1A3F3F718";
    public string url = "https://test.carjam.co.nz/a/vehicle:abcd?&key=";
    public string plate;
    InputField rawOutputBox;
    public GameObject inputBox;
    public string input;
    public GameObject Panel;
    InputField Report;
    public string Report2;
    public GameObject Panel1;
    InputField outputText;
    public string text;
    public GameObject Panel2;
    InputField errorOutputBox;


    void Start()
    {
        rawOutputBox = Panel1.transform.Find("RawOutputBox").GetComponent<InputField>();
        Report = transform.Find("Report").GetComponent<InputField>();
        outputText = Panel1.transform.Find("OutputText").GetComponent<InputField>();
        errorOutputBox = Panel2.transform.Find("ErrorOutputBox").GetComponent<InputField>();
        
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }
    
    public void OpenPanel2()
    {
        if (Panel2 != null)
        {
            bool isActive = Panel2.activeSelf;

            Panel2.SetActive(!isActive);
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
        rawOutputBox.text = "Loading...";
        GetInput();
        string uri = (url + key + "&plate=" + plate);
        using(UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            Report.text = request.downloadHandler.text;
            Report2 = request.error;

                if (Report2?.Length > 0)
                {   
                    OpenPanel2();
                    errorOutputBox.text = "Sorry that number plate could not be found";
                    
                }

                else if (Report.text == "null")
                {   
                    OpenPanel2();
                    errorOutputBox.text = "Sorry that number plate could not be found";
                    
                }

                else
                {   
                    text = request.downloadHandler.text;
                

                    OpenPanel();
                    rawOutputBox.text = request.downloadHandler.text;
                    
                    
                }
            
                
            
        }   
    }
}