﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        card = CardDataTable.Create();
        cardIndexer = new CardIndexer(card);
    }

    public CardDataTable card;
    public CardIndexer cardIndexer;    
}
