using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace TestsGenerator
{
    class TestGenerationCollector: CSharpSyntaxWalker
    {
        public ClassDeclarationSyntax ClassNode;
        public NamespaceDeclarationSyntax NamespaceNode;
        public ConstructorDeclarationSyntax ConstructorNode;
        public ICollection<MethodDeclarationSyntax> Nodes { get; } = new List<MethodDeclarationSyntax>();

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Modifiers.Count > 0 && node.Modifiers.First().ValueText == "public")
            {
                Console.WriteLine(node.Identifier.ToString());
                Nodes.Add(node);
            }
            base.VisitMethodDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            ClassNode = node;
            base.VisitClassDeclaration(node);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            NamespaceNode = node;
            base.VisitNamespaceDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            ConstructorNode = node;
            base.VisitConstructorDeclaration(node);
        }

    }
}
