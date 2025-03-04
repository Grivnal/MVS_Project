namespace pv311_mvc_project.ViewModels
{
    public class CartVM
    {
        public IEnumerable<ProductCartVM> Items { get; set; } = [];
        public int Shipping { get; set; } = 100;
        public int Discount { get; set; } = 0;
        public double TotalPrice { get => Items.Select(i => i.Cost).Aggregate(0, (total, next) => total + next); }
        public double TotalPriceWithDicount { get => (Items.Select(i => i.Cost).Aggregate(0, (total, next) => total + next) * (1 - (double)Discount / 100)); }
    }
}