using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabTabCompiler.Components;
using TabTabCompiler.Tools;

namespace TabTabCompiler.Compilation
{
    internal class Scanner
    {
        public readonly List<Token> Result;

        internal Scanner(string code)
        {
            var input = new MyStringReader(code);
            Result = new List<Token>(); 
            Scan(input);
        }

        private void Scan(MyStringReader input)
        {
            Token current = null;
            Trivia trivia = null;
            bool newEmptyLine = true;

            while (input.PeekNext() != -1)
            {
                char ch = (char) input.PeekNext();

                if (char.IsWhiteSpace(ch)) {
                    input.Read();
                    switch (ch)
                    {
                        case '\r':
                            if (input.PeekNext() == '\n')
                            {
                                trivia = new EOLDelimiter("\r\n");
                                input.Read();
                                newEmptyLine = true;
                            }
                            else
                            {
                                trivia = new EOLDelimiter("\r");
                                newEmptyLine = true;
                            }
                            break;
                        case '\n':
                            trivia = new EOLDelimiter("\n");
                            newEmptyLine = true;
                            break;
                        case ' ':
                            int len = 0;
                            while (input.PeekNext() == ' ')
                            {
                                len++;
                                input.Read();
                            }

                            trivia = new SpaceDelimiter(len);
                            break;
                        case '\t':
                            int lent = 0;
                            while (input.PeekNext() == '\t')
                            {
                                lent++;
                                input.Read();
                            }

                            trivia = new TabDelimiter(lent);
                            break;
                    }
                }
                else if (ch == '"')
                {
                    var sb = new StringBuilder();
                    input.Read();
                    while ((ch = (char)input.PeekNext()) != '"')
                    {
                        sb.Append(ch);
                        input.Read();
                        if (input.PeekNext() == -1)
                        {
                            break;
                        }
                    }
                    input.Read();

                    current = new StringLiteralToken(sb.ToString(),input);
                    SetTrivia();
                    Result.Add(current);
                }
                else if (ch == ';')
                {
                    input.Read();
                    if (newEmptyLine)
                    {
                        trivia = new Comment(input.ReadLine());
                        Result.LastOrDefault()?.DelimitersBefore.AddIfNotNull(trivia);
                        newEmptyLine = false;
                    }
                    else
                    {
                        trivia = new Comment(input.ReadLine());
                        Result.LastOrDefault()?.DelimitersAfter.AddIfNotNull(trivia);
                    }
                }
                else if (ch == ',')
                {
                    input.Read();
                    current = new CommaToken(input);
                    SetTrivia();
                    Result.Add(current);
                }
                else if (ch == '(')
                {
                    input.Read();
                    current = new OpenParenToken(input);
                    SetTrivia();
                    Result.Add(current);
                    
                }
                else if (ch == '{')
                {
                    input.Read();
                    current = new StartFontSetToken(input);
                    SetTrivia();
                    Result.Add(current);
                }
                else if (ch == ')')
                {
                    input.Read();
                    current = new CloseParenToken(input);
                    SetTrivia();
                    Result.Add(current);
                }
                else if (ch == '}')
                {
                    input.Read();
                    current = new EndFontSetToken(input);
                    SetTrivia();
                    Result.Add(current);
                }
                else if (char.IsNumber(ch))
                {
                    input.Read();
                    var sb = new StringBuilder();
                    while (char.IsNumber(ch = (char)input.PeekNext()))
                    {
                        sb.Append(ch);
                        input.Read();
                    }

                    current = new NumberLiteralToken(sb.ToString(), input);
                    SetTrivia();
                    Result.Add(current);
                    
                }
                else if (input.PeekNext(3) == "AND")
                {
                    current = new OperatorToken(OperatorType.AND_LITERAL, input);
                    SetTrivia();
                    Result.Add(current);
                    input.Read(3);
                }
                else if (input.PeekNext(3) == "OR")
                {
                    current = new OperatorToken(OperatorType.OR_LITERAL, input);
                    SetTrivia();
                    Result.Add(current);
                    input.Read(2);
                }
                //keyword
                else if(char.IsLetter(ch) || ch == '_') 
                {
                    var sb = new StringBuilder();
                    sb.Append(ch);
                    input.Read();

                    while ((ch = (char)input.PeekNext()) == '_' || char.IsLetter(ch))
                    {
                        sb.Append(ch); 
                        input.Read();
                        if(input.PeekNext() == -1)
                        {
                            break;
                        }
                    }

                    string res = sb.ToString();
                    switch (res)
                    {
                        case "ViewValues":
                            current = new ViewValuesToken(input);
                            break;
                        case "IgnoreCase":
                            current = new IgnoreCaseToken(input);
                            break;
                        default:
                            current = new IdentifierToken(res, input);
                            break;
                    }

                    SetTrivia();
                    Result.Add(current);
                }
                else if (ch == '#')
                {
                    input.Read();
                    var sb = new StringBuilder(6);
                    int i = 0;
                    while (i < 6)
                    {
                        int ci = input.Read();
                        if (ci == -1)
                        {
                            break;
                        }

                        sb.Append((char)ci);
                        i++;
                    }

                    string res = sb.ToString();
                    switch (res)
                    {
                        case "SWITCH":
                            current = new EventSwitchToken(input);
                            break;
                        case "ODKLON":
                            current = new EventOdklonToken(input);
                            break;
                        case "VYLUKA":
                            current = new EventVylukaToken(input);
                            break;
                        default:
                            Compiler.Outputs.Add(new ErrorOutput(input,$"Neplatny identifikator #{res}."));
                            break;
                    }

                    SetTrivia();
                    Result.Add(current);
                }
                //operator
                else
                {
                    var op = OperatorToken.GetOperator(input.PeekNext(2), out int len);
                    if (op != OperatorType.NONE)
                    {
                        input.Read(len);
                        current = new OperatorToken(op, input);

                        SetTrivia();
                        Result.Add(current);
                        
                    }
                    else
                    {
                        Compiler.Outputs.Add(new ErrorOutput(input, $"Neplatny znak '{op}'."));
                    }
                }
            }

            trivia = new EOFDelimiter();
            current.DelimitersAfter.AddIfNotNull(trivia);

            void SetTrivia()
            {
                if (newEmptyLine)
                {
                    Result.LastOrDefault()?.DelimitersBefore.AddIfNotNull(trivia);
                    newEmptyLine = false;
                }
                else
                {
                    Result.LastOrDefault()?.DelimitersAfter.AddIfNotNull(trivia);
                }
            }
        }
    }
}
