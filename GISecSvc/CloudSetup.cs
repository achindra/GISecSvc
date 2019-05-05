using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace GISecSvc
{
    public static class CloudSetup
    {
        private static readonly string DeviceId = Dns.GetHostName();
        private static readonly string _connectionString = "HostName=SecureCollector.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=d4PL4NoS12HE6jHZvAYep1d/1U+yWc70YvvcR9W/lsc=";
        private static readonly RegistryManager RegistryManager = RegistryManager.CreateFromConnectionString(_connectionString);
        
        public static DeviceClient DeviceClient = DeviceClient.CreateFromConnectionString(_connectionString, DeviceId);

        public static void Initialize()
        {
            AddDeviceAsync().Wait();
            ReceiveMessagesAsync();
        }

        private static async Task AddDeviceAsync()
        {
            Device device;
            try
            {
                device = await RegistryManager.AddDeviceAsync(new Device(DeviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await RegistryManager.GetDeviceAsync(DeviceId);
            }
            Console.WriteLine("{0} device key: {1}", DeviceId, device.Authentication.SymmetricKey.PrimaryKey);
        }

        public static async void SendMessagesAsync(string messageType, object objectToSend)
        {
            var messageString = JsonConvert.SerializeObject(objectToSend);
            var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));
            message.Properties.Add("MessageType", messageType);
            message.Properties.Add("DeviceId", DeviceId);

            await DeviceClient.SendEventAsync(message);
            //Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message.);
        }

        private static async void ReceiveMessagesAsync()
        {
            while (true)
            {
                Microsoft.Azure.Devices.Client.Message receivedMessage = await DeviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                var msg = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                Console.WriteLine("Received Message {0}",msg);
                
                await DeviceClient.CompleteAsync(receivedMessage);
            }
        }
    }
}
