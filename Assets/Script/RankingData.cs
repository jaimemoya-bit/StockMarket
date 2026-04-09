using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  
public class RankingData 
{
    public int rank;
    public string userName;

    public float score;
}
[Serializable]
public class RankingResponse
{
    public List<RankingData> rankingList;
   
   
    // TEST — devuelve datos falsos sin necesitar servidor
    public List<RankingData> GetTop10Falso()
    {
        return new List<RankingData>
        {
            new RankingData { rank = 1, userName = "Carlos", score = 4200 },
            new RankingData { rank = 2, userName = "Maria", score = 3800 },
            new RankingData { rank = 3, userName = "Juanma", score = 3100 },
            new RankingData { rank = 4, userName = "Ana", score = 2900 },
            new RankingData { rank = 5, userName = "Pedro", score = 2400 },
            new RankingData { rank = 6, userName = "Laura", score = 2100 },
            new RankingData { rank = 7, userName = "Sergio", score = 1800 },
            new RankingData { rank = 8, userName = "Sofia", score = 1500 },
            new RankingData { rank = 9, userName = "Miguel", score = 1200 },
            new RankingData { rank = 10, userName = "Elena", score = 900  }
        };
    }}