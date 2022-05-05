using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace prjClinica.Classes
{
    public class TabelaBase
    {
        public static String prefixo = "tweb";
        public Usuario usuarioCriacao {get; set;}
        public Usuario usuarioEdicao {get; set;}

        public DateTime dataCriacao { get; set; }
        public DateTime dataEdicao { get; set; }

        public bool stAtivo;

        public int criando(Conexao con, string tabela, int id)
        {
            try
            {
                String sql = String.Concat("UPDATE ", prefixo, tabela, " SET idUsuarioCriacao=", con.usuario.idUsuario, ", dataCriacao='", DateTime.Now.ToString("dd/MM/yyyy"), "' WHERE id", tabela, "=", id);
                return Conexao.executaQuery(con, sql, prefixo + tabela);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int atualizando(Conexao con, string tabela, int id)
        {
            try
            {
                String sql = String.Concat("UPDATE ", prefixo, tabela, " SET idUsuarioEdicao=", con.usuario.idUsuario, ", dataEdicao='", DateTime.Now.ToString("dd/MM/yyyy"), "' WHERE id", tabela, "=", id);
                return Conexao.executaQuery(con, sql, prefixo + tabela);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void carregaLog(Conexao con, string tabela, int id)
        {
            String sql = String.Concat("SELECT idUsuarioCriacao, idUsuarioEdicao, dataCriacao, dataEdicao, stAtivo FROM ", prefixo, tabela, " WHERE id", tabela, "=", id);
            DataTable dt = Conexao.executaSelect(con, sql);

            if (dt.Rows.Count == 0) throw new Exception("Erro inesperado em carregaLog(). id " + id + " não encontrado;");

            DataRow[] r = dt.Select();

            try
            {
                try
                {
                    usuarioCriacao = Usuario.busca(r[0][0].ToString(), con);
                }
                catch (Exception)
                {
                    // 
                }
                try
                {
                    usuarioEdicao = Usuario.busca(r[0][1].ToString(), con);
                }
                catch (Exception)
                {
                    // 
                }
                try
                {
                    dataCriacao = DateTime.Parse(r[0][2].ToString());
                }
                catch (Exception)
                {
                    // 
                }
                try
                {
                    dataEdicao = DateTime.Parse(r[0][3].ToString());
                }
                catch (Exception)
                {
                    // 
                }
                stAtivo = r[0][4].ToString() == "1";
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}