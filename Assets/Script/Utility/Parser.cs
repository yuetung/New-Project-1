using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Parser {

    // takes a string input, evaluates it and returns a float as output
    // used while running
    // References:
    // https://en.wikipedia.org/wiki/Shunting-yard_algorithm
    // https://en.wikipedia.org/wiki/Reverse_Polish_notation
    public static float parse(string input)
    {
        return 0f;
    }

    // checks if an expression is valid
    // used in editor
    public static bool checkValid(string input)
    {
        return true;
    }

    // Reads a string and outputs Reverse Polish notation
    private static LinkedList<Token> readTokens(string input)
    {
        LinkedList<Token> output = new LinkedList<Token>();
        LinkedList<OperatorToken> stack = new LinkedList<OperatorToken>();
        
        // remove all whitespaces
        input = input.Replace(" ", "");

        int index;
        Token token;
        while (input.Length > 0)
        {
            index = findFirstTokenIndexEnds(input);

            // Creates a token
            token = Token.Create(input.Substring(0, index));

            // do stuff
            if (token is NumberToken)
            {
                output.AddLast(token);
            } else if (token is OperatorToken)
            {
                // do other stuff
            }

            // remove the token, continue parsing
            input = input.Substring(index + 1);
        }

        // at the end, add all tokens remaining in stack to output
        foreach (Token node in stack) {
            output.AddLast(node);
        }

        return output;
    }

    // finds the index of where the first token ends
    private static int findFirstTokenIndexEnds(string input)
    {
        return 1;
    }
}

// represents a part of an expression
abstract class Token {
    
    public static Token Create(string token)
    {
        return new NumberToken();
    }
}
// represents a number
class NumberToken : Token {}
// represents a variable number (e.g. health, stamina etc)
class VarToken : NumberToken { }

// represents an operator (e.g. add, subtract, min(x,y), max(x,y), random(x,y))
class OperatorToken : Token {

    public enum Associativity {
        RIGHT,
        LEFT
    }

    public int precedence;
    public Associativity associativity;

}