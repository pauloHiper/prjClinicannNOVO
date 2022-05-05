using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjClinica.Classes
{
    [Serializable]
    public class Imc
    {
        private float peso;
        private float altura;

        public float getPeso()
        {
            return peso;
        }

        public float getAltura()
        {
            return altura;
        }
        public void setPeso(float peso)
        {
            this.peso = peso;
        }      

        public void setAltura(float altura)
        {
            this.altura = altura;
        }

        public Imc(float peso, float altura)
        {
            this.peso = peso;
            this.altura = altura;
        }

        public float calcula()
        {
            return peso / (altura * altura);
        }

        public String discute()
        {
            string mens = "";
            if (peso > 200f || peso < 20f)
            {
                mens += " - Peso do paciente inválido: " + peso + "Kg ";  
            }
            if (altura > 2.2f || altura < 1.2f)
            {
                mens += " - Altura do paciente inválida: " + altura + "m ";
            }

            if (mens != string.Empty) return mens;

            float resultado = calcula();

            if (resultado < 17) return "Muito abaixo do peso";
            if (resultado < 18.5) return "Abaixo do peso";
            if (resultado < 25) return "Peso normal";
            if (resultado < 30) return "Acima do peso";
            if (resultado < 35) return "Obesidade I";
            if (resultado < 40) return "Obesidade II (severa)";
            return "Obesidade III (mórbida)";
        }
    }
}