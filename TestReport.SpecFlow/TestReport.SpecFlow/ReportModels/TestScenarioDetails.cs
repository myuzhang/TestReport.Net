using System.Collections.Generic;

namespace TestReport.SpecFlow.ReportModels
{
    public class TestScenarioDetails
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<string> Tags { get; } = new List<string>();

        public List<string> Steps { get; } = new List<string>();

        public bool TestPass { get; set; }

        public string ErrorMessage { get; set; }

        public string ScreenShotFile { get; set; }
    }
}
