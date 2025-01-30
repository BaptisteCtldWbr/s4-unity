using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    string Bonjour(string qui){
        return "Bonjour " + qui;
        // return $"Bonjour {qui}";
    }
    int Carre(int nb){
        return nb*nb;
    }
    void Parle(){
        if(Input.GetKeyDown(KeyCode.V)){
            Debug.Log("Touche <color=red>V</color> pressée");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float NbDecimal = 3.14f;
        Debug.Log(NbDecimal);
        string monde = "le monde";
        Debug.Log(monde);
        int[] Nombres = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        //int[3] myInt = new Int[3]{0, 1, 2}
        Debug.Log(Nombres);
        Debug.Log(Bonjour(monde));
        Debug.Log(Carre(25));
    }

    // Update is called once per frame
    void Update()
    {
        Parle();
    }
}
