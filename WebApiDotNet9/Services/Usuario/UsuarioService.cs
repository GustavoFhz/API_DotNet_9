using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using WebApiDotNet9.Data;
using WebApiDotNet9.Dto.Usuario;
using WebApiDotNet9.Models;
using WebApiDotNet9.Services.Senha;

namespace WebApiDotNet9.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context, ISenhaInterface senhaInterface, IMapper mapper)
        {
           _context = context;
           _senhaInterface = senhaInterface;
           _mapper = mapper;
        }
        public async Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios()
        {
            ResponseModel<List<UsuarioModel>> response = new();

            try{

                var usuarios = await _context.Usuarios.ToListAsync(); //Entra no banco de dados e transforma tudo numa lista
                
                if(usuarios.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário cadastrado!";
                    return response;
                }

                response.Dados = usuarios;
                response.Mensagem = "Usuários localizados com sucesso!";
                return response;


            }
            catch(Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int id)
        {
            ResponseModel<UsuarioModel> response = new();

            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if(usuario == null)
                {
                    response.Mensagem = "Usuário não localizado!";
                    return response;
                }
                response.Dados = usuario;
                response.Mensagem = "Usuário localizado!";
                return response;



            }
            catch(Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> RemoverUsuario(int id)
        {
            ResponseModel<UsuarioModel> response = new();

            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if(usuario == null)
                {
                    response.Mensagem = "Usuário não localizado!";
                    return response;
                }
                _context.Remove(usuario);
                await _context.SaveChangesAsync();
                response.Mensagem = $"Usuário {usuario.Nome} removido com sucesso!";
                return response;


            }
            catch(Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            ResponseModel<UsuarioModel> response = new();

            try
            {
                if (!VerificaSeExisteEmailUsarioRepetidos(usuarioCriacaoDto))
                {
                    response.Mensagem = "Email/Usuário já cadastrado!";
                    return response;
                }

                _senhaInterface.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = _mapper.Map<UsuarioModel>(usuarioCriacaoDto);
                usuario.SenhaHash = senhaHash;
                usuario.SenhaSalt = senhaSalt;

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                response.Mensagem = "Usuário cadastrado com sucesso!";
                response.Dados = usuario;

                return response;
            }
            catch( Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
        private bool VerificaSeExisteEmailUsarioRepetidos(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(item => item.Email == usuarioCriacaoDto.Email ||
            item.Usuario == usuarioCriacaoDto.Usuario);

            if(usuario != null)
            {
                return false;
            }
            return true;
        }

    }
}
