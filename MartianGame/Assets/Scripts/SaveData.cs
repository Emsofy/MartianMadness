using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int playerLevel;
    public Vector3 playerPosition;
    public long lastLoginTime;
    public long lastOfflineSeconds;
    public List<string> completedDialogues;
}