namespace retailApp.Model
{
    public class DisqualificationReason
    {
        public int DisqualificationReasonId { get; set; }
        public int ProductId { get; set; }
        public string Reason { get; set; }
        public Product Product { get; set; }
    }
}