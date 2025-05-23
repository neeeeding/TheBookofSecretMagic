using System;
using System.Collections.Generic;
using UnityEngine;

//���� ���� ��ųʸ�
[Serializable]
public class SaveDictionary<K, V> : ISerializationCallbackReceiver
{
    [SerializeField] private List<K> ks = new List<K>(); //Ű��
    [SerializeField] private List<V> vs = new List<V>(); //����

    private Dictionary<K,V> dictionary = new Dictionary<K,V>(); //���� ��ųʸ�

    public void Add(K key, V value) //�� �߰�
    {
        dictionary.Add(key, value);
        ks.Add(key);
        vs.Add(value);
    }

    public void Clear() //�����
    {
        dictionary.Clear();
        ks.Clear();
        vs.Clear();
    }


    public Dictionary<K, V> ToDictionary() //��ųʸ� ���
    {
        return dictionary;
    }

    public void FromDictionary(Dictionary<K, V> source) //�ε��� �� ���
    {
        dictionary = new Dictionary<K, V>(source);
        ks.Clear();
        vs.Clear();
        foreach (var kv in source)
        {
            ks.Add(kv.Key);
            vs.Add(kv.Value);
        }
    }



    public void OnBeforeSerialize() //����ȭ
    {
        ks.Clear();//�� �ʱ�ȭ
        vs.Clear();
        foreach (var kv in dictionary)
        {
            ks.Add(kv.Key);
            vs.Add(kv.Value);
        }
    }

    public void OnAfterDeserialize() //������ȭ
    {
        dictionary = new Dictionary<K, V>();
        for (int i = 0; i < ks.Count && i < vs.Count; i++)
        {
            dictionary[ks[i]] = vs[i];
        }
    }

    public V this[K key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }
}

//public class SaveDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
//{
//    [SerializeField] private List<SaveList<K, V>> _dictionaryList; //���� �Ǵ� ��ü

//    public SaveDictionary()
//    {
//        _dictionaryList = new List<SaveList<K, V>>();
//    }


//    public void OnAfterDeserialize() //����ȭ �� ��
//    {
//        _dictionaryList.Clear();

//        foreach(var item in this) // ��ųʸ��� ������ ����Ʈ�� ����
//        {
//            _dictionaryList.Add(new SaveList<K, V>
//            {
//                key = item.Key,
//                value= item.Value
//            });

//            Debug.Log($"save : {item.Key}/{item.Value}");
//        }
//    }

//    public void OnBeforeSerialize() //������ȭ �� ��
//    {
//        this.Clear();

//        foreach(var item in _dictionaryList)
//        {
//            if(!this.TryAdd(item.key, item.value))
//            {
//                Debug.LogError($"key : {item.key}/ this key already have it.");
//            }
//            else
//            {
//                Debug.Log($"save key : {item.key} / value : {item.value}");
//            }
//        }
//    }
//}

////���� ���� ��ųʸ��� �����ϰ� ���ִ� ����Ʈ (Ŭ����)
//[Serializable]
//public class SaveList<K,V>
//{
//    public K key;
//    public V value;
//}