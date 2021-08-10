using BankAccountFormatter.Types;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaParser = BankAccountFormatter.Parser.Parser;

namespace BankAccountFormatter.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class BankAccountParserBenchmark
    {
        [Benchmark]
        public void BenchmarkParseSimpleFormat()
        {
            BaParser.parse("p-a/b");
        }
    }
}
