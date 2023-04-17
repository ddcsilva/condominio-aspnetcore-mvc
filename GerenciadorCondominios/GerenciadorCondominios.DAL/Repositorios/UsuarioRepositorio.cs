﻿using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GerenciadorCondominios.DAL.Repositorios;

public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
{
    private readonly Contexto _contexto;
    private readonly UserManager<Usuario> _gerenciadorUsuarios;
    private readonly SignInManager<Usuario> _gerenciadorLogin;
    public UsuarioRepositorio(Contexto contexto, UserManager<Usuario> gerenciadorUsuarios, SignInManager<Usuario> gerenciadorLogin) : base(contexto)
    {
        _contexto = contexto;
        _gerenciadorUsuarios = gerenciadorUsuarios;
        _gerenciadorLogin = gerenciadorLogin;
    }

    public async Task<IdentityResult> CriarUsuario(Usuario usuario, string senha)
    {
        try
        {
            return await _gerenciadorUsuarios.CreateAsync(usuario, senha);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao criar usuário", ex);
        }
    }

    public async Task IncluirUsuarioEmFuncao(Usuario usuario, string funcao)
    {
        try
        {
            await _gerenciadorUsuarios.AddToRoleAsync(usuario, funcao);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao incluir usuário na Função", ex);
        }
    }

    public async Task LogarUsuario(Usuario usuario, bool lembrar)
    {
        try
        {
            await _gerenciadorLogin.SignInAsync(usuario, lembrar);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao logar usuário", ex);
        }
    }

    public async Task DeslogarUsuario()
    {
        try
        {
            await _gerenciadorLogin.SignOutAsync();
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao deslogar usuário", ex);
        }
    }

    public int VerificarSeExisteRegistro()
    {
        try
        {
            return _contexto.Usuarios.Count();
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao verificar usuário", ex);
        }
    }

    public async Task<Usuario> PegarUsuarioPeloEmail(string email)
    {
        try
        {
            return await _gerenciadorUsuarios.FindByEmailAsync(email);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }

    public async Task AtualizarUsuario(Usuario usuario)
    {
        try
        {
            await _gerenciadorUsuarios.UpdateAsync(usuario);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao atualizar usuário", ex);
        }
    }

    public async Task<bool> VerificarSeUsuarioEstaEmFuncao(Usuario usuario, string funcao)
    {
        try
        {
            return await _gerenciadorUsuarios.IsInRoleAsync(usuario, funcao);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }

    public async Task<IList<string>> PegarFuncoesUsuario(Usuario usuario)
    {
        try
        {
            return await _gerenciadorUsuarios.GetRolesAsync(usuario);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }

    public async Task<IdentityResult> RemoverFuncoesUsuario(Usuario usuario, IEnumerable<string> funcoes)
    {
        try
        {
            return await _gerenciadorUsuarios.RemoveFromRolesAsync(usuario, funcoes);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }

    public async Task<IdentityResult> IncluirUsuarioEmFuncoes(Usuario usuario, IEnumerable<string> funcoes)
    {
        try
        {
            return await _gerenciadorUsuarios.AddToRolesAsync(usuario, funcoes);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }

    public async Task<Usuario> PegarUsuarioPeloNome(ClaimsPrincipal usuario)
    {
        try
        {
            return await _gerenciadorUsuarios.FindByNameAsync(usuario.Identity.Name);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }

    public async Task<Usuario> PegarUsuarioPeloId(string usuarioId)
    {
        try
        {
            return await _gerenciadorUsuarios.FindByIdAsync(usuarioId);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }

    public string CodificarSenha(Usuario usuario, string senha)
    {
        try
        {
            return _gerenciadorUsuarios.PasswordHasher.HashPassword(usuario, senha);
        }
        catch (Exception ex)
        {

            throw new RepositorioException("Erro ao retornar usuário", ex);
        }
    }
}
