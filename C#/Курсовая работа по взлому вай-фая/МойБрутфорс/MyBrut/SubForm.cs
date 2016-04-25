using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleWifi;
using SimpleWifi.Win32;

namespace MyBrut
{
    public partial class SubForm : Form
    {
        AccessPoint selectedAP;
        AuthRequest authRequest;
        bool overwrite;
        RichTextBox showRichTextBox2;
        public SubForm(AccessPoint ap, ref RichTextBox showRichTextBox2)
        {
            this.showRichTextBox2 = showRichTextBox2;
            InitializeComponent();

            this.selectedAP = ap;
            

            // Создаём объект аутентификации
            authRequest = new AuthRequest(selectedAP);
            overwrite = true;

                textBoxUsername.Enabled = authRequest.IsUsernameRequired;
                textBoxPassword.Enabled = authRequest.IsPasswordRequired;
                textBoxDomain.Enabled = authRequest.IsDomainSupported;
        }

        string connectionString;
        


        private void ConnectSubButton_Click(object sender, EventArgs e)
        {
            // Требуется ли пароль
            if (authRequest.IsPasswordRequired)
            {
                if (selectedAP.HasProfile) // Спросим - соединиться по старому профилю или по новому?
                {
                    showRichTextBox2.Text += ("\r\n-> Профиль уже существует, хочешь ли ты соединиться по старому паролю?");
                    if (MessageBox.Show("Профиль уже существует, хочешь ли ты соединиться по старому паролю? ", "Сообщение", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
                    {
                        overwrite = false;
                        textBoxUsername.Visible = false;
                        textBoxPassword.Visible = false;
                        textBoxDomain.Visible = false;
                        
                    }
                }

                if (overwrite)
                {
                    if (authRequest.IsUsernameRequired)
                    {
                        showRichTextBox2.Text += ("\r\n-> Пожалуйста, введите имя пользователя");
                        authRequest.Username = textBoxUsername.Text;
                    }

                    string password = string.Empty;
                    bool validPassFormat = false;
                    if (!validPassFormat)
                    {
                        showRichTextBox2.Text += ("\r\n-> Пожалуйста, введите пароль к точке ");
                        password = textBoxPassword.Text;

                        validPassFormat = selectedAP.IsValidPassword(password);

                        if (!validPassFormat)
                        {
                            showRichTextBox2.Text += ("\r\n-> Неверный пароль.");
                            MessageBox.Show("Не верный пароль.");
                            return;
                        }
                        else
                        {
                            authRequest.Password = password;
                        }
                    }

                    if (authRequest.IsDomainSupported)
                    {
                        showRichTextBox2.Text += ("\r\n-> Пожалуйста, введите имя домена: ");
                        authRequest.Domain = textBoxDomain.Text;
                    }
                }
            }
            bool success = selectedAP.Connect(authRequest, overwrite);
            connectionString = string.Format("\nСоединение удалось: {0}", success ? "да" : "нет");
            
            showRichTextBox2.Text += connectionString;

            this.Close();
        }
    }
}
