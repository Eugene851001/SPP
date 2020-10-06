using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator
{
    class IfCollector: CSharpSyntaxWalker
    {
        public ICollection<IfStatementSyntax> Nodes { get; } = new List<IfStatementSyntax>();

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            Console.WriteLine(node.Condition.ToString());
            Nodes.Add(node);
            base.VisitIfStatement(node);
        }
    }
}
