using System;

namespace Visual_Art_Galary.entity
{
    public class ArtworkGallery
    {
        public int ArtworkID { get; set; }
        public int GalleryID { get; set; }


        //constructor
        public ArtworkGallery() { }
        public ArtworkGallery(int artworkID, int galleryID)
        {
            ArtworkID = artworkID;
            GalleryID = galleryID;
        }

        public override string ToString()
        {
            return $"ArtworkGallery [ArtworkID={ArtworkID}, GalleryID={GalleryID}]";
        }
    }
}
