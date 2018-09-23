using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibBackend.Logic
{
    public class ErrorCode
    {
        public static readonly ErrorCode FileNotFound =
            new ErrorCode(0, "File Not Found", "The file {0} does not exist.");
        public static readonly ErrorCode InvalidCommand =
            new ErrorCode(1, "Invalid Command", "The command {0} is invalid.");
        public static readonly ErrorCode InvalidCommandArgument =
            new ErrorCode(2, "Invalid Command Argument", "The argument {0} is invalid.");
        public static readonly ErrorCode InvalidCommandArgumentNumbers =
            new ErrorCode(3, "Invalid Command Argument Numbers", "The command {0} use {1} argument(s).");
        public static readonly ErrorCode InvalidDefine =
            new ErrorCode(4, "Invalid Define", "The define {0} is invalid.");
        public static readonly ErrorCode InvalidLabel =
            new ErrorCode(5, "Invalid Label", "The label {0} is invalid.");
        public static readonly ErrorCode InvalidSublabel =
            new ErrorCode(6, "Invalid Sublabel", "Requires a label before the sublabel {0}.");
        public static readonly ErrorCode InvalidMacro =
            new ErrorCode(7, "Invalid Macro", "The macro {0} is invalid.");
        public static readonly ErrorCode DefineNotFound =
            new ErrorCode(8, "Define Not Found", "The Define {0} does not exist.");
        public static readonly ErrorCode LabelNotFound =
            new ErrorCode(9, "Label Not Found", "The label {0} does not exist.");
        public static readonly ErrorCode MacroNotFound =
            new ErrorCode(10, "Macro Not Found", "The macro {0} does not exist.");
        public static readonly ErrorCode Mode8Bits =
            new ErrorCode(11, "8 Bits Mode", "Only constants of 8 bits are allowed in this zone of the code.");
        public static readonly ErrorCode Mode16Bits =
            new ErrorCode(12, "16 Bits Mode", "Only constants of 8 bits are allowed in this zone of the code.");
        public static readonly ErrorCode LabelAlreadyExists =
            new ErrorCode(13, "Label Already Exists", "The label {0} already exists.");
        public static readonly ErrorCode InvalidDefineSignature =
            new ErrorCode(14, "Invalid Define Signature", "The define signature {0} is invalid.");
        public static readonly ErrorCode Unknown =
            new ErrorCode(15, "Unknown", "????");

        public int Code { get; private set; }
        private string message; 
        public string Name { get; private set; }

        private ErrorCode(int code, string name, string Message)
        {
            Code = code;
            Name = name;
            message = Message;
        }

        public string Message(params string[] args)
        {
            if (args == null || args.Length <= 0) return message;
            string m = message;
            for (int i = 0; i < args.Length; i++)
            {
                m = m.Replace("{" + i + "}", args[i]);
            }
            return m;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public class Error : IComparable<Error>
    {
        public int Line { get; set; }
        public int Start { get; set; }
        public string Message { get; private set; }
        public string Text { get; private set; }
        public ErrorCode Code { get; private set; }

        public Error(int line, int start, string text, ErrorCode code, params string[] args)
        {
            Line = line;
            Start = start;
            Text = text;
            Code = code;
            if (Code != null)
            {
                Message = Code.Message(args);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Error ");
            sb.Append(Code.Code.ToString("X"));
            sb.Append(" at line ");
            sb.Append(Line);
            sb.Append(" position ");
            sb.Append(Start);
            sb.Append("\n");
            sb.Append(Code);
            sb.Append(" Exception: ");
            sb.Append(Message);
            return sb.ToString();
        }

        public int CompareTo(Error other)
        {
            if (Line > other.Line) return 1;
            if (Line == other.Line && Start > other.Start) return 1;
            if (Line == other.Line && Start == other.Start) return 0;
            return -1;
        }
    }
}
