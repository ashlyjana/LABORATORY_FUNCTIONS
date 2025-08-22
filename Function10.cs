using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function10
{
    internal class Program
    {
        class CartLine
        {
            public string ItemCode;
            public int Qty;
            public decimal UnitPrice;
            public string Category;
        }

        static decimal LineTotal(int qty, decimal unitPrice)
        {
            return qty * unitPrice;
        }

        static decimal ApplyCartDiscounts(List<CartLine> lines)
        {
            decimal grocerySubtotal = 0;
            decimal electronicsSubtotal = 0;
            decimal otherSubtotal = 0;

            Console.WriteLine("\n--- Basket Details ---");
            foreach (var line in lines)
            {
                decimal lineTotal = LineTotal(line.Qty, line.UnitPrice);

                switch (line.Category.ToUpper())
                {
                    case "GROCERY":
                        if (line.Qty >= 10)
                        {
                            decimal discount = lineTotal * 0.05m;
                            lineTotal -= discount;
                            Console.WriteLine($"[{line.ItemCode}] GROCERY line discount 5% applied: -{discount:F2}");
                        }
                        grocerySubtotal += lineTotal;
                        break;

                    case "ELECTRONICS":
                        electronicsSubtotal += lineTotal;
                        break;

                    case "OTHER":
                        otherSubtotal += lineTotal;
                        break;
                }

                Console.WriteLine($"[{line.ItemCode}] {line.Qty} × {line.UnitPrice} = {lineTotal:F2}");
            }

            // --- Category discount (electronics subtotal ≥ 20,000) ---
            if (electronicsSubtotal >= 20000)
            {
                decimal discount = electronicsSubtotal * 0.08m;
                electronicsSubtotal -= discount;
                Console.WriteLine($"\nElectronics subtotal discount 8% applied: -{discount:F2}");
            }

            decimal grandTotal = grocerySubtotal + electronicsSubtotal + otherSubtotal;

            // --- Cart-level discount (≥ 5 distinct items) ---
            if (lines.Count >= 5)
            {
                grandTotal -= 50;
                Console.WriteLine("Cart-level discount applied: -50.00");
            }

            Console.WriteLine($"\nGrocery subtotal: {grocerySubtotal:F2}");
            Console.WriteLine($"Electronics subtotal: {electronicsSubtotal:F2}");
            Console.WriteLine($"Other subtotal: {otherSubtotal:F2}");
            Console.WriteLine($"\n>>> FINAL TOTAL = {grandTotal:F2}");

            return grandTotal;
        }

        static void Main()
        {
            List<CartLine> cart = new List<CartLine>();

            Console.Write("Enter number of items: ");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nItem #{i + 1}");

                Console.Write("Item code: ");
                string code = Console.ReadLine();

                Console.Write("Quantity: ");
                int qty = Convert.ToInt32(Console.ReadLine());
                if (qty < 1)
                {
                    Console.WriteLine("Invalid qty. Skipping item.");
                    continue;
                }

                Console.Write("Unit price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                if (price < 0)
                {
                    Console.WriteLine("Invalid price. Skipping item.");
                    continue;
                }

                Console.Write("Category (GROCERY/ELECTRONICS/OTHER): ");
                string category = Console.ReadLine().Trim().ToUpper();

                if (category != "GROCERY" && category != "ELECTRONICS" && category != "OTHER")
                {
                    Console.WriteLine("Invalid category. Skipping item.");
                    continue;
                }

                cart.Add(new CartLine { ItemCode = code, Qty = qty, UnitPrice = price, Category = category });
            }

            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty. No total to compute.");
            }
            else
            {
                ApplyCartDiscounts(cart);
            }
        }
    }
}
       