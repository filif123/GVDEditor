namespace TabTabCompiler.Components
{
    public enum OperatorType
    {
        NONE,
        ASSIGMENT, //=
        PLUS, // +
        MINUS, // -
        EQUALS, // ==
        NOT_EQUALS, // !=
        GREATER_THAN, // >
        SMALLER_THAN, // <
        GREATER_EQUALS_THAN, // >=
        SMALLER_EQUALS_THAN, // <=
        MULTIPLICATION, // *
        DIVISION, // /
        AND, // &&
        OR, // ||
        XOR, // ^
        AND_LITERAL, // AND
        OR_LITERAL, // OR
        XOR_LITERAL, // XOR
        NOT // !
    }

    public static class Operator
    {
        public static int GetPriority(string action)
        {
            switch (action)
            {
                //case "++":
                //case "--": 
                    //return 10;
                case "^": 
                    return 9;
                case "%":
                case "*":
                case "/": 
                    return 8;
                case "+":
                case "-": 
                    return 7;
                case "<":
                case ">":
                case ">=":
                case "<=": 
                    return 6;
                case "==":
                case "!=": 
                    return 5;
                case "&&": 
                    return 4;
                case "||": 
                    return 3;
                //case "+=":
                case "=": 
                    return 2;
            }

            return 0;
        }
    }
}