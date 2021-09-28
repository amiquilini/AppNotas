using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AppNotas
{
    public partial class AppNotas : Form
    {
        private List<double> notas = new List<double>();
        private List<string> notasLidas = new List<string>();
        public AppNotas()
        {
            InitializeComponent();
        }
        private void btnSalvarNotas_Click(object sender, EventArgs e)
        {
            if (ValidarNotas())
            {
                using (Stream saida = File.Open("notas.txt", FileMode.Create))
                {
                    using (StreamWriter escritor = new StreamWriter(saida))
                    {
                        escritor.Write(txtNota1.Text + "\r\n");
                        escritor.Write(txtNota2.Text + "\r\n");
                        escritor.Write(txtNota3.Text + "\r\n");
                        escritor.Write(txtNota4.Text);
                    }
                }
                MessageBox.Show("Texto gravado!");
            }
        }

        private void btnLerNotas_Click(object sender, EventArgs e)
        {
            notasLidas.Clear();

            if (File.Exists("notas.txt"))
            {
                using (Stream entrada = File.Open("notas.txt", FileMode.Open))
                {
                    using (StreamReader leitor = new StreamReader(entrada))
                    {
                        while (!leitor.EndOfStream)
                        {
                            notasLidas.Add(leitor.ReadLine());
                        }
                    }
                }
                txtNota1.Text = notasLidas[0];
                txtNota2.Text = notasLidas[1];
                txtNota3.Text = notasLidas[2];
                txtNota4.Text = notasLidas[3];
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado.");
            }
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            notas.Clear();

            if (ValidarNotas())
            {
                notas.Add(Convert.ToDouble(txtNota1.Text));
                notas.Add(Convert.ToDouble(txtNota2.Text));
                notas.Add(Convert.ToDouble(txtNota3.Text));
                notas.Add(Convert.ToDouble(txtNota4.Text));

                txtMaiorNota.Text = RetornaMaiorNota(notas).ToString();
                txtMenorNota.Text = RetornaMenorNota(notas).ToString();
                txtMedia.Text = RetornaMediadeNotas(notas).ToString();
                txtSomaPares.Text = RetornaSomaNotasPares(notas).ToString();
                txtSomaImpares.Text = RetornaSomaNotasImpares(notas).ToString();
                txtQntMaiorQueSete.Text = RetornaNotasMaiorIgualASete(notas).ToString();
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNota1.Text = "";
            txtNota2.Text = "";
            txtNota3.Text = "";
            txtNota4.Text = "";

            txtMaiorNota.Text = "";
            txtMenorNota.Text = "";
            txtMedia.Text = "";
            txtSomaPares.Text = "";
            txtSomaImpares.Text = "";
            txtQntMaiorQueSete.Text = "";

            notas.Clear();
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Métodos criados
        private double RetornaMaiorNota(List<double> notas)
        {
            double maior = notas.Max();
            return maior;
        }

        private double RetornaMenorNota(List<double> notas)
        {
            double menor = notas.Min();
            return menor;
        }

        private double RetornaMediadeNotas(List<double> notas)
        {
            double media = notas.Average();
            return media;
        }

        private double RetornaSomaNotasPares(List<double> notas)
        {
            double somaPares = notas.Where(n => n % 2 == 0).Sum();
            return somaPares;
        }
        private double RetornaSomaNotasImpares(List<double> notas)
        {
            double somaImpares = notas.Where(n => n % 2 != 0).Sum();
            return somaImpares;
        }
        private int RetornaNotasMaiorIgualASete(List<double> notas)
        {
            int maiorIgualSete = notas.Where(n => n >= 7).Count();
            return maiorIgualSete;
        }

        private bool ValidarNotas()
        {
            bool retornoValidacao = true;
            string mensagem = "";

            if (!double.TryParse(txtNota1.Text, out double valor1) || txtNota1.Text == "" || Convert.ToDouble(txtNota1.Text) < 0)
            {
                mensagem += "Preencha o campo Nota 1 corretamente.\n";
                retornoValidacao = false;
            }
            if (!double.TryParse(txtNota2.Text, out double valor2) || txtNota2.Text == "" || Convert.ToDouble(txtNota2.Text) < 0)
            {
                mensagem += "Preencha o campo Nota 2 corretamente.\n";
                retornoValidacao = false;
            }
            if (!double.TryParse(txtNota3.Text, out double valor3) || txtNota3.Text == "" || Convert.ToDouble(txtNota3.Text) < 0)
            {
                mensagem += "Preencha o campo Nota 3 corretamente.\n";
                retornoValidacao = false;
            }
            if (!double.TryParse(txtNota4.Text, out double valor4) || txtNota4.Text == "" || Convert.ToDouble(txtNota4.Text) < 0)
            {
                mensagem += "Preencha o campo Nota 4 corretamente.";
                retornoValidacao = false;
            }

            if (retornoValidacao == false)
            {
                MessageBox.Show(mensagem);
            }

            return retornoValidacao;
        }

        private void AppNotas_Load(object sender, EventArgs e)
        {

        }
    }
}
