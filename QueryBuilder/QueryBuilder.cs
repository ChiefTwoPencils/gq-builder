using System.Text;

namespace QueryBuilder
{
    public class QueryBuilder
    {
        private StringBuilder Builder { get; }

        private QueryBuilder()
        {
            Builder = new StringBuilder("g");
        }
        
        public static QueryBuilder Create() => new QueryBuilder();

        public QueryBuilder V() => Append(".V()");

        public QueryBuilder V(long id) => Append($".V({id}");

        public QueryBuilder HasLabel(string label) => Append($".hasLabel({label}");

        private QueryBuilder Append(string step)
        {
            Builder.Append(step);
            return this;
        }
    }
}