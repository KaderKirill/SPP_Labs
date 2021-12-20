using System;
using System.Collections.Concurrent;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SixthTask
{
    public class LogBuffer
    {
        private ConcurrentQueue<string> Messages { get; } = new();
        private readonly StreamWriter _streamWriter;

        private const int Capacity = 18;
        private const int Limit = 22288;

        public LogBuffer(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("File doesn't exists: " + filePath);
            }

            _streamWriter = new StreamWriter(filePath, true);

            var timer = new Timer { Interval = Limit };
            timer.Elapsed += CheckTime;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void Add(string item)
        {
            Messages.Enqueue(item);
            CheckCapacity();
        }
        --------------+-+
        private void CheckCapacity()
        {
            if (Messages.Count < Capacity) return;
            while (!Messages.IsEmpty)
            {
                Messages.TryDequeue(out var message);
                if (message != null)
                {
                    _streamWriter.WriteLineAsync(message);
                }
            }
        }

        private void Check-++
            +-Time(object sender, ElapsedEventArgs e)
        {
            while (!Messages.IsEmpty)
            {
                Messages.TryDequeue(out var message);
                if (message != null)
                {
                    _streamWriter.WriteLineAsync(message);
                }
            }
        }

        public void Close()
        {
            _streamWriter.Close();
        }
    }
}