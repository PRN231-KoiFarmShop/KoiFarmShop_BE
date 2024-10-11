namespace ks.application.Models.VNPays;
public class VNPayRequestModel 
{
    public string Version { get; set; } = "2.1.0";
    public string Command { get; set; } = "Pay";
    public string TmnCode { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
    public string BankCode { get; set; } = "VNPAYQR";
    public string CreateDate { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
    public string CurrCode { get; set; } = "VND";
    public string IpAddress { get; set; } = "127.0.0.1";
    public string Locale { get; set; } = "VN";
    public string OrderInfo { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
    public string OrderType { get; set; } = "other";
    public string ExpireDate { get; set; } = DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss");
    public string TxnRef { get; set; } = DateTime.Now.Ticks.ToString();
    public string SecureHash { get; set; } = string.Empty;
}