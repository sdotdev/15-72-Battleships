using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoatSpawnDamageController : MonoBehaviour
{
    [SerializeField] public int maxBoatNumMin;
    [SerializeField] public int maxBoatNumMax;
    [Space]
    [SerializeField] public int maxBoatNum;
    [SerializeField] public List<GameObject> playerBoats;
    [SerializeField] private GameObject boatPref;
    [SerializeField] private TMP_Text boatCountTMP;
    [SerializeField] private Button startWarButton;
    [SerializeField] private Button placeBoatButton;
    public int boatCount = 0;
    private bool boatsPlaced = false;
    private GameObject selectedSquare;
    private GameObject prevSquare;

    void Start()
    {
        maxBoatNum = Random.Range(maxBoatNumMin, maxBoatNumMax);
        boatCountTMP.text = (boatCount.ToString() + "/" + maxBoatNum.ToString());
    }

    void Update()
    {
        
        boatCountTMP.text = (boatCount.ToString() + "/" + maxBoatNum.ToString());
    }

    public void UpdateSelected(GameObject slctd)
    {
        selectedSquare = slctd;
        if (prevSquare) {
            prevSquare.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //Debug.Log(selectedSquare.transform.childCount);

        if (!boatsPlaced)
        {
            selectedSquare.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            selectedSquare.GetComponent<SpriteRenderer>().color = new Color(0.8f,0.8f,0.8f);
        }

        prevSquare = selectedSquare;
    }

    public void PlaceBoat()
    {
        if (selectedSquare.transform.Find("TempBoat(Clone)"))
        {
            Debug.Log("Square already has a boat!");
        }
        else
        {  
            GameObject x = Instantiate(boatPref, selectedSquare.transform);
            playerBoats.Add(x);
            boatCount++;
            boatCountTMP.text = (boatCount.ToString() + "/" + maxBoatNum.ToString()); 
            if (boatCount >= maxBoatNum)
            {
                startWarButton.gameObject.SetActive(true);
                placeBoatButton.gameObject.SetActive(false);
                boatsPlaced = true;
            }
        }
    }
}
