namespace Calculadora_de_Plano_de_Nuvem.Models
{
    public class Servidor
    {
            public int Id { get; set; }
            public int Ip { get; set; }
            public string Cpu { get; set; }
            public string Memoria { get; set; }
            public string Armazenamento { get; set; }
            public string Raid { get; set; }
            public string LicencaSistema { get; set; }
            public string SistemaOperacional { get; set; }


    }
    public class ServidorNuvem : Servidor
    {
        public string Nuvem { get; set; }
    }

    public class ServidorFisico : Servidor
    {
        public string FonteAlimentacao { get; set; }
        public string ManutencaoPreventiva { get; set; }
    }
}
