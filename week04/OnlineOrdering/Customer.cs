public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    // Add this method since your code calls LivesInUSA()
    public bool LivesInUSA()
    {
        return IsInUSA(); // Just call your existing method
    }

    public string GetName()
    {
        return name;
    }

    public string GetShippingAddress()
    {
        return address.GetFullAddress();
    }

    // Add this method since your code calls GetAddress()
    public Address GetAddress()
    {
        return address;
    }
}