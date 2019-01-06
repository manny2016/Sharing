

namespace Sharing.Core.Entities
{
    public class MCard
    {
        public virtual long Id { get; set; }
        public virtual string CardId { get; set; }
        public virtual long MerchantId { get; set; }
        public virtual string BrandName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Prerogative { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int TotalQuantity { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual string LogoUrl { get; set; }
        public virtual string RawData { get; set; }


        public string GenerateMySqlInsertValuesString()
        {
            return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})",
                this.MerchantId,
                this.CardId.ToSqlValue(),                
                this.Quantity,
                this.TotalQuantity,
                this.Discount,
                this.Title.ToSqlValue(),
                this.BrandName.ToSqlValue(),
                this.Prerogative.ToSqlValue(),
                this.LogoUrl.ToSqlValue(),
                this.RawData.ToSqlValue()
            );
        }
    }
}
