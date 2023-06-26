using UnityEngine;

public class SlapController : MonoBehaviour
{

    public GameObject hand;
    public float rotationRangeFrom = -40;
    public float rotationRangeTo = 40;

    public GameObject textHolder;
    public GameObject fixedText;
    public GameObject perfectFixtext;


    public GameObject waterEffect;
    public GameObject effectHolder;


    public float hitProbability = 1; // Ýòî òåñòîâàÿ ïåðåìåííàÿ, êîòîðàÿ îïðåäåëÿåò âåðîÿòíîñòü ïîïàäàíèÿ. Åñëè 1 - òî êàæäûé øëåïîê áóäåò êàê áû óäà÷íûì, è ìîæíî ïîâûøàòü ìíîæèòåëü. Åñëè îíà 0.5 - òî 50% ÷òî øëåïîê áóäåò êàê-áû ìèìî, è ìîæíî ïðîâåðèòü êàê ñáðàñûâàåòñÿ ìíîæèòåëü
    public float perfectHitProbabilty = 0.2f; // ýòî òîæå òåñòîâàÿ øòóêà. Ýòî âåðîÿòíîñòü òîãî, ÷òî åñëè ïîïàë (ïðåäûäóùàÿ ïðîâåðêà ïðîêíóëà), òî óäàð çàñ÷èòàåòñÿ êàê èäåàëüíûé. Â íîðìàëüíîé ðåàëèçàöèè íóæíî ñìîòðåòü íà óäàëåíèå öåëè îò öåíòðà íàêëåéêè, òèïà íàñêîëüêî òî÷íî çàêëåèë.

    TestScoreController scoreController;

    public AudioSource slapSound;

    // Start is called before the first frame update
    void Start()
    {
        scoreController = GetComponent<TestScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || Input.touches.Length > 0)
        {
            float i = Random.Range(rotationRangeFrom, rotationRangeTo);
            Instantiate(hand, transform.position, Quaternion.Euler(new Vector3(i, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)), transform); // ïîñëå íàæàòèÿ ñîçäàåì ðóêó ñ àíèìàöèåé, è ïîâîðà÷èâàåì åå íà ðàíäîìíûé ãðàäóñ, ÷òîá ÷óòü ðàçíûå ïîëîæåíèÿ áûëè ïðè óäàðå âíåøíå.
            Invoke("SlapReceived", 0.5f);
        }
    }

    public void SlapReceived() // çàïóñêàåòñÿ âî âðåìÿ øëåïêà, êîãäà ëàäîøêà êàñàåòñÿ ïîâåðõíîñòè, òðèããåð èç àíèìàöèè. Òóò îïðåäåëèì, êàêîé ïîëó÷èëñÿ óäàð - íàñêîëüêî óäà÷íûé, è èñõîäÿ èç ýòîãî âûçîâåì íóæíóþ ôóíêöèþ
    {
        float i = Random.value;
        if (i < hitProbability)
        {
            if (i < perfectHitProbabilty)
            {
                PerfectSlapReceived();
            } else
            {
                SuccesfullSlapReceived();
            }
        } else
        {
            MissedSlapReceived();
        }
    }

    public void SuccesfullSlapReceived()
    {
        print("Slap received");
        Instantiate(fixedText, textHolder.transform);

        GameObject newWaterEffect = Instantiate(waterEffect, effectHolder.transform);
        Destroy(newWaterEffect, 1);
        scoreController.IncreaseMultiplier();
    }



    public void PerfectSlapReceived()
    {
        Instantiate(perfectFixtext, textHolder.transform);
        GameObject newWaterEffect = Instantiate(waterEffect, effectHolder.transform);
        Destroy(newWaterEffect, 1);
        scoreController.IncreaseMultiplier();
        scoreController.perfectFixMade();
    }


    public void MissedSlapReceived()
    {
        scoreController.DropMultiplier();
        slapSound.Play();
    }
}
