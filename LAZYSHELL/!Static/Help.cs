using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace LAZYSHELL
{
    public static class Help
    {
        // txt document
        public static void CreateHelp(XmlDocument LAZYSHELL_xml, bool readme)
        {
            StringWriter writer = new StringWriter();
            XmlNode main = LAZYSHELL_xml.SelectSingleNode("LAZYSHELL");
            writer.WriteLine(main.Attributes["title"].Value);
            //
            foreach (XmlNode node in main.ChildNodes)
            {
                if (node.Name == "Properties")
                {
                    writer.WriteLine("Version: " + node.Attributes["version"].Value);
                    writer.WriteLine("Date: " + node.Attributes["date"].Value);
                    writer.WriteLine("Home Page: " + node.Attributes["homepage"].Value);
                    writer.WriteLine("Written by " + node.Attributes["author"].Value);
                    writer.WriteLine("");
                }
                else
                {
                    XmlNode header = node.SelectSingleNode("header");
                    XmlNode body = node.SelectSingleNode("body");
                    writer.WriteLine("_______________________________________________________________________");
                    writer.WriteLine("");
                    if (header != null) writer.WriteLine(header.InnerText.ToUpper());
                    writer.WriteLine("_______________________________________________________________________");
                    writer.WriteLine("");
                    if (body != null)
                    {
                        string innerText = body.InnerText;
                        writer.WriteLine(Do.WordWrap(innerText, 71));
                        writer.WriteLine("");
                    }
                }
                if (node.Name == "Read")
                {
                    XmlNode introduction = node.SelectSingleNode("introduction");
                    if (introduction != null)
                        writer.WriteLine(Do.WordWrap(introduction.InnerText, 71));
                    writer.WriteLine("");
                    foreach (XmlNode precaution in node.SelectNodes("precaution"))
                    {
                        writer.WriteLine(Do.WordWrap(precaution.InnerText, 71, 3));
                        writer.WriteLine("");
                    }
                    XmlNode conclusion = node.SelectSingleNode("conclusion");
                    writer.WriteLine(Do.WordWrap(conclusion.InnerText, 71));
                    writer.WriteLine("");
                }
                if (node.Name == "Files")
                {
                    writer.WriteLine("");
                    foreach (XmlNode file in node.SelectNodes("file"))
                    {
                        XmlNode description = file.SelectSingleNode("description");
                        string name = file.Attributes["name"].Value;
                        writer.WriteLine("\"" + name + "\"");
                        writer.WriteLine("");
                        writer.WriteLine(Do.WordWrap(description.InnerText, 71));
                        writer.WriteLine("");
                        writer.WriteLine("");
                    }
                }
                else if (node.Name == "FAQ")
                {
                    foreach (XmlNode entry in node.SelectNodes("entry"))
                    {
                        XmlNode question = entry.SelectSingleNode("question");
                        XmlNode answer = entry.SelectSingleNode("answer");
                        writer.WriteLine("Q: " + Do.WordWrap(question.InnerText, 68, 3));
                        writer.WriteLine("A: " + Do.WordWrap(answer.InnerText, 68, 3));
                        writer.WriteLine("");
                    }
                    foreach (XmlNode editor in node.SelectNodes("../Editors/*[name() != 'header']"))
                    {
                        XmlNode faq = editor.SelectSingleNode("FAQ");
                        string name = editor.Attributes["title"].Value;
                        writer.WriteLine(String.Empty.PadLeft(name.Length, '_'));
                        writer.WriteLine(name.ToUpper());
                        writer.WriteLine(String.Empty.PadLeft(name.Length, '¯'));
                        foreach (XmlNode entry in faq.SelectNodes("entry"))
                        {
                            XmlNode question = entry.SelectSingleNode("question");
                            XmlNode answer = entry.SelectSingleNode("answer");
                            writer.WriteLine("Q: " + Do.WordWrap(question.InnerText, 68, 3));
                            writer.WriteLine("A: " + Do.WordWrap(answer.InnerText, 68, 3));
                            writer.WriteLine("");
                        }
                    }
                }
                else if (node.Name == "Glossary")
                {
                    foreach (XmlNode entry in node.SelectNodes("entry"))
                    {
                        XmlNode definition = entry.SelectSingleNode("definition");
                        writer.WriteLine("\"" + entry.Attributes["term"].Value + "\"");
                        writer.WriteLine(Do.WordWrap(definition.InnerText, 71));
                        writer.WriteLine("");
                    }
                }
                else if (node.Name == "Editors" && !readme)
                {
                    foreach (XmlNode editor in node.SelectNodes("*[name() != 'header']"))
                        HelpEditor(writer, editor);
                }
                else if (node.Name == "Other" && !readme)
                {
                    foreach (XmlNode editor in node.SelectNodes("*[name() != 'header']"))
                        HelpEditor(writer, editor);
                }
            }
            //
            if (readme)
            {
                File.WriteAllText("readme.txt", writer.ToString(), Encoding.UTF8);
                Process.Start("readme.txt");
            }
            else
            {
                if (!Directory.Exists("help"))
                    Directory.CreateDirectory("help");
                File.WriteAllText("help//LAZYSHELL_txt.txt", writer.ToString(), Encoding.UTF8);
                Process.Start("help\\LAZYSHELL_txt.txt");
            }
        }
        private static void HelpEditor(StringWriter writer, XmlNode editor)
        {
            string name = editor.Attributes["title"].Value;
            writer.WriteLine(String.Empty.PadLeft(name.Length, '_'));
            writer.WriteLine(name.ToUpper());
            writer.WriteLine(String.Empty.PadLeft(name.Length, '¯'));
            XmlNode description = editor.SelectSingleNode("description");
            if (description != null)
                writer.WriteLine(Do.WordWrap(description.InnerText, 71));
            foreach (XmlNode attribute in editor.SelectNodes("*[name() = 'attribute']"))
                HelpAttribute(writer, attribute, 3);
            HelpTooltips(writer, editor, 3);
            foreach (XmlNode section in editor.SelectNodes("*[@type = 'section']"))
                HelpSection(writer, section, 3);
            foreach (XmlNode subwindow in editor.SelectNodes("*[@type = 'subwindow']"))
                HelpSubwindow(writer, subwindow, 3);
        }
        private static void HelpAttribute(StringWriter writer, XmlNode attribute, int indent)
        {
            string pad = String.Empty.PadLeft(indent, ' ');
            writer.WriteLine(pad);
            writer.WriteLine(pad + Do.WordWrap(attribute.InnerText, 71, indent));
        }
        private static void HelpSection(StringWriter writer, XmlNode section, int indent)
        {
            string name = section.Attributes["title"].Value;
            string pad = String.Empty.PadLeft(indent, ' ');
            writer.WriteLine(pad);
            writer.WriteLine(pad + String.Empty.PadLeft(name.Length, '_'));
            writer.WriteLine(pad + name);
            writer.WriteLine(pad + String.Empty.PadLeft(name.Length, '¯'));
            XmlNode description = section.SelectSingleNode("description");
            if (description != null)
                writer.WriteLine(pad + Do.WordWrap(description.InnerText, 71, indent));
            HelpTooltips(writer, section, indent + 3);
            foreach (XmlNode subeditor in section.SelectNodes("*[@type = 'subeditor']"))
                HelpSubwindow(writer, section, indent + 3);
        }
        private static void HelpSubwindow(StringWriter writer, XmlNode subwindow, int indent)
        {
            string name = subwindow.Attributes["title"].Value;
            string pad = String.Empty.PadLeft(indent, ' ');
            writer.WriteLine(pad);
            writer.WriteLine(pad + String.Empty.PadLeft(name.Length, '_'));
            writer.WriteLine(pad + name.ToUpper());
            writer.WriteLine(pad + String.Empty.PadLeft(name.Length, '¯'));
            XmlNode description = subwindow.SelectSingleNode("description");
            if (description != null)
                writer.WriteLine(pad + Do.WordWrap(description.InnerText, 71, indent));
            foreach (XmlNode attribute in subwindow.SelectNodes("*[name() = 'attribute']"))
                HelpAttribute(writer, attribute, indent + 3);
            HelpTooltips(writer, subwindow, indent + 3);
            foreach (XmlNode section in subwindow.SelectNodes("*[@type = 'section']"))
                HelpSection(writer, section, indent + 3);
        }
        private static void HelpTooltips(StringWriter writer, XmlNode parent, int indent)
        {
            string pad = String.Empty.PadLeft(indent, ' ');
            XmlNodeList tooltips = parent.SelectNodes("tooltip");
            if (tooltips.Count > 0)
            {
                writer.WriteLine(pad);
                writer.WriteLine(pad + "***TOOLTIPS***");
            }
            foreach (XmlNode tooltip in tooltips)
            {
                XmlNode title = tooltip.SelectSingleNode("title");
                XmlNode description = tooltip.SelectSingleNode("description");
                writer.WriteLine(pad + "[" + title.InnerText + "]");
                writer.WriteLine(pad + "  " + Do.WordWrap(description.InnerText, 71, indent + 2));
            }
        }
    }
}
