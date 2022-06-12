using RotaViagem.Models;
using System.Collections.Generic;

namespace RotaViagem.Service
{
    public interface IRotaService
    {
        void Add(Rota rota);
        void Delete(int id);
        List<Rota> Get();
        Rota Get(int id);
        void Update(Rota rota);
    }
}
