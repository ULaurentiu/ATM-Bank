using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Bills  
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public bool isPaid = false;

        public Bills(string Name, int Price)
        {
            this.Name = Name;
            this.Price = Price;

        }

    }
    class Program
    {
        BankAccount selectedBankAccount = null;

        class BankAccount
        {
            public string OwnerName { get; set; }
            public string Id { get; set; }
            public decimal Balance { get; set; }

            public BankAccount(string OwnerName, string Id, int Balance)
            {
                this.OwnerName = OwnerName;
                this.Id = Id;
                this.Balance = Balance;
            }
        }

        List<BankAccount> accounts;
        List<Bills> bills;

        Program()
        {

            

            bills = new List<Bills>     
            {
                new Bills("Apa",  1000),
                new Bills("Internet", 130),
                new Bills("Curent",  550)
            };


            accounts = new List<BankAccount>
            {
                new BankAccount("Furtos Stelian", "2851", -15),
                new BankAccount("Andrei Test", "2995", 592),
                new BankAccount("Luis Fonsi", "5555", 10),
                new BankAccount("Gandalf the Grey", "1111", 99999)
            };
        }
        void Paybills()  
        {
            foreach (var billstobepaid in bills)
            {
                if (billstobepaid.isPaid == false)
                    Console.WriteLine("You must pay :" + (bills.LastIndexOf(billstobepaid) + 1) + " " + billstobepaid.Name + " " + billstobepaid.Price + " lei ");

            }

            int billnumber;

            int.TryParse(Console.ReadLine(), out billnumber);


            if (bills[billnumber - 1].Price <= selectedBankAccount.Balance)
            {
                if (bills[billnumber - 1].isPaid == false)
                {
                    bills[billnumber - 1].isPaid = true;
                    selectedBankAccount.Balance -= bills[billnumber - 1].Price;
                    Console.WriteLine("Bill has been paid with success");
                }
                else
                    Console.WriteLine("Bill is already paid");

            }
            else
            {
                Console.WriteLine("You dont have enough money, you poor soul");
            }

            Console.WriteLine();

        }

        void ShowUserOptions()
        {
            Console.WriteLine("1.Insert card");
            Console.WriteLine("2.Withdraw card");
            Console.WriteLine("3.Block card");
        }
        void Remove()
        {
            if (selectedBankAccount != null)
            {
               accounts.Remove(selectedBankAccount);
            }
            Console.WriteLine("Your card has been blocked");
        }

        void ShowCardOptions()
        {
            Console.WriteLine("1.Withdraw money");
            Console.WriteLine("2.Deposit");
            Console.WriteLine("3.Pay bills");
            Console.WriteLine("4.Show balance");
        }

        void WithdrawMoney()
        {
            Console.WriteLine("Select the amount you want to withdraw");

            int amount;
            if (int.TryParse(Console.ReadLine(), out amount))
            {
                if (selectedBankAccount.Balance <= amount)
                {
                    Console.WriteLine("Not enough funds");
                    return;
                }

                selectedBankAccount.Balance -= amount;
                Console.WriteLine("Operation great success");
            }
        }
        void DepositMoney()
        {
            
            Console.WriteLine("Select the amount you want to deposit");
            int amount;
            if (int.TryParse(Console.ReadLine(), out amount))
            {

                selectedBankAccount.Balance += amount;
                Console.WriteLine("Deposited with success");
            }


        }
       


        void InsertCard()
        {
            Console.WriteLine("Card inserted, please provide a PIN");
            var pin = Console.ReadLine();

            selectedBankAccount = null;

            foreach (var bankAccount in accounts)
            {
                if (bankAccount.Id != pin)
                    continue;

                selectedBankAccount = bankAccount;
                break;
            }

            if (selectedBankAccount == null)
                throw new BankAccountNotFound();

            ShowCardOptions();

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        WithdrawMoney();
                        break;
                    case 2:
                        DepositMoney();
                        break;
                    case 3:
                        Paybills();
                        break;
                }
            }
        }



        static void Main(string[] args)
        {
            Program myProgram = new Program();

            while (true)
            {
                myProgram.ShowUserOptions();
                var input = Console.ReadLine();
                int choice;

                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                myProgram.InsertCard();
                            }
                            catch (BankAccountNotFound)
                            {
                                Console.WriteLine("No account was found");
                            }

                            break;
                        case 2:
                            return;
                        case 3:
                            try
                            {
                                myProgram.Remove();
                            }
                            catch (BankAccountNotFound)
                            {
                                Console.WriteLine("No account was found");
                            }
                            break;
                            
                    }
                }
                else
                {

                }
            }


        }
    }

    class BankAccountNotFound : Exception
    {

    }
}


