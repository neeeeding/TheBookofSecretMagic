[System.Serializable]
public class GameSaveData 
{
    public Sound sound = new Sound();
}

[System.Serializable]
public class Sound //¼Ò¸® (AudioSave)
{
    public float mainSound;
    public float bgmSound;
    public float effectSound;
}
