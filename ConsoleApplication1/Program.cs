using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConsoleApplication1
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Just testing git hub");

            // TODO: changes this to relative path
            var myClassText = File.ReadAllText(@"D:\src\GitHub\Repos\ConsoleApplication1\ConsoleApplication1\MyClass.cs");
            var tree = CSharpSyntaxTree.ParseText(myClassText);

            var syntaxRoot = tree.GetRoot();
            var myMethods = syntaxRoot.DescendantNodes()
                .OfType<MethodDeclarationSyntax>();
                //.First(n => n.ParameterList.Parameters.Any());

            //Find the type that contains this method
            //var containingType = myMethod.Ancestors().OfType<TypeDeclarationSyntax>();

            foreach (var myMethod in myMethods)
            {
                Console.WriteLine(myMethod.Identifier.ToString());
            }
            
            //Console.WriteLine(myMethod.ToString());

            //var syntaxRoot = tree.GetRoot();
            //var myClass = syntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            //var myMethod = syntaxRoot.DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            //Console.WriteLine(myClass.Identifier.ToString());
            //Console.WriteLine(myMethod.Identifier.ToString());

            Console.ReadKey();
        }
    }
}
