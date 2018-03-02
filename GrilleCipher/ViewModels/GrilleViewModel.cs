using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GrilleCipher.ViewModels
{
    public class GrilleViewModel : INotifyPropertyChanged
    {
        public Models.Grille Model { get; set; }

        public ICommand DoEncrypt { get; set; }
        public ICommand DoDecrypt { get; set; }

        public string TextMatrix
        {
            get
            {
                return Model.Matrix;
            }
            set
            {
                Model.Matrix = value;
                NotifyPropertyChanged(nameof(TextMatrix));
            }
        }

        public string OutputText
        {
            get
            {
                return Model.OutputText;
            }
            set
            {
                Model.OutputText = value;
                NotifyPropertyChanged(nameof(OutputText));
            }
        }

        public GrilleViewModel()
        {
            Model = new Models.Grille();
            DoEncrypt = new DelegateCommand(Encrypt);
            DoDecrypt = new DelegateCommand(Decrypt);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Encrypt()
        {
            try
            {
                var Matrix = new char[4, 4];
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        Matrix[i, j] = '-';
                    }
                }

                var Pattern = Model.Pattern;
                var n = 0;
                for (var k = 0; k < 4; k++)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        for (var j = 0; j < 4; j++)
                        {
                            if (Pattern[i][j] && n < Model.InputText.Length)
                            {
                                Matrix[i, j] = Model.InputText[n];
                                n++;
                            }
                        }
                    }
                    Pattern = Rotate(Pattern);
                }

                var result = string.Empty;
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j<4; j++)
                    {
                        result += $"{Matrix[i, j]} ";
                    }
                    result += '\n';
                }

                TextMatrix = result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }

        public void Decrypt()
        {
            try
            {
                var Matrix = new char[4, 4];
                var MatrixRows = TextMatrix.Replace(" ", "").Split('\n');
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        Matrix[i, j] = MatrixRows[i][j];
                    }
                }

                var Pattern = Model.Pattern;
                var solution = string.Empty;
                var n = 0;
                for (var k = 0; k < 4; k++)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        for (var j = 0; j < 4; j++)
                        {
                            if (Pattern[i][j] && Matrix[i, j] != '-')
                            {
                                solution += Matrix[i, j].ToString();
                            }
                        }
                    }
                    Pattern = Rotate(Pattern);
                }

                OutputText = solution;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }

        private List<List<bool>> Rotate(List<List<bool>> Matrix)
        {
            List<List<bool>> RotatedMatrix = new List<List<bool>>(4)
            {
                new List<bool>(4) {false ,false, false, false },
                new List<bool>(4) {false, false, false, false },
                new List<bool>(4) {false, false, false, false },
                new List<bool>(4) {false, false, false, false }
            };

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    RotatedMatrix[j][3 - i] = Matrix[i][j];
                }
            }

            return RotatedMatrix;
        }
    }
}
