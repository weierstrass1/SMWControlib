using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlibControls.LogicControls
{
    public enum UndoRedoAction { Insert,Delete };
    public struct UndoRedoStruct
    {
        public UndoRedoAction UndoRedoAction { get; private set; }
        BeforeModificationEventArgs e;
        public int Position
        {
            get
            {
                return e.Position;
            }
        }
        public int Length
        {
            get
            {
                return e.Text.Length;
            }
        }

        public string Text
        {
            get
            {
                return e.Text;
            }
        }

        public UndoRedoStruct(BeforeModificationEventArgs E, UndoRedoAction Action)
        {
            e = E;
            UndoRedoAction = Action;
        }

        public static bool operator ==(UndoRedoStruct a, UndoRedoStruct b)
        {
            return a.e == b.e && a.UndoRedoAction == b.UndoRedoAction;
        }

        public static bool operator !=(UndoRedoStruct a, UndoRedoStruct b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            if (default(UndoRedoStruct) == this) return "Default";
            if (UndoRedoAction == UndoRedoAction.Insert) return "Insert";
            return "Delete";
        }
    }
}
