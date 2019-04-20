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
    }
}