using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using UnityEditor;
using System;
public class PlateGetter : MonoBehaviour
{
    private const string key = "9B7E4A2F1F09C028BA2DF9B1B4F3C9E1A3F3F718";
    //private string of the API Key
    public string url = "https://test.carjam.co.nz/a/vehicle:abcd?&key=";
    //string of prefix of the api url
    public GameObject inputBox;
    //public gameobject used to get input user input from Unity and convert it into string
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
    InputField carsSpotted;
    //input field to display cars spotted
    public int cars_entered;
    //record how many cars have been succefully found
    public GameObject Unique_Cars_Button;
    //game objected used to link the Unique_Cars_Button to the code

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
        carsSpotted = transform.Find("Unique_Cars_Output").GetComponent<InputField>();
        // links the unique
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
    //calls to the api for the api_call string
        {
        yield return request.SendWebRequest();
        //return the call request
        Thread.Sleep(750);
        //wait 750 miliseconds
        report.text = request.downloadHandler.text;
        //report inputfield equals the api output
        report2 = request.error;
        //report2 string equals the api call error
        if (report2?.Length > 0)
        //if report2s length is less than 0
        {   
            OpenPanel2();
            //opens the error panel in Unity
            error_outputBox.text = "Sorry that number plate could not be found";
            //displaces text saying ...
                    
        }

        else if (report.text == "null")
        //if the api ouput is equal to 'null'
        {   
                OpenPanel2();
                //open error panel in Unity
                error_outputBox.text = "Sorry that number plate could not be found";
                //displaces text saying ...
                    
        }

        else
        // if else
            {   
            api_output = request.downloadHandler.text;
            //api_output equal to api output
            OpenPanel();
            //open api poutput panel in Unity
            string[] textsplit = api_output.Split(',');
            //create a string called textsplit which is equal to api_output but split at every ','
            plateText.text = textsplit[0];
            //plateText's text is equal to the first split in textsplit
            yearText.text = textsplit[2];
            //yearText's text is equal to the second split in textsplit
            makeText.text = textsplit[3];
            //makeText's text is equal to the third split in textsplit
            modelText.text = textsplit[4];
            //modelText's text is equal to the fourth split in textsplit
            subModelText.text = textsplit[5];
            //subModelText's text is equal to the fifth split in textsplit
            colourText.text = textsplit[10];
            //colourText's text is equal to the eleventh  split in textsplit
            cars_entered += 1;
            //adds one to the total of cars seen
            string unique_cars = "Unique Cars Spotted: ";
            // creates a string that is the prefix of the carsSpotted out text 
            carsSpotted.text = unique_cars + cars_entered.ToString();        
            // displays the unique cars string and the int of cars entered to the cars spotted text in unity
            }
        }
    }
            
                
    
        
           
    
}
                    