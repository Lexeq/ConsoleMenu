using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class MenuItem : IEnumerable<MenuItem>
    {
        private Action Action { get; set; }

        public List<MenuItem> Items { get; private set; }

        public MenuItem Parent { get; protected set; }

        public string Title { get; private set; }

        public MenuItem this[string title]
        {
            get
            {
                var item = Items.Find(m => m.Title == title);
                return item ?? throw new ArgumentException($"Menu doesn't contain item '{title}'", nameof(title));
            }
        }

        public MenuItem this[int index]
        {
            get
            {
                return Items[index];
            }
        }

        public MenuItem(string title, Action action)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Action = action;
            Items = new List<MenuItem>();
        }

        public MenuItem(string title)
            : this(title, null)
        {

        }

        public void Add(MenuItem item)
        {
            item.Parent = this;
            Items.Add(item);
        }

        public void PerformAction()
        {
            Action?.Invoke();
        }

        public IEnumerator<MenuItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Items.Count > 0 ? Title + " >" : Title;
        }
    }
}
