using QueryBuilder;
using Xunit;

namespace QueryBuilderTests
{
    public class QTests
    {
        [Fact]
        public void VTest_ShouldReturnEmptyV()
        {
            const string expected = "V()";
            var actual = Q.V();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VTest_ShouldReturnVWithId()
        {
            const int id = 42;
            var expected = $"V({id.ToString()})";
            var actual = Q.V(id);
            Assert.Equal(expected, actual);
        }
    }
}