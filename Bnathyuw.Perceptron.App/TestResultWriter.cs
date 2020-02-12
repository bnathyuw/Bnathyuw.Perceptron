using System;

namespace Bnathyuw.Perceptron.App
{
    internal static class TestResultWriter
    {
        public static void OutputResults(string title, TestResults testResults)
        {
            Console.WriteLine(title);
            Console.WriteLine($"Confusion Matrix:");
            Console.WriteLine($"+----------------------+----------+----------+");
            Console.WriteLine($"|                      | Actual T | Actual F |");
            Console.WriteLine($"|                      | {testResults.P,8} | {testResults.N,8} |");
            Console.WriteLine($"+----------------------+----------+----------+");
            Console.WriteLine(
                $"| Predicted T {testResults.PP,8} | {testResults.TP,8} | {testResults.FP,8} |");
            Console.WriteLine($"+----------------------+----------+----------+");
            Console.WriteLine(
                $"| Predicted F {testResults.PN,8} | {testResults.FN,8} | {testResults.TN,8} |");
            Console.WriteLine($"+----------------------+----------+----------+");
            Console.WriteLine($"Sensitivity/Recall/Hit Rate/True Positive Rate (TPR): {testResults.TPR}");
            Console.WriteLine($"Specificity/Selectivity/True Negative Rate (TNR): {testResults.TNR}");
            Console.WriteLine($"Precision/Positive Predictive Value (PPV): {testResults.PPV}");
            Console.WriteLine($"NegativePredictiveValue (NPV): {testResults.NPV}");
            Console.WriteLine($"Miss Rate / False Negative Rate (FNR): {testResults.FNR}");
            Console.WriteLine($"Fall-Out/False Positive Rate (FPR): {testResults.FPR}");
            Console.WriteLine($"False Discover Rate (FDR): {testResults.FDR}");
            Console.WriteLine($"False Omission Rate (FOR): {testResults.FOR}");
            Console.WriteLine($"Threat Score (TS)/Critical Success Index (CSI): {testResults.TS}");
            Console.WriteLine($"Accuracy (ACC): {testResults.ACC}");
            Console.WriteLine($"F1 Score: {testResults.F1}");
            Console.WriteLine($"Matthews Correlation Coefficient (MCC): {testResults.MCC}");
            Console.WriteLine($"Informedness/Bookmaker Informedness (BM): {testResults.BM}");
            Console.WriteLine($"Markedness (MK): {testResults.MK}");
        }
    }
}