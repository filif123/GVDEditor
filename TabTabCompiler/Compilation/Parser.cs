using System.Collections.Generic;
using System.Linq.Expressions;
using TabTabCompiler.Components;
using TabTabCompiler.Components.AST;
using TabTabCompiler.Tools;
using Expression = TabTabCompiler.Components.AST.Expression;

namespace TabTabCompiler.Compilation
{
    public class Parser
    {
        private readonly TokenList tokens;
        private Stack<Block> blocks;
        public List<Statement> Tree;

        public Parser(TokenList tokens)
        {
            this.tokens = tokens;
            blocks = new Stack<Block>();
            Tree = new List<Statement>();
            Parse();
        }

        private void Parse()
        {
            bool running = true;
            Block currentBlock = null;

            while (running)
            {
                var tok = tokens.Peek();

                switch (tok)
                {
                    case IdentifierToken:
                        var fun = Function.Parse(tok.Text);
                        if (fun != null)
                        {
                            var stmt = new ConditionStmt();
                            var conds = new List<Expression>();
                            stmt.Condition = ParseConditionExpr(null);
                        }
                        else
                        {
                            var trtype = TrType.Parse(tok.Text);
                            if (trtype != null)
                            {
                                //context = ContextType.SetFont;
                            }
                            else
                            {
                                Compiler.Outputs.Add(new ErrorOutput(tok.Info, "Neocakavany token"));
                                running = false;
                            }
                        }
                        break;
                    case OpenParenToken:
                        //context = ContextType.Condition;
                        break;
                    case StartFontSetToken:
                        //context = ContextType.SetFont;
                        break;
                    case NumberLiteralToken:
                        //context = ContextType.Condition;
                        break;
                    case StringLiteralToken:
                        //context = ContextType.SetFont;
                        break;
                    case ViewValuesToken:
                        //context = ContextType.ReplaceText;
                        break;
                    case EndOfTheFileToken:
                        running = false;
                        break;
                    default:
                        Compiler.Outputs.Add(new ErrorOutput(tok.Info,"Neocakavany token"));
                        break;
                }
            }
        }

        private Expression ParseConditionExpr(Expression prev)
        {
            var token = tokens.Peek();
            switch (token)
            {
                case IdentifierToken:
                    if (prev is null or BinExpr)
                    {
                        var invocation = ParseInvocationExpr();
                        return ParseConditionExpr(invocation);
                    }
                    //error
                    return null;
                case NumberLiteralToken:
                    if (prev is null or BinExpr)
                    {
                        var nlx = ParseNumericLiteralExpr();
                        return ParseConditionExpr(nlx);
                    }
                    //error
                    return null;
                case OperatorToken:
                    if (prev is null or BinExpr)
                    {
                        //error
                    }
                    var opex = ParseBinExpr();
                    opex.Left = prev;
                    opex.Right = ParseConditionExpr(opex);
                    return opex;
                case OpenParenToken:
                    tokens.GetToken();
                    return ParseConditionExpr(prev);
                case CloseParenToken:
                    tokens.GetToken();
                    return prev;
                case CommaToken:
                    tokens.GetToken();
                    return prev;
                default:
                    Compiler.Outputs.Add(new ErrorOutput(token.Info, $"Neocakavany token {token}"));
                    return null;
            }
        }

        private InvocationExpr ParseInvocationExpr()
        {
            var ident = tokens.GetToken() as IdentifierToken;
            var expr = new InvocationExpr();
            var identex = new IdentifierNameExpr(ident);
            expr.IdentifierName = identex;
            var next = tokens.PeekNext();
            if (next is OpenParenToken a)
            {
                var argList = new ArgumentList();
                expr.ArgumentList = argList;
                argList.Start = a;
                tokens.Position += 2;
                var next2 = tokens.Peek();
                Argument arg;
                switch (next2)
                {
                    case CloseParenToken t:
                        argList.End = t;
                        break;
                    case IdentifierToken identifier:
                        arg = new Argument();
                        arg.Identifier = new IdentifierNameExpr(identifier);
                        argList.Arguments.Add(arg);
                        break;
                    case StringLiteralToken slt:
                        arg = new Argument();
                        arg.Identifier = new StringLiteralExpr(slt);
                        argList.Arguments.Add(arg);
                        break;
                    case NumberLiteralToken nlt:
                        arg = new Argument();
                        arg.Identifier = new NumericLiteralExpr(nlt);
                        argList.Arguments.Add(arg);
                        break;
                    default:
                        Compiler.Outputs.Add(new ErrorOutput(next2.Info, "Ocakavany identifikator, retazec, cislo alebo prava zatvorka ')'"));
                        break;
                }
            }

            return expr;
        }

        private BinExpr ParseBinExpr()
        {
            var token = tokens.GetToken() as OperatorToken;
            return new BinExpr(token);
        }

        private NumericLiteralExpr ParseNumericLiteralExpr()
        {
            var token = tokens.GetToken() as NumberLiteralToken;
            return new NumericLiteralExpr(token);
        }
    }
}
