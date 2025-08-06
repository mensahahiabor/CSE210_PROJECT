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

        double shippingCost = _customer.LivesInUSA() ? 5.0 : 35.0;
        return productTotal + shippingCost;
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
        shippingLabel.AppendLine(_customer.GetAddress().GetFullAddress());
        
        return shippingLabel.ToString();
    }
}