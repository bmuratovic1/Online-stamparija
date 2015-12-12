using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Online_Stamparija
{
    public static class StandardniUiElementi
    {
        public static System.Collections.Generic.List<Online_Stamparija.Models.MenuItems.MetroItem> glavnaDugmad = new List<Online_Stamparija.Models.MenuItems.MetroItem>
    {
        new Online_Stamparija.Models.MenuItems.MetroItem
        {
            LinkUrl = "/Home/Index",
            ImageUrl = "/Images/home.B.png",
            Title="Početna Stranica",
            MinimumAllowedPosition = Models.PozicijaEnum.Radnik
        },
        new Online_Stamparija.Models.MenuItems.MetroItem
        {
            LinkUrl = "/Home/About",
            ImageUrl = "/Images/about.png",
            Title="O Nama",
            MinimumAllowedPosition = Models.PozicijaEnum.Radnik
        },
        new Online_Stamparija.Models.MenuItems.MetroItem
        {
            LinkUrl = "/Home/Contact",
            ImageUrl = "/Images/contacts.B.png",
            Title="Kontakti",
            MinimumAllowedPosition = Models.PozicijaEnum.Radnik
        },
        new Online_Stamparija.Models.MenuItems.MetroItem
        {
            LinkUrl = "/Users/Index",
            ImageUrl = "/Images/settings1.B.png",
            Title="Administrativna Ploča",
            MinimumAllowedPosition = Models.PozicijaEnum.Administrator
        },
        new Online_Stamparija.Models.MenuItems.MetroItem
        {
            LinkUrl = "/Posao/Index",
            ImageUrl = "/Images/posao.B.png",
            Title="Poslovi",
            MinimumAllowedPosition = Models.PozicijaEnum.Menadzer
        },
        new Online_Stamparija.Models.MenuItems.MetroItem
        {
            LinkUrl = "/Materijali/Index",
            ImageUrl = "/Images/appbar.brick.png",
            Title="Materijali",
            MinimumAllowedPosition = Models.PozicijaEnum.Menadzer
        }
    };
    }
}