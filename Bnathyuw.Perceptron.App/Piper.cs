using System;

namespace Bnathyuw.Perceptron.App
{
    public static class Piper
    {
        public static TOut Pipe<TIn, TOut>(this TIn input, Func<TIn, TOut> mapper) => mapper(input);
    }
}