using Irony.Parsing;

namespace Irony.Samples.SQL
{
    [Language("SqlSelect", "92", "SQL Select grammar")]
    public class SqlSelectGrammar : Grammar
    {
        // Standard stuff
        public readonly Terminal Empty = new Terminal("EMPTY");

        public SqlSelectGrammar() : base(false)
        {
            /*
                SELECT 
                    {[tableAlias.][*] || [tableAlias.]field [AS alias]}+, etc
                FROM {<tableName> [AS alias]}+
                [INNER, LEFT, RIGHT, CROSS] JOIN <tableName> [AS alias]
                    ON whereExpression 
                      {[AND | OR] whereExpression}+
                       ...
                [INTO] <tableName>
                [WHERE whereExpression]
                      {[AND | OR] whereExpression}+
                [GROUP BY {[tableAlias.]field}+]
                [HAVING whereExpression]
                [ORDER BY {[tableAlias.]field}+] [ASC | DESC]

             */


            var number = new NumberLiteral("number");
            var string_literal = new StringLiteral("string", "'", StringOptions.AllowsDoubledQuote);


            var SELECT = ToTerm("SELECT");
            var comma = ToTerm(",");
            var semiColon = ToTerm(";");
            var dot = new NonTerminal("dot");
            dot.Rule = ".";
            var asterisk = new NonTerminal("asterisk");
            asterisk.Rule = "*";
            var openParens = ToTerm("(");
            var closeParens = ToTerm(")");


            var AS = ToTerm("AS");
            var FROM = ToTerm("FROM");
            var INNER = ToTerm("INNER");
            var LEFT = ToTerm("LEFT");
            var RIGHT = ToTerm("RIGHT");
            var CROSS = ToTerm("CROSS");
            var JOIN = ToTerm("JOIN");

            var INTO = ToTerm("INTO");
            var WHERE = ToTerm("WHERE");
            var AND = ToTerm("AND");
            var OR = ToTerm("OR");
            var GROUP = ToTerm("GROUP");
            var BY = ToTerm("BY");
            var HAVING = ToTerm("HAVING");
            var ORDER = ToTerm("ORDER");


            var EQUALS = ToTerm("=");
            var NOT_EQUALS = ToTerm("<>");
            var LIKE = ToTerm("LIKE");
            var GT = ToTerm(">");
            var GTE = ToTerm(">=");
            var LT = ToTerm("<");
            var LTE = ToTerm("<=");

            var IS = ToTerm("LIKE");


            var ASC = ToTerm("ASC");

            var DESC = ToTerm("DESC");


            var query = new NonTerminal("query");

            var selectQuery = new NonTerminal("selectQuery");

            var selectStatement = new NonTerminal("selectStatement");
            var selectSimpleStatement = new NonTerminal("selectSimpleStatement");

            var fromStatement = new NonTerminal("fromStatement");
            var whereStatement = new NonTerminal("whereStatement");
            var orderByStatement = new NonTerminal("orderByStatement");
            var selectFromStatement = new NonTerminal("selectFromStatement");
            var selectFromStatementSimple = new NonTerminal("selectFromStatementSimple");
            var selectWhereStatement = new NonTerminal("selectWhereStatement");
            var selectWhereOrderByStatement = new NonTerminal("selectWhereOrderByStatement");

            var tablesSelectors = new NonTerminal("tablesSelectors");
            var whereConditions = new NonTerminal("whereConditions");
            var orderByClause = new NonTerminal("orderByClause");


            // SELECT query templates
            selectFromStatementSimple.Rule = selectSimpleStatement +
                                   fromStatement + semiColon;
            selectFromStatement.Rule = selectStatement +
                                   fromStatement + semiColon;

            selectWhereStatement.Rule = selectStatement +
                                   fromStatement +
                                   whereStatement + semiColon;

            selectWhereOrderByStatement.Rule = selectStatement +
                                   fromStatement +
                                   whereStatement +
                                   orderByStatement + semiColon;

            // setup field selection list
            // TODO: add group by variations (with aggregates
            var fieldIdentifier = TerminalFactory.CreateSqlExtIdentifier(this, "fieldIdentifier");
            var tableAlias = TerminalFactory.CreateSqlExtIdentifier(this, "tableAlias");

            var tableDot = new NonTerminal("tableDot");
            tableDot.Rule = tableAlias + dot;

            var columnTableAlias = new NonTerminal("columnTableAlias");
            columnTableAlias.Rule = Empty | tableDot;

            var columnReference = new NonTerminal("columnReference");
            columnReference.Rule = columnTableAlias + fieldIdentifier;
            var asteriskOrField = new NonTerminal("asteriskOrField");

            var asteriskField = new NonTerminal("asteriskField");
            asteriskField.Rule = tableDot + asteriskOrField;
            asteriskOrField.Rule = fieldIdentifier | "*";

            // SELECT ...
            var selectColumnReference = new NonTerminal("selectColumnReference");
            var selectColumnItemList = new NonTerminal("selectColumnItemList");

            selectStatement.Rule = SELECT + selectColumnItemList;

            selectColumnReference.Rule = columnReference | asteriskField;

            selectColumnItemList.Rule = MakePlusRule(selectColumnItemList, comma, selectColumnReference);

            // Simple
            var selectColumnSimpleItemList = new NonTerminal("selectColumnSimpleItemList");

            var selectColumnSimpleReference = new NonTerminal("selectColumnSimpleReference");

            selectColumnSimpleReference.Rule = fieldIdentifier | asterisk;
            selectColumnSimpleItemList.Rule = MakePlusRule(selectColumnSimpleItemList, comma, selectColumnSimpleReference);

            selectStatement.Rule = SELECT + selectColumnSimpleItemList;


            // FROM
            fromStatement.Rule = FROM + tablesSelectors;

            // Table Selectors (start with simple)
            // FROM tableName, tableName as t1, tableName t2 
            var fromTableList = new NonTerminal("fromTableList");
            var fromTableItemAs = new NonTerminal("fromTableItemAs");
            var fromTableItemT = new NonTerminal("fromTableItemT");
            var fromTableName = TerminalFactory.CreateSqlExtIdentifier(this, "fromTableName");
            var fromTableAlias = TerminalFactory.CreateSqlExtIdentifier(this, "fromTableAlias");

            fromTableList.Rule = fromTableName | fromTableItemAs | fromTableItemT;
            fromTableItemAs.Rule = fromTableName + AS + fromTableAlias;
            fromTableItemT.Rule = fromTableName + AS + fromTableAlias;

            tablesSelectors.Rule = MakePlusRule(tablesSelectors, comma, fromTableList);

            // Where expressions (again start simple)
            whereStatement.Rule = WHERE + whereConditions;
            var whereAndOr = new NonTerminal("whereAndOr");
            whereAndOr.Rule = AND | OR;
            var whereConstant = new NonTerminal("whereConstant");
            whereConstant.Rule = number | string_literal | "false" | "true" | "null";

            var whereOperators = new NonTerminal("whereOperators");
            whereOperators.Rule = EQUALS | NOT_EQUALS | LIKE | IS | GT | GTE | LT | LTE;

            var whereFieldOrConstant = new NonTerminal("whereFieldOrConstant");
            whereFieldOrConstant.Rule = whereConstant | columnReference;

            var whereSingleCondition = new NonTerminal("whereSingleFieldOrConstant");
            // Trying this...(with the  (..) |
            whereSingleCondition.Rule = ("(" + whereFieldOrConstant + whereOperators + whereFieldOrConstant) |
                                        (whereFieldOrConstant + whereOperators + whereFieldOrConstant) |
                                        (whereFieldOrConstant + whereOperators + whereFieldOrConstant + ")") |
                                        ("(" + whereFieldOrConstant + whereOperators + whereFieldOrConstant + ")");
            whereConditions.Rule = MakeStarRule(whereSingleCondition, whereAndOr, whereSingleCondition);


            // Order by
            orderByStatement.Rule = ORDER + BY + orderByClause;
            var orderByStatementAscOrDesc = new NonTerminal("orderByStatementAscOrDesc");
            orderByStatementAscOrDesc.Rule = ASC | DESC;

            var orderBySingleField = new NonTerminal("orderBySingleField");
            orderBySingleField.Rule = (columnReference + orderByStatementAscOrDesc) |
                                      columnReference;

            orderByClause.Rule = MakePlusRule(orderBySingleField, comma, orderBySingleField);

            // Combine all the select variants together
            selectQuery.Rule = selectFromStatement; // | selectWhereOrderByStatement | selectWhereStatement | selectStatement;

            // Set the Grammar
            query.Rule = selectQuery | query;
            this.Root = query;
        }

    }
}//namespace
