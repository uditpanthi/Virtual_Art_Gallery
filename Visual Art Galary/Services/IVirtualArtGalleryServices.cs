using Visual_Art_Galary.entity;

namespace Visual_Art_Galary.Services
{
    public interface IVirtualArtGalleryServices
    {
        bool Login(string username, string password);
        bool Register(Users newUser);
        List<Artwork> BrowseArtwork();
        List<Gallery> ViewGalleries();
        Users GetUserProfile(string username);
        bool Logout();
    }
}
