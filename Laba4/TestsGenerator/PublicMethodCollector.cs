using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator
{
    class PublicMethodCollector: MethodCollector
    {
        public ClassDeclarationSyntax ClassDeclaration;

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if(node.Modifiers.Count > 0 && node.Modifiers.First().ValueText == "public")
            {
                Console.WriteLine(node.Identifier.ToString());
                Nodes.Add(node);
            }
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            ClassDeclaration = node;
            base.VisitClassDeclaration(node);
        }
    }
}
