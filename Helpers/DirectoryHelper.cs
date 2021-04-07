using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Helpers
{
    public static class DirectoryHelper
    {
        public static readonly string AssetsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
    }
}
