// See https://aka.ms/new-console-template for more information

decimal loanAmount = 0;
decimal assetValue = 0;
int creditScore = 0;

while (true)
{
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. Apply for Loan.");
    Console.WriteLine("2. Number of Applicants.");
    Console.WriteLine("3. Total Value of Loans.");
    Console.WriteLine("X. Exit the Application.");
    Console.Write("Enter your choice (1-3): ");

    string? menu = Console.ReadLine();

    switch (menu)
    {
        case "1":
            Console.Write("Enter amount of the loan: ");
            while (!decimal.TryParse(Console.ReadLine(), out loanAmount))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("Enter the amount of the loan: ");
            }

            Console.Write("Enter the value of the asset: ");
            while (!decimal.TryParse(Console.ReadLine(), out assetValue))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("Enter the value of the asset: ");
            }

            Console.Write("Enter the credit score of the applicant of the asset: between 1 and 999: ");
            while (!int.TryParse(Console.ReadLine(), out creditScore))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("Enter the value of the credit score: ");
            }

            Console.Write("Loan application recieved.");
            break;

        case "2":
            Console.WriteLine("Total number of applicants: ");
            break;

        case "3":
            Console.WriteLine("Total value of loans: ");
            break;

        case "X":
            return;

        default:
            break;

    }      
}


