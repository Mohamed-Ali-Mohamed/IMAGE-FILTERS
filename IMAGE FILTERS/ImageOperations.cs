using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace IMAGE_FILTERS
{
    public class ImageOperations
    {
        /// <summary>
        /// Open an image, convert it to gray scale and load it into 2D array of size (Height x Width)
        /// </summary>
        /// <param name="ImagePath">Image file path</param>
        /// <returns>2D array of gray values</returns>
        public static byte[,] OpenImage(string ImagePath)
        {
            Bitmap original_bm = new Bitmap(ImagePath);
            int Height = original_bm.Height;
            int Width = original_bm.Width;

            byte[,] Buffer = new byte[Height, Width];

            unsafe
            {
                BitmapData bmd = original_bm.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, original_bm.PixelFormat);
                int x, y;
                int nWidth = 0;
                bool Format32 = false;
                bool Format24 = false;
                bool Format8 = false;

                if (original_bm.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    Format24 = true;
                    nWidth = Width * 3;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format32bppArgb || original_bm.PixelFormat == PixelFormat.Format32bppRgb || original_bm.PixelFormat == PixelFormat.Format32bppPArgb)
                {
                    Format32 = true;
                    nWidth = Width * 4;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    Format8 = true;
                    nWidth = Width;
                }
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (y = 0; y < Height; y++)
                {
                    for (x = 0; x < Width; x++)
                    {
                        if (Format8)
                        {
                            Buffer[y, x] = p[0];
                            p++;
                        }
                        else
                        {
                            Buffer[y, x] = (byte)((int)(p[0] + p[1] + p[2]) / 3);
                            if (Format24) p += 3;
                            else if (Format32) p += 4;
                        }
                    }
                    p += nOffset;
                }
                original_bm.UnlockBits(bmd);
            }

            return Buffer;
        }

        /// <summary>
        /// Get the height of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeight(byte[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidth(byte[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(1);
        }

        /// <summary>
        /// Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <param name="PicBox">PictureBox object to display the image on it</param>
        public static void DisplayImage(byte[,] ImageMatrix, PictureBox PicBox)
        {
            // Create Image:
            //==============
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            Bitmap ImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[0] = p[1] = p[2] = ImageMatrix[i, j];
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }
            PicBox.Image = ImageBMP;
        }

        ////1-INSERTION_SORT
        public static byte[] INSERTION_SORT(byte[] Array, int ArrayLength)
        {
            for (int j = 1; j < ArrayLength; j++)
            {
                byte key = Array[j];
                int i = j - 1;
                while (i >= 0 && Array[i] > key)
                    Array[i + 1] = Array[i--];
                Array[++i] = key;
            }
            return Array;
        }
        ////2-SELECTION_SORT
        public static byte[] SELECTION_SORT(byte[] Array, int ArrayLength)
        {
            for (int j = 0; j < ArrayLength - 1; j++)
            {
                int smallest = j;
                for (int i = j + 1; i < ArrayLength; i++)
                    if (Array[i] < Array[smallest])
                        smallest = i;
                if (smallest != j)
                {
                    byte Temp = Array[j];
                    Array[j] = Array[smallest];
                    Array[smallest] = Temp;
                }
            }
            return Array;
        }
        ////3-BUBBLE_SORT
        public static byte[] BUBBLE_SORT(byte[] Array, int ArrayLength)
        {
            for (int i = 0; i < ArrayLength - 1; i++)
            {
                for (int j = 0; j < ArrayLength - 1 - i; j++)
                    if (Array[j + 1] < Array[j])
                    {
                        byte Temp = Array[j];
                        Array[j] = Array[j + 1];
                        Array[j + 1] = Temp;
                    }
            }
            return Array;
        }
        ////4-MODIFIED_BUBBLE_SORT
        public static byte[] MODIFIED_BUBBLE_SORT(byte[] Array, int ArrayLength)
        {
            for (int i = 0; i < ArrayLength - 1; i++)
            {
                bool Sorted = true;
                for (int j = 0; j < ArrayLength - 1 - i; j++)
                    if (Array[j + 1] < Array[j])
                    {
                        Sorted = false;
                        byte Temp = Array[j];
                        Array[j] = Array[j + 1];
                        Array[j + 1] = Temp;
                    }
                if (Sorted)
                    break;
            }
            return Array;
        }
        ////5-MERGE_SORT
        public static void MERGE(byte[] Array, int p, int q, int r)
        {

            int n1 = q - p + 1;
            int n2 = r - q;
            byte[] LeftArray = new byte[n1];
            byte[] RightArray = new byte[n2];
            for (int i = 0; i < n1; i++)
                LeftArray[i] = Array[p + i];
            for (int i = 0; i < n2; i++)
                RightArray[i] = Array[q + i + 1];
            int k = 0, j = 0, index = 0;
            while (k < n1 && j < n2)
            {
                if (LeftArray[k] <= RightArray[j])
                    Array[p + index] = LeftArray[k++];
                else
                    Array[p + index] = RightArray[j++];
                index++;
            }
            if (k < n1)
                for (; k < n1; k++, index++)
                    Array[p + index] = LeftArray[k];
            else if (j < n2)
                for (; j < n2; j++, index++)
                    Array[p + index] = RightArray[j];
        }
        public static byte[] MERGE_SORT(byte[] Array, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                MERGE_SORT(Array, p, q);
                MERGE_SORT(Array, q + 1, r);
                MERGE(Array, p, q, r);
            }
            return Array;
        }
        ////6-QUICK_SORT
        public static int PARTITION(byte[] Array, int p, int r)
        {
            byte x = Array[r];
            byte Temp;
            int i = p;
            for (int j = p; j < r; j++)
            {
                if (Array[j] <= x)
                {
                    Temp = Array[j];
                    Array[j] = Array[i];
                    Array[i++] = Temp;
                }
            }
            Temp = Array[i];
            Array[i] = Array[r];
            Array[r] = Temp;
            return i;
        }
        public static byte[] QUICK_SORT(byte[] Array, int p, int r)
        {
            if (p < r)
            {
                int q = PARTITION(Array, p, r);
                QUICK_SORT(Array, p, q - 1);
                QUICK_SORT(Array, q + 1, r);
            }
            return Array;
        }
        ////7-COUNTING_SORT (XXXXX)
        public static byte[] COUNTING_SORT(byte[] Array, int ArrayLength, byte Max, byte Min)
        {
            byte[] count = new byte[Max - Min + 1];
            int z = 0;

            for (int i = 0; i < count.Length; i++) { count[i] = 0; }
            for (int i = 0; i < ArrayLength; i++) { count[Array[i] - Min]++; }

            for (int i = Min; i <= Max; i++)
            {
                while (count[i - Min]-- > 0)
                {
                    Array[z] = (byte)i;
                    z++;
                }
            }
            return Array;
        }
        ////8-HEAP_SORT
        public static int LEFT(int i)
        {
            return 2 * i + 1;
        }
        public static int RIGHT(int i)
        {
            return 2 * i + 2;
        }
        public static void MAX_HEAPIFY(byte[] Array, int ArrayLength, int i)
        {
            int Left = LEFT(i);
            int Right = RIGHT(i);
            int Largest;
            if (Left < ArrayLength && Array[Left] > Array[i])
                Largest = Left;
            else
                Largest = i;
            if (Right < ArrayLength && Array[Right] > Array[Largest])
                Largest = Right;
            if (Largest != i)
            {
                byte Temp = Array[i];
                Array[i] = Array[Largest];
                Array[Largest] = Temp;
                MAX_HEAPIFY(Array, ArrayLength, Largest);
            }
        }
        public static void BUILD_MAX_HEAP(byte[] Array, int ArrayLength)
        {
            for (int i = ArrayLength / 2 - 1; i >= 0; i--)
                MAX_HEAPIFY(Array, ArrayLength, i);
        }
        public static byte[] HEAP_SORT(byte[] Array, int ArrayLength)
        {
            int HeapSize = ArrayLength;
            BUILD_MAX_HEAP(Array, ArrayLength);
            for (int i = ArrayLength - 1; i > 0; i--)
            {
                byte Temp = Array[0];
                Array[0] = Array[i];
                Array[i] = Temp;
                HeapSize--;
                MAX_HEAPIFY(Array, HeapSize, 0);
            }
            return Array;
        }
        ////
        public static byte Filter1(byte[,] ImageMatrix, int x, int y, int Wmax, int Sort)
        {
            byte[] Array;
            int[] Dx, Dy;
            if (Wmax % 2 != 0)
            {
                Array = new byte[Wmax * Wmax];
                Dx = new int[Wmax * Wmax];
                Dy = new int[Wmax * Wmax];
            }
            else
            {
                Array = new byte[(Wmax + 1) * (Wmax + 1)];
                Dx = new int[(Wmax + 1) * (Wmax + 1)];
                Dy = new int[(Wmax + 1) * (Wmax + 1)];
            }
            int Index = 0;
            for (int _y = -(Wmax / 2); _y <= (Wmax / 2); _y++)
            {
                for (int _x = -(Wmax / 2); _x <= (Wmax / 2); _x++)
                {
                    Dx[Index] = _x;
                    Dy[Index] = _y;
                    Index++;
                }
            }
            byte Max, Min, Z;
            int ArrayLength, Sum, NewY, NewX, Avg;
            Sum = 0;
            Max = 0;
            Min = 255;
            ArrayLength = 0;
            Z = ImageMatrix[y, x];
            for (int i = 0; i < Wmax * Wmax; i++)
            {
                NewY = y + Dy[i];
                NewX = x + Dx[i];
                if (NewX >= 0 && NewX < GetWidth(ImageMatrix) && NewY >= 0 && NewY < GetHeight(ImageMatrix))
                {
                    Array[ArrayLength] = ImageMatrix[NewY, NewX];
                    if (Array[ArrayLength] > Max)
                        Max = Array[ArrayLength];
                    if (Array[ArrayLength] < Min)
                        Min = Array[ArrayLength];
                    Sum += Array[ArrayLength];
                    ArrayLength++;
                }
            }
            if (Sort == 1) Array = INSERTION_SORT(Array, ArrayLength);
            else if (Sort == 2) Array = SELECTION_SORT(Array, ArrayLength);
            else if (Sort == 3) Array = BUBBLE_SORT(Array, ArrayLength);
            else if (Sort == 4) Array = MODIFIED_BUBBLE_SORT(Array, ArrayLength);
            else if (Sort == 5) Array = MERGE_SORT(Array, 0, ArrayLength - 1);
            else if (Sort == 6) Array = QUICK_SORT(Array, 0, ArrayLength - 1);
            else if (Sort == 7) Array = COUNTING_SORT(Array, ArrayLength, Max, Min);
            else if (Sort == 8) Array = HEAP_SORT(Array, ArrayLength);
            Sum -= Max;
            Sum -= Min;
            ArrayLength -= 2;
            Avg = Sum / ArrayLength;
            return (byte)Avg;
        }
        ////
        public static byte Filter2(byte[,] ImageMatrix, int x, int y, int W, int Wmax, int Sort)
        {

            byte[] Array = new byte[W * W];
            int[] Dx = new int[W * W];
            int[] Dy = new int[W * W];
            int Index = 0;
            for (int _y = -(W / 2); _y <= (W / 2); _y++)
            {
                for (int _x = -(W / 2); _x <= (W / 2); _x++)
                {
                    Dx[Index] = _x;
                    Dy[Index] = _y;
                    Index++;
                }
            }
            byte Max, Min, Med, Z;
            int A1, A2, B1, B2, ArrayLength, NewY, NewX;
            Max = 0;
            Min = 255;
            ArrayLength = 0;
            Z = ImageMatrix[y, x];
            for (int i = 0; i < W * W; i++)
            {
                NewY = y + Dy[i];
                NewX = x + Dx[i];
                if (NewX >= 0 && NewX < GetWidth(ImageMatrix) && NewY >= 0 && NewY < GetHeight(ImageMatrix))
                {
                    Array[ArrayLength] = ImageMatrix[NewY, NewX];
                    if (Array[ArrayLength] > Max)
                        Max = Array[ArrayLength];
                    if (Array[ArrayLength] < Min)
                        Min = Array[ArrayLength];
                    ArrayLength++;
                }
            }
            if (Sort == 1) Array = INSERTION_SORT(Array, ArrayLength);
            else if (Sort == 2) Array = SELECTION_SORT(Array, ArrayLength);
            else if (Sort == 3) Array = BUBBLE_SORT(Array, ArrayLength);
            else if (Sort == 4) Array = MODIFIED_BUBBLE_SORT(Array, ArrayLength);
            else if (Sort == 5) Array = MERGE_SORT(Array, 0, ArrayLength - 1);
            else if (Sort == 6) Array = QUICK_SORT(Array, 0, ArrayLength - 1);
            else if (Sort == 7) Array = COUNTING_SORT(Array, ArrayLength, Max, Min);
            else if (Sort == 8) Array = HEAP_SORT(Array, ArrayLength);

            Min = Array[0];
            Med = Array[ArrayLength / 2];
            A1 = Med - Min;
            A2 = Max - Med;
            if (A1 > 0 && A2 > 0)
            {
                B1 = Z - Min;
                B2 = Max - Z;
                if (B1 > 0 && B2 > 0)
                    return Z;
                else
                {
                    if (W + 2 < Wmax)
                        return Filter2(ImageMatrix, x, y, W + 2, Wmax, Sort);
                    else
                        return Med;
                }
            }
            else
            {
                return Med;
            }

        }
        ////
        public static byte[,] ImageFilter(byte[,] ImageMatrix, int Max_Size, int Sort, int filter)
        {
            byte[,] ImageMatrix2 = ImageMatrix;
            for (int y = 0; y < GetHeight(ImageMatrix); y++)
            {
                for (int x = 0; x < GetWidth(ImageMatrix); x++)
                {
                    if (filter == 1)
                        ImageMatrix2[y, x] = Filter1(ImageMatrix, x, y, Max_Size, Sort);
                    else
                        ImageMatrix2[y, x] = Filter2(ImageMatrix, x, y, 3, Max_Size, Sort);
                }
            }

            return ImageMatrix2;
        }
    }
}
