namespace XIVITAStartupCommands
{
    using ImGuiNET;
    using System;
    using System.Numerics;
    public class PluginUI : IDisposable
    {
        #region Properties
        public ConfigWindow ConfigWindow { get; } = new ConfigWindow();
        #endregion
        
        public void Dispose()
        {
        }
        
        public void Draw()
        {
            this.ConfigWindow.Draw();
        }
    }
}