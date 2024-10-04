using System.ComponentModel.DataAnnotations.Schema;
using ks.domain.Enums;

namespace ks.domain.Entities;
[Table("order")]
public class Order : BaseEntity
{
    public int TotalQuantity { get; set; }
    public double TotalAmount { get; set; }
    public double ActualAmount { get; set; }
    public double SalePercent { get; set; } = 0;
    public string ShippingAddress { get; set; } = string.Empty;
    public PaymentMethodEnum PaymentMethod { get; set; } = PaymentMethodEnum.COD;

    #region Rel Config
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = new();
    public virtual ICollection<Feedback> Feedbacks { get; set; } = [];
    public virtual ICollection<Payment> Payments { get; set; } = [];
    public virtual ICollection<OrderLine> OrderLines { get; set; } = [];
    #endregion
}