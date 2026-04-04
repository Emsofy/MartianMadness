using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TreeSaveData
{
    public string id;
    public Vector3 position;
    public long endTimeTicks;
}
[Serializable]
public class SaveData
{
    public int playerLevel;
    public Vector3 playerPosition;
    public long lastLoginTime;
    public long lastOfflineSeconds;
    public List<string> completedDialogues;
    public List<TreeSaveData> trees;
}