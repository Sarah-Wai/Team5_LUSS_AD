using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.Models;

namespace LUSS_API.DB
{
    public class AddItemCategory
    {
        public static List<ItemCategory> getAllItemCategories()
        {
            List<ItemCategory> itemCategories = new List<ItemCategory>();

            ItemCategory cat1 = new ItemCategory();
            cat1.CategoryID = 1;
            cat1.CategoryName = "Envelope";

            ItemCategory cat2 = new ItemCategory();
            cat2.CategoryID = 2;
            cat2.CategoryName = "Clip";

            ItemCategory cat3 = new ItemCategory();
            cat3.CategoryID = 3;
            cat3.CategoryName = "Eraser";

            ItemCategory cat4 = new ItemCategory();
            cat4.CategoryID = 4;
            cat4.CategoryName = "Exercise";

            ItemCategory cat5 = new ItemCategory();
            cat5.CategoryID = 5;
            cat5.CategoryName = "File";

            ItemCategory cat6 = new ItemCategory();
            cat6.CategoryID = 6;
            cat6.CategoryName = "Pen";

            ItemCategory cat7 = new ItemCategory();
            cat7.CategoryID = 7;
            cat7.CategoryName = "Puncher";

            ItemCategory cat8 = new ItemCategory();
            cat8.CategoryID = 8;
            cat8.CategoryName = "Pad";

            ItemCategory cat9 = new ItemCategory();
            cat9.CategoryID = 9;
            cat9.CategoryName = "Paper";

            ItemCategory cat10 = new ItemCategory();
            cat10.CategoryID = 10;
            cat10.CategoryName = "Ruler";

            ItemCategory cat11 = new ItemCategory();
            cat11.CategoryID = 11;
            cat11.CategoryName = "Shorthand";

            ItemCategory cat12 = new ItemCategory();
            cat12.CategoryID = 12;
            cat12.CategoryName = "Stapler";

            ItemCategory cat13 = new ItemCategory();
            cat13.CategoryID = 13;
            cat13.CategoryName = "Tape";

            ItemCategory cat14 = new ItemCategory();
            cat14.CategoryID = 14;
            cat14.CategoryName = "Scissors";

            ItemCategory cat15 = new ItemCategory();
            cat15.CategoryID = 15;
            cat15.CategoryName = "Sharpener";

            ItemCategory cat16 = new ItemCategory();
            cat16.CategoryID = 16;
            cat16.CategoryName = "Tacks";

            ItemCategory cat17 = new ItemCategory();
            cat17.CategoryID = 17;
            cat17.CategoryName = "Tparency";

            ItemCategory cat18 = new ItemCategory();
            cat18.CategoryID = 18;
            cat18.CategoryName = "Tray";

            
            itemCategories.Add(cat1);
            itemCategories.Add(cat2);
            itemCategories.Add(cat3);
            itemCategories.Add(cat4);
            itemCategories.Add(cat5);
            itemCategories.Add(cat6);
            itemCategories.Add(cat7);
            itemCategories.Add(cat8);
            itemCategories.Add(cat9);
            itemCategories.Add(cat10);
            itemCategories.Add(cat11);
            itemCategories.Add(cat12);
            itemCategories.Add(cat13);
            itemCategories.Add(cat12);
            itemCategories.Add(cat14);
            itemCategories.Add(cat15);
            itemCategories.Add(cat16);
            itemCategories.Add(cat17);
            itemCategories.Add(cat18);

            return itemCategories;
        }
    }
}
