using RotaViagem.Models;
using System.Collections.Generic;

namespace RotaViagem.Business
{
    public class RotaBusiness
    {
        #region Calculo de Rota

        // Calculo do melhor preco usando algoritmo de Dijkstra
        public static string CalcularMelhorRota(List<Rota> rotas, string origem, string destino)
        {
            List<string> aeroportos = new List<string>();

            foreach (Rota rota in rotas)
            {
                if (!aeroportos.Contains(rota.Origem))
                {
                    aeroportos.Add(rota.Origem);
                }

                if (!aeroportos.Contains(rota.Destino))
                {
                    aeroportos.Add(rota.Destino);
                }
            }

            int[,] graphPreco = new int[aeroportos.Count, aeroportos.Count];
            string[,] graphCaminho = new string[aeroportos.Count, aeroportos.Count];

            foreach (Rota rota in rotas)
            {
                graphPreco[aeroportos.IndexOf(rota.Origem), aeroportos.IndexOf(rota.Destino)] = rota.Valor;
                graphCaminho[aeroportos.IndexOf(rota.Origem), aeroportos.IndexOf(rota.Destino)] = rota.Destino;
            }

            int src = aeroportos.IndexOf(origem);
            int[] preco = new int[aeroportos.Count];
            string[] caminho = new string[aeroportos.Count];
            bool[] sptSet = new bool[aeroportos.Count];

            for (int i = 0; i < aeroportos.Count; i++)
            {
                preco[i] = int.MaxValue;
                sptSet[i] = false;
            }

            preco[src] = 0;
            caminho[src] = origem;

            for (int count = 0; count < aeroportos.Count - 1; count++)
            {
                int min = int.MaxValue;
                int u = -1;

                for (int v = 0; v < aeroportos.Count; v++)
                {
                    if (!sptSet[v] && preco[v] <= min)
                    {
                        min = preco[v];
                        u = v;
                    }
                }

                sptSet[u] = true;

                for (int v = 0; v < aeroportos.Count; v++)
                {
                    if (!sptSet[v] && graphPreco[u, v] != 0 &&
                        preco[u] != int.MaxValue &&
                        preco[u] + graphPreco[u, v] < preco[v])
                    {
                        preco[v] = preco[u] + graphPreco[u, v];
                        caminho[v] = caminho[u] + " - " + graphCaminho[u, v];
                    }
                }
            }

            int melhorPreco = preco[aeroportos.IndexOf(destino)];
            string melhorCaminho = caminho[aeroportos.IndexOf(destino)];
            string result = melhorCaminho + " ao custo de $" + melhorPreco;

            return result;
        }

        #endregion
    }
}
