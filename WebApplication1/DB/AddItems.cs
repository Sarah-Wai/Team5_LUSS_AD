﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.Models;

namespace LUSS_API.DB
{
    public class AddItems
    {
        public static List<Item> getAllItem()
        {
            List<Item> items = new List<Item>();

           
                Item item = new Item()
                {
                    ItemID = 1,
                    ItemName = "Clips Double 1\"",
                    UOM = "Dozen",
                    ReStockQty = 30,
                    InStockQty = 60,
                    CategoryID = 1,
                    ItemCode = "C001",
                    ReStockLevel = 50,
                    StoreItemLocation = "C"


                };
            Item item1 = new Item(); item1.ItemID = 1; item1.ItemName = "Clips Double 1''"; item1.UOM = "Dozen"; item1.ReStockQty = 30; item1.InStockQty = 60; item1.CategoryID = 2; item1.ItemCode = "C001"; item1.ReStockLevel = 50; item1.StoreItemLocation = "C";
            Item item2 = new Item(); item2.ItemID = 2; item2.ItemName = "Clips Double 2''"; item2.UOM = "Dozen"; item2.ReStockQty = 30; item2.InStockQty = 60; item2.CategoryID = 2; item2.ItemCode = "C002"; item2.ReStockLevel = 50; item2.StoreItemLocation = "C";
            Item item3 = new Item(); item3.ItemID = 3; item3.ItemName = "Clips Double 3/4''"; item3.UOM = "Dozen"; item3.ReStockQty = 30; item3.InStockQty = 60; item3.CategoryID = 2; item3.ItemCode = "C003"; item3.ReStockLevel = 50; item3.StoreItemLocation = "C";
            Item item4 = new Item(); item4.ItemID = 4; item4.ItemName = "Clips Paper Large"; item4.UOM = "Box"; item4.ReStockQty = 30; item4.InStockQty = 60; item4.CategoryID = 2; item4.ItemCode = "C004"; item4.ReStockLevel = 50; item4.StoreItemLocation = "C";
            Item item5 = new Item(); item5.ItemID = 5; item5.ItemName = "Clips Paper Medium "; item5.UOM = "Box"; item5.ReStockQty = 30; item5.InStockQty = 60; item5.CategoryID = 2; item5.ItemCode = "C005"; item5.ReStockLevel = 50; item5.StoreItemLocation = "C";
            Item item6 = new Item(); item6.ItemID = 6; item6.ItemName = "Clips Paper Small"; item6.UOM = "Box"; item6.ReStockQty = 30; item6.InStockQty = 60; item6.CategoryID = 2; item6.ItemCode = "C006"; item6.ReStockLevel = 50; item6.StoreItemLocation = "C";
            Item item7 = new Item(); item7.ItemID = 7; item7.ItemName = "Envelope Brown (3''x6'')"; item7.UOM = "Each"; item7.ReStockQty = 400; item7.InStockQty = 720; item7.CategoryID = 1; item7.ItemCode = "E001"; item7.ReStockLevel = 600; item7.StoreItemLocation = "E";
            Item item8 = new Item(); item8.ItemID = 8; item8.ItemName = "Envelope Brown (3''x6'') w/ Window"; item8.UOM = "Each"; item8.ReStockQty = 400; item8.InStockQty = 720; item8.CategoryID = 1; item8.ItemCode = "E002"; item8.ReStockLevel = 600; item8.StoreItemLocation = "E";
            Item item9 = new Item(); item9.ItemID = 9; item9.ItemName = "Envelope Brown (5''x7'')"; item9.UOM = "Each"; item9.ReStockQty = 400; item9.InStockQty = 720; item9.CategoryID = 1; item9.ItemCode = "E003"; item9.ReStockLevel = 600; item9.StoreItemLocation = "E";
            Item item10 = new Item(); item10.ItemID = 10; item10.ItemName = "Envelope Brown (5''x7'') w/ Window"; item10.UOM = "Each"; item10.ReStockQty = 400; item10.InStockQty = 720; item10.CategoryID = 1; item10.ItemCode = "E004"; item10.ReStockLevel = 600; item10.StoreItemLocation = "E";
            Item item11 = new Item(); item11.ItemID = 11; item11.ItemName = "Envelope White (3''x6'')"; item11.UOM = "Each"; item11.ReStockQty = 400; item11.InStockQty = 720; item11.CategoryID = 1; item11.ItemCode = "E005"; item11.ReStockLevel = 600; item11.StoreItemLocation = "E";
            Item item12 = new Item(); item12.ItemID = 12; item12.ItemName = "Envelope White (3''x6'') w/ Window"; item12.UOM = "Each"; item12.ReStockQty = 400; item12.InStockQty = 720; item12.CategoryID = 1; item12.ItemCode = "E006"; item12.ReStockLevel = 600; item12.StoreItemLocation = "E";
            Item item13 = new Item(); item13.ItemID = 13; item13.ItemName = "Envelope White (5''x7'')"; item13.UOM = "Each"; item13.ReStockQty = 400; item13.InStockQty = 720; item13.CategoryID = 1; item13.ItemCode = "E007"; item13.ReStockLevel = 600; item13.StoreItemLocation = "E";
            Item item14 = new Item(); item14.ItemID = 14; item14.ItemName = "Envelope White (5''x7'') w/ Window"; item14.UOM = "Each"; item14.ReStockQty = 400; item14.InStockQty = 720; item14.CategoryID = 1; item14.ItemCode = "E008"; item14.ReStockLevel = 600; item14.StoreItemLocation = "E";
            Item item15 = new Item(); item15.ItemID = 15; item15.ItemName = "Eraser (hard)"; item15.UOM = "Each"; item15.ReStockQty = 20; item15.InStockQty = 60; item15.CategoryID = 3; item15.ItemCode = "E020"; item15.ReStockLevel = 50; item15.StoreItemLocation = "E";
            Item item16 = new Item(); item16.ItemID = 16; item16.ItemName = "Eraser (soft)"; item16.UOM = "Each"; item16.ReStockQty = 20; item16.InStockQty = 60; item16.CategoryID = 3; item16.ItemCode = "E021"; item16.ReStockLevel = 50; item16.StoreItemLocation = "E";
            Item item17 = new Item(); item17.ItemID = 17; item17.ItemName = "Exercise Book (100 pg)"; item17.UOM = "Each"; item17.ReStockQty = 50; item17.InStockQty = 120; item17.CategoryID = 4; item17.ItemCode = "E030"; item17.ReStockLevel = 100; item17.StoreItemLocation = "E";
            Item item18 = new Item(); item18.ItemID = 18; item18.ItemName = "Exercise Book (120 pg)"; item18.UOM = "Each"; item18.ReStockQty = 50; item18.InStockQty = 120; item18.CategoryID = 4; item18.ItemCode = "E031"; item18.ReStockLevel = 100; item18.StoreItemLocation = "E";
            Item item19 = new Item(); item19.ItemID = 19; item19.ItemName = "Exercise Book A4 Hardcover (100 pg)"; item19.UOM = "Each"; item19.ReStockQty = 50; item19.InStockQty = 120; item19.CategoryID = 4; item19.ItemCode = "E032"; item19.ReStockLevel = 100; item19.StoreItemLocation = "E";
            Item item20 = new Item(); item20.ItemID = 20; item20.ItemName = "Exercise Book A4 Hardcover (120 pg)"; item20.UOM = "Each"; item20.ReStockQty = 50; item20.InStockQty = 120; item20.CategoryID = 4; item20.ItemCode = "E033"; item20.ReStockLevel = 100; item20.StoreItemLocation = "E";
            Item item21 = new Item(); item21.ItemID = 21; item21.ItemName = "Exercise Book A4 Hardcover (200 pg)"; item21.UOM = "Each"; item21.ReStockQty = 50; item21.InStockQty = 120; item21.CategoryID = 4; item21.ItemCode = "E034"; item21.ReStockLevel = 100; item21.StoreItemLocation = "E";
            Item item22 = new Item(); item22.ItemID = 22; item22.ItemName = "Exercise Book Hardcover (100 pg)"; item22.UOM = "Each"; item22.ReStockQty = 50; item22.InStockQty = 120; item22.CategoryID = 4; item22.ItemCode = "E035"; item22.ReStockLevel = 100; item22.StoreItemLocation = "E";
            Item item23 = new Item(); item23.ItemID = 23; item23.ItemName = "Exercise Book Hardcover (120 pg)"; item23.UOM = "Each"; item23.ReStockQty = 50; item23.InStockQty = 120; item23.CategoryID = 4; item23.ItemCode = "E036"; item23.ReStockLevel = 100; item23.StoreItemLocation = "E";
            Item item24 = new Item(); item24.ItemID = 24; item24.ItemName = "File Separator"; item24.UOM = "Set"; item24.ReStockQty = 50; item24.InStockQty = 120; item24.CategoryID = 5; item24.ItemCode = "F020"; item24.ReStockLevel = 100; item24.StoreItemLocation = "F";
            Item item25 = new Item(); item25.ItemID = 25; item25.ItemName = "File-Blue Plain"; item25.UOM = "Each"; item25.ReStockQty = 100; item25.InStockQty = 240; item25.CategoryID = 5; item25.ItemCode = "F021"; item25.ReStockLevel = 200; item25.StoreItemLocation = "F";
            Item item26 = new Item(); item26.ItemID = 26; item26.ItemName = "File-Blue with Logo"; item26.UOM = "Each"; item26.ReStockQty = 100; item26.InStockQty = 240; item26.CategoryID = 5; item26.ItemCode = "F022"; item26.ReStockLevel = 200; item26.StoreItemLocation = "F";
            Item item27 = new Item(); item27.ItemID = 27; item27.ItemName = "File-Brown w/o Logo"; item27.UOM = "Each"; item27.ReStockQty = 150; item27.InStockQty = 240; item27.CategoryID = 5; item27.ItemCode = "F023"; item27.ReStockLevel = 200; item27.StoreItemLocation = "F";
            Item item28 = new Item(); item28.ItemID = 28; item28.ItemName = "File-Brown with Logo"; item28.UOM = "Each"; item28.ReStockQty = 150; item28.InStockQty = 240; item28.CategoryID = 5; item28.ItemCode = "F024"; item28.ReStockLevel = 200; item28.StoreItemLocation = "F";
            Item item29 = new Item(); item29.ItemID = 29; item29.ItemName = "Folder Plastic Blue"; item29.UOM = "Each"; item29.ReStockQty = 150; item29.InStockQty = 240; item29.CategoryID = 5; item29.ItemCode = "F031"; item29.ReStockLevel = 200; item29.StoreItemLocation = "F";
            Item item30 = new Item(); item30.ItemID = 30; item30.ItemName = "Folder Plastic Clear"; item30.UOM = "Each"; item30.ReStockQty = 150; item30.InStockQty = 240; item30.CategoryID = 5; item30.ItemCode = "F032"; item30.ReStockLevel = 200; item30.StoreItemLocation = "F";
            Item item31 = new Item(); item31.ItemID = 31; item31.ItemName = "Folder Plastic Green"; item31.UOM = "Each"; item31.ReStockQty = 150; item31.InStockQty = 240; item31.CategoryID = 5; item31.ItemCode = "F033"; item31.ReStockLevel = 200; item31.StoreItemLocation = "F";
            Item item32 = new Item(); item32.ItemID = 32; item32.ItemName = "Folder Plastic Pink"; item32.UOM = "Each"; item32.ReStockQty = 150; item32.InStockQty = 240; item32.CategoryID = 5; item32.ItemCode = "F034"; item32.ReStockLevel = 200; item32.StoreItemLocation = "F";
            Item item33 = new Item(); item33.ItemID = 33; item33.ItemName = "Folder Plastic Yellow"; item33.UOM = "Each"; item33.ReStockQty = 150; item33.InStockQty = 240; item33.CategoryID = 5; item33.ItemCode = "F035"; item33.ReStockLevel = 200; item33.StoreItemLocation = "F";
            Item item34 = new Item(); item34.ItemID = 34; item34.ItemName = "Highlighter Blue"; item34.UOM = "Box"; item34.ReStockQty = 80; item34.InStockQty = 120; item34.CategoryID = 6; item34.ItemCode = "H011"; item34.ReStockLevel = 100; item34.StoreItemLocation = "H";
            Item item35 = new Item(); item35.ItemID = 35; item35.ItemName = "Highlighter Green"; item35.UOM = "Box"; item35.ReStockQty = 80; item35.InStockQty = 120; item35.CategoryID = 6; item35.ItemCode = "H012"; item35.ReStockLevel = 100; item35.StoreItemLocation = "H";
            Item item36 = new Item(); item36.ItemID = 36; item36.ItemName = "Highlighter Pink"; item36.UOM = "Box"; item36.ReStockQty = 80; item36.InStockQty = 120; item36.CategoryID = 6; item36.ItemCode = "H013"; item36.ReStockLevel = 100; item36.StoreItemLocation = "H";
            Item item37 = new Item(); item37.ItemID = 37; item37.ItemName = "Highlighter Yellow"; item37.UOM = "Box"; item37.ReStockQty = 80; item37.InStockQty = 120; item37.CategoryID = 6; item37.ItemCode = "H014"; item37.ReStockLevel = 100; item37.StoreItemLocation = "H";
            Item item38 = new Item(); item38.ItemID = 38; item38.ItemName = "Hole Puncher 2 holes"; item38.UOM = "Each"; item38.ReStockQty = 20; item38.InStockQty = 60; item38.CategoryID = 7; item38.ItemCode = "H031"; item38.ReStockLevel = 50; item38.StoreItemLocation = "H";
            Item item39 = new Item(); item39.ItemID = 39; item39.ItemName = "Hole Puncher 3 holes"; item39.UOM = "Each"; item39.ReStockQty = 20; item39.InStockQty = 60; item39.CategoryID = 7; item39.ItemCode = "H032"; item39.ReStockLevel = 50; item39.StoreItemLocation = "H";
            Item item40 = new Item(); item40.ItemID = 40; item40.ItemName = "Hole Puncher Adjustable"; item40.UOM = "Each"; item40.ReStockQty = 20; item40.InStockQty = 60; item40.CategoryID = 7; item40.ItemCode = "H033"; item40.ReStockLevel = 50; item40.StoreItemLocation = "H";
            Item item41 = new Item(); item41.ItemID = 41; item41.ItemName = "Pad Postit Memo 1''x2''"; item41.UOM = "Packet"; item41.ReStockQty = 60; item41.InStockQty = 120; item41.CategoryID = 8; item41.ItemCode = "P010"; item41.ReStockLevel = 100; item41.StoreItemLocation = "P";
            Item item42 = new Item(); item42.ItemID = 42; item42.ItemName = "Pad Postit Memo 1/2''x1''"; item42.UOM = "Packet"; item42.ReStockQty = 60; item42.InStockQty = 120; item42.CategoryID = 8; item42.ItemCode = "P011"; item42.ReStockLevel = 100; item42.StoreItemLocation = "P";
            Item item43 = new Item(); item43.ItemID = 43; item43.ItemName = "Pad Postit Memo 1/2''x2''"; item43.UOM = "Packet"; item43.ReStockQty = 60; item43.InStockQty = 120; item43.CategoryID = 8; item43.ItemCode = "P012"; item43.ReStockLevel = 100; item43.StoreItemLocation = "P";
            Item item44 = new Item(); item44.ItemID = 44; item44.ItemName = "Pad Postit Memo 2''x3''"; item44.UOM = "Packet"; item44.ReStockQty = 60; item44.InStockQty = 120; item44.CategoryID = 8; item44.ItemCode = "P013"; item44.ReStockLevel = 100; item44.StoreItemLocation = "P";
            Item item45 = new Item(); item45.ItemID = 45; item45.ItemName = "Pad Postit Memo 2''x4''"; item45.UOM = "Packet"; item45.ReStockQty = 60; item45.InStockQty = 120; item45.CategoryID = 8; item45.ItemCode = "P014"; item45.ReStockLevel = 100; item45.StoreItemLocation = "P";
            Item item46 = new Item(); item46.ItemID = 46; item46.ItemName = "Pad Postit Memo 2''x4''"; item46.UOM = "Packet"; item46.ReStockQty = 60; item46.InStockQty = 120; item46.CategoryID = 8; item46.ItemCode = "P015"; item46.ReStockLevel = 100; item46.StoreItemLocation = "P";
            Item item47 = new Item(); item47.ItemID = 47; item47.ItemName = "Pad Postit Memo 3/4''x2''"; item47.UOM = "Packet"; item47.ReStockQty = 60; item47.InStockQty = 120; item47.CategoryID = 8; item47.ItemCode = "P016"; item47.ReStockLevel = 100; item47.StoreItemLocation = "P";
            Item item48 = new Item(); item48.ItemID = 48; item48.ItemName = "Paper Photostat A3"; item48.UOM = "Box"; item48.ReStockQty = 500; item48.InStockQty = 600; item48.CategoryID = 9; item48.ItemCode = "P020"; item48.ReStockLevel = 500; item48.StoreItemLocation = "P";
            Item item49 = new Item(); item49.ItemID = 49; item49.ItemName = "Paper Photostat A4"; item49.UOM = "Box"; item49.ReStockQty = 500; item49.InStockQty = 600; item49.CategoryID = 9; item49.ItemCode = "P021"; item49.ReStockLevel = 500; item49.StoreItemLocation = "P";
            Item item50 = new Item(); item50.ItemID = 50; item50.ItemName = "Pen Ballpoint Black"; item50.UOM = "Dozen"; item50.ReStockQty = 50; item50.InStockQty = 120; item50.CategoryID = 6; item50.ItemCode = "P030"; item50.ReStockLevel = 100; item50.StoreItemLocation = "P";
            Item item51 = new Item(); item51.ItemID = 51; item51.ItemName = "Pen Ballpoint Blue"; item51.UOM = "Dozen"; item51.ReStockQty = 50; item51.InStockQty = 120; item51.CategoryID = 6; item51.ItemCode = "P031"; item51.ReStockLevel = 100; item51.StoreItemLocation = "P";
            Item item52 = new Item(); item52.ItemID = 52; item52.ItemName = "Pen Ballpoint Red"; item52.UOM = "Dozen"; item52.ReStockQty = 50; item52.InStockQty = 120; item52.CategoryID = 6; item52.ItemCode = "P032"; item52.ReStockLevel = 100; item52.StoreItemLocation = "P";
            Item item53 = new Item(); item53.ItemID = 53; item53.ItemName = "Pen Felt Tip Black"; item53.UOM = "Dozen"; item53.ReStockQty = 50; item53.InStockQty = 120; item53.CategoryID = 6; item53.ItemCode = "P033"; item53.ReStockLevel = 100; item53.StoreItemLocation = "P";
            Item item54 = new Item(); item54.ItemID = 54; item54.ItemName = "Pen Felt Tip Blue"; item54.UOM = "Dozen"; item54.ReStockQty = 50; item54.InStockQty = 120; item54.CategoryID = 6; item54.ItemCode = "P034"; item54.ReStockLevel = 100; item54.StoreItemLocation = "P";
            Item item55 = new Item(); item55.ItemID = 55; item55.ItemName = "Pen Felt Tip Red"; item55.UOM = "Dozen"; item55.ReStockQty = 50; item55.InStockQty = 120; item55.CategoryID = 6; item55.ItemCode = "P035"; item55.ReStockLevel = 100; item55.StoreItemLocation = "P";
            Item item56 = new Item(); item56.ItemID = 56; item56.ItemName = "Pen Transparency Permanent"; item56.UOM = "Packet"; item56.ReStockQty = 50; item56.InStockQty = 120; item56.CategoryID = 6; item56.ItemCode = "P036"; item56.ReStockLevel = 100; item56.StoreItemLocation = "P";
            Item item57 = new Item(); item57.ItemID = 57; item57.ItemName = "Pen Transparency Soluble"; item57.UOM = "Packet"; item57.ReStockQty = 50; item57.InStockQty = 120; item57.CategoryID = 6; item57.ItemCode = "P037"; item57.ReStockLevel = 100; item57.StoreItemLocation = "P";
            Item item58 = new Item(); item58.ItemID = 58; item58.ItemName = "Pen Whiteboard Marker Black"; item58.UOM = "Box"; item58.ReStockQty = 50; item58.InStockQty = 120; item58.CategoryID = 6; item58.ItemCode = "P038"; item58.ReStockLevel = 100; item58.StoreItemLocation = "P";
            Item item59 = new Item(); item59.ItemID = 59; item59.ItemName = "Pen Whiteboard Marker Blue"; item59.UOM = "Box"; item59.ReStockQty = 50; item59.InStockQty = 120; item59.CategoryID = 6; item59.ItemCode = "P039"; item59.ReStockLevel = 100; item59.StoreItemLocation = "P";
            Item item60 = new Item(); item60.ItemID = 60; item60.ItemName = "Pen Whiteboard Marker Green"; item60.UOM = "Box"; item60.ReStockQty = 50; item60.InStockQty = 120; item60.CategoryID = 6; item60.ItemCode = "P040"; item60.ReStockLevel = 100; item60.StoreItemLocation = "P";
            Item item61 = new Item(); item61.ItemID = 61; item61.ItemName = "Pen Whiteboard Marker Red"; item61.UOM = "Box"; item61.ReStockQty = 50; item61.InStockQty = 120; item61.CategoryID = 6; item61.ItemCode = "P041"; item61.ReStockLevel = 100; item61.StoreItemLocation = "P";
            Item item62 = new Item(); item62.ItemID = 62; item62.ItemName = "Pencil 2B"; item62.UOM = "Dozen"; item62.ReStockQty = 50; item62.InStockQty = 120; item62.CategoryID = 6; item62.ItemCode = "P042"; item62.ReStockLevel = 100; item62.StoreItemLocation = "P";
            Item item63 = new Item(); item63.ItemID = 63; item63.ItemName = "Pencil 2B with Eraser End"; item63.UOM = "Dozen"; item63.ReStockQty = 50; item63.InStockQty = 120; item63.CategoryID = 6; item63.ItemCode = "P043"; item63.ReStockLevel = 100; item63.StoreItemLocation = "P";
            Item item64 = new Item(); item64.ItemID = 64; item64.ItemName = "Pencil 4H "; item64.UOM = "Dozen"; item64.ReStockQty = 50; item64.InStockQty = 120; item64.CategoryID = 6; item64.ItemCode = "P044"; item64.ReStockLevel = 100; item64.StoreItemLocation = "P";
            Item item65 = new Item(); item65.ItemID = 65; item65.ItemName = "Pencil B"; item65.UOM = "Dozen"; item65.ReStockQty = 50; item65.InStockQty = 120; item65.CategoryID = 6; item65.ItemCode = "P045"; item65.ReStockLevel = 100; item65.StoreItemLocation = "P";
            Item item66 = new Item(); item66.ItemID = 66; item66.ItemName = "Pencil B with Eraser End"; item66.UOM = "Dozen"; item66.ReStockQty = 50; item66.InStockQty = 120; item66.CategoryID = 6; item66.ItemCode = "P046"; item66.ReStockLevel = 100; item66.StoreItemLocation = "P";
            Item item67 = new Item(); item67.ItemID = 67; item67.ItemName = "Ruler 6''"; item67.UOM = "Dozen"; item67.ReStockQty = 20; item67.InStockQty = 60; item67.CategoryID = 10; item67.ItemCode = "R001"; item67.ReStockLevel = 50; item67.StoreItemLocation = "R";
            Item item68 = new Item(); item68.ItemID = 68; item68.ItemName = "Ruler 12''"; item68.UOM = "Dozen"; item68.ReStockQty = 20; item68.InStockQty = 60; item68.CategoryID = 10; item68.ItemCode = "R002"; item68.ReStockLevel = 50; item68.StoreItemLocation = "R";
            Item item69 = new Item(); item69.ItemID = 69; item69.ItemName = "Shorthand Book (100 pg)"; item69.UOM = "Each"; item69.ReStockQty = 80; item69.InStockQty = 120; item69.CategoryID = 11; item69.ItemCode = "S010"; item69.ReStockLevel = 100; item69.StoreItemLocation = "S";
            Item item70 = new Item(); item70.ItemID = 70; item70.ItemName = "Shorthand Book (120 pg)"; item70.UOM = "Each"; item70.ReStockQty = 80; item70.InStockQty = 120; item70.CategoryID = 11; item70.ItemCode = "S011"; item70.ReStockLevel = 100; item70.StoreItemLocation = "S";
            Item item71 = new Item(); item71.ItemID = 71; item71.ItemName = "Shorthand Book (80 pg)"; item71.UOM = "Each"; item71.ReStockQty = 80; item71.InStockQty = 120; item71.CategoryID = 11; item71.ItemCode = "S012"; item71.ReStockLevel = 100; item71.StoreItemLocation = "S";
            Item item72 = new Item(); item72.ItemID = 72; item72.ItemName = "Stapler No. 28"; item72.UOM = "Each"; item72.ReStockQty = 20; item72.InStockQty = 60; item72.CategoryID = 12; item72.ItemCode = "S020"; item72.ReStockLevel = 50; item72.StoreItemLocation = "S";
            Item item73 = new Item(); item73.ItemID = 73; item73.ItemName = "Stapler No. 36"; item73.UOM = "Each"; item73.ReStockQty = 20; item73.InStockQty = 60; item73.CategoryID = 12; item73.ItemCode = "S021"; item73.ReStockLevel = 50; item73.StoreItemLocation = "S";
            Item item74 = new Item(); item74.ItemID = 74; item74.ItemName = "Stapler No. 28"; item74.UOM = "Box"; item74.ReStockQty = 20; item74.InStockQty = 60; item74.CategoryID = 12; item74.ItemCode = "S022"; item74.ReStockLevel = 50; item74.StoreItemLocation = "S";
            Item item75 = new Item(); item75.ItemID = 75; item75.ItemName = "Stapler No. 36"; item75.UOM = "Box"; item75.ReStockQty = 20; item75.InStockQty = 60; item75.CategoryID = 12; item75.ItemCode = "S023"; item75.ReStockLevel = 50; item75.StoreItemLocation = "S";
            Item item76 = new Item(); item76.ItemID = 76; item76.ItemName = "Scotch Tape"; item76.UOM = "Each"; item76.ReStockQty = 20; item76.InStockQty = 60; item76.CategoryID = 13; item76.ItemCode = "S040"; item76.ReStockLevel = 50; item76.StoreItemLocation = "S";
            Item item77 = new Item(); item77.ItemID = 77; item77.ItemName = "Scotch Tape Dispenser"; item77.UOM = "Each"; item77.ReStockQty = 20; item77.InStockQty = 60; item77.CategoryID = 13; item77.ItemCode = "S041"; item77.ReStockLevel = 50; item77.StoreItemLocation = "S";
            Item item78 = new Item(); item78.ItemID = 78; item78.ItemName = "Scissors"; item78.UOM = "Each"; item78.ReStockQty = 20; item78.InStockQty = 60; item78.CategoryID = 14; item78.ItemCode = "S100"; item78.ReStockLevel = 50; item78.StoreItemLocation = "S";
            Item item79 = new Item(); item79.ItemID = 79; item79.ItemName = "Sharpener"; item79.UOM = "Each"; item79.ReStockQty = 20; item79.InStockQty = 60; item79.CategoryID = 15; item79.ItemCode = "S101"; item79.ReStockLevel = 50; item79.StoreItemLocation = "S";
            Item item80 = new Item(); item80.ItemID = 80; item80.ItemName = "Thumb Tacks Large"; item80.UOM = "Box"; item80.ReStockQty = 10; item80.InStockQty = 12; item80.CategoryID = 16; item80.ItemCode = "T001"; item80.ReStockLevel = 10; item80.StoreItemLocation = "T";
            Item item81 = new Item(); item81.ItemID = 81; item81.ItemName = "Thumb Tacks Medium"; item81.UOM = "Box"; item81.ReStockQty = 10; item81.InStockQty = 12; item81.CategoryID = 16; item81.ItemCode = "T002"; item81.ReStockLevel = 10; item81.StoreItemLocation = "T";
            Item item82 = new Item(); item82.ItemID = 82; item82.ItemName = "Thumb Tacks Small"; item82.UOM = "Box"; item82.ReStockQty = 10; item82.InStockQty = 12; item82.CategoryID = 16; item82.ItemCode = "T003"; item82.ReStockLevel = 10; item82.StoreItemLocation = "T";
            Item item83 = new Item(); item83.ItemID = 83; item83.ItemName = "Transparency Blue"; item83.UOM = "Box"; item83.ReStockQty = 200; item83.InStockQty = 120; item83.CategoryID = 17; item83.ItemCode = "T020"; item83.ReStockLevel = 100; item83.StoreItemLocation = "T";
            Item item84 = new Item(); item84.ItemID = 84; item84.ItemName = "Transparency Clear"; item84.UOM = "Box"; item84.ReStockQty = 400; item84.InStockQty = 600; item84.CategoryID = 17; item84.ItemCode = "T021"; item84.ReStockLevel = 500; item84.StoreItemLocation = "T";
            Item item85 = new Item(); item85.ItemID = 85; item85.ItemName = "Transparency Green"; item85.UOM = "Box"; item85.ReStockQty = 200; item85.InStockQty = 120; item85.CategoryID = 17; item85.ItemCode = "T022"; item85.ReStockLevel = 100; item85.StoreItemLocation = "T";
            Item item86 = new Item(); item86.ItemID = 86; item86.ItemName = "Transparency Red"; item86.UOM = "Box"; item86.ReStockQty = 200; item86.InStockQty = 120; item86.CategoryID = 17; item86.ItemCode = "T023"; item86.ReStockLevel = 100; item86.StoreItemLocation = "T";
            Item item87 = new Item(); item87.ItemID = 87; item87.ItemName = "Transparency Reverse Blue"; item87.UOM = "Box"; item87.ReStockQty = 200; item87.InStockQty = 120; item87.CategoryID = 17; item87.ItemCode = "T024"; item87.ReStockLevel = 100; item87.StoreItemLocation = "T";
            Item item88 = new Item(); item88.ItemID = 88; item88.ItemName = "Transparency Cover 3M"; item88.UOM = "Box"; item88.ReStockQty = 400; item88.InStockQty = 600; item88.CategoryID = 17; item88.ItemCode = "T025"; item88.ReStockLevel = 500; item88.StoreItemLocation = "T";
            Item item89 = new Item(); item89.ItemID = 89; item89.ItemName = "Trays In/Out"; item89.UOM = "Set"; item89.ReStockQty = 10; item89.InStockQty = 24; item89.CategoryID = 18; item89.ItemCode = "T100"; item89.ReStockLevel = 20; item89.StoreItemLocation = "T";


            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);
            items.Add(item7);
            items.Add(item8);
            items.Add( item9);
            items.Add( item10);
            items.Add( item11);
            items.Add( item12);
            items.Add( item13);
            items.Add( item14);
            items.Add( item15);
            items.Add( item16);
            items.Add( item17);
            items.Add( item18);
            items.Add( item19);
            items.Add( item20);
            items.Add( item21);
            items.Add( item22);
            items.Add( item23);
            items.Add( item24);
            items.Add( item25);
            items.Add( item26);
            items.Add(item27);
            items.Add( item28);
            items.Add( item29);
            items.Add( item30);
            items.Add( item31);
            items.Add( item32);
            items.Add( item33);
            items.Add( item34);
            items.Add( item35);
            items.Add( item36);
            items.Add( item37);
            items.Add( item38);
            items.Add( item39);
            items.Add( item40);
            items.Add( item41);
            items.Add( item42);
            items.Add( item43);
            items.Add( item44);
            items.Add( item45);
            items.Add( item46);
            items.Add( item47);
            items.Add( item48);
            items.Add( item49);
            items.Add( item50);
            items.Add( item51);
            items.Add( item52);
            items.Add( item53);
            items.Add( item54);
            items.Add( item55);
            items.Add( item56);
            items.Add( item57);
            items.Add( item58);
            items.Add( item59);
            items.Add( item60);
            items.Add( item61);
            items.Add( item62);
            items.Add( item63);
            items.Add( item64);
            items.Add( item65);
            items.Add(item66);
            items.Add( item67);
            items.Add( item68);
            items.Add( item69);
            items.Add( item70);
            items.Add( item71);
            items.Add( item72);
            items.Add( item73);
            items.Add( item74);
            items.Add( item75);
            items.Add( item76);
            items.Add( item77);
            items.Add( item78);
            items.Add( item79);
            items.Add( item80);
            items.Add( item81);
            items.Add( item82);
            items.Add( item83);
            items.Add( item84);
            items.Add( item85);
            items.Add( item86);
            items.Add( item87);
            items.Add(item88);
            items.Add( item89);

            return items;
        }
    }
}
