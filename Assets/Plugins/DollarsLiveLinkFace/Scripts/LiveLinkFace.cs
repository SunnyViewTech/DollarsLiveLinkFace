using UnityEngine;
using System.Linq;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace Dollars
{
    public class LiveLinkFace : MonoBehaviour
    {
        Thread receiveThread;
        UdpClient client;

        public int port = 11111;
        public Dictionary<string, float> values = new Dictionary<string, float>();
        IPEndPoint anyIP;

        public void Start()
        {
            for (var i = 0; i < LiveLinkTrackingData.Names.Length; i++)
            {
                values.Add(LiveLinkTrackingData.Names[i], 0f);
            }

            init();
            anyIP = new IPEndPoint(IPAddress.Any, 0);
        }

        private void OnDestroy()
        {
            receiveThread.Abort();
        }

        private void init()
        {
            receiveThread = new Thread(
                new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        private void ReceiveData()
        {
            client = new UdpClient(port);
            while (true)
            {
                byte[] recieveBytes = client.Receive(ref anyIP);

                if (recieveBytes.Length < 244)
                {
                    continue;
                }

                // There is a bunch of static data at the beginning of the packet, it may be variable length because it includes phone name
                // So grab the last 244 bytes of the packet sent using some Linq magic, since that's where our blendshapes live
                IEnumerable<Byte> trimmedBytes = recieveBytes.Skip(Math.Max(0, recieveBytes.Count() - 244));

                // More Linq magic, this splits our 244 bytes into 61, 4-byte chunks which we can then turn into floats
                List<List<Byte>> chunkedBytes = trimmedBytes
                    .Select((x, i) => new { Index = i, Value = x })
                    .GroupBy(x => x.Index / 4)
                    .Select(x => x.Select(v => v.Value).ToList())
                    .ToList();

                // Process each float in out chunked out list
                foreach (var item in chunkedBytes.Select((value, i) => new { i, value }))
                {
                    // First, reverse the list because the data will be in big endian, then convert it to a float
                    item.value.Reverse();
                    values[LiveLinkTrackingData.Names[item.i]] = BitConverter.ToSingle(item.value.ToArray(), 0);
                }

            }
        }
    }
}
