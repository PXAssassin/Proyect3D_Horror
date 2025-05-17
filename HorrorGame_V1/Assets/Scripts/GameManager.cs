using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// instancia del game manager
    private int score = 0;

    public int Score { get => score; set => score = value; }// propiedad para acceder al score desde otras clases

    private void Awake()
    {
        if (Instance == null)// comprueba si no existe una instancia del game manager
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);// instacia para que presista el game manager
        }
        else
        {
            Destroy(gameObject);// si ya existe una instancia, destruye la nueva
        }
    }
    // Start is called before the first frame update
    void Start()

    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void sumValues(int cont)//suma el score
    {
        score += cont;
    }

    public void resetScore()//resetear el score
    {
        score = 0;
    }
}
