using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    /// <summary>
    /// Contains various functions to apply patches.
    /// Binding from Asar 1.61, i only modified to use my format.
    /// </summary>
    public static unsafe class Asar
    {
        const int expectedApiVersion = 302;

        [DllImport("asar", EntryPoint = "asar_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool asar_init();

        [DllImport("asar", EntryPoint = "asar_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool asar_close();

        [DllImport("asar", EntryPoint = "asar_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int asar_version();

        [DllImport("asar", EntryPoint = "asar_apiversion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int asar_apiversion();

        [DllImport("asar", EntryPoint = "asar_reset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool asar_reset();

        [DllImport("asar", EntryPoint = "asar_patch", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool asar_patch(string patchLocation, byte* romData, int bufLen, int* romLength);

        [DllImport("asar", EntryPoint = "asar_patch_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool asar_patch_ex(ref rawPatchParams parameters);

        [DllImport("asar", EntryPoint = "asar_maxromsize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int asar_maxromsize();

        [DllImport("asar", EntryPoint = "asar_geterrors", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern rawAsarError* asar_geterrors(out int length);

        [DllImport("asar", EntryPoint = "asar_getwarnings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern rawAsarError* asar_getwarnings(out int length);

        [DllImport("asar", EntryPoint = "asar_getprints", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void** asar_getprints(out int length);

        [DllImport("asar", EntryPoint = "asar_getalllabels", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern rawAsarLabel* asar_getalllabels(out int length);

        [DllImport("asar", EntryPoint = "asar_getlabelval", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int asar_getlabelval(string labelName);

        [DllImport("asar", EntryPoint = "asar_getdefine", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr asar_getdefine(string defineName);

        [DllImport("asar", EntryPoint = "asar_getalldefines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern rawAsarDefine* asar_getalldefines(out int length);

        [DllImport("asar", EntryPoint = "asar_resolvedefines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr asar_resolvedefines(string data, bool learnNew);

        [DllImport("asar", EntryPoint = "asar_math", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern double asar_math(string math, out IntPtr error);

        [DllImport("asar", EntryPoint = "asar_getwrittenblocks", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern rawAsarWrittenBlock* asar_getwrittenblocks(out int length);

        [DllImport("asar", EntryPoint = "asar_getmapper", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern MapperType asar_getmapper();

        [DllImport("asar", EntryPoint = "asar_getsymbolsfile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr asar_getsymbolsfile(string format);

        /// <summary>
        /// Loads and initializes the DLL. You must call this before using any other Asar function.
        /// </summary>
        /// <returns>True if success</returns>
        public static bool Init()
        {
            try
            {
                if (ApiVersion < expectedApiVersion ||
                    (ApiVersion / 100) > (expectedApiVersion / 100))
                    return false;
                if (!asar_init()) return false;
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Closes Asar DLL. Call this when you're done using Asar functions.
        /// </summary>
        public static void Close()
        {
            asar_close();
        }

        /// <summary>
        /// Returns the version, in the format major*10000+minor*100+bugfix*1.
        /// This means that 1.2.34 would be returned as 10234.
        /// </summary>
        /// <returns>Asar version</returns>
        public static int Version
        {
            get
            {
                return asar_version();
            }
        }

        /// <summary>
        /// Returns the API version, format major*100+minor. Minor is incremented on backwards compatible
        ///  changes; major is incremented on incompatible changes. Does not have any correlation with the
        ///  Asar version.
        /// It's not very useful directly, since Asar.init() verifies this automatically.
        /// </summary>
        /// <returns>Asar API version</returns>
        public static int ApiVersion
        {
            get
            {
                return asar_apiversion();
            }
        }

        /// <summary>
        /// Clears out all errors, warnings and printed statements, and clears the file cache. Not useful for much, since patch() already does this.
        /// </summary>
        /// <returns>True if success</returns>
        public static bool Reset()
        {
            return asar_reset();
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private unsafe struct rawPatchParams
        {
            public int StructSize;
            public string PatchLoc;
            public byte* RomData;
            public int BufLen;
            public int* RomLen;
            public byte** IncludePaths;
            public int NumIncludePaths;
            [MarshalAs(UnmanagedType.I1)]
            public bool ShouldReset;
            public rawAsarDefine* AdditionalDefines;
            public int AdditionalDefineCount;
            public string StdIncludesFile;
            public string StdDefinesFile;
        };

        /// <summary>
        /// Applies a patch.
        /// </summary>
        /// <param name="patchLocation">The patch location.</param>
        /// <param name="romData">The rom data. It must not be headered.</param>
        /// <param name="includePaths">lists additional include paths</param>
        /// <param name="shouldReset">specifies whether asar should clear out all defines, labels,
        /// etc from the last inserted file. Setting it to False will make Asar act like the
        //  currently patched file was directly appended to the previous one.</param>
        /// <param name="additionalDefines">specifies extra defines to give to the patch</param>
        /// <param name="stdIncludeFile">path to a file that specifes additional include paths</param>
        /// <param name="stdDefineFile">path to a file that specifes additional defines</param>
        /// <returns>True if no errors.</returns>
        public static bool Patch(string patchLocation, ref byte[] romData, string[] includePaths = null, bool shouldReset = true, Dictionary<string, string> additionalDefines = null, string stdIncludeFile = null, string stdDefineFile = null)
        {
            if (includePaths == null) includePaths = new string[0];
            if (additionalDefines == null) additionalDefines = new Dictionary<string, string>();
            var includes = new byte*[includePaths.Length];
            var defines = new rawAsarDefine[additionalDefines.Count];
            try
            {
                for (int i = 0; i < includePaths.Length; i++)
                {
                    includes[i] = (byte*)Marshal.StringToCoTaskMemAnsi(includePaths[i]);
                }
                var keys = additionalDefines.Keys.ToArray();
                for (int i = 0; i < additionalDefines.Count; i++)
                {
                    var name = keys[i];
                    var value = additionalDefines[name];
                    defines[i].Name = Marshal.StringToCoTaskMemAnsi(name);
                    defines[i].Contents = Marshal.StringToCoTaskMemAnsi(value);
                }

                int newsize = MaxRomSize;
                int length = romData.Length;
                if (length < newsize) Array.Resize(ref romData, newsize);
                bool success;

                fixed (byte* ptr = romData)
                fixed (byte** includepaths = includes)
                fixed (rawAsarDefine* additional_defines = defines)
                {
                    var param = new rawPatchParams
                    {
                        PatchLoc = patchLocation,
                        RomData = ptr,
                        BufLen = newsize,
                        RomLen = &length,

                        ShouldReset = shouldReset,
                        IncludePaths = includepaths,
                        NumIncludePaths = includes.Length,
                        AdditionalDefines = additional_defines,
                        AdditionalDefineCount = defines.Length,
                        StdDefinesFile = stdDefineFile,
                        StdIncludesFile = stdIncludeFile
                    };
                    param.StructSize = Marshal.SizeOf(param);

                    success = asar_patch_ex(ref param);
                }
                if (length < newsize) Array.Resize(ref romData, length);
                return success;
            }
            finally
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    Marshal.FreeCoTaskMem((IntPtr)includes[i]);
                }
                foreach (var define in defines)
                {
                    Marshal.FreeCoTaskMem(define.Name);
                    Marshal.FreeCoTaskMem(define.Contents);
                }
            }
        }

        /// <summary>
        /// Returns the maximum possible size of the output ROM from asar_patch(). Giving this size to buflen
        /// guarantees you will not get any buffer too small errors; however, it is safe to give smaller
        /// buffers if you don't expect any ROMs larger than 4MB or something.
        /// It's not very useful directly, since Asar.patch() uses this automatically.
        /// </summary>
        /// <returns>Maximum output size of the ROM.</returns>
        public static int MaxRomSize
        {
            get
            {
                return asar_maxromsize();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct rawAsarError
        {
            public IntPtr FullErrData;
            public IntPtr RawErrData;
            public IntPtr Block;
            public IntPtr FileName;
            public int Line;
            public IntPtr CallErFileName;
            public int CallErLine;
        };

        private static Asarerror[] cleanErrors(rawAsarError* ptr, int length)
        {
            Asarerror[] output = new Asarerror[length];

            // Better create a new array
            // to avoid pointer erros, corruption and may other problems.
            for (int i = 0; i < length; i++)
            {
                output[i].Fullerrdata = Marshal.PtrToStringAnsi(ptr[i].FullErrData);
                output[i].Rawerrdata = Marshal.PtrToStringAnsi(ptr[i].RawErrData);
                output[i].Block = Marshal.PtrToStringAnsi(ptr[i].Block);
                output[i].Filename = Marshal.PtrToStringAnsi(ptr[i].FileName);
                output[i].Line = ptr[i].Line;
                output[i].Callerfilename = Marshal.PtrToStringAnsi(ptr[i].CallErFileName);
                output[i].Callerline = ptr[i].CallErLine;
            }

            return output;
        }

        /// <summary>
        /// Gets all Asar current errors. They're safe to keep for as long as you want.
        /// </summary>
        /// <returns>All Asar's errors.</returns>
        public static Asarerror[] Errors
        {
            get
            {
                rawAsarError* ptr = asar_geterrors(out int length);
                return cleanErrors(ptr, length);
            }
        }

        /// <summary>
        /// Gets all Asar current warning. They're safe to keep for as long as you want.
        /// </summary>
        /// <returns>All Asar's warnings.</returns>
        public static Asarerror[] Warnings
        {
            get
            {
                rawAsarError* ptr = asar_getwarnings(out int length);
                return cleanErrors(ptr, length);
            }
        }

        /// <summary>
        /// Gets all prints generated by the patch
        /// (Note: to see warnings/errors, check getwarnings() and geterrors()
        /// </summary>
        /// <returns>All prints</returns>
        public static string[] Prints
        {
            get
            {
                void** ptr = asar_getprints(out int length);
                string[] output = new string[length];

                // Too annoying!
                for (int i = 0; i < length; i++)
                {
                    output[i] = Marshal.PtrToStringAnsi((IntPtr)ptr[i]);
                }

                return output;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct rawAsarLabel
        {
            public IntPtr Name;
            public int Location;
        }

        /// <summary>
        /// Gets all Asar current labels. They're safe to keep for as long as you want.
        /// </summary>
        /// <returns>All Asar's labels.</returns>
        public static Asarlabel[] Labels
        {
            get
            {
                rawAsarLabel* ptr = asar_getalllabels(out int length);
                Asarlabel[] output = new Asarlabel[length];

                // Better create a new array
                // to avoid pointer erros, corruption and may other problems.
                for (int i = 0; i < length; i++)
                {
                    output[i].Name = Marshal.PtrToStringAnsi(ptr[i].Name);
                    output[i].Location = ptr[i].Location;
                }

                return output;
            }
        }

        /// <summary>
        /// Gets a value of a specific label. Returns "-1" if label has not found.
        /// </summary>
        /// <param name="labelName">The label name.</param>
        /// <returns>The value of label. If not found, it returns -1 here.</returns>
        public static int GetLabelVal(string labelName)
        {
            return asar_getlabelval(labelName);
        }

        /// <summary>
        /// Gets contents of a define. If define doesn't exists, a null string will be generated.
        /// </summary>
        /// <param name="defineName">The define name.</param>
        /// <returns>The define content. If define has not found, this will be null.</returns>
        public static string GetDefine(string defineName)
        {
            return Marshal.PtrToStringAnsi(asar_getdefine(defineName));
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct rawAsarDefine
        {
            public IntPtr Name;
            public IntPtr Contents;
        }
        /// <summary>
        /// Gets all Asar current defines. They're safe to keep for as long as you want.
        /// </summary>
        /// <returns>All Asar's defines.</returns>
        public static Asardefine[] AllDefines
        {
            get
            {
                rawAsarDefine* ptr = asar_getalldefines(out int length);
                Asardefine[] output = new Asardefine[length];

                // Better create a new array
                // to avoid pointer erros, corruption and may other problems.
                for (int i = 0; i < length; i++)
                {
                    output[i].Name = Marshal.PtrToStringAnsi(ptr[i].Name);
                    output[i].Contents = Marshal.PtrToStringAnsi(ptr[i].Contents);
                }

                return output;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="learnNew"></param>
        /// <returns></returns>
        public static string ResolveDefines(string data, bool learnNew)
        {
            return Marshal.PtrToStringAnsi(asar_resolvedefines(data, learnNew));
        }

        /// <summary>
        /// Parse a string of math.
        /// </summary>
        /// <param name="math">The math string, i.e "1+1"</param>
        /// <param name="error">If occurs any error, it will showed here.</param>
        /// <returns>Product.</returns>
        public static double Math(string math, out string error)
        {
            IntPtr err = IntPtr.Zero;
            double value = asar_math(math, out err);

            error = Marshal.PtrToStringAnsi(err);
            return value;
        }


        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct rawAsarWrittenBlock
        {
            public int PCOffSet;
            public int SNESOffSet;
            public int NumBytes;
        };

        private static Asarwrittenblock[] cleanwrittenblocks(rawAsarWrittenBlock* ptr, int length)
        {
            Asarwrittenblock[] output = new Asarwrittenblock[length];

            // Better create a new array
            // to avoid pointer erros, corruption and may other problems.
            for (int i = 0; i < length; i++)
            {
                output[i].Snesoffset = ptr[i].SNESOffSet;
                output[i].Numbytes = ptr[i].NumBytes;
                output[i].Pcoffset = ptr[i].PCOffSet;
            }

            return output;
        }

        /// <summary>
        /// Gets all Asar blocks written to the ROM. They're safe to keep for as long as you want.
        /// </summary>
        /// <returns>All Asar's blocks written to the ROM.</returns>
        public static Asarwrittenblock[] WrittenBlocks
        {
            get
            {
                rawAsarWrittenBlock* ptr = asar_getwrittenblocks(out int length);
                return cleanwrittenblocks(ptr, length);
            }
        }

        /// <summary>
        /// Gets mapper currently used by Asar.
        /// </summary>
        /// <returns>Returns mapper currently used by Asar.</returns>
        public static MapperType Mapper
        {
            get
            {
                MapperType mapper = asar_getmapper();
                return mapper;
            }
        }

        /// <summary>
        /// Generates the contents of a symbols file for in a specific format.
        /// </summary>
        /// <param name="format">The symbol file format to generate</param>
        /// <returns>Returns the textual contents of the symbols file.</returns>
        public static string GetSymbolsFile(string format = "wla")
        {
            return Marshal.PtrToStringAnsi(asar_getsymbolsfile(format));
        }
    }

    /// <summary>
    /// Contains full information of a Asar error or warning.
    /// </summary>
    public struct Asarerror
    {
        public String Fullerrdata;
        public String Rawerrdata;
        public String Block;
        public String Filename;
        public int Line;
        public String Callerfilename;
        public int Callerline;
    }

    /// <summary>
    /// Contains a label from Asar.
    /// </summary>
    public struct Asarlabel
    {
        public String Name;
        public int Location;
    }

    /// <summary>
    /// Contains a Asar define.
    /// </summary>
    public struct Asardefine
    {
        public String Name;
        public String Contents;
    }

    /// <summary>
    /// Contains full information on a block written to the ROM.
    /// </summary>
    public struct Asarwrittenblock
    {
        public int Pcoffset;
        public int Snesoffset;
        public int Numbytes;
    }

    /// <summary>
    /// Defines the mapper used by the assembler.
    /// </summary>
    public enum MapperType
    {
        /// <summary>
        /// Invalid
        /// </summary>
        InvalidMapper,

        /// <summary>
        /// LoROM
        /// </summary>
        LoRom,

        /// <summary>
        /// HiROM
        /// </summary>
        HiRom,

        /// <summary>
        /// SA-1 ROM
        /// </summary>
        Sa1Rom,

        /// <summary>
        /// Big SA-1 ROM
        /// </summary>
        BigSa1Rom,

        /// <summary>
        /// SFX ROM
        /// </summary>
        SfxRom,

        /// <summary>
        /// ExLoROM
        /// </summary>
        ExLoRom,

        /// <summary>
        /// ExHiROM
        /// </summary>
        ExHiRom,

        /// <summary>
        /// No ROM
        /// </summary>
        NoRom
    }
}
