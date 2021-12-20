using System;
using System.Collections;
using System.Collections.Generic;

namespace NinthTask
{
    public class DynamicList<T> : IEnumerable<T>
    {

        private T[] _array;

        private int _size = 0;
        private const int Capacity = 10;

        public int Count
        {
            get { return _size; }
        }

        public DynamicList()
        {
            _array = new T[Capacity];
        }

        public void Add(T element)
        {
            Resize();
            _array[_size++] = element;
        }

        public void Remove(T element)
        {
            RemoveAt(Array.IndexOf(_array, element));
        }

        public void RemoveAt(int index)
        {
            var newContainer = new T[_array.Length - 1];
            Array.Copy(_array, newContainer, index);
            Array.Copy(_array, index + 1, newContainer, index, _array.Length - index - 1);
            _array = newContainer;
            _size--;
        }

        public void Clear()
        {
            _array = Array.Empty<T>();
            _size = 0;
        }

        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        private void Resize()
        {
            if (_size != _array.Length)
            {
                return;
            }
            var newContainer = new T[_array.Length + Capacity];
            Array.Copy(_array, newContainer, _array.Length);
            _array = newContainer;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _size; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}