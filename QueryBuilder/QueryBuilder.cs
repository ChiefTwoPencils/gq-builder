using System.Collections.Generic;
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

        public QueryBuilder In() => Append("in");

        public QueryBuilder InV() => Append("inV");

        public QueryBuilder InE() => Append("inE");

        public QueryBuilder Out() => Append("out");

        public QueryBuilder OutV() => Append("outV");

        public QueryBuilder OutE() => Append("outE");

        public QueryBuilder HasLabel(string label) => AppendQuoted("hasLabel", label);

        public QueryBuilder As(string label) => AppendQuoted("as", label);

        public QueryBuilder Select(string label) => AppendQuoted("select", label);

        public QueryBuilder Select(string label, params string[] labels)
        {
            var sb = new StringBuilder(Q.Quote(label));
            var @params = labels
                .Aggregate(sb, (builder, s) => builder.Append($", {Q.Quote(s)}"));
            return Append("select", @params.ToString());
        }

        public QueryBuilder Bind(IEnumerable<string> keys) 
            => keys.Aggregate(
                    Builder.ToString(),
                    (query, key) => query.Replace($"'{key}'", key));
        
        public override string ToString() => Builder.ToString();
        
        private QueryBuilder Append(string step, string value = "")
        {
            Builder.Append($".{Q.Append(step, value)}");
            return this;
        }

        private QueryBuilder AppendQuoted(string step, string value) => Append(step, Q.Quote(value));

        public static implicit operator string(QueryBuilder qb) => qb.ToString();
        public static implicit operator QueryBuilder(string s) => new QueryBuilder(s);
    }

    public static class Q
    {
        private static QueryBuilder Builder(string step, string value = "")
            => new QueryBuilder(Append(step, value));
        public static QueryBuilder V() => Builder("V");
        public static QueryBuilder V(long id) => Builder("V", id.ToString());

        internal static string Append(string step, string value) => $"{step}({value})";
        internal static string Quote(string value) => $"'{value}'";
    }
}