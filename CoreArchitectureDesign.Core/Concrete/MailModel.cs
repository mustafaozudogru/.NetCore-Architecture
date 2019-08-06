namespace CoreArchitectureDesign.Core.Concrete
{
    public class MailModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string MailServer { get; set; }

        public string MailUser { get; set; }

        public string MailPassword { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public string AttachmentFilename { get; set; }
    }
}
