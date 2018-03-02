using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrilleCipher.Models
{
    public class Grille
    {
        public List<List<bool>> Pattern { get; set; } = new List<List<bool>>(4)
        {
            new List<bool>(4) {false ,false, false, false },
            new List<bool>(4) {false, false, false, false },
            new List<bool>(4) {false, false, false, false },
            new List<bool>(4) {false, false, false, false }
        };
        public string InputText { get; set; } = string.Empty;
        public string OutputText { get; set; } = string.Empty;
        public string Matrix { get; set; } = string.Empty;
    }
}
