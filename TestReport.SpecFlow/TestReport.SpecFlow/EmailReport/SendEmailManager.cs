using System;
using System.Configuration;
using System.IO.Compression;
using System.Net.Mail;
using System.Text;
using TestReport.SpecFlow.Configuration;
using TestReport.SpecFlow.MakeReport;

namespace TestReport.SpecFlow.EmailReport
{
    // refer to http://stackoverflow.com/questions/9201239/send-e-mail-via-smtp-using-c-sharp
    // refer to http://www.codeproject.com/Tips/520998/Send-Email-from-Yahoo-GMail-Hotmail-Csharp
    public class SendEmailManager
    {
        private static SendEmailManager _instance;
        private readonly MailSettingsElement _mailSettings;
        private readonly ReportSettingsElement _reportSettings;

        private SmtpClient _client;

        private SendEmailManager()
        {
            var section = (SpecFlowReportSection)ConfigurationManager.GetSection("specFlow.Report");
            _mailSettings = section.MailSettings;
            _reportSettings = section.ReportSettings;

            _client = new SmtpClient
            {
                Host = _mailSettings.Host,
                Port = _mailSettings.Port,
                EnableSsl = _mailSettings.EnableSsl,
                Timeout = 60000,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            if (_client.EnableSsl)
            {
                _client.Credentials = new System.Net.NetworkCredential(
                    _mailSettings.Username,
                    _mailSettings.Password);
            }
        }

        public static SendEmailManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SendEmailManager();
                }
                return _instance;
            }
        }

        public void SendEmail(string attachmentFolder = null)
        {
            MailMessage mm = new MailMessage()
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };
            mm.From = new MailAddress(_mailSettings.FromAddress);
            var sentToString = _mailSettings.ToAddresses;
            if (!string.IsNullOrEmpty(sentToString))
            {
                var sentTos = sentToString.Split(';');
                foreach (string to in sentTos)
                {
                    mm.To.Add(new MailAddress(to));
                }
            }
            //mm.Subject = "Test Result";
            mm.Subject = _mailSettings.Subject;
            mm.IsBodyHtml = true;
            TestResultMailBody mailBody = new TestResultMailBody();
            mailBody
                .AppendTestResultDetails(TestScenarioDetailsManager.Instance.GetTestResult())
                .AppendTestResultSummary(
                    TestScenarioDetailsManager.Instance.PassedTestRun,
                    TestScenarioDetailsManager.Instance.FailededTestRun);
            mm.Body = mailBody.Body;

            // refer to http://stackoverflow.com/questions/15241889/i-didnt-find-zipfile-class-in-the-system-io-compression-namespace
            if (!string.IsNullOrEmpty(attachmentFolder))
            {
                string testResultFolder = Environment.ExpandEnvironmentVariables(_reportSettings.Path);
                string zipFile = $@"{testResultFolder}\{DateTime.Now.ToString("yyyy-MM-dd HH_mm")}.zip";
                ZipFile.CreateFromDirectory(attachmentFolder, zipFile, CompressionLevel.Fastest, true);
                mm.Attachments.Add(new Attachment(zipFile));
            }

            _client.Send(mm);
        }
    }
}
