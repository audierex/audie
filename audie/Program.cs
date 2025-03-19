using System;

namespace SimplePaymentSystem
{
  
    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }

        public Customer(string name, string city)
        {
            Name = name;
            City = city;
        }
    }

   
    public interface IPaymentProcessor
    {
        void ProcessPayment(Customer customer, double amount);
    }

    
    public class CreditCardPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(Customer customer, double amount)
        {
            Console.WriteLine($"Processing Credit Card payment for {customer.Name} from {customer.City} for ₱{amount:C}.");
        }
    }

   
    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(Customer customer, double amount)
        {
            Console.WriteLine($"Processing PayPal payment for {customer.Name} from {customer.City} for ₱{amount:C}.");
        }
    }

   
    public class BankTransferPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(Customer customer, double amount)
        {
            Console.WriteLine($"Processing Bank Transfer payment for {customer.Name} from {customer.City} for ₱{amount:C}.");
        }
    }

   
    public class PaymentProcessor
    {
        private readonly IPaymentProcessor _paymentProcessor;

        public PaymentProcessor(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public void Process(Customer customer, double amount)
        {
            _paymentProcessor.ProcessPayment(customer, amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool continuePayment = true;

            while (continuePayment)
            {
                
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                Console.Write("Enter your city: ");
                string city = Console.ReadLine();

                Customer customer = new Customer(name, city);

                Console.WriteLine("\nChoose your payment method:");
                Console.WriteLine("1. Credit Card");
                Console.WriteLine("2. PayPal");
                Console.WriteLine("3. Bank Transfer");

                string paymentMethodChoice = Console.ReadLine();
                IPaymentProcessor paymentProcessor = null;

                switch (paymentMethodChoice)
                {
                    case "1":
                        paymentProcessor = new CreditCardPaymentProcessor();
                        break;
                    case "2":
                        paymentProcessor = new PayPalPaymentProcessor();
                        break;
                    case "3":
                        paymentProcessor = new BankTransferPaymentProcessor();
                        break;
                    default:
                        Console.WriteLine("Invalid payment method selected.");
                        continue;
                }

                
                PaymentProcessor processor = new PaymentProcessor(paymentProcessor);
                processor.Process(customer, 150.75);

               
                Console.Write("\nDo you want to make another payment? (y/n): ");
                string userChoice = Console.ReadLine().ToLower();

                if (userChoice != "y")
                {
                    continuePayment = false;
                    Console.WriteLine("Thank you for using the payment system. Goodbye!");
                }
            }
        }
    }
}
