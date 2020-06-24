using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlBuilder.Example
{
  class Program
  {
    static void Main()
    {
      // example 1
      var link = Tags.A.WithAttr("href", "http://google.com").WithText("google");
      Console.WriteLine(link.Pretty());

      // example 2
      var div = Tags.Div
                    .WithId("div1")
                    .WithClass("cls1")
                    .WithChildren(
                        Tags.Text("link: "),
                        link);
      Console.WriteLine(div.Pretty());

      // example 3
      var row1 = new[] { "cell1", "cell2" };
      var row2 = new[] { "cell1", "cell2", "cell3" };

      TagBuilder CreateRow(IEnumerable<string> items) => Tags.Tr.WithChildren(Tags.Td.WithChildren(items.Select(Tags.Span.WithText)));

      var table = Tags.Table.WithChildren(
          CreateRow(row1),
          CreateRow(row2),
          Tags.Tr.WithChildren(Tags.Td.WithAttr("colspan", 3).WithChildren(div)));
      Console.WriteLine(table.Pretty());
    }
  }
}