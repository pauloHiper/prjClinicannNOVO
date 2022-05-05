using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace prjClinica.Classes
{
    /*
     * 
     * CREATE TABLE [dbo].[twebUsuario] (
    [IdUsuario]        INT           IDENTITY (1, 1) NOT NULL,
    [nome]             NVARCHAR (50) NULL,
    [login]            NCHAR (20)    NULL,
    [senha]            NCHAR (20)    NULL,
    [perfil]           NCHAR (10)    NULL,
    [idUsuarioCriacao] INT           NULL,
    [dataCriacao]      DATE          NULL,
    [idUsuarioEdicao]  INT           NULL,
    [dataEdicao]       DATE          NULL,
    [stAtivo]          INT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC)
);
     */
    public class Usuario : TabelaBase
    {
       private static string tabela = "Usuario";
       private static string nomeId = "id" + tabela;
       private static string nomeTabela = prefixo + tabela;

       private static string campos = "nome, login, senha, perfil";

       public int idUsuario;
       public String login { get; set; }
       public String nome { get; set; }
       public String senha { get; set; }
       public String perfil { get; set; }
       public Usuario(String nome, String login, String senha, String perfil)
       {
           this.nome = nome;
           this.login = login;
           this.senha = senha;
           this.perfil = perfil;
       }

       public static Usuario busca(string id, Conexao con)
       {
           int iId;
           if (Int32.TryParse(id, out iId))
           {
               return busca(iId, con);
           }
           else
           {
               throw new Exception("Usuário " + id + " não encontrado: " + tabela);
           }
       }

       public int insere(Conexao con) 
       {
           try
           {
               String sql = String.Concat("INSERT INTO ", nomeTabela, " (", campos, ",stAtivo) Values ('", nome, "','", login, "','", senha + "'," + perfil + ",1)");
               idUsuario = Conexao.executaQuery(con, sql, prefixo + tabela);
               criando(con, tabela, idUsuario);
               return idUsuario;
           }
           catch (Exception)
           {
               throw;
           }
       }

       public int atualiza(Conexao con)
       {
           try
           {
               StringBuilder sql = new StringBuilder("UPDATE " + nomeTabela + " SET ");
               sql.Append("nome='" + nome + "',");
               sql.Append("login='" + login + "',");
               sql.Append("senha='" + senha + "',");
               sql.Append("perfil='" + perfil + "' "); 
               sql.Append(" WHERE id" + tabela + "=" + idUsuario);
               atualizando(con, tabela, idUsuario);
               return idUsuario;
           }
           catch (Exception)
           {
               throw;
           }
       }

       public static Usuario busca(int id, Conexao con)
       {
           try
           {
               String sql = String.Concat("SELECT ", campos,  " FROM ", nomeTabela, " WHERE ", nomeId, "=", id);
               DataTable dt = Conexao.executaSelect(con, sql);
               if (dt.Rows.Count == 0) throw new Exception("Erro inesperado, id" + tabela + " não encontrado");
               DataRow[] r = dt.Select();
               Usuario item = new Usuario(r[0][0].ToString(), r[0][1].ToString(), r[0][2].ToString(), r[0][3].ToString());
          //   item.carregaLog(con, tabela, id);
               item.idUsuario = id;
               return item;
           }
           catch (Exception)
           {
               throw;
           }
       }



       


    }
}