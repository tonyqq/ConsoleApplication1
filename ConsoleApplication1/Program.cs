namespace RoslynTinkering
{
    using System;
    using System.IO;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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

            // gets all methods from class
            foreach (var myMethod in myMethods)
            {
                Console.WriteLine(myMethod.Identifier.ToString());
            }
            
            var customWalker = new CustomWalker();
            customWalker.Visit(syntaxRoot);

            // another way to get all methods
            var classMethodWalker = new ClassMethodWalker();
            classMethodWalker.Visit(syntaxRoot);

            Console.ReadKey();
        }

        public class CustomWalker : CSharpSyntaxWalker
        {
            public CustomWalker() : base(SyntaxWalkerDepth.Token)
            {
            }

            static int tabs = 0;
            public override void Visit(SyntaxNode node)
            {
                tabs++;
                var indents = new string('\t', tabs);
                Console.WriteLine(indents + node.Kind());
                base.Visit(node);
                tabs--;
            }
            public override void VisitToken(SyntaxToken token)
            {
                var indents = new string('\t', tabs);
                Console.WriteLine(indents + token);
                base.VisitToken(token);
            }
        }

        public class ClassMethodWalker : CSharpSyntaxWalker
        {
            string className = string.Empty;
            public override void VisitClassDeclaration(ClassDeclarationSyntax node)
            {
                className = node.Identifier.ToString();
                base.VisitClassDeclaration(node);
            }

            public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                var methodName = node.Identifier.ToString();
                Console.WriteLine(className + '.' + methodName);
                base.VisitMethodDeclaration(node);
            }
        }
    }
}
