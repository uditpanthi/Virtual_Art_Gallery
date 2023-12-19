using System;
using Visual_Art_Galary.entity;

namespace Visual_Art_Galary.dao
{
    public interface IVirtualArtGallery
    {
        //Artwork Management
        Boolean AddArtwork(Artwork artwork);
        Boolean  UpdateArtwork(Artwork artwork);
        Boolean RemoveArtwork(int artworkID);
        Artwork GetArtworkByID(int artworkID);
        List<Artwork> SearchArtworks(String keyword);



        //User Favorites
        Boolean AddArtworkToFavorite(int userId, int artworkId);
        Boolean RemoveArtworkFromFavorite(int userId, int artworkId);
        List<Artwork> GetUserFavoriteArtworks(int userId);



        //Additional Methods
        Artwork ArtworkDetails();
    }
}
