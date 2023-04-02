namespace crm_software_back.DTOs
{
    public class DTOPaymentData
    {
        public int TotalFee { get; set; }

        public int Installments { get; set; }

        public double PaybleAmount { get; set; }

        public double NextInstallment { get; set; }

        public DateTime DueDate { get; set; }

        public double LastPayment { get; set; }

        public DateTime LastPaymentDate { get; set; }
    }
}
