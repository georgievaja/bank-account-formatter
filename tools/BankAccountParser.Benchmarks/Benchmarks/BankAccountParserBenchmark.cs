using BankAccountParser.Types;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaParser = BankAccountParser.Parser.Parser;

namespace BankAccountParser.Benchmarks.Benchmarks
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
