using PlainElastic.Net.Utils;

namespace PlainElastic.Net.Queries
{
    /// <summary>
    /// Matches documents that have fields that contain a term (not analyzed). The term query maps to Lucene TermQuery
    /// see http://www.elasticsearch.org/guide/reference/query-dsl/term-query.html
    /// </summary>
    public class TermQuery<T> : FieldQueryBase<T, TermQuery<T>>
    {
        private bool hasValue;


        public TermQuery<T> Value(object value)
        {
            if (value == null)
                return this;

            return Value(value.ToString());
        }

        public TermQuery<T> Value(string value)
        {
            if (!value.IsNullOrEmpty())
            {
                RegisterJsonPart("'value': {0}".AltQuoteF(value.LowerAndQuotate()));
                hasValue = true;
            }

            return this;
        }


        public TermQuery<T> Boost(double boost)
        {
            RegisterJsonPart("'boost': {0}".AltQuoteF(boost.AsString()) );
            
            return this;
        }


        protected override bool HasRequiredParts()
        {
            return hasValue;
        }

        protected override string ApplyJsonTemplate(string body)
        {
            if (RegisteredField.IsNullOrEmpty())
                return "{{ 'term': {{ {0} }} }}".AltQuoteF(body);

            return "{{ 'term': {{ {0}: {{ {1} }} }} }}".AltQuoteF(RegisteredField, body);
        }
        
    }
}