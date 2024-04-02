using System;
using System.Collections.Generic;

public class MortgageCalculator
{
    // Method to calculate monthly repayment amount
    public static double CalculateMonthlyRepayment(double loanAmount, double annualInterestRate, int loanTermYears)
    {
        double monthlyInterestRate = annualInterestRate / 12 / 100;
        int totalPayments = loanTermYears * 12;
        double monthlyRepayment = loanAmount * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalPayments)) /
            (Math.Pow(1 + monthlyInterestRate, totalPayments) - 1);
        return monthlyRepayment;
    }

    // Method to calculate total amount of interest paid over the life of the loan
    public static double CalculateTotalInterestPaid(double loanAmount, double annualInterestRate, int loanTermYears)
    {
        double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
        int totalPayments = loanTermYears * 12;
        double totalInterestPaid = (monthlyRepayment * totalPayments) - loanAmount;
        return totalInterestPaid;
    }

    // Method to calculate total amount paid over the life of the loan
    public static double CalculateTotalAmountPaid(double loanAmount, double annualInterestRate, int loanTermYears)
    {
        double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
        int totalPayments = loanTermYears * 12;
        double totalAmountPaid = monthlyRepayment * totalPayments;
        return totalAmountPaid;
    }

    // Method to generate amortization schedule
    public static List<AmortizationScheduleEntry> GenerateAmortizationSchedule(double loanAmount, double annualInterestRate, int loanTermYears)
    {
        List<AmortizationScheduleEntry> amortizationSchedule = new List<AmortizationScheduleEntry>();
        double monthlyInterestRate = annualInterestRate / 12 / 100;
        double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
        double remainingBalance = loanAmount;

        for (int i = 1; i <= loanTermYears * 12; i++)
        {
            double interestPaid = remainingBalance * monthlyInterestRate;
            double principalPaid = monthlyRepayment - interestPaid;
            remainingBalance -= principalPaid;

            if (remainingBalance < 0)
            {
                remainingBalance = 0; // Ensure remaining balance doesn't go negative
            }

            AmortizationScheduleEntry entry = new AmortizationScheduleEntry
            {
                PaymentNumber = i,
                PaymentAmount = monthlyRepayment,
                InterestPaid = interestPaid,
                PrincipalPaid = principalPaid,
                RemainingBalance = remainingBalance
            };

            amortizationSchedule.Add(entry);
        }

        return amortizationSchedule;
    }

    // Main method for testing
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Mortgage Calculator!");

        Console.Write("Enter the loan amount: ");
        double loanAmount = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the annual interest rate (DO NOT INCLUDE THE % ITS ALLREADY INCLUDED): ");
        double annualInterestRate = Convert.ToDouble(Console.ReadLine());

        int loanTermYears;
        bool validInput = false;

        do
        {
            Console.Write("Enter the loan term in years: ");
            string loanTermInput = Console.ReadLine();

            if (int.TryParse(loanTermInput, out loanTermYears) && loanTermYears > 0)
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid loan term (positive integer).");
            }
        } while (!validInput);

        // Calculate monthly repayment
        double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
        Console.WriteLine($"Monthly Repayment: ${Math.Round(monthlyRepayment)}");

        // Calculate total interest paid
        double totalInterestPaid = CalculateTotalInterestPaid(loanAmount, annualInterestRate, loanTermYears);
        Console.WriteLine($"Total Interest Paid: ${Math.Round(totalInterestPaid)}");

        // Calculate total amount paid
        double totalAmountPaid = CalculateTotalAmountPaid(loanAmount, annualInterestRate, loanTermYears);
        Console.WriteLine($"Total Amount Paid: ${Math.Round(totalAmountPaid)}");

        // Generate amortization schedule
        var amortizationSchedule = GenerateAmortizationSchedule(loanAmount, annualInterestRate, loanTermYears);
        foreach (var entry in amortizationSchedule)
        {
            Console.WriteLine($"Payment {entry.PaymentNumber}: Amount: ${Math.Round(entry.PaymentAmount)}, Interest: ${Math.Round(entry.InterestPaid)}, Principal: ${Math.Round(entry.PrincipalPaid)}, Remaining Balance: ${Math.Round(entry.RemainingBalance)}");
        }
    }
}

// Class to represent each entry in the amortization schedule
public class AmortizationScheduleEntry
{
    public int PaymentNumber { get; set; }
    public double PaymentAmount { get; set; }
    public double InterestPaid { get; set; }
    public double PrincipalPaid { get; set; }
    public double RemainingBalance { get; set; }
}
