using System;

[Serializable]
public class GameSaveData 
{
    public Sound sound = new Sound();
    public PlayerStatSC stat = new PlayerStatSC();
}

[Serializable]
public class Sound //�Ҹ� (AudioSave)
{
    public float mainSound;
    public float bgmSound;
    public float effectSound;
}
