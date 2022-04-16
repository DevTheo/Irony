using Irony.Parsing;

namespace Irony.Samples.SQL
{
    public class PgSqlGrammar : Grammar
    {
        public PgSqlGrammar() : base(false)
        {
            //SQL is case insensitive
            //Terminals
            var comment = new CommentTerminal("comment", "/*", "*/");
            var lineComment = new CommentTerminal("line_comment", "--", "\n", "\r\n");
            NonGrammarTerminals.Add(comment);
            NonGrammarTerminals.Add(lineComment);

            var number = new NumberLiteral("number");

            var string_literal = new StringLiteral("string", "'", StringOptions.AllowsDoubledQuote);
            var fieldName_simple = TerminalFactory.CreateSqlExtIdentifier(this, "fieldName_simple"); //covers normal identifiers (abc) and quoted id's ([abc d], "abc d")
            var comma = ToTerm(",");
            var dot = ToTerm(".");
            var star = ToTerm("*");
            var NULL = ToTerm("NULL");
            var NOT = ToTerm("NOT");
            var WITH = ToTerm("WITH");
            var INTO = ToTerm("INTO");
            var SELECT = ToTerm("SELECT");
            var FROM = ToTerm("FROM");
            var AS = ToTerm("AS");
            var COUNT = ToTerm("COUNT");
            var JOIN = ToTerm("JOIN");
            var WHERE = ToTerm("WHERE");
            var GROUP = ToTerm("GROUP");
            var HAVING = ToTerm("HAVING");
            var ORDER = ToTerm("ORDER");


            var DISTINCT = new StringLiteral("DISTINCT", "DISTINCT");
            var ALL = new StringLiteral("ALL", "ALL");


            var stmtList = new NonTerminal("stmtList");

            var selectStatement = new NonTerminal("selectStatement");
            var selectFieldListStatement = new NonTerminal("selectFieldListStatement");

            var fromOrLateralStatement = new NonTerminal("fromOrLateralStatement");
            var joinOrLateralStatement = new NonTerminal("joinOrLateralStatement");
            var whereStatement = new NonTerminal("whereStatement");
            var groupByStatement = new NonTerminal("groupByStatement");
            var havingStatement = new NonTerminal("havingStatement");
            var orderByStatement = new NonTerminal("orderByStatement");


            var simple_conditionStatement = new NonTerminal("simple_conditionStatement");

            var complex_conditionStatement = new NonTerminal("complex_conditionStatement");
            var subQueryStatement = new NonTerminal("subQueryStatement");


            this.Root = stmtList;

            stmtList.Rule = selectStatement | "GO";

            selectStatement.Rule = SELECT + (ALL | DISTINCT) +
                                   selectFieldListStatement +
                                   fromOrLateralStatement; /* +
                                       whereStatement +
                                       groupByStatement +
                                       havingStatement +
                                       orderByStatement;*/

            //var fieldPlusCommaStatement = new NonTerminal("fieldPlusCommaStatement");

            //selectFieldListStatement.Rule = ()

            subQueryStatement.Rule = "(" + selectStatement + ")";

        }
    }
}
