using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AnyVsCount
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();
        }
    }

    [ClrJob, MonoJob, CoreJob] // 可以針對不同的 CLR 進行測試
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();

        public TestRunner()
        {
        }

        [Benchmark]
        public void Any() => _test.Any();

        [Benchmark]
        public void Count() => _test.Count();
    }

    public class TestClass
    {
        private readonly List<int> _ints = Enumerable.Range(1, 10000).ToList();
        
        public void Any()
        {
            var result = _ints?.Any();
        }

        public void Count()
        {
            var result = _ints?.Count > 0;
        }
    }
}
