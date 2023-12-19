using System;

namespace Visual_Art_Galary.entity
{
    public class Gallery
    {
        public int GalleryId { get; set; }    //Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CuratorID {  get; set; }    // Reference to ArtistID
        public string OpeningHours { get; set; }


        public Gallery() { }
        public Gallery(int galleryId, string name, string description, 
                        string location,int curatorID,string openingHours)
        {
            GalleryId = galleryId;
            Name = name;
            Description = description;
            Location = location;
            CuratorID = curatorID;
            OpeningHours = openingHours;
        }

        public override string ToString()
        {
            return $"{GalleryId,-8} {Name,-25} {Description,-45} {Location,-35} {CuratorID,-8} {OpeningHours,-15}";
        }
    }
}
