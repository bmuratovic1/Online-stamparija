
namespace Online_Stamparija.Models.MenuItems
{
    public class MetroItem
    {
        public string LinkUrl { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public PozicijaEnum MinimumAllowedPosition { get; set; }

        public bool Upozorenje { get; set; }

    }
}