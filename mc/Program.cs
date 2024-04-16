﻿using System;
using Minsk.CodeAnalysis;

namespace Minsk
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showTree = false;

            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "#showtree"){
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees.");
                    continue;
                }else if(line == "#cls"){
                    Console.Clear();
                    continue;
                }



                
                var syntaxTree = SyntaxTree.Parse(line);

                if(showTree){
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = color;
                }

                if(!syntaxTree.Diagnostics.Any()){
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }else{
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach(var _diagnostic in syntaxTree.Diagnostics){
                        Console.WriteLine(_diagnostic);
                    }
                    
                    Console.ForegroundColor = color;
                }
            
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true){
            
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if(node is SyntaxToken t && t.Value != null){
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach(var child in node.GetChildren()){
                PrettyPrint(child, indent, child == lastChild);
            }
        }
    }



   
}