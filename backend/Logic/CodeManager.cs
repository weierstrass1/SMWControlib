using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SMWControlibBackend.Logic
{
    public class CodeManagerGFXType
    {
        public static int Length { get; private set; } = 0;
        internal static Dictionary<int, CodeManagerGFXType> instances = new Dictionary<int, CodeManagerGFXType>();
        public static CodeManagerGFXType Regular = new CodeManagerGFXType(0, "Regular");
        public static CodeManagerGFXType Dynamic = new CodeManagerGFXType(1, "Dynamic");
        public static CodeManagerGFXType SharedDynamic = new CodeManagerGFXType(2, "SharedDynamic");
        public static CodeManagerGFXType SemiDynamic = new CodeManagerGFXType(3, "SemiDynamic");
        public static CodeManagerGFXType Player = new CodeManagerGFXType(4, "Player");
        public int Value { get; private set; }
        public string Path { get; private set; }

        private CodeManagerGFXType(int val, string path)
        {
            Value = val;
            Path = path;
            Length++;
            instances.Add(val, this);
        }

        public static implicit operator int(CodeManagerGFXType cmgt) => cmgt.Value;

        public static implicit operator CodeManagerGFXType(int id)
        {
            if (instances.ContainsKey(id))
                return instances[id];

            return Regular;
        }
    }
    public class CodeManagerSpriteType
    {
        public static int Length { get; private set; } = 0;
        internal static Dictionary<int, CodeManagerSpriteType> instances = new Dictionary<int, CodeManagerSpriteType>();
        public static CodeManagerSpriteType Normal = new CodeManagerSpriteType(0, "Normal");
        public static CodeManagerSpriteType Cluster = new CodeManagerSpriteType(1, "Cluster");
        public static CodeManagerSpriteType Extended = new CodeManagerSpriteType(2, "Extended");
        public static CodeManagerSpriteType OW = new CodeManagerSpriteType(3, "OW");
        public int Value { get; private set; }
        public string Path { get; private set; }

        private CodeManagerSpriteType(int val,string path)
        {
            Value = val;
            Path = path;
            Length++;
            instances.Add(val, this);
        }

        public static implicit operator int(CodeManagerSpriteType cmst) => cmst.Value;

        public static implicit operator CodeManagerSpriteType(int id)
        {
            if (instances.ContainsKey(id))
                return instances[id];

            return Normal;
        }
    }
    public class CodeManagerPaths
    {
        public const string CodePath = "Code";
        public const string SpriteTypePath = "SpriteType";

        public const string DefinesPath = "Defines.asm";
        public const string InitPath = "Init.asm";
        public const string SpriteCodePath = "SpriteCode.asm";
        public const string DynamicRoutinePath = "DynamicRoutine.asm";
        public const string GraphicRoutinePath = "GraphicRoutine.asm";
        public const string AnimationRoutinePath = "AnimationRoutine.asm";
        public const string InteractionRoutinePath = "InteractionRoutine.asm";
        public const string InteractionPlayerSpriteTablePath = "InteractionPlayerSpriteTable.asm";
        public const string InteractionSpriteSpriteTablePath = "InteractionSpriteSpriteTable.asm";
        public const string InteractionClusterSpriteTablePath = "InteractionClusterSpriteTable.asm";
        public const string InteractionExtendedSpriteTablePath = "InteractionExtendedSpriteTable.asm";

        private CodeManagerPaths()
        {

        }
    }
    public class CodeManagerLabels
    {
        public const string NumberOfTilesTilesLabel = "#tiles";
        public const string GraphicSystemLabel = "graphic";
        public const string FlipLabel = "flip";
        public const string AnimSystemLabel = "anim";
        public const string PlayerInterSystemLabel = "playerInter";
        public const string SpriteInterSystemLabel = "spriteInter";
        public const string ClusterInterSystemLabel = "clusterInter";
        public const string ExtendedInterSystemLabel = "extendedInter";
        public const string RegularInter = "regularInteraction";

        private CodeManagerLabels()
        {

        }
    }
    public struct CodeManagerOptions
    {
        public int NumberOfTiles;
        public bool UseGraphicSystem;
        public bool CanFlip;
        public bool UseAnimSystem;
        public bool UsePlayerInterSystem;
        public bool UseSpriteInterSystem;
        public bool UseClusterInterSystem;
        public bool UseExtendedInterSystem;
        public bool UseRegularInter;
        public CodeManagerSpriteType SpriteType;
        public CodeManagerGFXType GFXType;

        public int GetIndex()
        {
            return (SpriteType * CodeManagerGFXType.Length) + GFXType;
        }
    }
    public class CodeManager
    {
        private string[] Defines;
        private string[] Init;
        private string[] SpriteCode;
        private string[] DynamicRoutine;
        private string[] GraphicRoutine;
        private string[] AnimationRoutine;
        private string[] InteractionRoutine;
        private string[] InteractionPlayerSpriteTable;
        private string[] InteractionSpriteSpriteTable;
        private string[] InteractionClusterSpriteTable;
        private string[] InteractionExtendedSpriteTable;

        private string[] spriteTypes, gfxTypes;

        private static CodeManager instance;
        private static CodeManager getInstance()
        {
            if(instance == null)
            {
                instance = new CodeManager();


                int l = CodeManagerSpriteType.Length * CodeManagerGFXType.Length;

                instance.Defines = new string[l];
                instance.Init = new string[l];
                instance.SpriteCode = new string[l];
                instance.DynamicRoutine = new string[l];
                instance.GraphicRoutine = new string[l];
                instance.AnimationRoutine = new string[l];
                instance.InteractionRoutine = new string[l];
                instance.InteractionPlayerSpriteTable = new string[l];
                instance.InteractionSpriteSpriteTable = new string[l];
                instance.InteractionClusterSpriteTable = new string[l];
                instance.InteractionExtendedSpriteTable = new string[l];

                instance.spriteTypes = new string[CodeManagerSpriteType.Length];
                instance.gfxTypes = new string[CodeManagerGFXType.Length];

                foreach(var kvp in CodeManagerSpriteType.instances)
                {
                    instance.spriteTypes[kvp.Key] = kvp.Value.Path;
                }

                foreach (var kvp in CodeManagerGFXType.instances)
                {
                    instance.gfxTypes[kvp.Key] = kvp.Value.Path;
                }

                string basepath = Path.Combine(CodeManagerPaths.CodePath, CodeManagerPaths.SpriteTypePath);

                int q = 0;
                for (int i = 0; i < instance.spriteTypes.Length; i++)
                {
                    for (int j = 0; j < instance.gfxTypes.Length; j++)
                    {
                        instance.Defines[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.DefinesPath);
                        instance.Init[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.InitPath);
                        instance.SpriteCode[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.SpriteCodePath);
                        instance.DynamicRoutine[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.DynamicRoutinePath);
                        instance.GraphicRoutine[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.GraphicRoutinePath);
                        instance.AnimationRoutine[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.AnimationRoutinePath);
                        instance.InteractionRoutine[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.InteractionRoutinePath);
                        instance.InteractionPlayerSpriteTable[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.InteractionPlayerSpriteTablePath);
                        instance.InteractionSpriteSpriteTable[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.InteractionSpriteSpriteTablePath);
                        instance.InteractionClusterSpriteTable[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.InteractionClusterSpriteTablePath);
                        instance.InteractionExtendedSpriteTable[q] = Path.Combine(basepath, instance.spriteTypes[i], instance.gfxTypes[j], CodeManagerPaths.InteractionExtendedSpriteTablePath);
                        q++;
                    }
                }
            }
            return instance;
        }
        
        public static string BuildFile(CodeManagerOptions Options)
        {
            int index = Options.GetIndex();

            StringBuilder sb = new StringBuilder();

            string tmp;

            if (File.Exists(instance.Defines[index]))
            {
                tmp = replaceLabels(instance.Defines[index], Options);

                sb.Append(tmp);
                sb.Append("\n");
            }
            if (File.Exists(instance.Init[index]))
            {
                tmp = replaceLabels(instance.Init[index], Options);

                tmp = tmp.Replace($">@{CodeManagerLabels.NumberOfTilesTilesLabel}.", 
                                    "#$" + Options.NumberOfTiles.ToString("X2"));

                sb.Append(tmp);
                sb.Append("\n");
            }
            if (File.Exists(instance.SpriteCode[index]))
            {
                tmp = replaceLabels(instance.SpriteCode[index], Options);

                sb.Append(tmp);
                sb.Append("\n");
            }
            if (File.Exists(instance.DynamicRoutine[index]))
            {
                sb.Append(replaceLabels(instance.DynamicRoutine[index], Options));
                sb.Append("\n");
            }
            if (File.Exists(instance.GraphicRoutine[index]) && Options.UseGraphicSystem)
            {
                sb.Append(replaceLabels(instance.GraphicRoutine[index], Options));
                sb.Append("\n");
            }
            if (File.Exists(instance.AnimationRoutine[index]) && Options.UseAnimSystem)
            {
                sb.Append(replaceLabels(instance.AnimationRoutine[index], Options));
                sb.Append("\n");
            }
            if (File.Exists(instance.InteractionRoutine[index]) && Options.UsePlayerInterSystem)
            {
                sb.Append(replaceLabels(instance.InteractionRoutine[index], Options));
                sb.Append("\n");
            }
            if (File.Exists(instance.InteractionPlayerSpriteTable[index]) && Options.UsePlayerInterSystem)
            {
                sb.Append(replaceLabels(instance.InteractionPlayerSpriteTable[index], Options));
                sb.Append("\n");
            }
            if (File.Exists(instance.InteractionSpriteSpriteTable[index]) && Options.UsePlayerInterSystem)
            {
                sb.Append(replaceLabels(instance.InteractionSpriteSpriteTable[index], Options));
                sb.Append("\n");
            }
            if (File.Exists(instance.InteractionClusterSpriteTable[index]) && Options.UsePlayerInterSystem)
            {
                sb.Append(replaceLabels(instance.InteractionClusterSpriteTable[index], Options));
                sb.Append("\n");
            }
            if (File.Exists(instance.InteractionExtendedSpriteTable[index]) && Options.UsePlayerInterSystem)
            {
                sb.Append(replaceLabels(instance.InteractionExtendedSpriteTable[index], Options));
            }

            return sb.ToString();
        }

        private static string replaceLabels(string path, CodeManagerOptions Options)
        {
            string tmp1 = Path.GetFileNameWithoutExtension(path);

            string tmp2;

            string tmp = File.ReadAllText(path); ;

            string tmp3 = tmp1 + $"{CodeManagerLabels.AnimSystemLabel}.asm";
            if (Options.UseAnimSystem && File.Exists(tmp3))
            {
                tmp2 = File.ReadAllText(tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.AnimSystemLabel}.", tmp2);
            }
            else if(File.Exists("no"+tmp3))
            {
                tmp2 = File.ReadAllText("no" + tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.AnimSystemLabel}.", tmp2);
            }
            else
            {
                tmp = tmp.Replace($">@{CodeManagerLabels.AnimSystemLabel}.", "");
            }

            tmp3 = tmp1 + $"{CodeManagerLabels.FlipLabel}.asm";
            if (Options.CanFlip && File.Exists(tmp3))
            {
                tmp2 = File.ReadAllText(tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.FlipLabel}.", tmp2);
            }
            else if (File.Exists("no" + tmp3))
            {
                tmp2 = File.ReadAllText("no" + tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.FlipLabel}.", tmp2);
            }
            else
            {
                tmp = tmp.Replace($">@{CodeManagerLabels.FlipLabel}.", "");
            }

            tmp3 = tmp1 + $"{CodeManagerLabels.RegularInter}.asm";
            if (Options.UseRegularInter && Options.UsePlayerInterSystem && File.Exists(tmp3))
            {
                tmp2 = File.ReadAllText(tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.RegularInter}.", tmp2);
            }
            else if (Options.UsePlayerInterSystem && File.Exists(tmp1 + $"{CodeManagerLabels.PlayerInterSystemLabel}.asm"))
            {
                tmp2 = File.ReadAllText(tmp1 + $"{CodeManagerLabels.PlayerInterSystemLabel}.asm");
                tmp = tmp.Replace($">@{CodeManagerLabels.PlayerInterSystemLabel}.", tmp2);
            }
            else if (!Options.UsePlayerInterSystem && File.Exists("no" + tmp1 + $"{CodeManagerLabels.PlayerInterSystemLabel}.asm"))
            {
                tmp2 = File.ReadAllText("no" + tmp1 + $"{CodeManagerLabels.PlayerInterSystemLabel}.asm");
                tmp = tmp.Replace($">@{CodeManagerLabels.PlayerInterSystemLabel}.", tmp2);
            }
            else
            {
                tmp = tmp.Replace($">@{CodeManagerLabels.PlayerInterSystemLabel}.", "");
            }

            tmp3 = tmp1 + $"{CodeManagerLabels.SpriteInterSystemLabel}.asm";
            if (Options.UseSpriteInterSystem && File.Exists(tmp3))
            {
                tmp2 = File.ReadAllText(tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.SpriteInterSystemLabel}.", tmp2);
            }
            else if (File.Exists("no" + tmp3))
            {
                tmp2 = File.ReadAllText("no" + tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.SpriteInterSystemLabel}.", tmp2);
            }
            else
            {
                tmp = tmp.Replace($">@{CodeManagerLabels.SpriteInterSystemLabel}.", "");
            }

            tmp3 = tmp1 + $"{CodeManagerLabels.ClusterInterSystemLabel}.asm";
            if (Options.UseClusterInterSystem && File.Exists(tmp3))
            {
                tmp2 = File.ReadAllText(tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.ClusterInterSystemLabel}.", tmp2);
            }
            else if (File.Exists("no" + tmp3))
            {
                tmp2 = File.ReadAllText("no" + tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.ClusterInterSystemLabel}.", tmp2);
            }
            else
            {
                tmp = tmp.Replace($">@{CodeManagerLabels.ClusterInterSystemLabel}.", "");
            }

            tmp3 = tmp1 + $"{CodeManagerLabels.ExtendedInterSystemLabel}.asm";
            if (Options.UseExtendedInterSystem && File.Exists(tmp3))
            {
                tmp2 = File.ReadAllText(tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.ExtendedInterSystemLabel}.", tmp2);
            }
            else if (File.Exists("no" + tmp3))
            {
                tmp2 = File.ReadAllText("no" + tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.ExtendedInterSystemLabel}.", tmp2);
            }
            else
            {
                tmp = tmp.Replace($">@{CodeManagerLabels.ExtendedInterSystemLabel}.", "");
            }

            tmp3 = tmp1 + $"{CodeManagerLabels.GraphicSystemLabel}.asm";
            if (Options.UseGraphicSystem && File.Exists(tmp3))
            {
                tmp2 = File.ReadAllText(tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.GraphicSystemLabel}.", tmp2);
            }
            else if (File.Exists("no" + tmp3))
            {
                tmp2 = File.ReadAllText("no" + tmp3);
                tmp = tmp.Replace($">@{CodeManagerLabels.GraphicSystemLabel}.", tmp2);
            }
            else
            {
                tmp = tmp.Replace($">@{CodeManagerLabels.GraphicSystemLabel}.", "");
            }

            return tmp;
        }
    }
}
