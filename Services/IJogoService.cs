using Catalago.Entities;
using Catalago.InputModel;
using Catalago.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalago.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);

        Task<JogoViewModel> Obter(Guid id);

        Task<JogoViewModel> Inserir(JogoInputModel jogo);

        Task Atualizar(Guid id, JogoInputModel jogo);

        Task Atualizar(Guid id, double preco);

        Task Remover(Guid id);
        //Task Atualizar(JogoViewModel entidadeJogo);
        //Task Inserir(Jogo jogoInsert);
    }
}
