using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class EnemyBrain : MonoBehaviour
{
    int playerBoatsNum;
    public LevelLoader ll;
    bool started = false;
    public bool turnToGetHit = false;
    bool onHitSpace = false;
    public TMP_Text winnerText;
    [SerializeField] bool gotHit = false;
    [SerializeField] bool checkmrk = false;
    //GameObject checkmark;
    //GameObject cross;
    //GameObject cross;
    GameObject newSelected;
    GameObject prevSelected;
    [Space]
    [SerializeField] private List<string> enemyBoats;
    private List<GameObject> alrHitSpaces;
    public int enemyBoatAmount;
    public Button startWarButton;
    public Button playerFireButton;
    public GameObject ExplosionParticle;
    public GameObject hitBoatTick;
    public GameObject hitBoatCross;
    private BoatSpawnDamageController bsdc;
    [SerializeField] private List<GameObject> playerBoats;
    private int maxBoatNum;
    private int number;
    private string newSquare;
    private string square;
    private int y;
    public List<GameObject> physTiles;
    [SerializeField] private List<string> tileNames;
    [SerializeField] private GameObject playerTilesParent;
    private List<string> openPlayerSpaces;
    private string chosenPlayerSpace;
    private GameObject physChosenPlayerSpace;
    [Space]
    [SerializeField] TMP_Text enemyBoatFraction;
    [Space]
    [SerializeField] private List<GameObject> enemyEmotions;
    private List<GameObject> playerHitBoats;
    int defaultEmotion = 0;
    [SerializeField] private GameObject placeholderGameOBJ;

    void Start()
    {
        bsdc = FindObjectOfType<BoatSpawnDamageController>();
        Time.timeScale = 1f;

        for (int i = 0; i < physTiles.Count; i++)
        {
            tileNames.Add(physTiles[i].name);
        }

        openPlayerSpaces = tileNames;
        gotHit = false;

        alrHitSpaces = new List<GameObject>();
        alrHitSpaces.Add(placeholderGameOBJ);
        playerHitBoats = new List<GameObject>();
        playerHitBoats.Add(placeholderGameOBJ);
    }

    void Update()
    {
        playerBoats = bsdc.playerBoats;
        enemyBoatAmount = enemyBoats.Count;
        enemyBoatFraction.text = (enemyBoatAmount + "/" + maxBoatNum.ToString());

        /*if (started)
        {
            if (enemyBoatAmount <= 0)
            {
                StartCoroutine(SomeoneWon(true));
            }

            if (playerBoats.Count <= 0)
            {
                StartCoroutine(SomeoneWon(false));
            }
        }*/
    }

    public void StartWar()
    {
        started = true;
        startWarButton.gameObject.SetActive(false);
        maxBoatNum = bsdc.maxBoatNum;

        for (int i = 0; i < maxBoatNum; i++)
        {
            square = tileNames[Random.Range(0, 57)];
            //enemyBoats.Add(square);

            for (int c = 0; c < enemyBoats.Count; c++)
            {
                if (square == enemyBoats[c])
                {
                    square = tileNames[57 + i];
                }
            }
            enemyBoats.Add(square);

        }

        for (int i = 0; i < enemyBoats.Count; i++)
        {
            Debug.Log(enemyBoats[i]);
        }

        StartCoroutine(ThinkPlaceBoats());

    } //error

     IEnumerator ThinkPlaceBoats()
     {
        for (int i = 0; i < enemyEmotions.Count; i++)
        {
            enemyEmotions[i].SetActive(false);
        }

        enemyEmotions[1].SetActive(true);

        yield return new WaitForSeconds(Random.Range(3f, 4.5f));

        enemyEmotions[1].SetActive(false);
        playerFireButton.gameObject.SetActive(true);
        enemyEmotions[defaultEmotion].SetActive(true);
        enemyBoatFraction.text = (enemyBoatAmount + "/" + maxBoatNum.ToString());
        turnToGetHit = true;
    }

    public void UpdateSelected(GameObject slctd)
    {
        newSelected = slctd;

        if (prevSelected)
        {
            prevSelected.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (turnToGetHit)
        {
            newSelected.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            newSelected.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
        }

        prevSelected = newSelected;
    }

    public void PlayerFired()
    {

        if (started && playerBoats.Count <= 0)
        {
            StartCoroutine(SomeoneWon(false));
        }

        if (started && enemyBoatAmount <= 0)
        {
            StartCoroutine(SomeoneWon(true));
        }

        playerFireButton.gameObject.SetActive(false);
        Instantiate(ExplosionParticle, newSelected.transform.position, Quaternion.identity);

        for (int i = 0; i < enemyBoatAmount; i++)
        {
            if (newSelected.name == enemyBoats[i])
            {
                /////Instantiate(ExplosionParticle, newSelected.transform.position, Quaternion.identity);
                Debug.Log("Target Hit");
                enemyBoats.RemoveAt(i);
                gotHit = true;
                break;
            }

            /*if(newSelected.name == enemyBoats[i])
            {
                StartCoroutine(DelayTickCross(false));
                break;
            }*/
        }
        if (started && enemyBoatAmount <= 0)
        {      
            StartCoroutine(SomeoneWon(true));     
        }

        if (started && playerBoats.Count <= 0)
        {
            StartCoroutine(SomeoneWon(false));
        }

        StartCoroutine(DelayTickCross());

        turnToGetHit = false;
        newSelected.GetComponent<SpriteRenderer>().color = new Color(0.45f, 0.45f, 0.45f);
    }

    IEnumerator DelayTickCross()
    {
        //Debug.Log("start");
        yield return new WaitForSeconds(1.1f);
        // Debug.Log("end");

        if (gotHit == true)
        {
            GameObject checkmark = Instantiate(hitBoatTick, newSelected.transform.position, Quaternion.identity);
            checkmark.transform.parent = newSelected.transform;
            checkmark.SetActive(true);
            checkmark.GetComponent<Animator>().SetTrigger("FadeIn");
        }
        
        if(gotHit == false)
        {
            GameObject cross = Instantiate(hitBoatCross, newSelected.transform.position, Quaternion.identity);
            cross.transform.parent = newSelected.transform;
            cross.SetActive(true);
            cross.GetComponent<Animator>().SetTrigger("FadeIn");
        }

        for (int i = 0; i < enemyEmotions.Count; i++)
        {
            enemyEmotions[i].SetActive(false);
        }
        enemyEmotions[1].SetActive(true);
        yield return new WaitForSeconds(Random.Range(0.7f,3.5f));
        gotHit = false;
        enemyEmotions[1].SetActive(false);
        enemyEmotions[defaultEmotion].SetActive(true);
        HitPlayer();
    }

    void HitPlayer() {

        if (started && playerBoats.Count <= 0)
        {
            StartCoroutine(SomeoneWon(false));
        }

        if (started && enemyBoatAmount <= 0)
        {
            StartCoroutine(SomeoneWon(true));
        }

        checkmrk = false;
        chosenPlayerSpace = openPlayerSpaces[Random.Range(0,openPlayerSpaces.Count)];
        physChosenPlayerSpace = playerTilesParent.transform.Find(chosenPlayerSpace).gameObject;
        Instantiate(ExplosionParticle, physChosenPlayerSpace.transform.position, Quaternion.identity);

        for (int i = 0; i < playerBoats.Count; i++)
        {
            //for (int ji = 0; ji < playerHitBoats.Count; ji++){
            


            if (physChosenPlayerSpace.name == playerBoats[i].transform.parent.name)
            {
                    
                checkmrk = true;
                playerBoatsNum = i;
                physChosenPlayerSpace.CompareTag("HitByEnemy");
                playerHitBoats.Add(physChosenPlayerSpace);
                break;
                   
            }
            else
            {
               
            }
            
        }

        /*for (int j = 0; j < alrHitSpaces.Count; j++)
        {
            if (alrHitSpaces[j] == physChosenPlayerSpace)
            {
                checkmrk = false;
                break;
            }
            else
            {
                checkmrk = true;
                bsdc.playerBoats.RemoveAt(playerBoatsNum);
                alrHitSpaces.Add(physChosenPlayerSpace);
                //break;
            }
        }*/

        Debug.Log("Hit player is " + checkmrk.ToString());

        if (started && playerBoats.Count <= 0)
        {
            StartCoroutine(SomeoneWon(false));
        }

        if (started && enemyBoatAmount <= 0)
        {
            StartCoroutine(SomeoneWon(true));
        }

        StartCoroutine(PlayerSideCrossOrCheckMark(physChosenPlayerSpace));

    }

    IEnumerator PlayerSideCrossOrCheckMark(GameObject prefabLocation)
    {
        yield return new WaitForSeconds(1.1f);

        if(checkmrk == true)
        {
            if (prefabLocation.transform.Find("HitBoatTick(Clone)")) { }
            else
            {
                bsdc.boatCount -= 1;
            }
            GameObject checkmark = Instantiate(hitBoatTick, prefabLocation.transform.position, Quaternion.identity);
            checkmark.transform.parent = prefabLocation.transform;
            checkmark.SetActive(true);
            checkmark.GetComponent<Animator>().SetTrigger("FadeIn");          
        } 
        else if(checkmrk == false)
        {
            GameObject cross = Instantiate(hitBoatCross, prefabLocation.transform.position, Quaternion.identity);
            cross.transform.parent = prefabLocation.transform;
            cross.SetActive(true);
            cross.GetComponent<Animator>().SetTrigger("FadeIn");
        }

        yield return new WaitForSeconds(0.75f);
        checkmrk = false;
        turnToGetHit = true;
        playerFireButton.gameObject.SetActive(true);
    }

    IEnumerator SomeoneWon(bool playerWon)
    {
        yield return new WaitForSeconds(0.7f);

        if (playerWon)
        {
            Debug.Log("Player Won");
            //UtilsClass.CreateWorldTextPopup("Player Won",new Vector3(0,0,0),0.5f);
            winnerText.text = "Winner: Player";
                 
        }

        if (!playerWon)
        {
            Debug.Log("Enemy Won");
            //UtilsClass.CreateWorldTextPopup("Enemy Won", new Vector3(0, 0, 0), 0.5f);
            winnerText.text = "Winner: Enemy";
            
        }

        yield return new WaitForSeconds(0.3f);
        ll.switchScene("MainMenu");
    }
}
