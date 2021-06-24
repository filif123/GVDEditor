using System.Collections.Generic;

namespace TabTabCompiler.Components.AST
{
    public abstract class Statement
    {
        public Operation Operation;
    }

    

    public class ReplaceTextStmt : Statement
    {
        public IdentifierNameExpr OldIdent;
        public OperatorToken Op;
        public IdentifierNameExpr NewIdent;
    }

    public class ConditionStmt : Statement
    {
        public Expression Condition;
        public ConditionStmtsBlock Block;
        public ConditionStmt Next;
    }

    public class SetFontStmt : Statement
    {
        public IdentifierNameExpr New;
        public OperatorToken Op;
        public IdentifierNameExpr Old;
    }
}
