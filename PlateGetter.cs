using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using UnityEditor;
public class PlateGetter : MonoBehaviour
{
    private const string key = "9B7E4A2F1F09C028BA2DF9B1B4F3C9E1A3F3F718";
    //private string of the API Key
    public string url = "https://test.carjam.co.nz/a/vehicle:abcd?&key=";
    //string of prefix of the api url
    public GameObject inputBox;
    public string input;
    //string for the users number plat input
    public GameObject Panel;
    InputField Report;
    public string Report2;
    public GameObject Panel1;
    public string text;
    public GameObject Panel2;
    InputField errorOutputBox;
    InputField yearText;
    InputField plateText;
    InputField makeText;
    InputField modelText;
    InputField subModelText;
    InputField colourText;

    void Start()
    {
        Report = transform.Find("Report").GetComponent<InputField>();
        errorOutputBox = Panel2.transform.Find("ErrorOutputBox").GetComponent<InputField>();
        yearText = Panel1.transform.Find("YearText").GetComponent<InputField>();
        plateText = Panel1.transform.Find("PlateText").GetComponent<InputField>();
        makeText = Panel1.transform.Find("MakeText").GetComponent<InputField>();
        modelText = Panel1.transform.Find("ModelText").GetComponent<InputField>();
        subModelText = Panel1.transform.Find("SubModelText").GetComponent<InputField>();
        colourText = Panel1.transform.Find("ColourText").GetComponent<InputField>();
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
    }

    IEnumerator GetData_Coroutine()
    {
    
    GetInput();
    string uri = (url + key + "&plate=" + input);
    using(UnityWebRequest request = UnityWebRequest.Get(uri))
        {
        yield return request.SendWebRequest();
        Thread.Sleep(750);
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
                   
            //Dictionary<string, string> CarDetails = new Dictionary<string, string>();
            //CarDetails = text.Split(',');
            //foreach(KeyValuePair<string, string> kvp in CarDetails)
                    
            //IDictionary<string, string> numberNames = new Dictionary<string, string>();
            //numberNames = text.Split(',');
	        //foreach(KeyValuePair<string, string> kvp in numberNames);
			//plateText.text = ("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
		
		    //foreach(var kvp in cities)
			//Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
	


            //var result = request.downloadHandler.text                 
            //.Select(line => line.Split(','))
            //.Where( split => split[0] != "Id" )
            //.ToDictionary(split => int.Parse(split[0]), split => new CarDetails(split[1], split[2], split[3], split[4],split[10]));


            //string[] textsplit = text.Split(',');

           //plateText.text = textsplit[0];
            //yearText.text = textsplit[1];
            //makeText.text = textsplit[2];
           //modelText.text = textsplit[3];
            //subModelText.text = textsplit[4];
            //colourText.text = textsplit[10];
            WriteString();
            ReadString();
            OpenPanel();
                    
                    
                    
            }
        }
    }
            
                
    
    static void WriteString()
    {
    
        string path = "Assets/Resources/text.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();
        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path); 
        TextAsset asset = Resources.Load(text) as TextAsset;
        //Print the text from the file
        Debug.Log(asset.text);
    }
    
    static void ReadString()
    {
        string path = "Assets/Resources/text.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }    
            
           
    
}
                    