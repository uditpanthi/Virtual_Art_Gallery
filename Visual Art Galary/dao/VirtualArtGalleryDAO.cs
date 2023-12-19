using System.Data.SqlClient;
using Visual_Art_Galary.entity;
using Visual_Art_Galary.Exceptions;
using Visual_Art_Galary.Utility;

namespace Visual_Art_Galary.dao
{
    public class VirtualArtGalleryDAO : IVirtualArtGallery
    {
        public List<Artwork> artworks;
        public List<FavoriteArtwork> favoriteArtwork;

        public string connectionString;
        SqlCommand cmd = null;

        public VirtualArtGalleryDAO()
        {
            artworks = new List<Artwork>();
            favoriteArtwork = new List<FavoriteArtwork>();

            connectionString = DBConnection.GetConnectionString();
            cmd = new SqlCommand();
        }           

        public bool AddArtwork(Artwork artwork)
        {
            using(SqlConnection sqlconnection=new SqlConnection(connectionString))
            {
                cmd.CommandText= "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL, ArtistID)" +
                    "values(@Title,@Discription,@creationDate,@medium,@imageURL,@artistId)";
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Discription", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@medium", artwork.medium);
                cmd.Parameters.AddWithValue("@imageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@artistId", artwork.artistID);

                cmd.Connection=sqlconnection;
                sqlconnection.Open();

                int addArtworkStatus=cmd.ExecuteNonQuery();
                return addArtworkStatus > 0;
            }
        }

        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            using(SqlConnection sqlConnection=new SqlConnection(connectionString))
            {
                cmd.CommandText = "Insert into FavoriteArtwork(UserID, ArtworkID) VALUES (@userId, @artworkId)";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@artworkId", artworkId);

                cmd.Connection=sqlConnection;
                sqlConnection.Open();

                int addFavoriteStatus=cmd.ExecuteNonQuery();
                return (addFavoriteStatus > 0); 
            }
        }


        public Artwork GetArtworkByID(int artworkID)
        {
            Artwork artwork = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    cmd.CommandText = "SELECT * FROM Artwork WHERE ArtworkID = @artworkID";
                    cmd.Parameters.AddWithValue("@artworkID", artworkID);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            artwork = new Artwork
                            {
                                ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                medium = reader["Medium"].ToString(),
                                ImageURL = reader["ImageURL"].ToString(),
                                artistID = Convert.ToInt32(reader["ArtistID"])
                            };
                        }
                    }

                    if (artwork == null)
                    {
                        throw new ArtWorkNotFoundException($"Artwork with ID {artworkID} not found.");
                    }
                    else
                    {
                        return artwork;
                    }
                }
                catch (ArtWorkNotFoundException anf)
                {
                    Console.WriteLine(anf.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
                    return null;
                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
        }



        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            List<Artwork> FavoriteArtworkList = new List<Artwork>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    cmd.CommandText = "SELECT A.* FROM Artwork A " +
                        "INNER JOIN FavoriteArtworks F " +
                        "ON A.ArtworkID = F.ArtworkID WHERE F.UserID = @userId";

                    cmd.Parameters.Clear();  // Clear existing parameters
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Artwork temp = new Artwork
                        {
                            ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                            medium = reader["Medium"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            artistID = Convert.ToInt32(reader["ArtistID"])
                        };
                        FavoriteArtworkList.Add(temp);
                    }
                }
                catch (ArtWorkNotFoundException anf) {
                    Console.WriteLine("Artwork Not Found");
                }
                catch (UserNotFoundException unf) {
                    Console.WriteLine("User Not Found");
                }
            }

            return FavoriteArtworkList;
        }



        public bool RemoveArtwork(int artworkID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM Artwork WHERE ArtworkID = @artworkId";
                    cmd.Parameters.AddWithValue("@artworkId", artworkID);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    int removeArtworkStatus = cmd.ExecuteNonQuery();
                    return removeArtworkStatus > 0;
                }
                catch (ArtWorkNotFoundException ex)
                {
                    Console.WriteLine("ArtWork Not Found");
                    return false;
                }
            }
        }

        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM FavoriteArtwork WHERE UserID = @userId AND ArtworkID = @artworkId";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@artworkId", artworkId);

                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int removeFavoriteStatus = cmd.ExecuteNonQuery();
                return removeFavoriteStatus > 0;
            }
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            List<Artwork> searchResults = new List<Artwork>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Artwork WHERE Title LIKE @keyword OR Description LIKE @keyword";
                cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Artwork artwork = new Artwork
                        {
                            ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                            medium = reader["Medium"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            artistID = Convert.ToInt32(reader["ArtistID"])
                        };

                        searchResults.Add(artwork);
                    }
                }
            }

            return searchResults;
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL, ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Description", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@Medium", artwork.medium);
                    cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    cmd.Parameters.AddWithValue("@ArtistID", artwork.artistID);
                    cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    int updateStatus = cmd.ExecuteNonQuery();
                    return updateStatus > 0;
                }
                catch(ArtWorkNotFoundException)
                {
                    Console.WriteLine("ArtWork Not Found");
                    return false;
                }
            }
        }

        public Artwork ArtworkDetails()
        {
            Console.WriteLine("Enter Artwork Details:");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Creation Date (yyyy-MM-dd): ");
            DateTime creationDate;
            while (!DateTime.TryParse(Console.ReadLine(), out creationDate))
            {
                Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd): ");
            }

            Console.Write("Medium: ");
            string medium = Console.ReadLine();

            Console.Write("Image URL: ");
            string imageURL = Console.ReadLine();

            Console.Write("Artist ID: ");
            int artistID;
            while (!int.TryParse(Console.ReadLine(), out artistID))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for Artist ID: ");
            }

            Artwork artwork = new Artwork
            {
                Title = title,
                Description = description,
                CreationDate = creationDate,
                medium = medium,
                ImageURL = imageURL,
                artistID = artistID
            };

            return artwork;
        }

    }
}
