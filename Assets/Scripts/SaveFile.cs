using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveFile{

    public string LastLevelPlayed = "";
    public int TotalPlaytime = 0;
    public bool CaesarCompleted = false;
    public bool PlayfairCompleted = false;
    public bool ADFGXCompleted = false;
    public int CaesarCompletionTime = 0;
    public int PlayfairCompletionTime = 0;
    public int ADFGXCompletionTime = 0;
}
