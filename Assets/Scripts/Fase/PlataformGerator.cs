using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformGerator : MonoBehaviour
{

    public GameObject thePlatform;

    public static PlataformGerator plataformGerator;
    public Transform generationPoint;
    public ObjectPooler[] theObjectPools;
    public float distanceBtween;
    


    private int NumKeysWin;
    private int platformSelector;
    public float[] platformWidths;

    private KeysGenerator theKeysGenerator;
    private EnemyGenerator theEnemyGenerator;

    //public GameObject[] thePlatforms;


    // Start is called before the first frame update
    public void Start()
    {
        NumKeysWin = LevelManager.levelManager.NumKeysWin;
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        theKeysGenerator = FindObjectOfType<KeysGenerator>();
        theEnemyGenerator = FindObjectOfType<EnemyGenerator>();

    }

    // Update is called once per frame
    public void Update()
    {
            if (transform.position.x < generationPoint.position.x)
            {   
            if (LevelManager.levelManager.keysAtual < NumKeysWin)
            {
                platformSelector = Random.Range(0, theObjectPools.Length - 1);
            }else 
            {
                platformSelector = (theObjectPools.Length-1);
            }
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 3) + distanceBtween, transform.position.y);

                GameObject newPlataform = theObjectPools[platformSelector].GetPooledObject();

                newPlataform.transform.position = transform.position;
                newPlataform.transform.rotation = transform.rotation;
                newPlataform.SetActive(true);
            if (Random.Range(0, 2) == 1)
            {
                theKeysGenerator.SpawnKeys(new Vector3(transform.position.x, transform.position.y + Random.Range(1, 5)));
            }
            if (platformSelector != 4 && Random.Range(0, 5) == 1)
            {
                theEnemyGenerator.SpawnEnemy(new Vector3(transform.position.x, transform.position.y + 1F));
            }
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBtween, transform.position.y);

            }
        }

}

