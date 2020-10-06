using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator
{
    class MethodCollector: CSharpSyntaxWalker
    {

        public ICollection<MethodDeclarationSyntax> Nodes { get; } = new List<MethodDeclarationSyntax>();

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            Console.WriteLine($" {node.Modifiers[0]} {node.Identifier.ToString()}");
            Nodes.Add(node);
            base.VisitMethodDeclaration(node);
        }
    }
}
