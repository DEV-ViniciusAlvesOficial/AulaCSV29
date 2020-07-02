using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula_27_28_29_30 {

    public class Produto {

     

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public float Preco { get; set; }

        private const string PATH = "Database/produto.csv";

       

        public Produto () {

            string DataBase = PATH.Split ('/') [0];
            if (!Directory.Exists (DataBase)) {
                Directory.CreateDirectory (DataBase);
            }

    
            if (!File.Exists (PATH)) {
        
                File.Create (PATH).Close ();
            }
        }

      

        public void Cadastrar (Produto prod) {
            var linha = new string[] { PrepararLinha (prod) };
            File.AppendAllLines (PATH, linha);
        }

        public List<Produto> Ler () {

           

            List<Produto> produtos = new List<Produto> ();

         
            string[] linhas = File.ReadAllLines (PATH);

            foreach (var linha in linhas) {

            

                string[] dado = linha.Split (";");

                Produto p = new Produto ();
                p.Codigo = Int32.Parse (Separar (dado[0]));
                p.Nome = Separar (dado[1]);
                p.Preco = float.Parse (Separar (dado[2]));

             

                produtos.Add (p);

            }
    
            produtos = produtos.OrderBy (y => y.Nome).ToList ();
            return produtos;
        }

     
        public void Remover (string _termo) {
           
            List<string> linhas = new List<string> ();

            using (StreamReader arquivo = new StreamReader (PATH)) {

                string linha;
                while ((linha = arquivo.ReadLine ()) != null) {

                    linhas.Add (linha);
                }
            }

            linhas.RemoveAll (l => l.Contains (_termo));

         
            using (StreamWriter output = new StreamWriter (PATH)) {

                foreach (string ln in linhas) {
                    output.Write (ln + "\n");
                }
            }

        }

        public List<Produto> Filtrar (string _nome) {
            return Ler ().FindAll (x => x.Nome == _nome);

        }

        private string Separar (string _coluna) {
            return _coluna.Split ("=") [1];
        }

    
        public string PrepararLinha (Produto p) {
            return $"Código = {p.Codigo}; Nome = {p.Nome}; Preço = {p.Preco}";
        }
    }

}