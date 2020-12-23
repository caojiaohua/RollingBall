using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class ConvertHelper 
   {
        static  Random random = new  Random();
        public static int getRandomNumber(int min = 0,int max = 100)
        {
            return random.Next(min,max);
        }
   }

