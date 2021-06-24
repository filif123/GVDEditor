using System.Collections.Generic;

namespace TabTabCompiler.Components.AST
{
    public abstract class Statement
    {
        public Operation Operation;
    }

    /* <expr> := <string> * | <int> * | <arith_expr> * | <ident> */

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

    public class ConditionBlock : Block
    {
        public CommaToken StartToken;
        public CommaToken EndToken;
    }

    public class ConditionStmt : Statement
    {
        public Expression Condition;
        public ConditionBlock Block;
        public ConditionStmt Next;
    }

    public class ReplaceTextBlock : Block
    {
        public new List<ReplaceText> Statements;

        public ReplaceTextBlock()
        {
            Statements = new List<ReplaceText>();
        }
    }

    public class SetFontBlock : Block
    {
    }

    //STATEMENTS 

    // <new_ident> = <old_ident>
    public class ReplaceText : Statement
    {
        //public 
        public string OldIdent;
        public string NewIdent;

        public ReplaceText(string oldIdent, string newIdent)
        {
            OldIdent = oldIdent;
            NewIdent = newIdent;
        }
    }

    
}
