using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace HtmlBuilder
{
  public class TagBuilder
  {
    private readonly HtmlNode _node;

    private TagBuilder(HtmlNode node) => _node = node;

    internal static TagBuilder FromTag(string tagName) => new TagBuilder(HtmlNode.CreateNode($"<{tagName}></{tagName}>"));
    internal static TagBuilder FromText(string text) => new TagBuilder(HtmlNode.CreateNode(WebUtility.HtmlEncode(text)));

    public void AddChild(TagBuilder tagBuilder) => _node.AppendChild(tagBuilder._node);

    public void AddChild(IEnumerable<TagBuilder> tabBuilders)
    {
      foreach (var tagBuilder in tabBuilders) AddChild(tagBuilder);
    }

    public void SetAttribute(string name, string value) => _node.Attributes.Add(name, value);
    public void SetAttribute(string name, int value) => _node.Attributes.Add(name, value.ToString());
    public void SetDataAttribute(string name, string value) => _node.Attributes.Add("data-" + name, value);

    public void SetId(string id) => _node.Id = id;

    public void SetText(string text) => SetHtml(WebUtility.HtmlEncode(text));

    public void SetHtml(string html) => _node.InnerHtml = html;

    public void AddClass(string name) => _node.AddClass(name);

    public void AddClass(params string[] names) => AddClass((IEnumerable<string>)names);

    public void AddClass(IEnumerable<string> names)
    {
      foreach (var name in names) { _node.AddClass(name); }
    }

    public string Render() => _node.OuterHtml;

    public string Pretty() => XElement.Parse(Render()).ToString();

    public HtmlNode Build() => _node.CloneNode(true);

    public TagBuilder WithAttr(string name, string value) => CloneModifyTag(x => x.SetAttribute(name, value));
    public TagBuilder WithAttr(string name, int value) => CloneModifyTag(x => x.SetAttribute(name, value));
    public TagBuilder WithData(string name, string value) => CloneModifyTag(x => x.SetDataAttribute(name, value));

    public TagBuilder WithClass(string name) => CloneModifyTag(x => x.AddClass(name));
    public TagBuilder WithClass(params string[] names) => CloneModifyTag(x => x.AddClass(names));
    public TagBuilder WithClass(IEnumerable<string> names) => CloneModifyTag(x => x.AddClass(names));

    public TagBuilder WithStyle(string style) => WithAttr("style", style);

    public TagBuilder WithId(string id) => CloneModifyTag(x => x.SetId(id));
    public TagBuilder WithText(string text) => CloneModifyTag(x => x.SetText(text));
    public TagBuilder WithHtml(string text) => CloneModifyTag(x => x.SetHtml(text));

    public TagBuilder WithChildren(IEnumerable<TagBuilder> tags) => CloneModifyTag(x => x.AddChild(tags));
    public TagBuilder WithChildren(params TagBuilder[] tags) => CloneModifyTag(x => x.AddChild(tags));
    public TagBuilder WithChildren(Action<TagBuilder> tag) => CloneModifyTag(tag);

    private TagBuilder CloneModifyTag(Action<TagBuilder> action)
    {
      var newTagBuilder = new TagBuilder(_node.CloneNode(true));
      action(newTagBuilder);
      return newTagBuilder;
    }
  }
}