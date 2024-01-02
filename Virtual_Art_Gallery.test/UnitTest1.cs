using Visual_Art_Galary.dao;
using Visual_Art_Galary.entity;
using Visual_Art_Galary.Services;
using System.Transactions;
using Visual_Art_Galary.Exceptions;

namespace Virtual_Art_Gallery.test
{
    public class Tests
    {
        private IVirtualArtGalleryServices service;
        private IVirtualArtGallery dao;

        [SetUp]
        public void Setup()
        {
            service = new VirtualArtGalleryServices();
            dao = new VirtualArtGalleryDAO();
        }

        [Test]
        [Ignore("ignore")]
        public void NewUserCanRegisterSuccessfully()
        {
            using (var transaction = new TransactionScope())
            {
                // Arrange
                Users newUser = new Users
                {
                    UserName = "udit",
                    Password = "1234",
                    Email = "udit.co.in",
                    FirstName = "udit",
                    LastName = "panthi",
                    DateOfBirth = new DateTime(2001, 8, 31)
                };
                // Act
                bool registrationStatus = service.Register(newUser);
                // Assert
                Assert.IsTrue(registrationStatus);
            } // The transaction will be rolled back at the end of the using block
        }



        [Test]
        public void ExistingUserCannotRegisterWithSameUsername()
        {
            // Arrange
            Users newUser = new Users
            {
                UserName = "uditpanthi",
                Password = "Upanthi@123",
            };
             // Act
             bool registrationStatus = service.Register(newUser);
            // Assert
            Assert.IsFalse(registrationStatus);
        }

        [Test]
        public void RegisteredUserCanLoginSuccessfully()
        {
            // Arrange
            // Act
            bool loginStatus = service.Login("demo", "demopass");
            // Assert
            Assert.IsTrue(loginStatus);
        }

        [Test]
        public void UnregisteredUserFailsToLogin()
        {
            // Arrange
            // Attempt to login with an unregistered username
            // Act
            bool loginStatus = service.Login("unregisteredUsername", "password"); 
            // Assert
            Assert.IsFalse(loginStatus);
        }









        [Test]
        public void UploadNewArtworkToGallery()
        {
            // Arrange
            using (var transaction = new TransactionScope())
            {
                Artwork newArtwork = new Artwork
                {
                    Title = "Test Artwork",
                    Description = "This is a test artwork.",
                    CreationDate = DateTime.Now,
                    medium = "Oil on canvas",
                    ImageURL = "https://www.example.com/test-artwork.jpg",
                    artistID = 1  // Replace with a valid artist ID
                };

                // Act
                bool uploadStatus = dao.AddArtwork(newArtwork);

                // Assert
                Assert.IsTrue(uploadStatus);
            }
        }

        [Test]
        public void UpdateArtworkDetails()
        {
            // Arrange
            using (var transaction = new TransactionScope())
            {
                Artwork existingArtwork = dao.GetArtworkByID(1); 
                existingArtwork.Title = "Starry Night";

                // Act
                bool updateStatus = dao.UpdateArtwork(existingArtwork);

                // Assert
                Assert.IsTrue(updateStatus);
            }
        }

        [Test]
        public void RemoveArtworkFromGallery()
        {
            using (var transaction = new TransactionScope())
            {
                // Arrange
                int artworkIdToRemove = 2;

                // Act
                bool removeStatus = dao.RemoveArtwork(artworkIdToRemove);

                // Assert
                Assert.IsTrue(removeStatus);
            }
        }

        [Test]
        public void SearchArtworksReturnsExpectedResults()
        {
            // Arrange
            string keyword = "painting";

            // Act
            List<Artwork> searchResults = dao.SearchArtworks(keyword);

            // Assert
            Assert.IsNotNull(searchResults);
            Assert.IsTrue(searchResults.Count > 0);
        }










        [Test]
        public void ErrorHandlingForInvalidUserRegistration()
        {
            // Arrange
            Users invalidUser = new Users
            {
                // Provided incomplete or invalid user details
                UserName = "invaliduser",
                Password = "123",
            };

            // Act
            bool registrationStatus = service.Register(invalidUser);

            // Assert
            Assert.IsFalse(registrationStatus);
        }

        [Test]
        public void ErrorHandlingDuringLogin()
        {
            // Arrange
            string invalidUsername = "nonexistentuser";
            string invalidPassword = "invalidpassword";

            // Act
            bool loginStatus = service.Login(invalidUsername, invalidPassword);

            // Assert
            Assert.IsFalse(loginStatus);
        }

        [Test]
        public void HandlingExceptionsForNonexistentArtworks()
        {
            // Arrange
            int nonExistentArtworkId = 999;

            // Act and Assert
            
            Assert.Throws<ArtWorkNotFoundException>(() => dao.GetArtworkByID(nonExistentArtworkId));
        }

    }
}