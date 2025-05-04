using System;
using System.Numerics;
using UnityEditor.SceneManagement;

[Serializable]
public class GameSaveData 
{
    public Sound sound = new Sound();
    public PlayerStatSC stat = new PlayerStatSC();
}

[Serializable]
public class Sound //¼Ò¸® (AudioSave)
{
    public float mainSound;
    public float bgmSound;
    public float effectSound;
}
