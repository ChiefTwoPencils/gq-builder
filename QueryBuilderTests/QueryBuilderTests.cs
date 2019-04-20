using System.Collections.Generic;
using System.Linq;
using static QueryBuilder.QueryBuilder;
using Xunit;

namespace QueryBuilderTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void EmptyTest_ShouldReturnEmptyString()
        {
            var expected = string.Empty;
            var actual = Empty();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void CreateTest_ShouldReturnG()
        {
            const string expected = "g";
            var actual = Create();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void VTest_ShouldReturnEmptyV()
        {
            const string expected = "g.V()";
            var actual = Create().V();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VTest_ShouldReturnVWithId()
        {
            const int id = 42;
            var expected = $"g.V({id})";
            var actual = Create().V(id);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InTest_ShouldReturnEmptyIn()
        {
            const string expected = "g.in()";
            var actual = Create()
                .In();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OutTest_ShouldReturnEmptyOut()
        {
            const string expected = "g.out()";
            var actual = Create()
                .Out();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InVTest_ShouldReturnEmptyInV()
        {
            const string expected = "g.inV()";
            var actual = Create()
                .InV();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OutVTest_ShouldReturnEmptyOutV()
        {
            const string expected = "g.outV()";
            var actual = Create()
                .OutV();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InETest_ShouldReturnEmptyInE()
        {
            const string expected = "g.inE()";
            var actual = Create()
                .InE();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OutETest_ShouldReturnEmptyOutE()
        {
            const string expected = "g.outE()";
            var actual = Create()
                .OutE();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HasLabelTest_ShouldReturnHasWithLabel()
        {
            const string label = "label";
            var expected = $"g.hasLabel('{label}')";
            var actual = Create().HasLabel(label);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VHasLabelChainTest_ShouldReturnVAndHasChainedFromG()
        {
            const int id = 42;
            const string label = "label";
            var expected = $"g.V({id}).hasLabel('{label}')";
            var actual = Create()
                .V(id)
                .HasLabel(label);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AsTest_ShouldReturnAsWithLabel()
        {
            const string label = "label";
            var expected = $"g.as('{label}')";
            var actual = Create()
                .As(label);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectTest_ShouldReturnSelectWithSingleParam()
        {
            const string param = "param";
            var expected = $"g.select('{param}')";
            var actual = Create()
                .Select(param);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectTest_ShouldReturnSelectWithMultipleParams()
        {
            var @params = new[] {"first", "second", "third"};
            var args = string.Join(", ", @params
                .Select(s => $"'{s}'")
                .ToArray());
            var expected = $"g.select({args})";
            var actual = Create()
                .Select(@params[0], @params[1], @params[2]);
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
            var actualT = Create()
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
            var actual = Create()
                .Property(prop, val);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PropertyTest_ShouldReturnPropertyWithIntValue()
        {
            const string prop = "prop";
            const int i = 42;
            var expected = $"g.property('{prop}', {i})";
            var actual = Create()
                .Property(prop, i);
            Assert.Equal(expected, actual);
        }
    }
}