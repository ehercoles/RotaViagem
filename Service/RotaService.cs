using RotaViagem.Models;
using System.Collections.Generic;
using System.Linq;

namespace RotaViagem.Service
{
    public class RotaService : IRotaService
    {
        private readonly List<Rota> rotas;

        public RotaService()
        {
            this.rotas = new List<Rota>
            {
                new Rota { Id=1, Origem="GRU", Destino="BRC", Valor=10 },
                new Rota { Id=2, Origem="BRC", Destino="SCL", Valor=5 },
                new Rota { Id=3, Origem="GRU", Destino="CDG", Valor=75 },
                new Rota { Id=4, Origem="GRU", Destino="SCL", Valor=20 },
                new Rota { Id=5, Origem="GRU", Destino="ORL", Valor=56 },
                new Rota { Id=6, Origem="ORL", Destino="CDG", Valor=5 },
                new Rota { Id=7, Origem="SCL", Destino="ORL", Valor=20 }
            };
        }

        public void Add(Rota rota)
        {
            this.rotas.Add(rota);
        }

        public void Delete(int id)
        {
            Rota rota = this.rotas.Where(x => x.Id == id).FirstOrDefault();

            this.rotas.Remove(rota);
        }

        public List<Rota> Get()
        {
            return this.rotas.ToList();
        }

        public Rota Get(int id)
        {
            return this.rotas.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(Rota model)
        {
            Rota rota = this.rotas.Where(x => x.Id == model.Id).FirstOrDefault();

            rota.Origem = model.Origem;
            rota.Destino = model.Destino;
            rota.Valor = model.Valor;
        }
    }
}
