namespace SMWControlibControls.LogicControls
{
    public class UndoRedoDynamicArray<T>
    {
        T[] array;
        public int Index { get; private set; }
        public int Length { get; private set; }
        private int updateSize;
        public T CurrentElement
        {
            get
            {
                if (Index < 0) return default(T);
                if (Index >= Length) return default(T);
                return array[Index];
            }
        }

        public UndoRedoDynamicArray(ushort InitialSize, ushort UpdateSize)
        {
            if (InitialSize < 1) InitialSize = 1;
            if (UpdateSize < 1) UpdateSize = 1;
            array = new T[InitialSize];
            updateSize = UpdateSize;
            Index = -1;
        }

        public T Redo()
        {
            if (Index >= Length) return default(T);
            Index++;
            if (Index >= Length) Index = Length;

            return array[Index - 1];
        }

        public T Undo()
        {
            if (Index < 0) return default(T);
            Index--;
            if (Index < 0) Index = -1;
            return array[Index + 1];
        }

        public void Add(T element)
        {
            Index++;
            if (Index >= array.Length)
            {
                T[] narr = new T[array.Length + updateSize];
                for (int i = 0; i < array.Length; i++)
                {
                    narr[i] = array[i];
                }
                array = narr;
            }

            array[Index] = element;
            Length = Index + 1;
        }
    }
}
