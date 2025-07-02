using WebApiDotNet9.Models;

namespace WebApiDotNet9.Services.Senha
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] SenhaSalt);
        string CriarToken(UsuarioModel usuario);
    }
}
