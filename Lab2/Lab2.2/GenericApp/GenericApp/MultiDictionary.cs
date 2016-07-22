using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    public class MultiDictionary<K, V> : IMultiDictionary<K, V>, IEnumerable<KeyValuePair<K, IEnumerable<V>>>
            where K : struct
            where V : new()
    {
        private Dictionary<K, LinkedList<V>> Dic = new Dictionary<K, LinkedList<V>>();

        public ICollection<K> Keys => Dic.Keys;

        public ICollection<V> Values
        {
            get
            {
                ICollection<V> collect = new List<V>();
                foreach (var key in Dic.Keys)
                {
                    foreach (var val in Dic[key])
                    {
                        collect.Add(val);
                    }
                }
                return collect;
            }
        }

        public int Count => Dic.Keys.Sum(key => Dic[key].Count);

        public void Add(K key, V value)
        {
            if (!Attribute.IsDefined(typeof(K), typeof(KeyAttribute)))
            {
                throw new ArgumentException("Key must be KeyAttribute", nameof(key));
            };

            if (Dic.ContainsKey(key))
            {
                Dic[key].AddLast(value);
            }
            else
            {
                Dic[key] = new LinkedList<V>();
                Dic[key].AddLast(value);
            }
        }

        public bool Remove(K key)
        {
            var containsKey = Dic.ContainsKey(key);
            if (!containsKey) return false;
            Dic[key].Clear();
            return true;
        }

        public void CreateNewValue(K key)
        {
            var v = new V();
            Add(key, v);
        }

        public bool Remove(K key, V value)
        {
            return Dic.ContainsKey(key) && Dic[key].Remove(value);
        }

        public void Clear()
        {
            Dic.Clear();
        }

        public bool ContainsKey(K key)
        {
            return Dic.ContainsKey(key);
        }

        public bool Contains(K key, V value)
        {
            if (Dic.ContainsKey(key))
            {
                foreach (var val in Dic[key])
                {
                    if (Equals(val, value)) return true;
                }
            }
            return false;
        }

        public IEnumerator<KeyValuePair<K, IEnumerable<V>>> GetEnumerator()
        {
            var newList = new List<KeyValuePair<K, IEnumerable<V>>>();
            foreach (var item in Dic)
            {
                newList.Add(new KeyValuePair<K, IEnumerable<V>>(item.Key, item.Value));
            }
            return newList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
