using Irony.Parsing;
using Irony.Samples.SQL;
using Xunit;

namespace Irony.Samples.Core.Tests.Sql
{
    public class SqlSelectGrammarTests
    {
        private readonly SqlSelectGrammar _sut;
        private readonly LanguageData _language;

        public SqlSelectGrammarTests()
        {
            _sut = new SqlSelectGrammar();
            _language = new LanguageData(_sut);
        }

        private Parser GetParser()
        {
            return new Parser(_language);
        }

        [Theory]
        [InlineData("  Select * FROM TableName;")]
        [InlineData("  Select * \n  FROM TableName;")]
        [InlineData("  Select t.* FROM TableName;")]
        [InlineData("  Select f_id FROM TableName;")]
        [InlineData("  Select t.F_Id FROM TableName;")]
        public void Select_HappyPath(string sql)
        {
            var parser = GetParser();
            ParseTree? result = parser.Parse(sql);

            Assert.NotNull(result);
            Assert.Empty(result.ParserMessages);
            Assert.False(parser.Context.HasErrors);
        }

    }
}