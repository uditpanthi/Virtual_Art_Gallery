using System;

namespace Visual_Art_Galary.entity
{
    public class FavoriteArtwork
    {
        public int UserID { get; set; }
        public int ArtworkID { get; set; }


        public FavoriteArtwork() { }
        public FavoriteArtwork(int UserID, int ArtworkID)
        {
            this.UserID = UserID;
            this.ArtworkID = ArtworkID;
        }
        public override string ToString()
        {
            return $"UserFavoriteArtwork [UserID={UserID}, ArtworkID={ArtworkID}]";
        }
    }
}
