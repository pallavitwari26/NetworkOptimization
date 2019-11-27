using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class MaxHeap
    {
        public int Size { get; set; } = 0;
        public int[] D { get; set; }
        public int[] H { get; set; }

        public MaxHeap(int capacity)
        {
            this.H = new int[capacity];
            this.D = new int[capacity];
            Size = 0;
            Array.Clear(D, 0,capacity);
            Array.Clear(H, 0, capacity);
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        private bool IsHeapFull()
        {
            return Size == D.Length;
        }
        
        public int ExtractMaximum()
        {

            Size--;
            if (Size < 0)
                return -1;
            int max = H[0];
            H[0] = H[Size];
            HeapifyDown();

            return max;
        }

        public int GetMaximum()
        {
            return H[0];
        }


      
        // Inserts a new vertex to max heap 
        public void Insert(int vertex,int bandwidth)
        {
            try
            {
               
                D[vertex] = bandwidth;
                H[Size] = vertex;
                HeapifyUp(Size);
                Size++;

            }
            catch(Exception ex) {
            Console.WriteLine("Error in insert max heap. Message: " + ex.Message+ " vertex= "+vertex);
            }
        }

        public void Modify(int vertex, int weight)
        {
            int i = 0;
            D[vertex] = weight;
            for (i = 0; i < Size; i++)
            {
                if (H[i] == vertex)
                    break;
            }
            HeapifyUp(i);

        }

        private void HeapifyDown()
        {
            int largest = 0, index = 0;
            try
            {
                while (LeftChild(index) < Size)
                {
                    largest = LeftChild(index);
                    int leftChildBW = LeftChildBandwidth(index);
                    int rightChildBW = RightChildBandwidth(index);
                    if (RightChild(index) < Size && leftChildBW < rightChildBW)
                    {
                        largest = RightChild(index);
                    }

                    if (D[H[largest]] > D[H[index]])
                    {
                        Swap(largest, index);
                    }
                    index = largest;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error in heapifyDown. Message: " + ex.Message + " index= " + index);
            }
        }

        private void HeapifyUp(int index)
        {
            try
            {                
                  while ((index-1)/2>=0 && D[H[index]] > ParentBandwidth(index))
                {
                    Swap(index, Parent(index));
                    index = Parent(index);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in heapifyUp. Message: " + ex.Message + " index= " + index);
            }
        }
     
        private int Parent(int index)
        {
            return (index-1) / 2;
        }

        private int LeftChild(int index)
        {
            return (index*2)+1;
        }

        private int RightChild(int index)
        {
            return index*2+2;
        }

        private int LeftChildBandwidth(int index)
        {
            return D[H[LeftChild(index)]];
        }

        private int RightChildBandwidth(int index)
        {
            return D[H[RightChild(index)]];
        }

        private int ParentBandwidth(int index)
        {
            return D[H[Parent(index)]];
        }

        private void Swap(int first, int second)
        {
            int temp;
           
            temp = H[first];
            H[first] = H[second];
            H[second] = temp;
        }

      

    }
}
