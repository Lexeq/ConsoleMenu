using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public sealed class Menu : MenuItem
    {
        private bool Showing { get; set; }

        private MenuItem CurrentMenu { get; set; }

        public bool CyclicScrolling { get; set; }

        public bool ShowNavigationBar { get; set; }

        public int SelectedIndex { get; set; }

        public Menu()
            : this("Menu")
        { }

        public Menu(string title)
            : base(title)
        {
            CurrentMenu = this;
        }

        private void DrawMenu()
        {
            Console.ResetColor();
            Console.Clear();
            if (ShowNavigationBar)
            {
                DrawLine(GetPath(CurrentMenu));
                DrawLine(string.Empty);
            }
            for (int i = 0; i < CurrentMenu.Items.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    InvertColors();
                    DrawLine(CurrentMenu.Items[i].ToString());
                    InvertColors();
                }
                else
                {
                    DrawLine(CurrentMenu.Items[i].ToString());
                }
            }

        }

        private void DrawLine(string text)
        {
            Console.WriteLine(text);
        }

        private string GetPath(MenuItem item)
        {
            if (item == null)
                return string.Empty;

            return GetPath(item.Parent) + item.Title + ">";
        }

        private void InvertColors()
        {
            var tmp = Console.BackgroundColor;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.ForegroundColor = tmp;
        }

        public void Show()
        {
            Showing = true;

            while (Showing)
            {
                DrawMenu();
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MoveSelection(-1);
                        break;

                    case ConsoleKey.DownArrow:
                        MoveSelection(1);
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Enter:
                        var mi = CurrentMenu.Items[SelectedIndex];
                        if (mi.Items.Count == 0)
                            Hide();
                        mi.PerformAction();
                        if (mi.Items.Count > 0)
                        {
                            CurrentMenu = CurrentMenu.Items[SelectedIndex];
                            SelectedIndex = 0;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.Escape:
                        NavigateBack();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveSelection(int count)
        {
            if(CyclicScrolling)
            {
                SelectedIndex = (CurrentMenu.Items.Count + SelectedIndex + count) % CurrentMenu.Items.Count;
            }
            else
            {
                int newIdex = SelectedIndex + count;
                SelectedIndex = Math.Max(0, (Math.Min(newIdex, Items.Count - 1)));
            }
        }

        private void NavigateBack()
        {
            if (CurrentMenu.Parent == null)
                Hide();
            else
                CurrentMenu = CurrentMenu.Parent;
        }

        public void Hide()
        {
            Showing = false;
            Console.Clear();
        }
    }
}
