using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LerVariaveisMergeArquivo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        StringBuilder strVariaveis = new StringBuilder();
        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            txtFile.Text = openFileDialog1.FileName;
        }

        private void BtnProcessar_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtFile.Text))
            {
                strVariaveis.Clear();
                progressBar1.Visible = true;
                processarArquivo(txtFile.Text, txtDelimitador.Text);

                richTextBox1.Text = "";
                richTextBox1.Text = strVariaveis.ToString();
                progressBar1.Visible = false;
            }
            else{
                MessageBox.Show("Arquivo nao encontrado para leitura");
            }
        }

        private async void processarArquivo(string arquivo, string delimitador)
        {
            await LendoArquivo(arquivo, delimitador);


        }

        private async Task LendoArquivo(string arquivo, string delimitador)
        {
            try
            {
                string line = "";
                StreamReader reader = new StreamReader(arquivo);
                char delim = ' ';
                while ((line = reader.ReadLine()) != null)
                {
                    if (!delimitador.Trim().Equals(""))
                    {

                        delim = Convert.ToChar(delimitador);
                    }

                    string[] palavras = line.Split(delim);
                    foreach (var word in palavras)
                    {
                        if (word.Contains("<!$MG_"))
                        {
                            strVariaveis.Append(word + ";");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
