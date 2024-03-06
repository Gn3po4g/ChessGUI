using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChessGUI {
    class Engine {
        private Process engineProcess;
        public Engine(string name, Action<string> onReceive) {
            engineProcess = new() {
                StartInfo = new() {
                    FileName = name,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    //UseShellExecute = true,
                    CreateNoWindow = true
                }
            };
            engineProcess.OutputDataReceived += (sender, args) => {
                if (!string.IsNullOrEmpty(args.Data)) { onReceive(args.Data); }
            };

            engineProcess.Start();
            engineProcess.BeginOutputReadLine();
        }

        public void Send(string message) {
            engineProcess.StandardInput.WriteLine(message);
            engineProcess.StandardInput.Flush();
        }
    }
}
