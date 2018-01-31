using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WMIGatherer
{
    public class WmiClassCollection : ICollection<WmiClass>
    {
        private List<WmiClass> _classes;


        public int Count
        {
            get => _classes.Count();
        }

        public bool IsReadOnly
        {
            get => false;
        }


        public WmiClass this[int classIndex]
        {
            get => _classes[classIndex];
        }



        public WmiClassCollection()
        {
            this._classes = new List<WmiClass>();
        }

        public WmiClassCollection(ICollection<WmiClass> collection)
        {
            this._classes = collection.ToList();
        }



        public void Add(WmiClass item)
        {
            _classes.Add(item);
        }


        public void Clear()
        {
            _classes.Clear();
        }


        public bool Contains(WmiClass item)
        {
            return _classes.Contains(item);
        }


        public void CopyTo(WmiClass[] array, int arrayIndex)
        {
            _classes.CopyTo(array, arrayIndex);
        }


        public IEnumerator<WmiClass> GetEnumerator()
        {
            return _classes.GetEnumerator();
        }


        public bool Remove(WmiClass item)
        {
            return _classes.Remove(item);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _classes.GetEnumerator();
        }
    }
}
