namespace XIVITAStartupCommands
{
    using System;
    using System.Numerics;
    using ImGuiNET;


    public class ConfigWindow : UIWindow
    {
        private const float MinimumWindowWidth = 450;
        private const float MinimumWindowHeight = 170;
        
        #region Properties
        public override Vector2 WindowSize
        {
            get
            {
                int customCommandCount = Plugin.Configuration.CustomCommands != null
                                             ? Plugin.Configuration.CustomCommands.Count
                                             : 0;
                
                float flexHeight = 27 * customCommandCount;

                float newWidth = MinimumWindowWidth;
                float newHeight = MinimumWindowHeight + flexHeight;

                return new Vector2(newWidth, newHeight);
            }
        }



        protected override ImGuiWindowFlags WindowFlags { get; } =
            ImGuiWindowFlags.NoScrollbar 
            | ImGuiWindowFlags.NoScrollWithMouse
            | ImGuiWindowFlags.NoCollapse
            | ImGuiWindowFlags.NoResize;


        protected override string WindowTitle { get; } = "Configurazione XIVITA Custom Startup Commands";
        #endregion


        protected override void OnDraw()
        {
            // NOTE: Can't ref config properties, so always create a local copy first for UI.
            
            DrawChatChannelSelection();

            ImGui.Spacing();
            ImGui.Spacing();

            DrawCustomCommands();

            ImGui.NewLine();
            ImGui.NewLine();

            DrawExecuteButton();
            
            ImGui.SameLine(this.WindowSize.X - 146);
            
        }


        private static void DrawExecuteButton()
        {
            if (ImGui.Button("Esegui Adesso"))
            {
                Plugin.StartupHandlers.RunStartupBehaviors();
            }
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Esegui tutte le azioni adesso.");
            }
        }


        private void DrawCustomCommands()
        {
            ImGui.Separator();
            ImGui.Text("Comandi di Startup Personalizzati:");
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Comandi di chat personalizzati da eseguire al login.");
            }
            
            ImGui.SameLine();
            if (ImGui.Button("Aggiungi"))
            {
                Plugin.Configuration.CustomCommands.Add(new CharacterConfiguration.CustomCommand());
            }
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Aggiungi nuovo comando personalizzato.");
            }

            bool updated = false;
            CharacterConfiguration.CustomCommand commandToRemove = null;
            int index = 0;
            foreach (CharacterConfiguration.CustomCommand customCommand in Plugin.Configuration.CustomCommands)
            {
                string currentChatCommand = customCommand.Command;
                ImGui.Bullet();
                ImGui.SameLine();
                ImGui.PushItemWidth(this.WindowSize.X - 105);

                updated = ImGui.InputText($"###inputText_Command_{index}", ref currentChatCommand, 512);

                ImGui.SameLine();
                if (ImGui.Button($"Rimuovi###button_Remove{index}"))
                {
                    commandToRemove = customCommand;
                }

                if (updated)
                {
                    Plugin.Configuration.CustomCommands[index].Command = currentChatCommand;
                    Plugin.Configuration.Save();
                }

                index++;
            }

            if (commandToRemove != null)
            {
                Plugin.Configuration.CustomCommands.Remove(commandToRemove);
                Plugin.Configuration.Save();
            }
        }


        private static void DrawChatChannelSelection()
        {
            string defaultChatChannelName = Plugin.Configuration.DefaultChatChannel.ToName();

            if (ImGui.BeginCombo("Canale Chat di Default", defaultChatChannelName))
            {
                foreach (ChatChannel channel in Enum.GetValues(typeof(ChatChannel)))
                {
                    string channelName = channel.ToName();
                    bool selected = channelName == defaultChatChannelName;
                    if (ImGui.Selectable(channelName, selected))
                        Plugin.Configuration.DefaultChatChannel = channel;
                    if (selected)
                    {
                        ImGui.SetItemDefaultFocus();
                    }
                }

                ImGui.EndCombo();
                Plugin.Configuration.Save();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Canale di chat In-game da usare al login. Usare None per disabilitare.");
            }
        }
    }
}