using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GrilleCipher.ViewModels
{
    public class GrilleViewModel
    {
        public Models.Grille Model { get; set; }
        public ICommand DoEncrypt { get; set; }
        public ICommand DoDecrypt { get; set; }

        public GrilleViewModel()
        {
            Model = new Models.Grille();
            DoEncrypt = new DelegateCommand(Encrypt);
            Model.Pattern.ForEach(o => o.ForEach(n => n = false));
        }

        public void Encrypt()
        {
            int i = 1;
        }

        public void Decrypt()
        {

        }
    }
}
