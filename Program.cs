using HighSchool.Core;
using HighSchool.Data;
using HighSchool.UI;
using HighSchool.UI.Services;
using System.Dynamic;
using System.Reflection;

namespace HighSchool
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var menu = new Menu();
            menu.ShowMainMenu();
        }
    }
}