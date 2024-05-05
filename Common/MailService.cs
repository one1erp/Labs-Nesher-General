using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Outlook;
using Attachment = System.Net.Mail.Attachment;
using Exception = System.Exception;
using System.Reflection;


namespace Common
{
    public static class MailService
    {

        public static bool Send(MailDetails mailDetails)
        {
            try
            {

                if (mailDetails.To.Count < 1 || (mailDetails.To.Count == 1 && string.IsNullOrEmpty(mailDetails.To[0])))
                {
                    MessageBox.Show("חסרה כתובת הנמען!");
                    return false;
                }
                MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient smtpServer = new SmtpClient(mailDetails.SmtpClient);

                mail.From = new MailAddress(mailDetails.FromAddress);


                foreach (var item in mailDetails.To)
                {
                    if (!string.IsNullOrEmpty(item))
                        mail.To.Add(item);
                }
                foreach (var item in mailDetails.CC)
                {
                    if (!string.IsNullOrEmpty(item))
                        mail.CC.Add(item);
                }


                mail.Subject = mailDetails.Subject;
                mail.Body = mailDetails.Body;
                foreach (var path in mailDetails.AtachmentPathes)
                {
                    if (File.Exists(path))
                    {
                        var attachment = new Attachment(path);
                        attachment.Name = Path.GetFileName(path);//23-02-2023 Ashi
                        mail.Attachments.Add(attachment);
                    }

                    else
                    {
                        var dr = MessageBox.Show("The path " + path + " dosen't Exists,Do you want to continue?",
                            "Nautilus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                        {
                            return false;
                        }
                    }

                }

                //SmtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(mailDetails.UserName, mailDetails.Password);
                //var SmtpUser = new System.Net.NetworkCredential("domain\\username", "password");
                //SmtpServer.EnableSsl = true;
                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpServer.Send(mail);

                //    MessageBox.Show("המייל נשלח בהצלחה", "Nautilus");
                return true;


            }
            catch (Exception ex)
            {
                Logger.WriteLogFile(ex);
                MessageBox.Show("שליחת המייל נכשלה,אנא פנה לתמיכה.", "Nautilus", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;



            }

        }

        private static MailItem oMailItem;

        public static bool OpenOutlook(MailDetails mailDetails)
        {
            
            SentFromOutlook = false;
            try
            {
                object oApp = Activator.CreateInstance(Type.GetTypeFromProgID("Outlook.Application"));

                if (oApp != null)
                {
                    object oNS = oApp.GetType().InvokeMember("GetNamespace", BindingFlags.InvokeMethod, null, oApp, new object[] { "MAPI" });

                    MailItem oMailItem = (MailItem)oApp.GetType().InvokeMember("CreateItem", BindingFlags.InvokeMethod, null, oApp, new object[] { OlItemType.olMailItem });

                    oMailItem.To = mailDetails.To.Where(item => !string.IsNullOrEmpty(item))
                        .Aggregate("", (current, item) => current + (item + ";"));
                    oMailItem.CC = mailDetails.CC.Where(item => !string.IsNullOrEmpty(item))
                        .Aggregate("", (current, item) => current + (item + ";"));

                    foreach (var item in mailDetails.AtachmentPathes)
                    {
                        if (!string.IsNullOrEmpty(item) && File.Exists(item))
                        {
                            var iAttachType = (int)OlAttachmentType.olByValue;
                            var newAtch = oMailItem.Attachments.Add(item, iAttachType, /*iPosition*/1, item);
                            newAtch.DisplayName = Path.GetFileName(item);
                        }
                    }

                    oMailItem.Subject = mailDetails.Subject;

                    ((ItemEvents_10_Event)oMailItem).Send += (MailService_Send);
                    ((ItemEvents_10_Event)oMailItem).Close += (ThisAddIn_Close);

                    oMailItem.GetType().InvokeMember("Display", BindingFlags.InvokeMethod, null, oMailItem, new object[] { true });

                    return SentFromOutlook;
                }
                else
                {
                    MessageBox.Show("Failed to create Outlook Application object.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogFile(ex);
                MessageBox.Show("שליחת המייל נכשלה,אנא פנה לתמיכה.", "Nautilus", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;

            }

        }


        public static bool SentFromOutlook;

        static void MailService_Send(ref bool Cancel)
        {
            SentFromOutlook = true;
        }



        static void ThisAddIn_Close(ref bool Cancel)
        {

        }







    }






    public class MailDetails
    {
        public MailDetails()
        {
            To = new List<string>();
            CC = new List<string>();
            AtachmentPathes = new List<string>();
        }


        public string SmtpClient { get; set; }
        public string FromAddress { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> AtachmentPathes { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public HeaderDetails HeaderDtls { get; set; }


    }
    public class HeaderDetails
    {
        public HeaderDetails()
        {
                
        }
        public string OrderName { get; set; }
        public string CoaName { get; set; }
        public string ClientId { get; set; }
        public string FirstSampleDetails { get; set; }

        public override string ToString()
        {
            return $"{OrderName}{(string.IsNullOrEmpty(CoaName) ? "" : " - ")}{(string.IsNullOrEmpty(CoaName) ? "" : CoaName)}{(string.IsNullOrEmpty(CoaName) || string.IsNullOrEmpty(ClientId) ? "" : " - ")}{(string.IsNullOrEmpty(ClientId) ? "" : ClientId)}{(string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(FirstSampleDetails) ? "" : " - ")}{(string.IsNullOrEmpty(FirstSampleDetails) ? "" : FirstSampleDetails)}";
        }

    }
}
