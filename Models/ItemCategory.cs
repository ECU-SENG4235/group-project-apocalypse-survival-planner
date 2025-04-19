namespace ASP.Models
{
    public class ItemCategory
    {
        public string Name { get; set; } = "";

        public string[] Labels { get; set; }
        public decimal[] Prices { get; set; }
        public int[] Quantities { get; set; }

        public ItemCategory() { }

        public ItemCategory(string name)
        {
            Name = name;
        }

        public List<string> GetSelected()
        {
            var list = new List<string>();
            for (int i = 0; i < Quantities.Length; i++)
            {
                if (Quantities[i] > 0)
                    list.Add($"{Labels[i]} x{Quantities[i]}");
            }
            return list;
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            for (int i = 0; i < Quantities.Length; i++)
            {
                total += Quantities[i] * Prices[i];
            }
            return total;
        }

        public Dictionary<string, (int quantity, decimal price)> GetItemCounts()
        {
            var dict = new Dictionary<string, (int, decimal)>();
            for (int i = 0; i < Quantities.Length; i++)
            {
                if (Quantities[i] > 0)
                    dict[Labels[i]] = (Quantities[i], Prices[i]);
            }
            return dict;
        }
    }
}
