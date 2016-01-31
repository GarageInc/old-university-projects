using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SpamEmAll
{
    public partial class Form1 : Form
    {
        private static int xuint=0;
        struct data
        {
            internal string from;
            internal string pswd;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread th;
            string tmp;
            StreamReader file = new StreamReader("D://file.txt");
            while ((tmp = file.ReadLine()) != null)
            {
                data ps = new data();
                ps.from = tmp.Split(':').First();
                ps.pswd = tmp.Split(':').Last();
                //Send(ps);
                th = new Thread(Send);
                th.Start(ps);
                
            }
             MessageBox.Show("Усё");
            
        }
        private void Send(object obj)
        {
            if (obj.GetType() != typeof(data))   return;
            data ps = (data)obj;
            while(SendMail("smtp.mail.ru", ps.from, ps.pswd, "rich815@bk.ru", "Ахтунг!!!", "Твоя задница под угрозой."))
            {
                if (this.InvokeRequired)
                {
                    Action action = RefreshText;
                    Invoke(action);
                }
                else
                    RefreshText();

            }
        }
        private void RefreshText()
        {
            xuint++;
            txtOut.Text = xuint.ToString();
        }


        public static bool SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
             //   throw new Exception("Mail.Send: " + e.Message);
            }
            return false;
        }
    }
}
