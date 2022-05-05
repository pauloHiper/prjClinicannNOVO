using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace prjClinica.Classes
{
    public class Tabela
    {
        private int linhas;
        private int colunas;

        public string[,] celula;

        public Tabela(int linhas, int colunas)
        {
            celula = new string[linhas,colunas];
            this.linhas = linhas;
            this.colunas = colunas;
        }

        public string tabela(string [] titulo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<table>");
            str.Append("<TR><TD colspan='" + colunas + "'><hr /><TD><TR>");
            str.Append("<tr>");
            for (int col = 0; col < colunas; col++)
            {
                str.Append("<TD>&nbsp;<B>" + titulo[col] + "</B>&nbsp;</TD>");
            }
            str.Append("</tr>");
            str.Append("<TR><TD colspan='" + colunas + "'><hr /><TD><TR>");
            for (int lin = 0; lin < linhas; lin++)
            {
                str.Append("<tr>");
                for(int col = 0; col < colunas; col++)
                {
                    str.Append("<TD>&nbsp;" + celula[lin, col] + "&nbsp;</TD>");
                }
                str.Append("</tr>");
            }
            str.Append("<TR><TD colspan='" + colunas + "'><hr /><TD><TR>");
            str.Append("</table>");
            return str.ToString();
        }
     }
}