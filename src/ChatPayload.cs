namespace XIVITAStartupCommands
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Explicit)]
    [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
    public readonly struct ChatPayload : IDisposable
    {
        [FieldOffset(0)] private readonly IntPtr textPointer;

        [FieldOffset(16)] private readonly ulong textLength;

        [FieldOffset(8)] private readonly ulong unk1;

        [FieldOffset(24)] private readonly ulong unk2;

        internal ChatPayload(string text)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(text);
            this.textPointer = Marshal.AllocHGlobal(stringBytes.Length + 30);
            Marshal.Copy(stringBytes, 0, this.textPointer, stringBytes.Length);
            this.textLength = (ulong) (stringBytes.Length + 1);
            this.unk1 = 64;
            this.unk2 = 0;
        }

        public void Dispose()
        {

        }
    }
}