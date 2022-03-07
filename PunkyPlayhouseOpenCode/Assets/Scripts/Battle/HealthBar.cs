using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour { 

    public Image healthBar;

    public BattleChar myChar;

    public Text HPText;

    float HP, maxHP, lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        HP = myChar.currentHP;
        maxHP = myChar.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        HP = myChar.currentHP;

        HPText.text = HP.ToString();

        if (HP > maxHP)
        {
            HP = maxHP;
        }

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, HP / maxHP, lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (HP / maxHP));

        healthBar.color = healthColor;
    }

    public void Damage (float DP)
    {
        if (HP > 0)
        {
            HP -= DP;
        }
    }

    public void Heal (float healingPoints)
    {
        if (HP < maxHP)
        {
            HP += healingPoints;
        }
    }
}
