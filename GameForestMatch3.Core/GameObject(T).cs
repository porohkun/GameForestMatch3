using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public abstract class GameObject<T> : GameObject, IList<T> where T : GameObject
    {
        private List<T> _children = new List<T>();

        public GameObject(SpriteBatch spriteBatch) : base(spriteBatch)
        {
        }

        #region IList

        public int Count => _children.Count;

        public bool IsReadOnly => (_children as IList<T>).IsReadOnly;

        public T this[int index] { get => _children[index]; set => _children[index] = value; }

        public int IndexOf(T item)
        {
            return _children.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _children.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _children.RemoveAt(index);
        }

        public void Add(T item)
        {
            _children.Add(item);
        }

        public T1 AddComponent<T1>(T1 item) where T1 : T
        {
            _children.Add(item);
            return item;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _children.AddRange(collection);
        }

        public void Clear()
        {
            _children.Clear();
        }

        public bool Contains(T item)
        {
            return _children.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _children.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _children.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_children as IEnumerable).GetEnumerator();
        }

        #endregion

        protected internal sealed override void Update(GameTime gameTime)
        {
            if (!Enabled) return;
            OnUpdate(gameTime);
            foreach (var child in _children)
                child.Update(gameTime);
        }

        protected internal sealed override void Draw(GameTime gameTime)
        {
            if (!Enabled) return;
            OnDraw(gameTime);
            foreach (var child in _children)
            {
                child.Draw(gameTime);
            }
        }

        protected virtual void OnUpdate(GameTime gameTime) { }

        protected virtual void OnDraw(GameTime gameTime) { }

    }
}
