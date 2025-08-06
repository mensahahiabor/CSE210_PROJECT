using System;

class Program
{
    static void Main()
    {
        // Create addresses
        Address address1 = new Address("123 Elm Street", "New York", "NY", "USA");
        Address address2 = new Address("45 Maple Avenue", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Alice Smith", address2);

        // Create orders
        Order order1 = new Order(customer1);
        Order order2 = new Order(customer2);

        // Add products to orders
        order1.AddProduct(new Product("Laptop", "L123", 800, 1));
        order1.AddProduct(new Product("Mouse", "M456", 20, 2));

        order2.AddProduct(new Product("Phone", "P789", 500, 1));
        order2.AddProduct(new Product("Headphones", "H101", 100, 1));

        // Display order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}");
    }
}