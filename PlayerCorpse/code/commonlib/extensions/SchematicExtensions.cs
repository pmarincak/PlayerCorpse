using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.Datastructures;
using Vintagestory.ServerMods.WorldEdit;

namespace CommonLib.Extensions
{
    public static class SchematicExtensions
    {

        public static void ImportToWorldEdit(this BlockSchematic schematic, IServerPlayer player)
        {
            var api = player.Entity.Api;
            var worldEdit = api.ModLoader.GetModSystem<WorldEdit>();
            if (WorldEdit.CanUseWorldEdit(player, true))
            {
                var clipboardBlockDataField = typeof(WorldEditWorkspace).GetField("clipboardBlockData", BindingFlags.Instance | BindingFlags.NonPublic);
                var workSpace = worldEdit.GetWorkSpace(player.PlayerUID);
                clipboardBlockDataField?.SetValue(workSpace, schematic);

                var caller = new Caller() { Player = player };
                api.ChatCommands.ExecuteUnparsed("/we tool import", new TextCommandCallingArgs() { Caller = caller });
                api.ChatCommands.ExecuteUnparsed("/we imc", new TextCommandCallingArgs() { Caller = caller });
            }
        }
    }
}
