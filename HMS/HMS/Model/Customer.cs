namespace HMS.Model
{
    public class Customer
    {
        private Guid _customerId;
        public Guid CustomerId { get { return _customerId; }
                                set { _customerId = value; } }

        private string _customer_name;
        public string CustomerName { get { return _customer_name; }
                                     set { _customer_name = value;} }

        private string _customer_email;
        public string CustomerEmail { get { return _customer_email; }
                                      set { _customer_email = value;} }

        private string _customer_phone;
        public string CustomerPhone { get { return _customer_phone; }
                                      set { _customer_phone = value;} }

        public ICollection<Reservation> Reservations { get; set; }
        public Customer() { }

        public Customer(string customer_name, string customer_email,
                        string customer_phone)
        {
            this.CustomerName = customer_name;
            this.CustomerEmail = customer_email;
            this.CustomerPhone = customer_phone;
            this.Reservations = new List<Reservation>();
        }
    }
}
