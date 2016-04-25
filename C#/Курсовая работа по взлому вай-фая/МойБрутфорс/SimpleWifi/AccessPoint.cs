using SimpleWifi.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SimpleWifi.Win32.Interop;
using System.Windows.Forms;

namespace SimpleWifi
{
	public class AccessPoint
	{
		private WlanInterface _interface;
		private WlanAvailableNetwork _network;

		public AccessPoint(WlanInterface interfac, WlanAvailableNetwork network)
		{
			_interface = interfac;
			_network = network;
		}

		public string Name
		{
			get
			{
				return Encoding.ASCII.GetString(_network.dot11Ssid.SSID, 0, (int)_network.dot11Ssid.SSIDLength);
			}
		}

		public uint SignalStrength
		{
			get
			{
				return _network.wlanSignalQuality;
			}
		}

		/// <summary>
		/// If the computer has a connection profile stored for this access point
		/// </summary>
		public bool HasProfile
		{
			get
			{
                return _interface.GetProfiles().Where(p => p.profileName == Name).Any();
			}
		}
		
		public bool IsSecure
		{
			get
			{
				return _network.securityEnabled;
			}
		}

		public bool IsConnected
		{
			get
			{
					var a = _interface.CurrentConnection; // This prop throws exception if not connected, which forces me to this try catch. Refactor plix.
					return a.profileName == _network.profileName;
			}

		}

		/// <summary>
		/// Returns the underlying network object.
		/// </summary>
		public WlanAvailableNetwork Network
		{
			get
			{
				return _network;
			}
		}


		/// <summary>
		/// Returns the underlying interface object.
		/// </summary>
		public WlanInterface Interface
		{
			get
			{
				return _interface;
			}
		}

		/// <summary>
		/// Checks that the password format matches this access point's encryption method.
		/// </summary>
		public bool IsValidPassword(string password)
		{
			return PasswordHelper.IsValid(password, _network.dot11DefaultCipherAlgorithm);
		}		
		
		/// <summary>
		/// Connect synchronous to the access point.
		/// </summary>
        public bool Connect(AuthRequest request, bool overwriteProfile = false, int timeout = 6000)
		{
			// No point to continue with the connect if the password is not valid if overwrite is true or profile is missing.
			if (!request.IsPasswordValid && (!HasProfile || overwriteProfile))
				return false;


            bool result = false;
			// If we should create or overwrite the profile, do so.
			if (!HasProfile || overwriteProfile)
			{				
				if (HasProfile)
					_interface.DeleteProfile(Name);

                    request.Process();
			}
            bool connect = false;
            //while (!connect)
            //{
                    //result = _interface.ConnectSynchronously(WlanConnectionMode.Profile, _network.dot11BssType, Name, timeout);
                    //while (_interface.InterfaceState == WlanInterfaceState.Associating ||
                    //        _interface.InterfaceState == WlanInterfaceState.Authenticating)
                    //{
                    //    Thread.Sleep(10);
                    //}
                    while (_interface.InterfaceState == WlanInterfaceState.Associating ||
                            _interface.InterfaceState == WlanInterfaceState.Authenticating)
                    {
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(2500);
                    if (_interface.InterfaceState == WlanInterfaceState.Connected)
                    {
                        result = true;
                        connect = true;
                    }
            //}
            
            // TODO: Auth algorithm: IEEE80211_Open + Cipher algorithm: None throws an error.
            // Probably due to connectionmode profile + no profile exist, cant figure out how to solve it though.
            return result;
		}

		/// <summary>
		/// Connect asynchronous to the access point.
		/// </summary>
		public void ConnectAsync(AuthRequest request, bool overwriteProfile = false)
		{
			// TODO: Refactor -> Use async connect in wlaninterface.
			ThreadPool.QueueUserWorkItem(new WaitCallback((o) => {
                    Connect(request, overwriteProfile);
			}));
		}
				
		public string GetProfileXML()
		{
			if (HasProfile)
				return _interface.GetProfileXml(Name);
			else
				return string.Empty;
		}

		public void DeleteProfile()
		{
				if (HasProfile)
					_interface.DeleteProfile(Name);
		}

		public ListViewItem ShowActive()
		{
            // создаём экземпляр элемента листвью
            ListViewItem listItemWiFi = new ListViewItem();

            // назначаем ему имя найденой сети, в конце убираем нулевые символы - Trim((char)0)
            listItemWiFi.Text = System.Text.ASCIIEncoding.ASCII.GetString(_network.dot11Ssid.SSID).Trim((char)0);

            // узнаём дополнительную информацию о сети и так же добавляем её в листвью, но уже в наш только что созданый итем.
            listItemWiFi.SubItems.Add(_network.wlanSignalQuality.ToString() + "%"); //Качество
            listItemWiFi.SubItems.Add(_network.dot11DefaultAuthAlgorithm.ToString().Trim((char)0)); // Тип безопасности
            listItemWiFi.SubItems.Add(_network.dot11DefaultCipherAlgorithm.ToString().Trim((char)0));// Тип шифрования

            listItemWiFi.SubItems.Add(string.Join(",", _network.dot11BssType));// Тип сети: специальная или инфраструктура
            listItemWiFi.SubItems.Add(string.Join(",", _network.Dot11PhyTypes));// Поддерживаемая система 
            listItemWiFi.SubItems.Add(_network.profileName.ToString());// Профильное имя
            listItemWiFi.SubItems.Add(_network.networkConnectable.ToString());// Возможность соединения

            listItemWiFi.ImageIndex = 0;

            return listItemWiFi;
		}
	}
}
