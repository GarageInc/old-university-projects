using SimpleWifi.Win32.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleWifi.Win32
{
	public struct WlanConnectionNotificationEventData
	{
		public WlanNotificationData notifyData;
		public WlanConnectionNotificationData connNotifyData;
	}

	public struct WlanReasonNotificationData
	{
		public WlanNotificationData notifyData;
		public WlanReasonCode reasonCode;
	}
}
