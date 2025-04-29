[System.Serializable]
public class GameSaveData 
{
    public Sound sound = new Sound();
}

[System.Serializable]
public class Sound //�Ҹ� (AudioSave)
{
    public float mainSound;
    public float bgmSound;
    public float effectSound;
}
