namespace EstacionaCareca.Servico.DTO
{
    public class VeiculoDTO
    {
        public string Placa { get; set; } = string.Empty;
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public decimal ValorPago { get; set; }

        public VeiculoDTO(string placa, DateTime dataEntrada, DateTime dataSaida, decimal valorPago)
        {
            Placa = placa;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            ValorPago = valorPago;
        }

        public VeiculoDTO() { }

    }
}
