using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangaWorld_admin.Controllers
{
    public class Validate
    {
        public static bool genreFilter(String mangaGen, String[] listGen)
        {
            for(int i = 0; i < listGen.Length; i++)
            {
                if (!mangaGen.Contains(listGen[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}