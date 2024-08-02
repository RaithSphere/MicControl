using System;
using System.Threading;
using NAudio.CoreAudioApi;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            // List available microphones
            Console.WriteLine($"CytraX Microphone Monitor");
            Console.WriteLine($"This will monitor the selected microphone and keep it unmuted");
            Console.WriteLine($"This was created due to my broken Vive Pro eye muting the mic due to a short");
            Console.WriteLine($"-------------------------");
            Console.WriteLine("Available microphones:");
            for (int i = 0; i < devices.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {devices[i].FriendlyName}");
            }

            int selectedIndex = -1;
            while (selectedIndex < 0 || selectedIndex >= devices.Count)
            {
                Console.Write("Select a microphone by number: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out selectedIndex) || selectedIndex < 1 || selectedIndex > devices.Count)
                {
                    Console.WriteLine("Invalid selection. Please enter a number between 1 and {0}.", devices.Count);
                    selectedIndex = -1;
                }
                else
                {
                    selectedIndex--; 
                }
            }

            MMDevice selectedDevice = devices[selectedIndex];
            Console.Clear();
            Console.WriteLine($"CytraX Microphone Monitor");
            Console.WriteLine($"Selected microphone: {selectedDevice.FriendlyName}");

            while (true)
            {
                bool isMuted = selectedDevice.AudioEndpointVolume.Mute;
                if (isMuted)
                {
                    Console.WriteLine("Microphone is muted. Unmuting...");
                    selectedDevice.AudioEndpointVolume.Mute = false; 
                    Console.Beep(); 
                }

                Thread.Sleep(5000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
