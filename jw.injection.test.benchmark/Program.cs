using BenchmarkDotNet.Running;
using jw.injection.test.benchmark;

var result = BenchmarkRunner.Run<ContainerBuilderTests>();