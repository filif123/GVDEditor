using System.Collections.Generic;

namespace TabTabCompiler.Components.AST
{
    public class Block : Statement
    {
        public List<Statement> Statements;

        public Block()
        {
            Statements = new List<Statement>();
        }

        public void AddStmt(Statement statement)
        {
            Statements.Add(statement);
        }
    }

    /// <summary>
    ///     Zakladna trieda obalujuca TabTab obsahujuci podmienky
    /// </summary>
    public class ConditionTypeBlock : Block
    {
    }

    /// <summary>
    ///     Zakladna trieda obalujuca TabTab, v ktorom sa prevadzaju retazce a kategorie vlakov s pismami tabul
    /// </summary>
    public class SetFontsTypeBlock : Block
    {
    }

    /// <summary>
    ///     Zakladna trieda obalujuca TabTab, v ktorom sa prevadzaju akekolvek texty do tabul na ine
    /// </summary>
    public class ReplaceTextTypeBlock : Block
    {
        public ViewValuesToken Token;
        public new List<ReplaceTextStmt> Statements;

        public ReplaceTextTypeBlock()
        {
            Statements = new List<ReplaceTextStmt>();
        }
    }

    public class ConditionStmtsBlock : Block
    {
        public CommaToken StartToken;
        public CommaToken EndToken;
    }
}