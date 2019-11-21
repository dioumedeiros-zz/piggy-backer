using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Transform refEnimy;
    public GameObject enimy1;
    public GameObject enimy2;

    public float timeRespawn;

    private float lastResp;
    private int bornCount;
    private int limitOrda;
    // Start is called before the first frame update
    void Start()
    {
        lastResp = 0;
        bornCount = 0;
        StartCoroutine(this.respawEnimy());
    }

    // Update is called once per frame
    void Update()
    {
        // score = PlayerPrefs.GetInt("deads");
        // if ((Time.time - lastResp >= timeRespawn))
        // {
        //     lastResp = Time.time;
        //     respawEnimy();
        // }
    }

    IEnumerator respawEnimy()
    {
        limitOrda = PlayerPrefs.GetInt("limitOrda");
        
        while (bornCount < limitOrda)
        {
            bornCount++;
            float born = Random.Range(1, 7);
            float speed = Random.Range(2, 7);
            GameObject enimySelected = enimy1;
            if (born >= 3 && born <= 5)
            {
                enimySelected = enimy2;
            }
            else if (born > 5)
            {
                enimySelected = enimy1;
            }
            GameObject newEnimy = Instantiate(enimySelected);
            newEnimy.GetComponent<EnimyController>().speed = speed;
            newEnimy.transform.position = refEnimy.position;
            yield return new WaitForSeconds(timeRespawn);
        }

    }

}
