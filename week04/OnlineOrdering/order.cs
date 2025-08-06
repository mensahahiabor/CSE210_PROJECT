using System;
using System.Collections.Generic;
using System.Text;

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public List<Product> GetProducts()
    {
        return _products;
    }

    public Customer GetCustomer()
    {
        return _customer;
    }

    public void SetCustomer(Customer customer)
    {
        _customer = customer;
    }

    public double CalculateTotalCost()
    {
        double productTotal = 0;
        foreach (Product product in _products)
        {
            productTotal += product.GetTotalCost();
        }

        // Fixed: Use IsInUSA() instead of LivesInUSA()
        double shippingCost = _customer.IsInUSA() ? 5.0 : 35.0;
        return productTotal + shippingCost;
    }

    // Add this method since Program.cs calls GetTotalPrice()
    public double GetTotalPrice()
    {
        return CalculateTotalCost();
    }

    public string GetPackingLabel()
    {
        StringBuilder packingLabel = new StringBuilder();
        packingLabel.AppendLine("PACKING LABEL:");
        packingLabel.AppendLine("==============");
        
        foreach (Product product in _products)
        {
            packingLabel.AppendLine($"Product: {product.GetName()} (ID: {product.GetProductId()})");
        }
        
        return packingLabel.ToString();
    }

    public string GetShippingLabel()
    {
        StringBuilder shippingLabel = new StringBuilder();
        shippingLabel.AppendLine("SHIPPING LABEL:");
        shippingLabel.AppendLine("===============");
        shippingLabel.AppendLine($"Customer: {_customer.GetName()}");
        shippingLabel.AppendLine("Address:");
        // Fixed: Use GetShippingAddress() instead of GetAddress().GetFullAddress()
        shippingLabel.AppendLine(_customer.GetShippingAddress());
        
        return shippingLabel.ToString();
    }
}