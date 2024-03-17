using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadLaba
{
    public class SaveAsThread
    {
        private int[] array;
        private string filename;

        public SaveAsThread(int[] array, string filename)
        {
            this.array = array;
            this.filename = filename;
        }

        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (int num in array)
                {
                    writer.WriteLine(num);
                }
            }
            Console.WriteLine($"Massif {filename} written to a file.");
        }
    }

}
