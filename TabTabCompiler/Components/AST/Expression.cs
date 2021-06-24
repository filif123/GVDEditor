using System;
using System.Collections.Generic;

namespace TabTabCompiler.Components.AST
{
    public abstract class Expression
    {
        public Operation Operation;
    }

    // <string> := " <string_elem>* "
    public class StringLiteralExpr : Expression
    {
        public StringLiteralToken Token;
        public string Value;
        public SetFontExpr SetFont;

        public StringLiteralExpr(StringLiteralToken token)
        {
            Token = token;
            Value = token.Text;
        }
    }

    public class SetFontExpr : Expression
    {
        public StartFontSetToken StartToken;
        public EndFontSetToken EndToken;
        public NumberLiteralExpr FontId;
    }

    public class NumberLiteralExpr : Expression
    {
        public NumberLiteralToken Token;
        public int Value;

        public NumberLiteralExpr(NumberLiteralToken token)
        {
            Token = token;
            Value = Convert.ToInt32(token.Text);
        }
    }

    public class IdentifierNameExpr : Expression
    {
        public IdentifierToken Token;
        public string Name;

        public IdentifierNameExpr(IdentifierToken token)
        {
            Token = token;
            Name = token.Text;
        }
    }

    public class ArgumentList
    {
        public OpenParenToken Start;
        public List<Argument> Arguments;
        public CloseParenToken End;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ArgumentList()
        {
            Arguments = new List<Argument>();
        }
    }

    public class Argument
    {
        public Operation Operation;
        public Expression Identifier;
    }

    public class InvocationExpr : Expression
    {
        public IdentifierNameExpr IdentifierName;
        public ArgumentList ArgumentList;
    }

    // <arith_expr> := <expr> <arith_op> <expr>
    public class BinExpr : Expression
    {
        public Expression Left;
        public Expression Right;
        public OperatorType Op;
        public OperatorToken Token;

        public BinExpr(OperatorToken token)
        {
            Op = token.Operator;
            Token = token;
        }
    }
}