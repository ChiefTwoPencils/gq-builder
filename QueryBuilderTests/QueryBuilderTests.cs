using System;
using System.Collections.Generic;
using System.Linq;
using static QueryBuilder.QueryBuilder;
using Xunit;

namespace QueryBuilderTests
{
    internal static class T
    {
        internal static void SimpleTest(string expected, Func<string> actual)
            => Assert.Equal(expected, actual());
    }
    public class QueryBuilderTests
    {
        [Fact]
        public void EmptyTest_ShouldReturnEmptyString()
            => T.SimpleTest(string.Empty, () => Empty());

        [Fact]
        public void CreateTest_ShouldReturnG() 
            => T.SimpleTest("g", () => Create());

        [Fact]
        public void GTest_ShouldReturnG()
            => T.SimpleTest("g", () => G()); 

        [Fact]
        public void VTest_ShouldReturnEmptyV()
            => T.SimpleTest("g.V()", () => G().V());

        [Fact]
        public void VTest_ShouldReturnVWithId()
            => T.SimpleTest($"g.V({42})", () => G().V(42));

        [Fact]
        public void InTest_ShouldReturnEmptyIn()
            => T.SimpleTest("g.in()", () => G().In());

        [Fact]
        public void OutTest_ShouldReturnEmptyOut()
            => T.SimpleTest("g.out()", () => G().Out());

        [Fact]
        public void InVTest_ShouldReturnEmptyInV()
            => T.SimpleTest("g.inV()", () => G().InV());

        [Fact]
        public void OutVTest_ShouldReturnEmptyOutV()
            => T.SimpleTest("g.outV()", () => G().OutV());

        [Fact]
        public void InETest_ShouldReturnEmptyInE()
            => T.SimpleTest("g.inE()", () => G().InE());

        [Fact]
        public void OutETest_ShouldReturnEmptyOutE()
            => T.SimpleTest("g.outE()", () => G().OutE());

        [Fact]
        public void HasLabelTest_ShouldReturnHasWithLabel()
        {
            const string label = "label";
            var expected = $"g.hasLabel('{label}')";
            var actual = G().HasLabel(label);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VHasLabelChainTest_ShouldReturnVAndHasChainedFromG()
        {
            const int id = 42;
            const string label = "label";
            var expected = $"g.V({id}).hasLabel('{label}')";
            var actual = G()
                .V(id)
                .HasLabel(label);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AsTest_ShouldReturnAsWithLabel()
        {
            const string label = "label";
            var expected = $"g.as('{label}')";
            var actual = G().As(label);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectTest_ShouldReturnSelectWithSingleParam()
        {
            const string param = "param";
            var expected = $"g.select('{param}')";
            var actual = G().Select(param);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectTest_ShouldReturnSelectWithMultipleParams()
        {
            var @params = new[] {"first", "second", "third"};
            var args = string.Join(
                ", ", 
                @params
                    .Select(s => $"'{s}'")
                    .ToArray());
            var expected = $"g.select({args})";
            var actual = G().Select(@params[0], @params[1], @params[2]);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BindTest_ShouldBindAQuotedHasLabelParam()
        {
            const string label = "label";
            const string @as = "something";
            var dict = new Dictionary<string, object>
            {
                { label, "" },
                { @as, "" }
            };
            const string expected = "g.V().hasLabel(label).as(something)";
            var actualT = G()
                .V()
                .HasLabel(label)
                .As(@as);
            string firstActual = actualT;
            Assert.NotEqual(expected, firstActual);
            var secondActual = actualT.Bind(dict.Keys);
            Assert.Equal(expected, secondActual);
        }

        [Fact]
        public void PropertiesTest_ShouldReturnPropertyWithStringValue()
        {
            const string prop = "prop";
            const string val = "value";
            var expected = $"g.property('{prop}', '{val}')";
            var actual = G().Property(prop, val);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PropertyTest_ShouldReturnPropertyWithIntValue()
        {
            const string prop = "prop";
            const int i = 42;
            var expected = $"g.property('{prop}', {i})";
            var actual = G().Property(prop, i);
            Assert.Equal(expected, actual);
        }
    }
}