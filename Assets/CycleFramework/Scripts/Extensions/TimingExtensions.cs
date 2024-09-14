using System;
using System.Net;
using System.Net.Sockets;

namespace CycleFramework.Extensions
{
    public static class TimingExtensions
    {
        public static string SecsToMins(this int secs, bool twoZeroesInStart = true)
        {
            int mins = secs / 60;
            secs %= 60;

            string minsStr = mins.ToString();
            string secsStr = secs.ToString();

            if (minsStr.Length == 1 && twoZeroesInStart)
                minsStr = $"0{minsStr}";

            if (secsStr.Length == 1)
                secsStr = $"0{secsStr}";

            return $"{minsStr}:{secsStr}";
        }

        public static string SecsToMins(this float secs, bool twoZeroesInStart = true)
        {
            return SecsToMins((int)secs, twoZeroesInStart);
        }

        public static bool TryGetNetworkTime(out DateTime dateTime)
        {
            dateTime = DateTime.Now;

            const string ntpServer = "ntp.ix.ru";
            var ntpData = new byte[48];

            ntpData[0] = 0x1B;

            try
            {
                var addresses = Dns.GetHostEntry(ntpServer).AddressList;
                var ipEndPoint = new IPEndPoint(addresses[0], 123);

                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    socket.Connect(ipEndPoint);

                    socket.ReceiveTimeout = 3000;
                    socket.Send(ntpData);

                    socket.Receive(ntpData);
                    socket.Close();
                }

                bool allIsZero = true;
                for (int i = 1; i < ntpData.Length; i++)
                    if(ntpData[i] != 0)
                    {
                        allIsZero = false;
                        break;
                    }

                if (allIsZero)
                    return false;

                const byte serverReplyTime = 40;

                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

                dateTime = networkDateTime.ToLocalTime();
                return true;
            }
            catch
            {
                return false;
            }

            static uint SwapEndianness(ulong x)
            {
                return (uint)(((x & 0x000000ff) << 24) +
                              ((x & 0x0000ff00) << 8) + ((x & 0x00ff0000) >> 8) +
                              ((x & 0xff000000) >> 24));
            }
        }

    }
}
