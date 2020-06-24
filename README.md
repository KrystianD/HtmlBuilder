HtmlBuilder
======

Fluent HTML builder based on HtmlAgilityPack

### Examples

```c#
var link = Tags.A.WithAttr("href", "http://google.com").WithText("google")
```

```
<a href="http://google.com">google</a>
```

---

```c#
var div = Tags.Div
    .WithId("div1")
    .WithClass("cls1")
    .WithChildren(
        Tags.Text("link: "),
        link);
```

```
<div id="div1" class="cls1">link: <a href="http://google.com">google</a></div>
```

---

```c#
var row1 = new[] { "cell1", "cell2" };
var row2 = new[] { "cell1", "cell2", "cell3" };

TagBuilder CreateRow(IEnumerable<string> items) => Tags.Tr.WithChildren(Tags.Td.WithChildren(items.Select(Tags.Span.WithText)));

var table = Tags.Table.WithChildren(
    CreateRow(row1),
    CreateRow(row2),
    Tags.Tr.WithChildren(Tags.Td.WithAttr("colspan", 3).WithChildren(div)));
```


```
<table>
  <tr>
    <td>
      <span>cell1</span>
      <span>cell2</span>
    </td>
  </tr>
  <tr>
    <td>
      <span>cell1</span>
      <span>cell2</span>
      <span>cell3</span>
    </td>
  </tr>
  <tr>
    <td colspan="3">
      <div class="cls1" id="div1">link: <a href="http://google.com">google</a></div>
    </td>
  </tr>
</table>
```