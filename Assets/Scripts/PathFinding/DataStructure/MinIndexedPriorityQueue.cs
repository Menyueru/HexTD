using System.Collections.Generic;

namespace HexTD.PathFinding.DataStructure
{
    public class MinIndexedPriorityQueue<TModel>
    {
        private IList<TModel> keys;
        private int[] heap;
        private int[] inverseHeap;
        private int size;

        private void Swap(int i, int j)
        {
            int temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;

            inverseHeap[heap[i]] = i;
            inverseHeap[heap[j]] = j;
        }

        private void ReorderUp(int k)
        {
            while (k > 1 && Comparer<TModel>.Default.Compare(keys[heap[k / 2]], keys[heap[k]]) > 0)
            {
                Swap(k / 2, k);
                k /= 2;
            }
        }

        private void ReorderDown(int k, int heapSize)
        {
            while (2 * k <= heapSize)
            {
                int child = 2 * k;
                if (child < heapSize && Comparer<TModel>.Default.Compare(keys[heap[child / 2]], keys[heap[child + 1]]) > 0)
                {
                    ++child;
                }
                if (Comparer<TModel>.Default.Compare(keys[heap[k]], keys[heap[child]]) <= 0)
                {
                    break;
                }
                Swap(child, k);
                k = child;
            }
        }
        public MinIndexedPriorityQueue(IList<TModel> keys, int maxSize)
        {
            this.heap = new int[maxSize + 1];
            this.inverseHeap = new int[maxSize + 1];
            this.size = 0;
            this.keys = keys;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Insert(int id)
        {
            ++size;
            heap[size] = id;
            inverseHeap[id] = size;
            ReorderUp(size);
        }

        public int Pop()
        {
            Swap(1, size);
            ReorderDown(1, size - 1);
            return heap[size--];
        }

        public void ChangePriority(int id)
        {
            ReorderUp(inverseHeap[id]);
        }
    }
}
