namespace crm_software_back.DTOs
{
    public class DTOPaymentData
    {
        public int TotalFee { get; set; }

        public int Installments { get; set; }

        public int PaybleAmount { get; set; }

        public int NextInstallment { get; set; }

        public DateTime DueDate { get; set; }

        public int LastPayment { get; set; }

        public DateTime LastPaymentDate { get; set; }
    }
}
