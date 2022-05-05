using prjClinica.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace prjClinica.Classes
{
    /*
     * 
     * 
     *
    CREATE TABLE [dbo].[twebPaciente] (
    [IdPaciente]       INT           IDENTITY (1, 1) NOT NULL,
    [nome]             NVARCHAR (50) NULL,
    [sexo]             NCHAR (1)     NULL,
    [dataNascimento]   DATE          NULL,
    [peso]             FLOAT (53)    NULL,
    [altura]           FLOAT (53)    NULL,
    [idUsuarioCriacao] INT           NULL,
    [dataCriacao]      DATE          NULL,
    [idUsuarioEdicao]  INT           NULL,
    [dataEdicao]       DATE          NULL,
    [stAtivo]          INT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([IdPaciente] ASC)
);

     * 
     */
    public class Paciente : TabelaBase
    {
        private int idPaciente;      

        private DateTime dataNascimento;

        private String nome;

        private char sexo;

        private Imc imc;

        private static string tabela = "Paciente";
        private static string nomeId = "id" + tabela;
        private static string nomeTabela = prefixo + tabela;

        private static string campos = "nome, sexo, peso, altura, dataNascimento";

        public String getNome()
        {
            return nome;
        }
        public void setNome(String nome)
        {
            this.nome = nome;
        }
        public DateTime getDataNascimento()
        {
            return dataNascimento;
        }

        public void setDataNascimento(DateTime dataNascimento)
        {
            this.dataNascimento = dataNascimento;
        }

        public char getSexo()
        {
            return sexo;
        }

        public void setSexo(char sexo)
        {
            this.sexo = sexo;
        }

        public float getPeso()
        {
            return imc.getPeso();
        }

        public void setPeso(float peso)
        {
            imc.setPeso(peso);
        }

        public float getAltura()
        {
            return imc.getAltura();
        }
        public void setAltura(float altura)
        {
            imc.setAltura(altura);
        }
        public Paciente()
        {
            //
        }
        public Paciente(String nome, char sexo, float peso, float altura, DateTime dataNascimento)
        {          
            popula(nome, sexo, peso, altura, dataNascimento);
        }
        public void popula(String nome, char sexo, float peso, float altura, DateTime dataNascimento)
        {
            this.nome = nome;
            this.sexo = sexo;
            this.dataNascimento = dataNascimento;

            imc = new Imc(peso, altura);
        }
        public int getId()
        {
            return idPaciente;
        }
        public String diagnosticoPeso()
        {
            return imc.discute();
        }

        public static List<Paciente> buscaLike(String nome, Conexao con)
        {
            List<Paciente> lista = new List<Paciente>();
            String sql = String.Concat("SELECT id", tabela, " FROM ", nomeTabela, " where nome like @1 order by nome");
           
            SqlCommand comando = new SqlCommand(sql, con.con());

            comando.CommandType = CommandType.Text;
            comando.Prepare();

            comando.Parameters.AddWithValue("@1", "%" + nome + "%");

            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = comando;

            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }         

            DataRow[] linhas = dt.Select();

            foreach (DataRow linha in linhas)
            {
                Paciente p = Paciente.busca(linha[0].ToString(), con);
                lista.Add(p);
            }

            return lista;
        }
      
        public int insere(Conexao con)
        {
            try
            {
                String sql = String.Concat("INSERT INTO ",
                    nomeTabela, " (", campos, 
                    ",stAtivo) Values (@1,@2,@3,@4,@5,1)");

                SqlCommand comando = new SqlCommand(sql, con.con());

                comando.CommandType = CommandType.Text;
                comando.Prepare();

                comando.Parameters.AddWithValue("@1", nome);
                comando.Parameters.AddWithValue("@2", sexo);
                comando.Parameters.AddWithValue("@3", (imc.getPeso() + "").Replace(",", "."));
                comando.Parameters.AddWithValue("@4", (imc.getAltura() + "").Replace(",", "."));
                comando.Parameters.AddWithValue("@5", dataNascimento.ToString("dd/MM/yyyy"));

                if (comando.ExecuteNonQuery() > 0)
                {
                    DataTable dt = Conexao.executaSelect(con,
                        "SELECT IDENT_CURRENT('" + nomeTabela + "')");
                    DataRow[] result = dt.Select();
                    idPaciente = result.Count() > 0 ? Convert.ToInt32(result[0][0].ToString()) : -1;
                    criando(con, tabela, idPaciente);
                }
                return idPaciente;
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
                String sql = String.Concat ("UPDATE " , nomeTabela , " SET nome=@1,sexo=@2,peso=@3,altura=@4,dataNascimento=@5 WHERE id" , tabela , "=" , idPaciente);

                SqlCommand comando = new SqlCommand(sql, con.con());

                comando.CommandType = CommandType.Text;                
                comando.Prepare();

                comando.Parameters.AddWithValue("@1", nome);
                comando.Parameters.AddWithValue("@2", sexo);
                comando.Parameters.AddWithValue("@3", (imc.getPeso() + "").Replace(",", "."));
                comando.Parameters.AddWithValue("@4", (imc.getAltura() + "").Replace(",", "."));
                comando.Parameters.AddWithValue("@5", dataNascimento.ToString("dd/MM/yyyy"));

                comando.ExecuteNonQuery();

                atualizando(con, tabela, idPaciente);
                return idPaciente;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Paciente busca(string id, Conexao con)
        {
            try
            {
                int iId;
                if (Int32.TryParse(id, out iId))
                {
                    return busca(iId, con);
                }
                else
                {
                    throw new Exception("Erro inesperado, id passado como texto inválido: " + tabela);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Paciente busca(int id, Conexao con)
        {
            try
            {
                String sql = String.Concat("SELECT ", campos, " FROM ", nomeTabela, " WHERE ", nomeId, "=", id);
                DataTable dt = Conexao.executaSelect(con, sql);
                if (dt.Rows.Count == 0) return null;

                DataRow[] r = dt.Select(); // "nome, sexo, peso, altura, dataNascimento";

                double peso;
                double altura;
                DateTime data;

                if (!Double.TryParse(r[0][2].ToString(), out peso)) throw new Exception("Erro lendo peso");
                if (!Double.TryParse(r[0][3].ToString(), out altura)) throw new Exception("Erro lendo altura");
                if (!DateTime.TryParse(r[0][4].ToString(), out data)) throw new Exception("Erro lendo data");

                Paciente item = new Paciente(r[0][0].ToString(), r[0][1].ToString().ToCharArray(0, 1)[0], (float)peso, (float)altura, data);
                item.carregaLog(con, tabela, id);
                item.idPaciente = id;
                return item;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Paciente> listaPacientes(Conexao con)
        {
            try
            {
                List<Paciente> lista = new List<Paciente>();

                String sql = String.Concat("SELECT id", tabela, " FROM " + nomeTabela + " WHERE stAtivo=1");
                DataTable dt = Conexao.executaSelect(con, sql);
                DataRow[] linhas = dt.Select();  

                foreach (DataRow linha in linhas)
                {
                    Paciente p = Paciente.busca(linha[0].ToString(), con);                
                    lista.Add(p);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}