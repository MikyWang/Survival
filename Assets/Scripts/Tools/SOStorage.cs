using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOData<TId, TSO>
{
    public TId id;
    public TSO so;
}

[System.Serializable]
public class SOStorage<TId, TSO> : IDictionary<TId, TSO>
{
    [SerializeField] List<SOData<TId, TSO>> dataList;
    Dictionary<TId, TSO> _dictionary;
    public TSO this[TId key] { get => ((IDictionary<TId, TSO>)dictionary)[key]; set => ((IDictionary<TId, TSO>)dictionary)[key] = value; }
    public ICollection<TId> Keys => ((IDictionary<TId, TSO>)dictionary).Keys;
    public ICollection<TSO> Values => ((IDictionary<TId, TSO>)dictionary).Values;
    public int Count => ((ICollection<KeyValuePair<TId, TSO>>)dictionary).Count;
    public bool IsReadOnly => ((ICollection<KeyValuePair<TId, TSO>>)dictionary).IsReadOnly;
    Dictionary<TId, TSO> dictionary
    {
        get
        {
            if (_dictionary == null)
            {
                _dictionary = InitDictionary();
            }
            return _dictionary;
        }
    }

    public void Add(TId key, TSO value)
    {
        ((IDictionary<TId, TSO>)dictionary).Add(key, value);
    }

    public void Add(KeyValuePair<TId, TSO> item)
    {
        ((ICollection<KeyValuePair<TId, TSO>>)dictionary).Add(item);
    }

    public void Clear()
    {
        ((ICollection<KeyValuePair<TId, TSO>>)dictionary).Clear();
    }

    public bool Contains(KeyValuePair<TId, TSO> item)
    {
        return ((ICollection<KeyValuePair<TId, TSO>>)dictionary).Contains(item);
    }

    public bool ContainsKey(TId key)
    {
        return ((IDictionary<TId, TSO>)dictionary).ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TId, TSO>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<TId, TSO>>)dictionary).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TId, TSO>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<TId, TSO>>)dictionary).GetEnumerator();
    }

    public bool Remove(TId key)
    {
        return ((IDictionary<TId, TSO>)dictionary).Remove(key);
    }

    public bool Remove(KeyValuePair<TId, TSO> item)
    {
        return ((ICollection<KeyValuePair<TId, TSO>>)dictionary).Remove(item);
    }

    public bool TryGetValue(TId key, out TSO value)
    {
        return ((IDictionary<TId, TSO>)dictionary).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)dictionary).GetEnumerator();
    }

    Dictionary<TId, TSO> InitDictionary()
    {
        var dictionary = new Dictionary<TId, TSO>();
        foreach (var data in dataList)
        {
            if (!dictionary.ContainsKey(data.id))
            {
                dictionary.Add(data.id, data.so);
            }
        }
        return dictionary;
    }
}
