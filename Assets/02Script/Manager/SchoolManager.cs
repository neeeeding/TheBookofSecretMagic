
public class SchoolManager : Singleton<SchoolManager>
{
    public SaveDictionary<int,PlayerJob> todayClass; //���� ����
    public bool setTodayClass; // true : ���� ���� ���� ��/ false : ������ ���� ���� �� ��.

    private void Awake()
    {
        GameManager.OnNextDay += ClearToday;
    }

    public void ClearToday() //�б� ���� �� ����
    {
        todayClass.Clear();
    }

    public void SettingTodayClass() //���� ���� �� ���� �˷��ֱ�.
    {
        setTodayClass = true;
        //���� �� UI ��� ����.
    }
}
