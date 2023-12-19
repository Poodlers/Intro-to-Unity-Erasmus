using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update

    public int score = 0;
    private TextMeshProUGUI textMeshPro;
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int score)
    {
        this.score += score;
        textMeshPro.text = "Score: " + this.score.ToString();
        SpecialEffects.specialEffects.CoinVFX();

    }
}
