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
        public int Position { get; private set; }
        public int Length { get; private set; }

        public string Text { get; private set; }

        public UndoRedoStruct(BeforeModificationEventArgs e, UndoRedoAction Action)
        {
            Position = e.Position;
            Length = e.Text.Length;
            Text = e.Text;
            UndoRedoAction = Action;
        }

        public static bool operator ==(UndoRedoStruct a, UndoRedoStruct b)
        {
            return a.Position == b.Position &&
                a.UndoRedoAction == b.UndoRedoAction && 
                a.Text == b.Text;
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

        public override bool Equals(object obj)
        {
            if (!(obj is UndoRedoStruct))
            {
                return false;
            }

            var @struct = (UndoRedoStruct)obj;
            return UndoRedoAction == @struct.UndoRedoAction &&
                   Position == @struct.Position &&
                   Text == @struct.Text;
        }

        public override int GetHashCode()
        {
            var hashCode = 742094758;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + UndoRedoAction.GetHashCode();
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            return hashCode;
        }
    }
}
