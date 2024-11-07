using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class Arvore
    {
        public Guid ArvoreID { get; set; }
        public bool IsDeleted { get; set; }
        public string Nome { get; set; }
        public string DescricaoBotanica { get; set; }
        public string Frutificacao { get; set; }
        public string Dispersao { get; set; }
        public string OcorrenciaNatural { get; set; }
        public string AspectosEcologicos { get; set; }
        public string RegeneracaoNatural { get; set; }
        public string AproveitamentoAlimentacao { get; set; }
        public string DadosNutricionais { get; set; }
        public string FormasDeConsumo { get; set; }
        public string AproveitamentoBiotecnologico { get; set; }
        public string Composicao { get; set; }
        public string PotenciaisBioprodutos { get; set; }
        public string Bioatividade { get; set; }
        public string Paisagismo { get; set; }
        public string ColheitaBeneficiamentoSementes { get; set; }
        public string ProducaoMudas { get; set; }
        public string Transplante { get; set; }
        public string CuidadosAgua { get; set; }
        public string CuidadosSolo { get; set; }

        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<string> Fotos { get; set; }
    }

}
