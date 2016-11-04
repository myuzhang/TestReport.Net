using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using TestReport.SpecFlow.ReportModels;

namespace TestReport.SpecFlow.MakeReport
{
    public class TestResultReportInHtmlManager
    {
        private string _htmlFilePath;

        private Dictionary<int, TestScenarioDetails> _results;

        public TestResultReportInHtmlManager(string htmlFilePath, Dictionary<int, TestScenarioDetails> results)
        {
            _htmlFilePath = htmlFilePath;
            _results = results;
        }

        public void GenerateReport()
        {
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                // html:
                writer.WriteBeginTag("html");
                writer.Write(HtmlTextWriter.TagRightChar);

                // head:
                writer.WriteBeginTag("head");
                writer.Write(HtmlTextWriter.TagRightChar);

                writer.RenderBeginTag("style");
                writer.Write("table { border - collapse: collapse; } ");
                writer.Write("table, th, td { border: 2px solid black; }");
                writer.Write("th { background-color: #4CAF50; color: white; } ");
                writer.Write("tr.pass {background-color: #FEF5E7} ");
                writer.Write("tr.fail {background-color: #F6DDCC} ");
                writer.Write("tr:hover {background-color: #FEF9E7} ");
                writer.Write("td { vertical-align: top; } ");
                writer.Write("a { color: hotpink; background-color: yellow; } ");
                writer.RenderEndTag();

                writer.WriteBeginTag("title");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write("Test Result Report:");
                writer.WriteEndTag("title");

                writer.WriteEndTag("head");

                // body:
                writer.WriteBeginTag("body");
                writer.Write(HtmlTextWriter.TagRightChar);

                writer.WriteBeginTag("h3");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write("Test Result Summary:");
                writer.WriteEndTag("h3");

                writer.WriteBeginTag("p");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write($"Total test run: {_results.Count}");
                writer.WriteEndTag("p");

                writer.WriteBeginTag("p");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write($"Total passed tests: {_results.Count(r => r.Value.TestPass)}");
                writer.WriteEndTag("p");

                writer.WriteBeginTag("p");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write($"Total failed tests: {_results.Count(r => !r.Value.TestPass)}");
                writer.WriteEndTag("p");

                writer.WriteBeginTag("h4");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write("Test Result Details:");
                writer.WriteEndTag("h4");

                // table:
                writer.RenderBeginTag("table");

                writer.RenderBeginTag("thead");
                writer.RenderBeginTag("tr");

                writer.RenderBeginTag("th");
                writer.Write(nameof(TestScenarioDetails.Id));
                writer.RenderEndTag();

                writer.RenderBeginTag("th");
                writer.Write(nameof(TestScenarioDetails.Title));
                writer.RenderEndTag();

                writer.RenderBeginTag("th");
                writer.Write(nameof(TestScenarioDetails.Tags));
                writer.RenderEndTag();

                writer.RenderBeginTag("th");
                writer.Write(nameof(TestScenarioDetails.Steps));
                writer.RenderEndTag();

                writer.RenderBeginTag("th");
                writer.Write(nameof(TestScenarioDetails.TestPass));
                writer.RenderEndTag();

                writer.RenderBeginTag("th");
                writer.Write(nameof(TestScenarioDetails.ErrorMessage));
                writer.RenderEndTag();

                writer.RenderEndTag(); // tr
                writer.RenderEndTag(); // thead

                writer.RenderBeginTag("tbody");

                foreach (var result in _results)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, result.Value.TestPass ? "pass" : "fail");
                    writer.RenderBeginTag("tr");

                    writer.RenderBeginTag("td");
                    writer.Write(result.Value.Id);
                    writer.RenderEndTag();

                    writer.RenderBeginTag("td");
                    writer.Write(result.Value.Title);
                    writer.RenderEndTag();

                    writer.RenderBeginTag("td");
                    foreach (string tag in result.Value.Tags)
                    {
                        // writer.RenderBeginTag("p");
                        writer.Write(tag);
                        // writer.RenderEndTag();
                        writer.WriteBeginTag("br");
                        writer.Write(HtmlTextWriter.TagRightChar);
                    }
                    writer.RenderEndTag();

                    writer.RenderBeginTag("td");
                    for (int i = 0; i < result.Value.Steps.Count - 1; i++)
                    {
                        // writer.RenderBeginTag("p");
                        writer.Write(result.Value.Steps[i]);
                        // writer.RenderEndTag();
                        writer.WriteBeginTag("br");
                        writer.Write(HtmlTextWriter.TagRightChar);
                    }
                    if (!result.Value.TestPass)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, result.Value.ScreenShotFile);
                        writer.AddAttribute(HtmlTextWriterAttribute.Target, "_blank");
                        writer.RenderBeginTag("a");
                        writer.Write(result.Value.Steps.Last());
                        writer.RenderEndTag();
                    }
                    else
                    {
                        writer.Write(result.Value.Steps.Last());
                    }
                    writer.RenderEndTag();

                    writer.RenderBeginTag("td");
                    writer.Write(result.Value.TestPass ? "Passed" : "Failed");
                    writer.RenderEndTag();

                    writer.RenderBeginTag("td");
                    writer.Write(result.Value.ErrorMessage);
                    writer.RenderEndTag();

                    writer.RenderEndTag(); // tr
                }

                writer.RenderEndTag(); // tbody

                writer.RenderEndTag(); // table

                writer.WriteEndTag("body");

                writer.WriteEndTag("html");
            }

            // write to a file:
            if (File.Exists(_htmlFilePath))
            {
                File.Delete(_htmlFilePath);
            }
            File.WriteAllText(_htmlFilePath, stringWriter.ToString());
        }
    }
}
