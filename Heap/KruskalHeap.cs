using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class KruskalMaxHeap
    {
        public int Size { get; set; } 
        public Edge[] E { get; set; }


       public KruskalMaxHeap(int capacity)
        {
            E = new Edge[capacity];
            Size = 0;
        }

        private void HeapifyDown(int index)
        {
            try
            {
              
                int largestChild = 0;
                while (LeftChild(index) < Size)
                {
                    largestChild = 2 * index + 1;
                    if (RightChild(index) < Size && E[RightChild(index)].Weight < E[LeftChild(index)].Weight)
                    {
                        largestChild = 2 * index + 2;
                    }

                    if (E[index].Weight < E[largestChild].Weight)
                    {
                        Swap(index, largestChild);
                    }

                    index = largestChild;
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
                while ((GetParentIndex(index) >= 0 && E[index].Weight > E[(GetParentIndex(index))].Weight))
                {
                    int parentIndex = GetParentIndex(index);
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in heapifyUp. Message: " + ex.Message + " index= " + index);
            }
        }

        public int GetParentIndex(int i)
        {
            return (i - 1) / 2;
        }

        private int LeftChild(int index)
        {
            return (index * 2) + 1;
        }

        private int RightChild(int index)
        {
            return index * 2 + 2;
        }
        public void Swap(int first, int second)
        {
            Edge temp = E[first];
            E[first] = E[second];
            E[second] = temp;
           
        }

        public  void Insert(Edge edge)
        {
            try
            {
                E[Size] = edge;
                HeapifyUp(Size);
                Size++;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in insert max heap. Message: " + ex.Message + " Edge= " + edge+" size= "+Size);
            }
        }

       

        public Edge ExtractMaximum()
        {
            Edge max = null;
            try
            {
                Size--;
                if (Size < 0)
                    throw new Exception("Heap size < 0");
                max = E[0];
                E[0] = E[Size];
                HeapifyDown(0);

            }
            catch(Exception ex)
            {

                Console.WriteLine("Error while extracting maximum. Message= " + ex.Message);
            }
            return max;
        }


      
    }

}
