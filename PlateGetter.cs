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
    //public gameobject used to get input user input from UNity and convert it into string
    public string input;
    //string used for the users number plat input
     public string plate;
     //string used for adding the users input into the api
    public GameObject panel;
    //public gameobject used to open the valid api output panel in Unity
    InputField report;
    //Input Field used to test if api output has a error message
    public string report2;
    // string used to test if the api output is 'null'
    public GameObject panel1;
    //GameObject used to link the output text boxes to the strings through the panel they are in in Unity
    public string api_output;
    //string used to turn the api output into a string
    public GameObject panel2;
    //gameobject used to open the error output message panel in Unity
    InputField error_outputBox;
    //inputfield to displace api output error e.g. invalid number plate input
    InputField yearText;
    //inputfield to displace car year from api output 
    InputField plateText;
    //inputfield to displace car number platefrom api output
    InputField makeText;
    //inputfield to displace car make from api output
    InputField modelText;
    //inputfield to displace car model from api output
    InputField subModelText;
    //inputfield to displace car submodel from api output
    InputField colourText;
    //inputfield to displace car colour from api output

    void Start()
    {
        report = transform.Find("Report").GetComponent<InputField>();
        // links the Report input field to the input box in Unity
        error_outputBox = panel2.transform.Find("ErrorOutputBox").GetComponent<InputField>();
        // links the error output input field to the input box in Unity
        yearText = panel1.transform.Find("YearText").GetComponent<InputField>();
        // links the car year input field to the input box in Unity
        plateText = panel1.transform.Find("PlateText").GetComponent<InputField>();
        // links the car number plate input field to the input box in Unity
        makeText = panel1.transform.Find("MakeText").GetComponent<InputField>();
        // links the car make input field to the input box in Unity
        modelText = panel1.transform.Find("ModelText").GetComponent<InputField>();
        // links the car model input field to the input box in Unity
        subModelText = panel1.transform.Find("SubModelText").GetComponent<InputField>();
        // links the car submodel input field to the input box in Unity
        colourText = panel1.transform.Find("ColourText").GetComponent<InputField>();
        // links the car colour input field to the input box in Unity
    }

    public void OpenPanel()
    //function that opens and closes panels(folders of object in Unity) 
    {
        if (panel != null)
        {
        bool isActive = panel.activeSelf;
        panel.SetActive(!isActive);
        }
    }
    
    public void OpenPanel2()
    //function that opens and closes panels(folders of object in Unity). this one opens another panel
    {
        if (panel2 != null)
        {
            bool isActive = panel2.activeSelf;

            panel2.SetActive(!isActive);
        }
    }

    public void GetData() => StartCoroutine(GetData_Coroutine());
    //

    public void GetInput()
    //function to get the users number plate input and make the string plate equal to it
    {
        input = inputBox.GetComponent<Text>().text;
        //get users input
        plate = input;
        //make the plate string equal to the input
    }

    IEnumerator GetData_Coroutine()
    //call to the api for the users inputted number plate
    {
    
    GetInput();
    string api_call = (url + key + "&plate=" + plate);
    //adds to api url prefix to the API key plus '&plate' plus the users number plate input to a string
    using(UnityWebRequest request = UnityWebRequest.Get(api_call))
    // calls to the api for the api_call string
        {
        yield return request.SendWebRequest();
        Thread.Sleep(750);
        report.text = request.downloadHandler.text;
        report2 = request.error;

        if (report2?.Length > 0)
        {   
            OpenPanel2();
            error_outputBox.text = "Sorry that number plate could not be found";
                    
        }

        else if (report.text == "null")
        {   
                OpenPanel2();
                error_outputBox.text = "Sorry that number plate could not be found";
                    
        }

        else
            {   
            api_output = request.downloadHandler.text;
            OpenPanel();
                   
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


            string[] textsplit = api_output.Split(',');

            plateText.text = textsplit[0];
            yearText.text = textsplit[1];
            makeText.text = textsplit[2];
           modelText.text = textsplit[3];
            subModelText.text = textsplit[4];
            colourText.text = textsplit[10];
            //WriteString();
            //ReadString();
            //OpenPanel();
                    
                    
                    
            }
        }
    }
            
                
    
        
           
    
}
                    