using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{

    public string charName;
    public int playerLevel = 1;
    public int currentEXP;

    public int[] EXPtoNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 10;
    public int str;
    public int def;
    public int wpn;
    public int armor;

    public int[] mplvlBonus;

    public string equippedWeapon;
    public string equippedArmor;

    public Sprite charImage;


    // Use this for initialization
    void Start()
    {

        EXPtoNextLevel = new int[maxLevel];
        EXPtoNextLevel[1] = baseEXP;

        for (int i = 2; i < EXPtoNextLevel.Length; i++)
        {
            EXPtoNextLevel[i] = Mathf.FloorToInt(EXPtoNextLevel[i - 1] * 1.05f);

        }

        mplvlBonus = new int[maxLevel];
        mplvlBonus[1] = maxMP;

        for (int i = 2; i < mplvlBonus.Length; i++)
        {
            mplvlBonus[i] = mplvlBonus[i - 1] + 1;

        }


    }

    // Update is called once per frame
    void Update()
    {

        

    }

    public void addEXP(int EXPtoAdd)
    {
        currentEXP += EXPtoAdd;

        if (playerLevel < maxLevel)
        {
            if (currentEXP >= EXPtoNextLevel[playerLevel])
            {
                //when reaching max go to next level and reset EXP 
                currentEXP -= EXPtoNextLevel[playerLevel];

                playerLevel++;

                //determine whether to add str or def based on odd or even

                if (playerLevel % 2 == 0)
                {
                    str++;
                }
                else
                {
                    def++;
                }

                //add new HP and MP
                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                maxMP = mplvlBonus[playerLevel];
                currentMP = maxMP;


            }
        }

        //if player level too 

        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }

    }


}

