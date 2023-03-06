// See https://aka.ms/new-console-template for more information
using Lending.Domain;

decimal loanAmount = 0;
decimal assetValue = 0;
int creditScore = 0;
string name = string.Empty;

var lender = new Lender();

while (true)
{
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. Apply for a Loan.");
    Console.WriteLine("2. Number of Applicants.");
    Console.WriteLine("3. Total Value of Loans.");
    Console.WriteLine("4. Mean Average Loan to Value.");
    Console.WriteLine("x. Exit the Application.");
    Console.Write("Enter your choice (1-4): ");

    string? menu = Console.ReadLine();

    switch (menu)
    {
        case "1":

            Console.Write("Enter name of applicant: ");
            name = Console.ReadLine() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid input. Plese enter valid name: ");
                Console.Write("Enter name of applicant: ");
                name = Console.ReadLine() ?? string.Empty;
            }

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

            Console.Write("Enter the credit score of the applicant between 1 and 999: ");
            while (!int.TryParse(Console.ReadLine(), out creditScore))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("Enter the value of the credit score: ");
            }

            Applicant applicant;

            try
            {
                applicant = Applicant.Create(name, loanAmount, assetValue, creditScore, "GBP");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                break;
            }

            var loanResult = lender.Apply(applicant);
            Console.WriteLine("Loan application result: " + loanResult.result + " " + loanResult.message);
            break;

        case "2":
            Console.WriteLine("Total number of applicants: ");
            var applicants = lender.GetNumberApplicants();
            foreach(var app in applicants)
            {
                Console.WriteLine(app.Item2 + " " + app.Item1);
            }
            break;

        case "3":
            var totalValue = lender.GetTotalValueOfLoans();
            Console.WriteLine("Total value of loans: " + totalValue.ToString());
            break;

        case "4":
            var meanAverageLTV = lender.GetAverageLTV();
            Console.WriteLine("Mean Average Loan to Value of all applications: " + meanAverageLTV.ToString());
            break;

        case "X":
            return;

        case "x":
            return;

        default:
            break;

    }      
}


