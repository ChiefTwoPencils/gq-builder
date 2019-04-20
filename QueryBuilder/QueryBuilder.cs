using System.Linq;
using System.Text;

namespace QueryBuilder
{
    public class QueryBuilder
    {
        private StringBuilder Builder { get; }

        private QueryBuilder() : this("g") { }

        internal QueryBuilder(string firstStep)
        {
            Builder = new StringBuilder(firstStep);
        }
        
        public static QueryBuilder Create() => new QueryBuilder();

        public static QueryBuilder Empty()
        {
            var qb = Create();
            qb.Builder.Clear();
            return qb;
        }

        public QueryBuilder V() => Append("V");

        public QueryBuilder V(long id) => Append("V", id.ToString());

        public QueryBuilder HasLabel(string label) => Append($"hasLabel", label);

        public QueryBuilder As(string label) => Append($"as", label);

        public QueryBuilder Select(string label) => Append($"select", label);

        public QueryBuilder Select(string label, params string[] labels)
        {
            var @params = labels
                .Aggregate(new StringBuilder(label), (builder, s) => builder.Append($", {s}"));
            return Select(@params.ToString());
        }

        public override string ToString() => Builder.ToString();
        
        private QueryBuilder Append(string step, string value = "")
        {
            Builder.Append($".{Q.Append(step, value)}");
            return this;
        }

        public static implicit operator string(QueryBuilder qb) => qb.ToString();
    }

    public static class Q
    {
        private static QueryBuilder Builder(string step, string value = "")
            => new QueryBuilder(Append(step, value));
        public static QueryBuilder V() => Builder("V");
        public static QueryBuilder V(long id) => Builder("V", id.ToString());

        public static string Append(string step, string value) => $"{step}({value})";
    }
}