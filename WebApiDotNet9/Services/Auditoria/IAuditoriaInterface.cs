using WebApiDotNet9.Models;

namespace WebApiDotNet9.Services.Auditoria
{
    public interface IAuditoriaInterface
    {
        Task RegistrarAuditoriaAsync(string acao, string UsuarioId, string dadosAlterados);
        Task<List<AuditoriaModel>> BuscarAuditorias();
    }
}
