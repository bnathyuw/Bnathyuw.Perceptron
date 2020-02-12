using System;
using System.Linq;

namespace Bnathyuw.Perceptron.App
{
    internal class TestResults
    {
        public TestResults((double actual, double predicted)[] results) => _results = results;

        private readonly (double actual, double predicted)[] _results;
        
        public double P => _results.Count(x => x.actual.IsMatch());
        public double N => _results.Count(x => !x.actual.IsMatch());

        public double PP => _results.Count(x => x.predicted.IsMatch());
        public double PN => _results.Count(x => !x.predicted.IsMatch());

        public double TP => _results.Count(x => x.actual.IsMatch() && x.predicted.IsMatch());
        public double TN => _results.Count(x => !x.actual.IsMatch() && !x.predicted.IsMatch());
        public double FP => _results.Count(x => !x.actual.IsMatch() && x.predicted.IsMatch());
        public double FN => _results.Count(x => x.actual.IsMatch() && !x.predicted.IsMatch());

        public double TPR => TP /
                             P;

        public double TNR => TN /
                             N;

        public double PPV => TP /
                             (TP + FP);

        public double NPV => TN /
                             (TN + FN);

        public double FNR => FN /
                             P;

        public double FPR => FP /
                             N;

        public double FDR => FP /
                             (FP + TN);

        public double FOR => FN /
                             (FN + TN);

        public double TS => TP /
                            (TP + FN + FP);

        public double ACC => (TP + TN) /
                             (P + N);

        public double F1 => 2d * (PPV * TPR) /
                            (PPV + TPR);

        public double MCC => (TP * TN - FP * FN) /
                             Math.Sqrt((TP + FP) * (TP + FN) * (TN + FP) * (TN + FN));

        public double BM => TPR + TNR - 1;

        public double MK => PPV + NPV - 1;
    }
}