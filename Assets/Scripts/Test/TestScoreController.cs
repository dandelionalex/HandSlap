using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScoreController : MonoBehaviour
{

    public GameObject scoreUI;
    TextMeshProUGUI tmp;
    public float scoreIncreaseRate;

    int multiplier = 0;
    int maxMultiplier = 5;
    int score = 1;

    public GameObject multipliers;
    List<GameObject> multiplierIcons;

    Sequence animateMultiplier;
    Sequence animateScore;

    public AudioSource increaseMultiplierSound;

    public GameObject startLevelTextUI;

    // Start is called before the first frame update
    void Start()
    {

        GameObject levelStartText = Instantiate(startLevelTextUI);
        Destroy(levelStartText, 3f);

        DOTween.Init();

        animateMultiplier = DOTween.Sequence();
        animateMultiplier.Append(multipliers.transform.DOScale(1.3f, 0.1f));
        animateMultiplier.Append(multipliers.transform.DOScale(1, 0.1f));
        animateMultiplier.SetAutoKill(false);

        animateScore = DOTween.Sequence();
        animateScore.Append(scoreUI.transform.DOScale(2f, 0.1f));
        animateScore.Append(scoreUI.transform.DOScale(1, 0.1f));
        animateScore.SetAutoKill(false);

        //t = multipliers.GetComponent<DOTweenAnimation>().GetTweens()[0];

        tmp = scoreUI.GetComponent<TextMeshProUGUI>();
        tmp.SetText(score.ToString());
        StartCoroutine(IncreaseScore());

        multiplierIcons = new List<GameObject>();
        foreach (Transform child in multipliers.transform)
        {
            multiplierIcons.Add(child.gameObject);
        }
        IncreaseMultiplier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IncreaseScore()
    {
        while (true)
        {
            score += 1 * multiplier;
            tmp.SetText(score.ToString());
            yield return new WaitForSeconds(1 / (scoreIncreaseRate * multiplier + 1));
        }
    }

    public void IncreaseMultiplier()
    {
        if (multiplier < maxMultiplier)
        {
            UpdateMultiplierGUI();

            float p = 1f + multiplier * 0.1f;
            increaseMultiplierSound.pitch = p;
            increaseMultiplierSound.Play();

            multiplier += 1;
        }
    }

    public void DropMultiplier()
    {
        if (multiplier > 1)
        {
            multiplier = 0;
            UpdateMultiplierGUI();
            multiplier += 1;
        }
    }

    void UpdateMultiplierGUI()
    {
        foreach (GameObject m in multiplierIcons)
        {
            m.SetActive(false);
        }
        multiplierIcons[multiplier].SetActive(true);
        animateMultiplier.SetLoops(1, LoopType.Restart);
        animateMultiplier.Restart();
    }

    public void perfectFixMade()
    {
        score += 1000;
        AnimateScoreIncrease();
    }

    void AnimateScoreIncrease()
    {
        animateScore.SetLoops(1, LoopType.Restart);
        animateScore.Restart();

    }
}
